using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IGrupoPermisoRepositorio
    {
        Task<int> CrearAsync(SEG_GrupoPermiso grupoPermiso);
        Task ModificarAsync(SEG_GrupoPermiso grupoPermiso);
        Task<bool> EliminarAsync(int id);
        Task<SEG_GrupoPermiso?> ObtenerGrupoPermisoAsync(int grupoId, int permisoId);
        Task<SEG_GrupoPermiso?> ObtenerPorIdAsync(int id);
        IQueryable<SEG_GrupoPermiso> ListarPermisosPorGrupo(int grupoId);
        IQueryable<SEG_GrupoPermiso> Listar();
    }
}
