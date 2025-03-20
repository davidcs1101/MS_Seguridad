using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Implementaciones;
using SEG.Repositorio.Interfaces;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Servicio.Implementaciones
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
            var programasResultado = await _programaRepositorio.Listar().ToListAsync();

            return _apiResponseServicio.CrearRespuesta<List<ProgramaDto>?>(true, "", programasResultado);
        }

    }
}
