using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSDatosComunesContextoWebServicio
    {
        Task<HttpResponseMessage> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoListaIdDetalleRequest codigoListaIdDetalleRequest);
    }
}
