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
    public class PermisoServicio : IPermisoServicio
    {
        private readonly IPermisoRepositorio _permisoRepositorio;
        public readonly IAccionRepositorio _accionRepositorio;
        public readonly IEntidadValidador<SEG_Accion> _accionValidador;
        public readonly IProgramaRepositorio _programaRepositorio;
        public readonly IEntidadValidador<SEG_Programa> _programaValidador;
        public readonly IEntidadValidador<SEG_Permiso> _permisoValidador;
        public readonly IUsuarioContextoServicio _usuarioContextoServicio;
        public readonly IMapperPerfiles _mapper;
        public readonly IApiResponse _apiResponse;
        public readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly IAppSettings _appSettings;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public PermisoServicio(IPermisoRepositorio permisoRepositorio, IAccionRepositorio accionRepositorio, IEntidadValidador<SEG_Accion> accionValidador, IEntidadValidador<SEG_Programa> programaValidador, IProgramaRepositorio programaRepositorio, IUsuarioContextoServicio usuarioContextoServicio, IMapperPerfiles mapper, IApiResponse apiResponseServicio, IEntidadValidador<SEG_Permiso> permisoValidador, ISincronizadorMicroservicios sincronizadorMicroservicios, IProcesadorTransacciones procesadorTransacciones, IColaSolicitudServicio colaSolicitudServicio, IAppSettings appSettings, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _permisoRepositorio = permisoRepositorio;
            _accionRepositorio = accionRepositorio;
            _accionValidador = accionValidador;
            _programaRepositorio = programaRepositorio;
            _programaValidador = programaValidador;
            _usuarioContextoServicio = usuarioContextoServicio;
            _mapper = mapper;
            _apiResponse = apiResponseServicio;
            _permisoValidador = permisoValidador;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
            _procesadorTransacciones = procesadorTransacciones;
            _colaSolicitudServicio = colaSolicitudServicio;
            _appSettings = appSettings;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<ApiResponseDto<string>> ModificarAsync(PermisoModificacionRequest permisoModificacionRequest)
        {
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var permisoExiste = await _permisoRepositorio.ObtenerPorIdAsync(permisoModificacionRequest.Id);
                _permisoValidador.ValidarDatoNoEncontrado(permisoExiste, Textos.Permisos.MENSAJE_PERMISO_NO_EXISTE_ID);

                var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                permisoExiste!.Nombre = permisoModificacionRequest.Nombre;
                permisoExiste.EstadoActivo = permisoModificacionRequest.EstadoActivo;
                permisoExiste.UsuarioModificadorId = usuarioId;
                permisoExiste.FechaModificado = DateTime.UtcNow;

                _permisoRepositorio.MarcarModificar(permisoExiste);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de permisos después de modificar un permiso
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponseDto<PermisoDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var permisoExiste = await _permisoRepositorio.ObtenerPorCodigoAsync(codigo);
            _permisoValidador.ValidarDatoNoEncontrado(permisoExiste, Textos.Permisos.MENSAJE_PERMISO_NO_EXISTE_CODIGO(codigo));

            var permisoDto = _mapper.Map(permisoExiste!);

            return _apiResponse.CrearRespuesta<PermisoDto?>(true, "", permisoDto);
        }

        public async Task<ApiResponseDto<List<PermisoDto>?>> ListarAsync()
        {
            var permisos = await _permisoRepositorio.Listar().ToListAsync();

            var permisoResultado = permisos
                .Select(g => new PermisoDto
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

            return _apiResponse.CrearRespuesta<List<PermisoDto>?>(true, "", permisoResultado);
        }
    }
}
