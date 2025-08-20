using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSDatosComunes
    {
        Task<int> ObtenerIdListaDetallePorCodigoListaYCodigoListaDetalleAsync(CodigoDetalleRequest codigoListaIdDetalleRequest);
        Task<int> ObtenerIdListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoListaIdDetalleRequest);

        Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoListaAsync(string codigoLista);
        Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante);
    }
}
