using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface IMSEnvioCorreosServicio
    {
        Task<ApiResponse<string>> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
