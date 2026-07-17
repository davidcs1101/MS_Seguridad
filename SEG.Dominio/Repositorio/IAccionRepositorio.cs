using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IAccionRepositorio
    {
        Task<int> CrearAsync(SEG_Accion accion);
        Task ModificarAsync(SEG_Accion accion);
        Task<bool> EliminarAsync(int id);
        Task<SEG_Accion?> ObtenerPorIdAsync(int id);
        Task<SEG_Accion?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<SEG_Accion> Listar();
    }
}
