﻿using AutoMapper;
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
        private readonly IGrupoRepositorio _grupoRepositorio;
        private readonly IUsuarioSedeGrupoRepositorio _usuarioSedeGrupoRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IUsuarioValidador _usuarioValidador;
        private readonly IGrupoValidador _grupoValidador;
        private readonly IUsuarioSedeGrupoValidador _usuarioSedeGrupoValidador;
        private readonly IApiResponseServicio _apiResponseServicio;

        public UsuarioSedeGrupoServicio(IUsuarioRepositorio usuarioRepositorio, IGrupoRepositorio grupoRepositorio, IUsuarioSedeGrupoRepositorio usuarioSedeGrupoRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, IUsuarioValidador usuarioValidador, IGrupoValidador grupoValidador, IUsuarioSedeGrupoValidador usuarioSedeGrupoValidador, IApiResponseServicio apiResponseServicio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _grupoRepositorio = grupoRepositorio;
            _usuarioSedeGrupoRepositorio = usuarioSedeGrupoRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _usuarioValidador = usuarioValidador;
            _grupoValidador = grupoValidador;
            _usuarioSedeGrupoValidador = usuarioSedeGrupoValidador;
            _apiResponseServicio = apiResponseServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacionRequest) 
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoCreacionRequest.UsuarioId);
            _usuarioValidador.ValidarDatoNoEncontrado(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoCreacionRequest.GrupoId);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var usuarioSedeExiste = await _usuarioSedeGrupoRepositorio.ObtenerUsuarioSedeAsync(usuarioSedeGrupoCreacionRequest.UsuarioId, usuarioSedeGrupoCreacionRequest.SedeId);
            _usuarioSedeGrupoValidador.ValidarDatoYaExiste(usuarioSedeExiste, Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_YA_TIENE_SEDE_ASOCIADA);

            //PENDIENTE: VALIDAR QUE LA SEDE EXISTA EN EL MICROSERRVICIO DE EMPRESAS

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioSedeGrupo = _mapper.Map<SEG_UsuarioSedeGrupo>(usuarioSedeGrupoCreacionRequest);
            usuarioSedeGrupo.FechaCreado = DateTime.Now;
            usuarioSedeGrupo.UsuarioCreadorId = usuarioId;

            var id = await _usuarioSedeGrupoRepositorio.CrearAsync(usuarioSedeGrupo);

            return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponse<string>> ModificarAsync(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest)
        {
            var usuarioSedeGrupoExiste = await _usuarioSedeGrupoRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoModificacionRequest.Id);
            _usuarioSedeGrupoValidador.ValidarDatoNoEncontrado(usuarioSedeGrupoExiste, Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_EXISTE_ID);

            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(usuarioSedeGrupoModificacionRequest.GrupoId);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            _mapper.Map(usuarioSedeGrupoModificacionRequest, usuarioSedeGrupoExiste);
            usuarioSedeGrupoExiste.FechaModificado = DateTime.Now;
            usuarioSedeGrupoExiste.UsuarioModificadorId = usuarioId;

            await _usuarioSedeGrupoRepositorio.ModificarAsync(usuarioSedeGrupoExiste);

            return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id) 
        {
            var usuarioSedeGrupo = await _usuarioSedeGrupoRepositorio.ObtenerPorIdAsync(id);
            _usuarioSedeGrupoValidador.ValidarDatoNoEncontrado(usuarioSedeGrupo, Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_EXISTE_ID);

            var eliminado = await _usuarioSedeGrupoRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponseServicio.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponseServicio.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponse<UsuarioSedeGrupoDto?>> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId) 
        {
            var usuarioSedeExiste = await _usuarioSedeGrupoRepositorio.ObtenerUsuarioSedeAsync(usuarioId, sedeId);
            _usuarioSedeGrupoValidador.ValidarDatoNoEncontrado(usuarioSedeExiste, Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_EXISTE_USUARIO_SEDE);

            var usuarioSedeGrupoDto = _mapper.Map<UsuarioSedeGrupoDto>(usuarioSedeExiste);

            return _apiResponseServicio.CrearRespuesta<UsuarioSedeGrupoDto?>(true, "", usuarioSedeGrupoDto);
        }

        public async Task<ApiResponse<List<UsuarioSedeGrupoDto>?>> ListarPorUsuarioIdLogueadoAsync() 
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioId);
            _usuarioValidador.ValidarDatoNoEncontrado(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            var sedesResultado = await _usuarioSedeGrupoRepositorio.ListarPorUsuarioId(usuarioId)
                .Where(s => s.EstadoActivo).ToListAsync();
            if (sedesResultado.Count() == 0)
                return _apiResponseServicio.CrearRespuesta<List<UsuarioSedeGrupoDto>?>(false, Textos.UsuariosSedesGrupos.MENSAJE_USUARIOSEDEGRUPO_NO_TIENE_SEDES_ACTIVAS);

            return _apiResponseServicio.CrearRespuesta<List<UsuarioSedeGrupoDto>?>(true, "", sedesResultado);
        }
    }
}
