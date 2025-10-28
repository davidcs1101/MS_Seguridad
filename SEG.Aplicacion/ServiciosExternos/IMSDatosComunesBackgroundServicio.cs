using SEG.Dtos;
using Refit;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSDatosComunesBackgroundServicio
    {
        [Post("/listasDetalles/obtenerPorCodigoListaYCodigoListaDetalle")]
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoListaYCodigoListaDetalleAsync([Body] CodigoDetalleRequest codigoDetalleRequest);

        [Post("/listasDetalles/obtenerPorCodigoConstanteYCodigoListaDetalle")]
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync([Body] CodigoDetalleRequest codigoDetalleRequest);

        [Get("/listasDetalles/listarPorCodigoLista")]
        Task<HttpResponseMessage> ListarListasDetallePorCodigoListaAsync([Query] string codigoLista);

        [Get("/listasDetalles/listarPorCodigoConstante")]
        Task<HttpResponseMessage> ListarListasDetallePorCodigoConstanteAsync([Query] string codigoConstante);
    }
}
