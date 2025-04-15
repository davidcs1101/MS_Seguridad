using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEnvioCorreosServicio
    {
        Task<HttpResponseMessage> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
