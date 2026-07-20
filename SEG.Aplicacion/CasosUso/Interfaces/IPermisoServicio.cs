using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IPermisoServicio
    {
        Task<ApiResponse<string>> ModificarAsync(PermisoModificacionRequest permisoModificacionRequest);
        Task<ApiResponse<PermisoDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<PermisoDto>?>> ListarAsync();
    }
}
