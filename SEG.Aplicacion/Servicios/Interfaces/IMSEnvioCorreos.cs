using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSEnvioCorreos
    {
        Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
