﻿using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Implementaciones
{
    public class GrupoProgramaRepositorio : IGrupoProgramaRepositorio
    {
        private readonly AppDbContext _context;
        public GrupoProgramaRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_GrupoPrograma grupoPrograma) {
            _context.SEG_GruposProgramas.Add(grupoPrograma);
            await _context.SaveChangesAsync();
            return grupoPrograma.Id;
        }

        public async Task ModificarAsync(SEG_GrupoPrograma grupoPrograma) {
            _context.SEG_GruposProgramas.Update(grupoPrograma);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id) {
            var eliminado = await _context.SEG_GruposProgramas.Where(g => g.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_GrupoPrograma?> ObtenerGrupoProgramaAsync(int grupoId, int programaId)
        {
            return await _context.SEG_GruposProgramas
                .FirstOrDefaultAsync(x => x.GrupoId == grupoId && x.ProgramaId == programaId);
        }

        public async Task<SEG_GrupoPrograma?> ObtenerPorIdAsync(int id) {
            return await _context.SEG_GruposProgramas.FindAsync(id);
        }

        public IQueryable<ProgramaDto> ListarProgramasPorGrupo(int grupoId)
        {
            var programas = _context.SEG_GruposProgramas
                             .Include(gp => gp.Programa)
                             .Include(p => p.UsuarioCreador)
                             .Include(p => p.UsuarioModificador)
                             .Where(gp => gp.GrupoId == grupoId)
                             .Select(gp => new ProgramaDto
                             {
                                 Id = gp.Programa.Id,
                                 Codigo = gp.Programa.Codigo,
                                 Nombre = gp.Programa.Nombre,
                                 UsuarioCreadorId = gp.UsuarioCreadorId,
                                 NombreUsuarioCreador = gp.UsuarioCreador.NombreUsuario,
                                 FechaCreado = gp.FechaCreado,
                                 UsuarioModificadorId = gp.UsuarioModificadorId,
                                 NombreUsuarioModificador = gp.UsuarioModificador != null ? gp.UsuarioModificador.NombreUsuario : null,
                                 FechaModificado = gp.FechaModificado,
                                 EstadoActivo = gp.EstadoActivo
                             });
            return programas;
        }
    }
}
