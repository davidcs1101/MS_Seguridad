using SEG.Dtos;
using Utilidades.Dtos;
namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IPermisoServicio
    {
        Task<ApiResponseDto<string>> ModificarAsync(PermisoModificacionRequest permisoModificacionRequest);
        Task<ApiResponseDto<PermisoDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponseDto<List<PermisoDto>?>> ListarAsync();
    }
}
