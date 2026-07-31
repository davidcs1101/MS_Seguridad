using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces.Cache
{
    public interface IMaestroExternoCache
    {
        Task InicializarAsync();
        ApiResponseDto<string> Actualizar(List<MaestroExternoDto> nuevosDatos);
        IReadOnlyList<MaestroExternoDto> ListarPorCodigoMaestro(string codigoMaestro);
        MaestroExternoDto? ObtenerPorCodigoMaestroYOrigenId(string codigoMaestro, int origenId);
        MaestroExternoDto? ObtenerPorCodigoMaestroYCodigo(string codigoMaestro, string codigo);
        Task RefrescarAsync();
    }
}
