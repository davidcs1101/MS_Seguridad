using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class AccionRepositorio : IAccionRepositorio
    {
        private readonly AppDbContext _context;
        public AccionRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_Accion accion) 
        {
            _context.SEG_Acciones.Add(accion);
            await _context.SaveChangesAsync();
            return accion.Id;
        }

        public async Task ModificarAsync(SEG_Accion accion) 
        {
            _context.SEG_Acciones.Update(accion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.SEG_Acciones.Where(a => a.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_Accion?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Acciones.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<SEG_Accion?> ObtenerPorCodigoAsync(string codigo) 
        {
            return await _context.SEG_Acciones.FirstOrDefaultAsync(a => a.Codigo == codigo);
        }

        public IQueryable<SEG_Accion> Listar() 
        {
            return _context.SEG_Acciones
                .Include(a => a.UsuarioCreador)
                .Include(a => a.UsuarioModificador);
        }
    }
}
