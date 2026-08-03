using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Implementaciones;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Aplicacion.ServiciosExternos.Mapeo;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Dtos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class GrupoServicio : IGrupoServicio
    {
        private readonly IGrupoRepositorio _grupoRepositorio;
        private readonly IMapperPerfiles _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponse _apiResponse;
        private readonly IEntidadValidador<SEG_Grupo> _grupoValidador;
        private readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly IAppSettings _appSettings;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public GrupoServicio(IGrupoRepositorio grupoRepositorio, IMapperPerfiles mapper, IUsuarioContextoServicio usuarioContextoServicio, IApiResponse apiResponseServicio, IEntidadValidador<SEG_Grupo> grupoValidador, ISincronizadorMicroservicios sincronizadorMicroservicios, IProcesadorTransacciones procesadorTransacciones, IColaSolicitudServicio colaSolicitudServicio, IAppSettings appSettings, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _grupoRepositorio = grupoRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _apiResponse = apiResponseServicio;
            _grupoValidador = grupoValidador;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
            _procesadorTransacciones = procesadorTransacciones;
            _colaSolicitudServicio = colaSolicitudServicio;
            _appSettings = appSettings;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<ApiResponseDto<int>> CrearAsync(GrupoCreacionRequest grupoCreacionRequest)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorCodigoAsync(grupoCreacionRequest.Codigo);
            _grupoValidador.ValidarDatoYaExiste(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var grupo = _mapper.Map(grupoCreacionRequest);
            grupo.UsuarioCreadorId = usuarioId;

            var id = await _grupoRepositorio.CrearAsync(grupo);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);

        }

        public async Task<ApiResponseDto<string>> ModificarAsync(GrupoModificacionRequest grupoModificacionRequest)
        {
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(grupoModificacionRequest.Id);
                _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

                var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _mapper.Map(grupoModificacionRequest, grupoExiste!);
                grupoExiste!.FechaModificado = DateTime.Now;
                grupoExiste.UsuarioModificadorId = usuarioId;

                _grupoRepositorio.MarcarModificar(grupoExiste);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de permisos después de crear un grupo
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO,"");
        }

        public async Task<ApiResponseDto<string>> EliminarAsync(int id)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(id);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var eliminado = await _grupoRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponseDto<GrupoDto?>> ObtenerPorIdAsync(int id)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(id);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var grupoDto = _mapper.Map(grupoExiste!);

            return _apiResponse.CrearRespuesta<GrupoDto?>(true, "", grupoDto);
        }

        public async Task<ApiResponseDto<GrupoDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorCodigoAsync(codigo);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_CODIGO);

            var grupoDto = _mapper.Map(grupoExiste!);

            return _apiResponse.CrearRespuesta<GrupoDto?>(true, "", grupoDto);
        }

        public async Task<ApiResponseDto<List<GrupoDto>?>> ListarAsync()
        {
            var grupos = await _grupoRepositorio.Listar().ToListAsync();

            var gruposResultado = grupos
                .Select(g => new GrupoDto
                {
                    Id = g.Id,
                    Codigo = g.Codigo,
                    Nombre = g.Nombre,
                    UsuarioCreadorId = g.UsuarioCreadorId,
                    NombreUsuarioCreador = g.UsuarioCreador.NombreUsuario,
                    FechaCreado = g.FechaCreado,
                    UsuarioModificadorId = g.UsuarioModificadorId,
                    NombreUsuarioModificador = g.UsuarioModificador != null ? g.UsuarioModificador.NombreUsuario : null,
                    FechaModificado = g.FechaModificado,
                    EstadoActivo = g.EstadoActivo
                }).ToList();

            return _apiResponse.CrearRespuesta<List<GrupoDto>?>(true, "", gruposResultado);
        }

    }
}
