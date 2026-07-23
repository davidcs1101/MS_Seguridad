using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSDatosComunes
    {
        Task<List<ListaDetalleDto?>> ListarListasDetalleAsync();
    }
}
