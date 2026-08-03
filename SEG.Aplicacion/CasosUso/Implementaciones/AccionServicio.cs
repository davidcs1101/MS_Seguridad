using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces;
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
    public class AccionServicio : IAccionServicio
    {
        private readonly IAccionRepositorio _accionRepositorio;
        private readonly IMapperPerfiles _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponse _apiResponse;
        private readonly IEntidadValidador<SEG_Accion> _accionValidador;
        private readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly IAppSettings _appSettings;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;


        public AccionServicio(IAccionRepositorio accionRepositorio, IMapperPerfiles mapper, IUsuarioContextoServicio usuarioContextoServicio, IEntidadValidador<SEG_Accion> accionValidador, IApiResponse apiResponseServicio, ISincronizadorMicroservicios sincronizadorMicroservicios, IProcesadorTransacciones procesadorTransacciones, IColaSolicitudServicio colaSolicitudServicio, IAppSettings appSettings, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _accionRepositorio = accionRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _accionValidador = accionValidador;
            _apiResponse = apiResponseServicio;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
            _procesadorTransacciones = procesadorTransacciones;
            _colaSolicitudServicio = colaSolicitudServicio;
            _appSettings = appSettings;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<ApiResponseDto<int>> CrearAsync(AccionCreacionRequest accionCreacionRequest)
        {
            var accionExiste = await _accionRepositorio.ObtenerPorCodigoAsync(accionCreacionRequest.Codigo);
            _accionValidador.ValidarDatoYaExiste(accionExiste, Textos.Acciones.MENSAJE_ACCION_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var accion = _mapper.Map(accionCreacionRequest);
            accion.UsuarioCreadorId = usuarioId;

            var id = await _accionRepositorio.CrearAsync(accion);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponseDto<string>> ModificarAsync(AccionModificacionRequest accionModificacionRequest)
        {
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var accionExiste = await _accionRepositorio.ObtenerPorIdAsync(accionModificacionRequest.Id);
                _accionValidador.ValidarDatoNoEncontrado(accionExiste, Textos.Acciones.MENSAJE_ACCION_NO_EXISTE_ID);

                var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _mapper.Map(accionModificacionRequest, accionExiste);
                accionExiste.FechaModificado = DateTime.Now;
                accionExiste.UsuarioModificadorId = usuarioId;

                _accionRepositorio.MarcarModificar(accionExiste);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de permisos después de modificar una acción
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponseDto<string>> EliminarAsync(int id)
        {
            var accionExiste = await _accionRepositorio.ObtenerPorIdAsync(id);
            _accionValidador.ValidarDatoNoEncontrado(accionExiste, Textos.Acciones.MENSAJE_ACCION_NO_EXISTE_ID);

            var eliminado = await _accionRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponseDto<AccionDto?>> ObtenerPorIdAsync(int id)
        {
            var accionExiste = await _accionRepositorio.ObtenerPorIdAsync(id);
            _accionValidador.ValidarDatoNoEncontrado(accionExiste, Textos.Acciones.MENSAJE_ACCION_NO_EXISTE_ID);

            var accionDto = _mapper.Map(accionExiste!);

            return _apiResponse.CrearRespuesta<AccionDto?>(true, "", accionDto);
        }

        public async Task<ApiResponseDto<AccionDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var accionExiste = await _accionRepositorio.ObtenerPorCodigoAsync(codigo);
            _accionValidador.ValidarDatoNoEncontrado(accionExiste, Textos.Acciones.MENSAJE_ACCION_NO_EXISTE_CODIGO);

            var accionDto = _mapper.Map(accionExiste!);

            return _apiResponse.CrearRespuesta<AccionDto?>(true, "", accionDto);
        }

        public async Task<ApiResponseDto<List<AccionDto>?>> ListarAsync()
        {
            var acciones = await _accionRepositorio.Listar().ToListAsync();

            var accionesResultado = acciones
                .Select(a => new AccionDto
                {
                    Id = a.Id,
                    Codigo = a.Codigo,
                    Nombre = a.Nombre,
                    UsuarioCreadorId = a.UsuarioCreadorId,
                    NombreUsuarioCreador = a.UsuarioCreador.NombreUsuario,
                    FechaCreado = a.FechaCreado,
                    UsuarioModificadorId = a.UsuarioModificadorId,
                    NombreUsuarioModificador = a.UsuarioModificador != null ? a.UsuarioModificador.NombreUsuario : null,
                    FechaModificado = a.FechaModificado,
                    EstadoActivo = a.EstadoActivo
                }).ToList();

            return _apiResponse.CrearRespuesta<List<AccionDto>?>(true, "", accionesResultado);
        }

    }
}
