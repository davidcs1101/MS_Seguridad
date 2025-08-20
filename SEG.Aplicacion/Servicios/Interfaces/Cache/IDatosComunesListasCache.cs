using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces.Cache
{
    public interface IDatosComunesListasCache
    {
        Task InicializarAsync();
        Task ActualizarAsync(List<ListaDetalleDto> nuevosDatos);
        IReadOnlyList<ListaDetalleDto> ListarPorCodigoLista(string codigoLista);
        ListaDetalleDto? ObtenerPorId(int id);
        ListaDetalleDto? ObtenerPorCodigoDetalle(string codigoDetalle);

    }
}
