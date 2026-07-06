using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAutorizacionServicio
    {
        Task<ApiResponse<List<AutorizacionDto>?>> ObtenerCatalogoAutorizacionAsync();
        Task<List<AutorizacionDto>> ListarCatalogoAutorizacionAsync();
    }
}
