using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IProgramaServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(ProgramaCreacionRequest grupoCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(ProgramaModificacionRequest grupoModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<ProgramaDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponseDto<ProgramaDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponseDto<List<ProgramaDto>?>> ListarAsync();
    }
}
