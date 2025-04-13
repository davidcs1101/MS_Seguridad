using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio.Interfaces
{
    public interface IUsuarioSedeGrupoRepositorio
    {
        Task<int> CrearAsync(SEG_UsuarioSedeGrupo usuariosSedesGrupos);
        Task ModificarAsync(SEG_UsuarioSedeGrupo usuariosSedesGrupos);
        Task<bool> EliminarAsync(int id);
        Task<SEG_UsuarioSedeGrupo?> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId);
        Task<SEG_UsuarioSedeGrupo?> ObtenerPorIdAsync(int id);
        IQueryable<SEG_UsuarioSedeGrupo> ListarPorUsuarioId(int usuarioId);
    }
}
