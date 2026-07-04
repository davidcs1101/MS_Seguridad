using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IGrupoPermisoServicio
    {
        //Task<ApiResponse<int>> CrearAsync(GrupoPermisoCreacionRequest grupoPermisoCreacionRequest);
        //Task<ApiResponse<string>> ModificarAsync(GrupoPermisoModificacionRequest grupoPermisoModificacionRequest);
        //Task<ApiResponse<string>> EliminarAsync(int id);
        //Task<ApiResponse<GrupoPermisoDto?>> ObtenerGrupoPermisoAsync(int grupoId, int permisoId);
        Task<ApiResponse<List<GrupoPermisoDto>?>> ListarAsync();
    }
}
