using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IProgramaRepositorio
    {
        Task<int> CrearAsync(SEG_Programa programa);
        Task ModificarAsync(SEG_Programa programa);
        Task<bool> EliminarAsync(int id);
        Task<SEG_Programa?> ObtenerPorIdAsync(int id);
        Task<SEG_Programa?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<SEG_Programa> Listar();
    }
}
