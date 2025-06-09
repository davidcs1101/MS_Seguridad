using SEG.Dominio.Repositorio;
using SEG.DataAccess;
using SEG.Dominio.Entidades;

namespace SEG.Intraestructura.Dominio.Repositorio
{
    public class ColaSolicitudRepositorio : IColaSolicitudRepositorio
    {
        private readonly AppDbContext _context;

        public ColaSolicitudRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public void MarcarCrear(SEG_ColaSolicitud colaSolicitud)
        {

            _context.SEG_ColaSolicitudes.Add(colaSolicitud);
        }

        public IQueryable<SEG_ColaSolicitud> Listar()
        {
            return _context.SEG_ColaSolicitudes;
        }
    }
}
