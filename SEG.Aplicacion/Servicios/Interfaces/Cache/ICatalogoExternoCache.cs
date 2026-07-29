using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces.Cache
{
    public interface ICatalogoExternoCache
    {
        Task InicializarAsync();
        ApiResponseDto<string> Actualizar(List<CatalogoExternoDto> nuevosDatos);
        IReadOnlyList<CatalogoExternoDto> ListarPorCodigoCatalogo(string codigoCatalogo);
        CatalogoExternoDto? ObtenerPorCodigoCatalogoYOrigenId(string codigoCatalogo, int origenId);
        CatalogoExternoDto? ObtenerPorCodigoCatalogoYCodigo(string codigoCatalogo, string codigo);
        Task RefrescarAsync();
    }
}
