using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class ProgramaRepositorio : IProgramaRepositorio
    {
        private readonly AppDbContext _context;
        public ProgramaRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_Programa programa) 
        {
            _context.SEG_Programas.Add(programa);
            await _context.SaveChangesAsync();
            return programa.Id;
        }

        public async Task ModificarAsync(SEG_Programa programa) 
        {
            _context.SEG_Programas.Update(programa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.SEG_Programas.Where(p => p.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_Programa?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Programas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<SEG_Programa?> ObtenerPorCodigoAsync(string codigo) 
        {
            return await _context.SEG_Programas.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public IQueryable<SEG_Programa> Listar() 
        {
            return _context.SEG_Programas
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador);
        }
    }
}
