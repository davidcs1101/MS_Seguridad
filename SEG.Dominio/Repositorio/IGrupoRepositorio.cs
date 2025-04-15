using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IGrupoRepositorio
    {
        Task<int> CrearAsync(SEG_Grupo grupo);
        Task ModificarAsync(SEG_Grupo grupo);
        Task<bool> EliminarAsync(int id);
        Task<SEG_Grupo?> ObtenerPorIdAsync(int id);
        Task<SEG_Grupo?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<SEG_Grupo> Listar();
    }
}
