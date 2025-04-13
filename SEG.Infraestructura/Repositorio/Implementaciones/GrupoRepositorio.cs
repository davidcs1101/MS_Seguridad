using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Dominio.Repositorio.Interfaces;

namespace SEG.Infraestructura.Repositorio.Implementaciones
{
    public class GrupoRepositorio : IGrupoRepositorio
    {
        private readonly AppDbContext _context;
        public GrupoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_Grupo grupo) 
        {
            _context.SEG_Grupos.Add(grupo);
            await _context.SaveChangesAsync();
            return grupo.Id;
        }

        public async Task ModificarAsync(SEG_Grupo grupo) 
        {
            _context.SEG_Grupos.Update(grupo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.SEG_Grupos.Where(u => u.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_Grupo?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Grupos.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<SEG_Grupo?> ObtenerPorCodigoAsync(string codigo) 
        {
            return await _context.SEG_Grupos.FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public IQueryable<SEG_Grupo> Listar() 
        {
            return _context.SEG_Grupos
                .Include(g => g.UsuarioCreador)
                .Include(g => g.UsuarioModificador);
        }
    }
}
