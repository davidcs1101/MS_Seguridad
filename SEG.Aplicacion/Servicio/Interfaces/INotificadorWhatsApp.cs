using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface INotificadorWhatsApp
    {
        Task<bool> EnviarAsync(DatoWhatsAppRequest datoWhatsAppRequest);
    }
}
