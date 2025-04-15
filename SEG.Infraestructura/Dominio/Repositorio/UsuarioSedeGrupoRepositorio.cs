using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
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

        public IQueryable<SEG_UsuarioSedeGrupo> ListarPorUsuarioId(int usuarioId)
        {
            return _context.SEG_UsuariosSedesGrupos
                .Include(us => us.UsuarioCreador)
                .Include(us => us.UsuarioModificador);
        }
    }
}
