using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Implementaciones;
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
    public class GrupoPermisoServicio : IGrupoPermisoServicio
    {
        private readonly IGrupoPermisoRepositorio _grupoPermisoRepositorio;
        public readonly IGrupoRepositorio _grupoRepositorio;
        public readonly IEntidadValidador<SEG_Grupo> _grupoValidador;
        public readonly IPermisoRepositorio _permisoRepositorio;
        public readonly IEntidadValidador<SEG_Permiso> _permisoValidador;
        public readonly IEntidadValidador<SEG_GrupoPermiso> _grupoPermisoValidador;
        public readonly IUsuarioContextoServicio _usuarioContextoServicio;
        public readonly IMapperPerfiles _mapper;
        public readonly IApiResponse _apiResponse;
        public readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly IAppSettings _appSettings;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public GrupoPermisoServicio(IGrupoPermisoRepositorio grupoPermisoRepositorio, IPermisoRepositorio permisoRepositorio, IEntidadValidador<SEG_Grupo> grupoValidador, IEntidadValidador<SEG_Permiso> permisoValidador, IGrupoRepositorio grupoRepositorio, IEntidadValidador<SEG_GrupoPermiso> grupoPermisoValidador, IUsuarioContextoServicio usuarioContextoServicio, IMapperPerfiles mapper, IApiResponse apiResponseServicio, ISincronizadorMicroservicios sincronizadorMicroservicios, IProcesadorTransacciones procesadorTransacciones, IColaSolicitudServicio colaSolicitudServicio, IAppSettings appSettings, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _grupoPermisoRepositorio = grupoPermisoRepositorio;
            _permisoRepositorio = permisoRepositorio;
            _grupoRepositorio = grupoRepositorio;
            _grupoValidador = grupoValidador;
            _permisoValidador = permisoValidador;
            _grupoPermisoValidador = grupoPermisoValidador;
            _usuarioContextoServicio = usuarioContextoServicio;
            _mapper = mapper;
            _apiResponse = apiResponseServicio;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
            _procesadorTransacciones = procesadorTransacciones;
            _colaSolicitudServicio = colaSolicitudServicio;
            _appSettings = appSettings;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<ApiResponseDto<int>> CrearAsync(GrupoPermisoCreacionRequest grupoPermisoCreacionRequest)
        {
            var id = 0;
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerGrupoPermisoAsync(grupoPermisoCreacionRequest.GrupoId, grupoPermisoCreacionRequest.PermisoId);
                _grupoPermisoValidador.ValidarDatoYaExiste(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_YA_EXISTE);

                var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(grupoPermisoCreacionRequest.GrupoId);
                _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

                var permisoExiste = await _permisoRepositorio.ObtenerPorIdAsync(grupoPermisoCreacionRequest.PermisoId);
                _permisoValidador.ValidarDatoNoEncontrado(permisoExiste, Textos.Permisos.MENSAJE_PERMISO_NO_EXISTE_ID);

                var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                var grupoPermiso = _mapper.Map(grupoPermisoCreacionRequest);
                grupoPermiso.UsuarioCreadorId = usuarioId;

                _grupoPermisoRepositorio.MarcarCrear(grupoPermiso);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();

                id = grupoPermiso.Id;
            });

            // Llamada para actualizar la sincronización de permisos después de modificar un permiso
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponseDto<string>> ModificarAsync(GrupoPermisoModificacionRequest grupoPermisoModificacionRequest)
        {
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerPorIdAsync(grupoPermisoModificacionRequest.Id);
                _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_NO_EXISTE_ID);

                var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                grupoPermisoExiste!.EstadoActivo = grupoPermisoModificacionRequest.EstadoActivo;
                grupoPermisoExiste.FechaModificado = DateTime.UtcNow;
                grupoPermisoExiste.UsuarioModificadorId = usuarioId;

                _grupoPermisoRepositorio.MarcarModificar(grupoPermisoExiste);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de permisos después de modificar un permiso
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponseDto<string>> EliminarAsync(int id)
        {
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerPorIdAsync(id);
                _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_NO_EXISTE_ID);

                _grupoPermisoRepositorio.MarcarEliminar(grupoPermisoExiste!);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de permisos después de modificar un permiso
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");
        }

        public async Task<ApiResponseDto<GrupoPermisoDto?>> ObtenerGrupoPermisoAsync(int grupoId, int permisoId)
        {
            var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerGrupoPermisoAsync(grupoId, permisoId);
            _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_NO_EXISTE_GRUPO_PERMISO);

            var grupoPermisoDto = _mapper.Map(grupoPermisoExiste!);

            return _apiResponse.CrearRespuesta<GrupoPermisoDto?>(true, "", grupoPermisoDto);
        }

        public async Task<ApiResponseDto<List<GrupoPermisoDto>?>> ListarAsync()
        {
            var grupoPermisos = await _grupoPermisoRepositorio.Listar().ToListAsync();
            var grupoPermisosDto = _mapper.Map(grupoPermisos);
            return _apiResponse.CrearRespuesta<List<GrupoPermisoDto>?>(true, "", grupoPermisosDto);
        }
    }
}
