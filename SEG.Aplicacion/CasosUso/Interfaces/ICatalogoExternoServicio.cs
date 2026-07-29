using SEG.Dtos;
using Utilidades.Dtos;
namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface ICatalogoExternoServicio
    {
        Task<ApiResponseDto<int>> SincronizarDatosAsync();
        Task<ApiResponseDto<List<ProgramaDto>?>> ListarAsync();
    }
}
