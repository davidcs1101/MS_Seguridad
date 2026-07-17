using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IPermisoRepositorio
    {
        Task ModificarAsync(SEG_Permiso permiso);
        Task<SEG_Permiso?> ObtenerPorProgramaIdYAccionIdAsync(int programaId, int accionId);
        Task<SEG_Permiso?> ObtenerPorIdAsync(int id);
        Task<SEG_Permiso?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<SEG_Permiso> Listar();
    }
}