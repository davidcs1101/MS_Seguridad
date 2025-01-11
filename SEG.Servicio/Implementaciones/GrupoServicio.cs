using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
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
    public class GrupoServicio : IGrupoServicio
    {
        private readonly IGrupoRepositorio _grupoRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;

        public GrupoServicio(IGrupoRepositorio grupoRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio)
        {
            _grupoRepositorio = grupoRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(GrupoCreacionRequest grupoCreacionRequest) 
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorCodigoAsync(grupoCreacionRequest.Codigo);
            if (grupoExiste != null)
                throw new DbUpdateException(Textos.Grupos.MENSAJE_GRUPO_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var grupo = _mapper.Map<SEG_Grupo>(grupoCreacionRequest);
            grupo.FechaCreado = DateTime.Now;
            grupo.UsuarioCreadorId = usuarioId;

            var id = await _grupoRepositorio.CrearAsync(grupo);

            return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        }

        public async Task<ApiResponse<string>> ModificarAsync(GrupoModificacionRequest grupoModificacionRequest) 
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(grupoModificacionRequest.Id);
            if (grupoExiste == null)
                throw new KeyNotFoundException(Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            _mapper.Map(grupoModificacionRequest,grupoExiste);
            grupoExiste.FechaModificado = DateTime.Now;
            grupoExiste.UsuarioModificadorId = usuarioId;

            await _grupoRepositorio.ModificarAsync(grupoExiste);

            return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO };
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id) 
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(id);
            if (grupoExiste == null)
                throw new KeyNotFoundException(Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var eliminado = await _grupoRepositorio.EliminarAsync(id);

            if (eliminado)
                return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ELIMINADO };

            return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO };
        }

        public async Task<ApiResponse<GrupoDto?>> ObtenerPorIdAsync(int id) 
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(id);
            if (grupoExiste == null)
                throw new KeyNotFoundException(Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var grupoDto = _mapper.Map<GrupoDto>(grupoExiste);

            return new ApiResponse<GrupoDto?> { Correcto = true, Mensaje = "", Data = grupoDto };
        }

        public async Task<ApiResponse<GrupoDto?>> ObtenerPorCodigoAsync(string codigo) 
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorCodigoAsync(codigo);
            if (grupoExiste == null)
                throw new KeyNotFoundException(Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_CODIGO);

            var grupoDto = _mapper.Map<GrupoDto>(grupoExiste);

            return new ApiResponse<GrupoDto?> { Correcto = true, Mensaje = "", Data = grupoDto };
        }

        public async Task<ApiResponse<List<GrupoDto>?>> ListarAsync()
        {
            var gruposResultado = await _grupoRepositorio.Listar().ToListAsync();

            return new ApiResponse<List<GrupoDto>?> { Correcto = true, Mensaje = "", Data = gruposResultado };
        }

    }
}
