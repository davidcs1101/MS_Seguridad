using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{   
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;
        public UsuarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SEG_Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SEG_Usuario?> ObtenerPorUsuarioAsync(string usuario)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == usuario);
        }

        public async Task<SEG_Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<SEG_Usuario?> ObtenerPorIdentificacionAsync(int tipoIdentificacionId, string identificacion)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.TipoIdentificacionId == tipoIdentificacionId && x.Identificacion == identificacion);
        }

        public async Task<int> CrearAsync(SEG_Usuario usuario)
        {
            _context.SEG_Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario.Id;
        }

        public void MarcarCrear(SEG_Usuario usuario)
        {
            _context.SEG_Usuarios.Add(usuario);
        }

        public async Task ModificarAsync(SEG_Usuario usuario) 
        {
            _context.SEG_Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public void MarcarModificar(SEG_Usuario usuario)
        {
            _context.SEG_Usuarios.Update(usuario);
        }

        public IQueryable<SEG_Usuario> Listar()
        {
            return _context.SEG_Usuarios;
        }
    }
}
