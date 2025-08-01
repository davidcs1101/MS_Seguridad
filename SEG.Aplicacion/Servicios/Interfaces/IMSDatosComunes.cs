using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSDatosComunes
    {
        Task<int> ObtenerIdListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoListaIdDetalleRequest);
    }
}
