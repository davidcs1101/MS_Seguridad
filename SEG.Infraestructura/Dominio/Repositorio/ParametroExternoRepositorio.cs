using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class ParametroExternoRepositorio : IParametroExternoRepositorio
    {
        private readonly AppDbContext _context;
        public ParametroExternoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_ParametroExterno parametroExterno) 
        {
            _context.SEG_ParametrosExternos.Add(parametroExterno);
            await _context.SaveChangesAsync();
            return parametroExterno.Id;
        }

        public async Task ModificarAsync(SEG_ParametroExterno parametroExterno) 
        {
            _context.SEG_ParametrosExternos.Update(parametroExterno);
            await _context.SaveChangesAsync();
        }

        public IQueryable<SEG_ParametroExterno> Listar() 
        {
            return _context.SEG_ParametrosExternos
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador);
        }
    }
}
