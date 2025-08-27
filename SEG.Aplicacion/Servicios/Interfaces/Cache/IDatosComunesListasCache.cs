using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces.Cache
{
    public interface IDatosComunesListasCache
    {
        Task InicializarAsync();
        ApiResponse<string> Actualizar(List<ListaDetalleDto> nuevosDatos);
        IReadOnlyList<ListaDetalleDto> ListarPorCodigoLista(string codigoLista);
        ListaDetalleDto? ObtenerPorCodigoListaYId(string codigoLista,int id);
        ListaDetalleDto? ObtenerPorCodigoListaYCodigoListaDetalle(string codigoLista,string codigoDetalle);

    }
}
