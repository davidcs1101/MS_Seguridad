using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio.Interfaces
{
    public interface IGrupoProgramaRepositorio
    {
        Task<int> CrearAsync(SEG_GrupoPrograma grupoPrograma);
        Task ModificarAsync(SEG_GrupoPrograma grupoPrograma);
        Task<bool> EliminarAsync(int id);
        Task<SEG_GrupoPrograma?> ObtenerGrupoProgramaAsync(int grupoId, int programaId);
        Task<SEG_GrupoPrograma?> ObtenerPorIdAsync(int id);
        IQueryable<SEG_GrupoPrograma> ListarProgramasPorGrupo(int grupoId);
    }
}
