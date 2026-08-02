using Refit;
namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSDatosComunesBackgroundServicio
    {
        [Get("/listasDetalles/listar")]
        Task<HttpResponseMessage> ListarListasDetalleAsync();


        [Post("/listasDetalles/listarPorCodigosConstante")]
        Task<HttpResponseMessage> ListarListasDetallePorCodigosConstanteAsync(List<string> codigosConstante);
    }
}
