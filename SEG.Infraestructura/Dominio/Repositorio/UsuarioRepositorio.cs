using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ParametrosConsultas;
using SEG.Dominio.Repositorio;
using SEG.Infraestructura.Dominio.Repositorio.Helpers;
using System.Linq.Expressions;
using Utilidades.Helpers;

namespace SEG.Infraestructura.Dominio.Repositorio
{   
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;
        private static readonly Dictionary<string,
            Expression<Func<SEG_Usuario, object>>> ColumnasOrdenables =
            new()
            {
                { "Identificacion", x => x.Identificacion },
                { "Nombre1", x => x.Nombre1 },
                { "Nombre2", x => x.Nombre2 },
                { "Apellido1", x => x.Apellido1 },
                { "Apellido2", x => x.Apellido2 },
                { "Email", x => x.Email },
                { "NombreUsuario", x => x.NombreUsuario },
                { "EstadoActivo", x => x.EstadoActivo }
            };

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

        public async Task<List<SEG_Usuario>> ListarAsync()
        {
            return await _context.SEG_Usuarios
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<List<SEG_Usuario>> ListarAsync(UsuarioConsulta consulta)
        {
            IQueryable<SEG_Usuario> query = _context.SEG_Usuarios
                .Include(x => x.UsuarioCreador)
                .Include(x => x.UsuarioModificador)
                .AsNoTracking();

            query = UsuarioQueryHelper.AplicarFiltros(query, consulta);

            query = QueryOrdenamientoHelper.Aplicar(
                query,
                consulta.CampoOrden,
                consulta.Descendente,
                ColumnasOrdenables);

            query = QueryPaginacionHelper.Aplicar(
                query,
                consulta.Pagina,
                consulta.RegistrosPorPagina);

            return await query.ToListAsync();
        }

        public async Task<int> ContarRegistrosAsync(UsuarioConsulta consulta)
        {
            IQueryable<SEG_Usuario> query = _context.SEG_Usuarios.AsNoTracking();

            query = UsuarioQueryHelper.AplicarFiltros(query, consulta);

            return await query.CountAsync();
        }
    }
}
