namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEmpresasContextoWebServicio
    {
        Task<HttpResponseMessage> ObtenerSedePorIdAsync(int id);
    }
}
