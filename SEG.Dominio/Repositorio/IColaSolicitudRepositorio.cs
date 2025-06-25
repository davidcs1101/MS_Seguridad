using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IColaSolicitudRepositorio
    {
        void MarcarCrear(SEG_ColaSolicitud colaSolicitud);
        void MarcarModificar(SEG_ColaSolicitud colaSolicitud);
        Task<SEG_ColaSolicitud?> ObtenerPorIdAsync(int id);
        IQueryable<SEG_ColaSolicitud> Listar();
    }
}
