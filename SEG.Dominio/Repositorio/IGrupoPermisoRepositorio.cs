using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ModelosVistas;
namespace SEG.Dominio.Repositorio
{
    public interface IGrupoPermisoRepositorio
    {
        void MarcarCrear(SEG_GrupoPermiso grupoPermiso);
        void MarcarModificar(SEG_GrupoPermiso grupoPermiso);
        void MarcarEliminar(SEG_GrupoPermiso grupoPermiso);
        Task<SEG_GrupoPermiso?> ObtenerGrupoPermisoAsync(int grupoId, int permisoId);
        Task<SEG_GrupoPermiso?> ObtenerPorIdAsync(int id);
        IQueryable<SEG_GrupoPermiso> ListarPermisosPorGrupo(int grupoId);
        IQueryable<SEG_GrupoPermiso> Listar();

        IQueryable<AutorizacionMV> ListarPermisosCache();
    }
}
