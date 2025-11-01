using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSDatosComunes
    {
        Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoListaAsync(string codigoLista);
        Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante);
    }
}
