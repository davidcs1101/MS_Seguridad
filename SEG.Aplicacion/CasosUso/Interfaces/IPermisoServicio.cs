using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IPermisoServicio
    {
        Task<ApiResponse<string>> ModificarAsync(PermisoModificacionRequest permisoModificacionRequest);
        Task<ApiResponse<PermisoDto?>> ObtenerPermisoAsync(int id);
        Task<ApiResponse<List<PermisoDto>?>> ListarAsync();
    }
}
