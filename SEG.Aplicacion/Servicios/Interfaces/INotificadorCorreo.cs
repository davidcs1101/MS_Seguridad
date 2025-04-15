using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface INotificadorCorreo
    {
        Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
