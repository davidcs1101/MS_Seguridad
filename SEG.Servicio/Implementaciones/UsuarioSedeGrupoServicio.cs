using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Implementaciones;
using SEG.Repositorio.Interfaces;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Servicio.Implementaciones
{
    public class UsuarioSedeGrupoServicio : IUsuarioSedeGrupoServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IGrupoServicio _grupoRepositorio;
        private readonly IUsuarioSedeGrupoRepositorio _usuarioSedeGrupoRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;

        public UsuarioSedeGrupoServicio(IUsuarioRepositorio usuarioRepositorio, IGrupoServicio grupoRepositorio,IUsuarioSedeGrupoRepositorio usuarioSedeGrupoRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _grupoRepositorio = grupoRepositorio;
            _usuarioSedeGrupoRepositorio = usuarioSedeGrupoRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacionRequest) 
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoCreacionRequest.UsuarioId);
            if (usuarioExiste == null)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            //PENDIENTE: VALIDAR QUE LA SEDE EXISTA EN EL MICROSERRVICIO DE EMPRESAS

            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoCreacionRequest.GrupoId);
            if (grupoExiste == null)
                throw new KeyNotFoundException(Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var usuarioSedeExiste = await _usuarioSedeGrupoRepositorio.ObtenerUsuarioSedeAsync(usuarioSedeGrupoCreacionRequest.UsuarioId, usuarioSedeGrupoCreacionRequest.SedeId);
            if (usuarioSedeExiste != null)
                throw new DbUpdateException(Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_YA_TIENE_SEDE_ASOCIADA);

            var usuarioSedeGrupo = _mapper.Map<SEG_UsuarioSedeGrupo>(usuarioSedeGrupoCreacionRequest);
            usuarioSedeGrupo.FechaCreado = DateTime.Now;
            usuarioSedeGrupo.UsuarioCreadorId = usuarioId;

            var id = await _usuarioSedeGrupoRepositorio.CrearAsync(usuarioSedeGrupo);

            return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        }

        public async Task<ApiResponse<string>> ModificarAsync(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest)
        {
            var usuarioSedeGrupoExiste = await _usuarioSedeGrupoRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoModificacionRequest.Id);
            if (usuarioSedeGrupoExiste == null)
                throw new KeyNotFoundException(Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoModificacionRequest.GrupoId);
            if (grupoExiste == null)
                throw new KeyNotFoundException(Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            _mapper.Map(usuarioSedeGrupoModificacionRequest, usuarioSedeGrupoExiste);
            usuarioSedeGrupoExiste.FechaModificado = DateTime.Now;
            usuarioSedeGrupoExiste.UsuarioModificadorId = usuarioId;

            await _usuarioSedeGrupoRepositorio.ModificarAsync(usuarioSedeGrupoExiste);

            return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO };
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id) 
        {
            var usuarioSedeGrupo = await _usuarioSedeGrupoRepositorio.ObtenerPorIdAsync(id);
            if (usuarioSedeGrupo == null)
                throw new KeyNotFoundException(Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_EXISTE_ID);

            var eliminado = await _usuarioSedeGrupoRepositorio.EliminarAsync(id);

            if (eliminado)
                return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ELIMINADO };

            return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO };
        }

        public async Task<ApiResponse<UsuarioSedeGrupoDto?>> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId) 
        {
            var usuarioSedeExiste = await _usuarioSedeGrupoRepositorio.ObtenerUsuarioSedeAsync(usuarioId, sedeId);
            if (usuarioSedeExiste == null)
                throw new KeyNotFoundException(Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_EXISTE_USUARIO_SEDE);

            var usuarioSedeGrupoDto = _mapper.Map<UsuarioSedeGrupoDto>(usuarioSedeExiste);

            return new ApiResponse<UsuarioSedeGrupoDto?> { Correcto = true, Mensaje = "", Data = usuarioSedeGrupoDto };
        }

        public async Task<ApiResponse<List<UsuarioSedeGrupoDto>?>> ListarPorUsuarioIdAsync(int? usuarioId) 
        {
            if (usuarioId == null)
                return new ApiResponse<List<UsuarioSedeGrupoDto>?> { Correcto = false, Mensaje = Textos.Usuarios.MENSAJE_TOKEN_INCORRECTO };

            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync((int)usuarioId);
            if (usuarioExiste == null)
                return new ApiResponse<List<UsuarioSedeGrupoDto>?> { Correcto = false, Mensaje = Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID };

            var sedesResultado = await _usuarioSedeGrupoRepositorio.ListarPorUsuarioId((int)usuarioId)
                .Where(s => s.EstadoActivo).ToListAsync();
            if (sedesResultado.Count()==0)
                return new ApiResponse<List<UsuarioSedeGrupoDto>?> { Correcto = false, Mensaje = Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_TIENE_SEDES_ACTIVAS };

            return new ApiResponse<List<UsuarioSedeGrupoDto>?> { Correcto = true, Mensaje = "", Data = sedesResultado };
        }
    }
}
