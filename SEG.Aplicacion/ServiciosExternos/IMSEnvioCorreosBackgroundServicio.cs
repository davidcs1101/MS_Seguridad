using Refit;
using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IMSEnvioCorreosBackgroundServicio
    {
        [Post("/correos/enviarCorreo")]
        Task<HttpResponseMessage> EnviarCorreoAsync([Body] DatoCorreoRequest datoCorreoRequest);
    }
}
