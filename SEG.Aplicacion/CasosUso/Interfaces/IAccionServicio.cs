using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAccionServicio
    {
        Task<ApiResponse<int>> CrearAsync(AccionCreacionRequest accionCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(AccionModificacionRequest accionModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<AccionDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<AccionDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<AccionDto>?>> ListarAsync();
    }
}
