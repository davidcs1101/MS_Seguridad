using Refit;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEmpresasContextoWebServicio
    {
        [Get("/sedes/obtenerPorId")]
        Task<HttpResponseMessage> ObtenerSedePorIdAsync([Query] int id);
    }
}
