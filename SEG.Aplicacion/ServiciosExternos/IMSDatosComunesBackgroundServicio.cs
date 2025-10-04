using SEG.Dtos;
using Refit;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSDatosComunesBackgroundServicio
    {
        [Post("/listasDetalles/obtenerPorCodigoListaYCodigoListaDetalle")]
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoListaYCodigoListaDetalleAsync([Body] CodigoDetalleRequest codigoDetalleRequest);
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoDetalleRequest);
        Task<HttpResponseMessage> ListarListasDetallePorCodigoListaAsync(string codigoLista);
        Task<HttpResponseMessage> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante);
    }
}
