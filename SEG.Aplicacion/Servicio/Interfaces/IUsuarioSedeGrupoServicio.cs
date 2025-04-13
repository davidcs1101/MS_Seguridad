using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface IUsuarioSedeGrupoServicio
    {
        Task<ApiResponse<int>> CrearAsync(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacion);
        Task<ApiResponse<string>> ModificarAsync(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<UsuarioSedeGrupoDto?>> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId);
        Task<ApiResponse<List<UsuarioSedeGrupoDto>?>> ListarPorUsuarioIdLogueadoAsync();
    }
}
