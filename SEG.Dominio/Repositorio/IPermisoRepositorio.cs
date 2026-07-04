using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IPermisoRepositorio
    {
        Task<int> CrearAsync(SEG_Permiso permiso);
        Task ModificarAsync(SEG_Permiso permiso);
        Task<bool> EliminarAsync(int id);
        //Task<SEG_Permiso?> ObtenerPorProgramaIdYPermisoIdAsync(int programaId, int permisoId);
        Task<SEG_Permiso?> ObtenerPorIdAsync(int id);
        Task<SEG_Permiso?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<SEG_Permiso> ListarPermisosPorPrograma(int programaId);
    }
}