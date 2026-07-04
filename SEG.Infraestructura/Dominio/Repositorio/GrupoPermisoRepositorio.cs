using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class GrupoPermisoRepositorio : IGrupoPermisoRepositorio
    {
        private readonly AppDbContext _context;
        public GrupoPermisoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_GrupoPermiso grupoPermiso) {
            _context.SEG_GruposPermisos.Add(grupoPermiso);
            await _context.SaveChangesAsync();
            return grupoPermiso.Id;
        }

        public async Task ModificarAsync(SEG_GrupoPermiso grupoPermiso) {
            _context.SEG_GruposPermisos.Update(grupoPermiso);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id) {
            var eliminado = await _context.SEG_GruposPermisos.Where(g => g.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_GrupoPermiso?> ObtenerGrupoPermisoAsync(int grupoId, int permisoId)
        {
            return await _context.SEG_GruposPermisos
                .FirstOrDefaultAsync(x => x.GrupoId == grupoId && x.PermisoId == permisoId);
        }

        public async Task<SEG_GrupoPermiso?> ObtenerPorIdAsync(int id) {
            return await _context.SEG_GruposPermisos.FindAsync(id);
        }

        public IQueryable<SEG_GrupoPermiso> ListarPermisosPorGrupo(int grupoId)
        {
            return _context.SEG_GruposPermisos
                .Include(gp => gp.Grupo)
                .Include(gp => gp.Permiso)
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador)
                .Where(gp => gp.GrupoId == grupoId);
        }

        public IQueryable<SEG_GrupoPermiso> Listar()
        {
            return _context.SEG_GruposPermisos
                .Include(gp => gp.Grupo)
                .Include(gp => gp.Permiso)
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador);
        }
    }
}
