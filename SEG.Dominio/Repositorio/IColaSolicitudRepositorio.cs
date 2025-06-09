using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IColaSolicitudRepositorio
    {
        void MarcarCrear(SEG_ColaSolicitud colaSolicitud);

        IQueryable<SEG_ColaSolicitud> Listar();
    }
}
