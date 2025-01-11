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

        public ProgramaServicio(IProgramaRepositorio programaRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio)
        {
            _programaRepositorio = programaRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(ProgramaCreacionRequest programaCreacionRequest)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorCodigoAsync(programaCreacionRequest.Codigo);
            if (programaExiste != null)
                throw new DbUpdateException(Textos.Programas.MENSAJE_PROGRAMA_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var programa = _mapper.Map<SEG_Programa>(programaCreacionRequest);
            programa.FechaCreado = DateTime.Now;
            programa.UsuarioCreadorId = usuarioId;

            var id = await _programaRepositorio.CrearAsync(programa);

            return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        }

        public async Task<ApiResponse<string>> ModificarAsync(ProgramaModificacionRequest programaModificacionRequest)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(programaModificacionRequest.Id);
            if (programaExiste == null)
                throw new KeyNotFoundException(Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            _mapper.Map(programaModificacionRequest, programaExiste);
            programaExiste.FechaModificado = DateTime.Now;
            programaExiste.UsuarioModificadorId = usuarioId;

            await _programaRepositorio.ModificarAsync(programaExiste);

            return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO };
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(id);
            if (programaExiste == null)
                throw new KeyNotFoundException(Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var eliminado = await _programaRepositorio.EliminarAsync(id);

            if (eliminado)
                return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ELIMINADO };

            return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO };
        }

        public async Task<ApiResponse<ProgramaDto?>> ObtenerPorIdAsync(int id)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(id);
            if (programaExiste == null)
                throw new KeyNotFoundException(Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var programaDto = _mapper.Map<ProgramaDto>(programaExiste);

            return new ApiResponse<ProgramaDto?> { Correcto = true, Mensaje = "", Data = programaDto };
        }

        public async Task<ApiResponse<ProgramaDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorCodigoAsync(codigo);
            if (programaExiste == null)
                throw new KeyNotFoundException(Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_CODIGO);

            var programaDto = _mapper.Map<ProgramaDto>(programaExiste);

            return new ApiResponse<ProgramaDto?> { Correcto = true, Mensaje = "", Data = programaDto };
        }

        public async Task<ApiResponse<List<ProgramaDto>?>> ListarAsync()
        {
            var programasResultado = await _programaRepositorio.Listar().ToListAsync();

            return new ApiResponse<List<ProgramaDto>?> { Correcto = true, Mensaje = "", Data = programasResultado };
        }

    }
}
