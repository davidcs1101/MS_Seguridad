using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IGrupoServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(GrupoCreacionRequest grupoCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(GrupoModificacionRequest grupoModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<GrupoDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponseDto<GrupoDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponseDto<List<GrupoDto>?>> ListarAsync();
    }
}
