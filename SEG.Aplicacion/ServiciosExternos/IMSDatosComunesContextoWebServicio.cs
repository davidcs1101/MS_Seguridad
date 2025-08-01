using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSDatosComunesContextoWebServicio
    {
        Task<HttpResponseMessage> ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoDetalleRequest);
    }
}
