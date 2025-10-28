using Refit;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEmpresasContextoWebServicio
    {
        [Post("/sedes/obtenerPorId")]
        Task<HttpResponseMessage> ObtenerSedePorIdAsync([Query] int id);
    }
}
