using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSDatosComunesBackgroundServicio
    {
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoListaYCodigoListaDetalleAsync(CodigoDetalleRequest codigoDetalleRequest);
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoDetalleRequest);
        Task<HttpResponseMessage> ListarListasDetallePorCodigoListaAsync(string codigoLista);
        Task<HttpResponseMessage> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante);
    }
}
