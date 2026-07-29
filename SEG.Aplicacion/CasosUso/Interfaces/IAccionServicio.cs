using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAccionServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(AccionCreacionRequest accionCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(AccionModificacionRequest accionModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<AccionDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponseDto<AccionDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponseDto<List<AccionDto>?>> ListarAsync();
    }
}
