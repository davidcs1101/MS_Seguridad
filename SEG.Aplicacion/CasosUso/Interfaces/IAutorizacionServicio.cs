using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAutorizacionServicio
    {
        Task<ApiResponseDto<List<AutorizacionDto>?>> ObtenerCatalogoAutorizacionAsync();
        Task<List<AutorizacionDto>> ListarCatalogoAutorizacionAsync();
    }
}
