using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface INotificadorWhatsApp
    {
        Task<bool> EnviarAsync(DatoWhatsAppRequest datoWhatsAppRequest);
    }
}
