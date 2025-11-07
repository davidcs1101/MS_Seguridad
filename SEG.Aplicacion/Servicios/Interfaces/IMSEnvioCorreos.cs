using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSEnvioCorreos
    {
        Task<string> EnviarAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
