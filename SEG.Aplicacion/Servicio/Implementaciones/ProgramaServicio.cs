using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEG.Dtos;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio.Interfaces;
using Utilidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class ProgramaServicio : IProgramaServicio
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponseServicio _apiResponseServicio;
        private readonly IProgramaValidador _programaValidador;

        public ProgramaServicio(IProgramaRepositorio programaRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, IProgramaValidador programaValidador, IApiResponseServicio apiResponseServicio)
        {
            _programaRepositorio = programaRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _programaValidador = programaValidador;
            _apiResponseServicio = apiResponseServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(ProgramaCreacionRequest programaCreacionRequest)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorCodigoAsync(programaCreacionRequest.Codigo);
            _programaValidador.ValidarDatoYaExiste(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var programa = _mapper.Map<SEG_Programa>(programaCreacionRequest);
            programa.FechaCreado = DateTime.Now;
            programa.UsuarioCreadorId = usuarioId;

            var id = await _programaRepositorio.CrearAsync(programa);

            return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponse<string>> ModificarAsync(ProgramaModificacionRequest programaModificacionRequest)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(programaModificacionRequest.Id);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            _mapper.Map(programaModificacionRequest, programaExiste);
            programaExiste.FechaModificado = DateTime.Now;
            programaExiste.UsuarioModificadorId = usuarioId;

            await _programaRepositorio.ModificarAsync(programaExiste);

            return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(id);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var eliminado = await _programaRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponseServicio.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponse<ProgramaDto?>> ObtenerPorIdAsync(int id)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(id);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var programaDto = _mapper.Map<ProgramaDto>(programaExiste);

            return _apiResponseServicio.CrearRespuesta<ProgramaDto?>(true, "", programaDto);
        }

        public async Task<ApiResponse<ProgramaDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorCodigoAsync(codigo);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_CODIGO);

            var programaDto = _mapper.Map<ProgramaDto>(programaExiste);

            return _apiResponseServicio.CrearRespuesta<ProgramaDto?>(true, "", programaDto);
        }

        public async Task<ApiResponse<List<ProgramaDto>?>> ListarAsync()
        {
            var programas = await _programaRepositorio.Listar().ToListAsync();

            var programasResultado = programas
                .Select(p => new ProgramaDto
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    UsuarioCreadorId = p.UsuarioCreadorId,
                    NombreUsuarioCreador = p.UsuarioCreador.NombreUsuario,
                    FechaCreado = p.FechaCreado,
                    UsuarioModificadorId = p.UsuarioModificadorId,
                    NombreUsuarioModificador = p.UsuarioModificador != null ? p.UsuarioModificador.NombreUsuario : null,
                    FechaModificado = p.FechaModificado,
                    EstadoActivo = p.EstadoActivo
                }).ToList();

            return _apiResponseServicio.CrearRespuesta<List<ProgramaDto>?>(true, "", programasResultado);
        }

    }
}
