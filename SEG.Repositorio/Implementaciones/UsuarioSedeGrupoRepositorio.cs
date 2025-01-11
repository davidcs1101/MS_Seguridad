using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Implementaciones
{
    public class UsuarioSedeGrupoRepositorio : IUsuarioSedeGrupoRepositorio
    {
        private readonly AppDbContext _context;
        public UsuarioSedeGrupoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_UsuarioSedeGrupo usuariosSedesGrupos) 
        {
            _context.SEG_UsuariosSedesGrupos.Add(usuariosSedesGrupos);
            await _context.SaveChangesAsync();
            return usuariosSedesGrupos.Id;
        }

        public async Task ModificarAsync(SEG_UsuarioSedeGrupo usuariosSedesGrupos)
        {
            _context.SEG_UsuariosSedesGrupos.Update(usuariosSedesGrupos);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.SEG_UsuariosSedesGrupos.Where(u => u.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_UsuarioSedeGrupo?> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId)
        {
            return await _context.SEG_UsuariosSedesGrupos
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.SedeId == sedeId);
        }

        public async Task<SEG_UsuarioSedeGrupo?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_UsuariosSedesGrupos.FindAsync(id);
        }

        public IQueryable<UsuarioSedeGrupoDto> ListarPorUsuarioId(int usuarioId)
        {
            var usuarioSedes = _context.SEG_UsuariosSedesGrupos
                .Include(us => us.UsuarioCreador)
                .Include(us => us.UsuarioModificador)
                .Select(us => new UsuarioSedeGrupoDto
                {
                    Id = us.Id,
                    UsuarioId = us.UsuarioId,
                    SedeId = us.SedeId,
                    GrupoId = us.GrupoId,
                    UsuarioCreadorId = us.UsuarioCreadorId,
                    NombreUsuarioCreador = us.UsuarioCreador.NombreUsuario,
                    FechaCreado = us.FechaCreado,
                    UsuarioModificadorId = us.UsuarioModificadorId,
                    NombreUsuarioModificador = us.UsuarioModificador != null ? us.UsuarioModificador.NombreUsuario : null,
                    FechaModificado = us.FechaModificado,
                    EstadoActivo = us.EstadoActivo
                });
            return usuarioSedes;
        }
    }
}
