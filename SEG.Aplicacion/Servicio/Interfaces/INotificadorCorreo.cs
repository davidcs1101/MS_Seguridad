using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface INotificadorCorreo
    {
        Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
