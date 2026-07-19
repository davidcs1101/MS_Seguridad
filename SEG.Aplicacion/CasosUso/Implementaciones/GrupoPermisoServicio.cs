using SEG.Dominio.Entidades;
using SEG.Dtos;
using Utilidades;
using SEG.Dominio.Repositorio;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Dominio.Servicios.Interfaces;
using static Utilidades.Textos;
using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.ServiciosExternos.Mapeo;

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
        public readonly IAutorizacionSincronizacion _autorizacionSincronizacion;

        public GrupoPermisoServicio(IGrupoPermisoRepositorio grupoPermisoRepositorio, IPermisoRepositorio permisoRepositorio, IEntidadValidador<SEG_Grupo> grupoValidador, IEntidadValidador<SEG_Permiso> permisoValidador, IGrupoRepositorio grupoRepositorio, IEntidadValidador<SEG_GrupoPermiso> grupoPermisoValidador, IUsuarioContextoServicio usuarioContextoServicio, IMapperPerfiles mapper, IApiResponse apiResponseServicio, IAutorizacionSincronizacion autorizacionSincronizacion)
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
            _autorizacionSincronizacion = autorizacionSincronizacion;
        }

        public async Task<ApiResponse<int>> CrearAsync(GrupoPermisoCreacionRequest grupoPermisoCreacionRequest)
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

            var id = await _grupoPermisoRepositorio.CrearAsync(grupoPermiso);

            // Llamada para actualizar la sincronización de permisos
            await _autorizacionSincronizacion.SincronizarPermisosAsync();

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponse<string>> ModificarAsync(GrupoPermisoModificacionRequest grupoPermisoModificacionRequest)
        {
            var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerPorIdAsync(grupoPermisoModificacionRequest.Id);
            _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            grupoPermisoExiste!.EstadoActivo = grupoPermisoModificacionRequest.EstadoActivo;
            grupoPermisoExiste.FechaModificado = DateTime.UtcNow;
            grupoPermisoExiste.UsuarioModificadorId = usuarioId;

            await _grupoPermisoRepositorio.ModificarAsync(grupoPermisoExiste);

            // Llamada para actualizar la sincronización de permisos
            await _autorizacionSincronizacion.SincronizarPermisosAsync();

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerPorIdAsync(id);
            _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_NO_EXISTE_ID);

            var eliminado = await _grupoPermisoRepositorio.EliminarAsync(id);

            if (eliminado)
            {
                // Llamada para actualizar la sincronización de permisos
                await _autorizacionSincronizacion.SincronizarPermisosAsync();
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");
            }

            return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponse<GrupoPermisoDto?>> ObtenerGrupoPermisoAsync(int grupoId, int permisoId)
        {
            var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerGrupoPermisoAsync(grupoId, permisoId);
            _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.GruposPermisos.MENSAJE_GRUPOPERMISO_NO_EXISTE_GRUPO_PERMISO);

            var grupoPermisoDto = _mapper.Map(grupoPermisoExiste!);

            return _apiResponse.CrearRespuesta<GrupoPermisoDto?>(true, "", grupoPermisoDto);
        }

        public async Task<ApiResponse<List<GrupoPermisoDto>?>> ListarAsync()
        {
            var grupoPermisos = await _grupoPermisoRepositorio.Listar().ToListAsync();
            var grupoPermisosDto = _mapper.Map(grupoPermisos);
            return _apiResponse.CrearRespuesta<List<GrupoPermisoDto>?>(true, "", grupoPermisosDto);
        }
    }
}
