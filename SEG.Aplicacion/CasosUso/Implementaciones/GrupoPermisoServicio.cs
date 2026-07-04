using AutoMapper;
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
        public readonly IMapper _mapper;
        public readonly IApisResponse _apiResponse;

        public GrupoPermisoServicio(IGrupoPermisoRepositorio grupoPermisoRepositorio, IPermisoRepositorio permisoRepositorio, IEntidadValidador<SEG_Grupo> grupoValidador, IEntidadValidador<SEG_Permiso> permisoValidador, IGrupoRepositorio grupoRepositorio, IEntidadValidador<SEG_GrupoPermiso> grupoPermisoValidador, IUsuarioContextoServicio usuarioContextoServicio, IMapper mapper, IApisResponse apiResponseServicio)
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
        }

        //public async Task<ApiResponse<int>> CrearAsync(GrupoPermisoCreacionRequest grupoPermisoCreacionRequest)
        //{
        //    var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerGrupoPermisoAsync(grupoPermisoCreacionRequest.GrupoId, grupoPermisoCreacionRequest.PermisoId);
        //    _grupoPermisoValidador.ValidarDatoYaExiste(grupoPermisoExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_YA_EXISTE);

        //    var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(grupoPermisoCreacionRequest.GrupoId);
        //    _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

        //    var permisoExiste = await _permisoRepositorio.ObtenerPorIdAsync(grupoPermisoCreacionRequest.PermisoId);
        //    _permisoValidador.ValidarDatoNoEncontrado(permisoExiste, Textos.Permisos.MENSAJE_PERMISO_NO_EXISTE_ID);

        //    var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

        //    var grupoPrograma = _mapper.Map<SEG_GrupoPrograma>(grupoPermisoCreacionRequest);
        //    grupoPrograma.UsuarioCreadorId = usuarioId;

        //    var id = await _grupoProgramaRepositorio.CrearAsync(grupoPrograma);

        //    return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        //}

        //public async Task<ApiResponse<string>> ModificarAsync(GrupoProgramaModificacionRequest grupoProgramaModificacionRequest)
        //{
        //    var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerPorIdAsync(grupoProgramaModificacionRequest.Id);
        //    _grupoProgramaValidador.ValidarDatoNoEncontrado(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_NO_EXISTE_ID);

        //    var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

        //    grupoProgramaExiste.EstadoActivo = grupoProgramaModificacionRequest.EstadoActivo;
        //    grupoProgramaExiste.FechaModificado = DateTime.Now;
        //    grupoProgramaExiste.UsuarioModificadorId = usuarioId;

        //    await _grupoProgramaRepositorio.ModificarAsync(grupoProgramaExiste);

        //    return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        //}

        //public async Task<ApiResponse<string>> EliminarAsync(int id) { 
        //    var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerPorIdAsync(id);
        //    _grupoProgramaValidador.ValidarDatoNoEncontrado(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_NO_EXISTE_ID);

        //    var eliminado = await _grupoProgramaRepositorio.EliminarAsync(id);

        //    if (eliminado)
        //        return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO,"");

        //    return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        //}

        //public async Task<ApiResponse<GrupoProgramaDto?>> ObtenerGrupoProgramaAsync(int grupoId, int programaId) {
        //    var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerGrupoProgramaAsync(grupoId, programaId);
        //    _grupoProgramaValidador.ValidarDatoNoEncontrado(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_NO_EXISTE_GRUPO_PROGRAMA);

        //    var grupoProgramaDto = _mapper.Map<GrupoProgramaDto>(grupoProgramaExiste);

        //    return _apiResponse.CrearRespuesta<GrupoProgramaDto?> (true, "", grupoProgramaDto);
        //}

        public async Task<ApiResponse<List<GrupoPermisoDto>?>> ListarAsync()
        {
            var grupoPermisos = await _grupoPermisoRepositorio.Listar().ToListAsync();
            var grupoPermisosDto = _mapper.Map<List<GrupoPermisoDto>>(grupoPermisos);
            return _apiResponse.CrearRespuesta<List<GrupoPermisoDto>?>(true, "", grupoPermisosDto);
        }
    }
}
