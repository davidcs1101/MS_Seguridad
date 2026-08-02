using Refit;
namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEmpresasBackgroundServicio
    {
        [Get("/sedes/obtenerPorId")]
        Task<HttpResponseMessage> ObtenerSedePorIdAsync([Query] int id);

        [Get("/sedes/listar")]
        Task<HttpResponseMessage> ListarSedesAsync();
    }
}
