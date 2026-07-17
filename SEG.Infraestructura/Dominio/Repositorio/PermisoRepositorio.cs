using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class PermisoRepositorio : IPermisoRepositorio
    {
        private readonly AppDbContext _context;

        public PermisoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task ModificarAsync(SEG_Permiso permiso)
        {
            _context.SEG_Permisos.Update(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task<SEG_Permiso?> ObtenerPorProgramaIdYAccionIdAsync(int programaId, int accionId)
        {
            return await _context.SEG_Permisos
                .Include(p => p.Programa)
                .Include(p => p.Accion)
                .FirstOrDefaultAsync(p => p.ProgramaId == programaId && p.AccionId == accionId);
        }

        public async Task<SEG_Permiso?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Permisos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<SEG_Permiso?> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.SEG_Permisos.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public IQueryable<SEG_Permiso> Listar()
        {
            return _context.SEG_Permisos
                .Include(p => p.Programa)
                .Include(p => p.Accion)
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador);
        }
    }
}
