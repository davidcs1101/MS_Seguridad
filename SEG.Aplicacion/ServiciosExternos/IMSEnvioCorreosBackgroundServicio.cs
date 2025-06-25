using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEnvioCorreosBackgroundServicio
    {
        Task<HttpResponseMessage> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
