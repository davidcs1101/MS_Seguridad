using AutoMapper;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Dominio.Repositorio.Interfaces;
using Utilidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class GrupoProgramaServicio : IGrupoProgramaServicio
    {
        private readonly IGrupoProgramaRepositorio _grupoProgramaRepositorio;
        public readonly IGrupoRepositorio _grupoRepositorio;
        public readonly IGrupoValidador _grupoValidador;
        public readonly IProgramaRepositorio _programaRepositorio;
        public readonly IProgramaValidador _programaValidador;
        public readonly IGrupoProgramaValidador _grupoProgramaValidador;
        public readonly IUsuarioContextoServicio _usuarioContextoServicio;
        public readonly IMapper _mapper;
        public readonly IApiResponseServicio _apiResponseServicio;

        public GrupoProgramaServicio(IGrupoProgramaRepositorio grupoProgramaRepositorio, IProgramaRepositorio programaRepositorio, IGrupoValidador grupoValidador, IProgramaValidador programaValidador, IGrupoRepositorio grupoRepositorio, IGrupoProgramaValidador grupoProgramaValidador, IUsuarioContextoServicio usuarioContextoServicio, IMapper mapper, IApiResponseServicio apiResponseServicio)
        {
            _grupoProgramaRepositorio = grupoProgramaRepositorio;
            _grupoRepositorio = grupoRepositorio;
            _grupoValidador = grupoValidador;
            _programaRepositorio = programaRepositorio;
            _programaValidador = programaValidador;
            _grupoProgramaValidador = grupoProgramaValidador;
            _usuarioContextoServicio = usuarioContextoServicio;
            _mapper = mapper;
            _apiResponseServicio = apiResponseServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(GrupoProgramaCreacionRequest grupoProgramaCreacionRequest)
        {
            var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerGrupoProgramaAsync(grupoProgramaCreacionRequest.GrupoId, grupoProgramaCreacionRequest.ProgramaId);
            _grupoProgramaValidador.ValidarDatoYaExiste(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_YA_EXISTE);

            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(grupoProgramaCreacionRequest.GrupoId);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(grupoProgramaCreacionRequest.ProgramaId);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var grupoPrograma = _mapper.Map<SEG_GrupoPrograma>(grupoProgramaCreacionRequest);
            grupoPrograma.FechaCreado = DateTime.Now;
            grupoPrograma.UsuarioCreadorId = usuarioId;

            var id = await _grupoProgramaRepositorio.CrearAsync(grupoPrograma);

            return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponse<string>> ModificarAsync(GrupoProgramaModificacionRequest grupoProgramaModificacionRequest)
        {
            var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerPorIdAsync(grupoProgramaModificacionRequest.Id);
            _grupoProgramaValidador.ValidarDatoNoEncontrado(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            grupoProgramaExiste.EstadoActivo = grupoProgramaModificacionRequest.EstadoActivo;
            grupoProgramaExiste.FechaModificado = DateTime.Now;
            grupoProgramaExiste.UsuarioModificadorId = usuarioId;

            await _grupoProgramaRepositorio.ModificarAsync(grupoProgramaExiste);

            return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id) { 
            var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerPorIdAsync(id);
            _grupoProgramaValidador.ValidarDatoNoEncontrado(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_NO_EXISTE_ID);

            var eliminado = await _grupoProgramaRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO,"");

            return _apiResponseServicio.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponse<GrupoProgramaDto?>> ObtenerGrupoProgramaAsync(int grupoId, int programaId) {
            var grupoProgramaExiste = await _grupoProgramaRepositorio.ObtenerGrupoProgramaAsync(grupoId, programaId);
            _grupoProgramaValidador.ValidarDatoNoEncontrado(grupoProgramaExiste, Textos.GruposProgramas.MENSAJE_GRUPOPROGRAMA_NO_EXISTE_GRUPO_PROGRAMA);

            var grupoProgramaDto = _mapper.Map<GrupoProgramaDto>(grupoProgramaExiste);

            return _apiResponseServicio.CrearRespuesta<GrupoProgramaDto?> (true, "", grupoProgramaDto);
        }
    }
}
