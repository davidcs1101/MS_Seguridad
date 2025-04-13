using SEG.Dtos;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class NotificadorWhatsApp : INotificadorWhatsApp
    {
        private readonly IMSEnvioCorreosServicio _msEnvioCorreosServicio;

        public NotificadorWhatsApp(IMSEnvioCorreosServicio msEnvioCorreosServicio, IConstructorTextosNotificacion constructorTextosNotificacion)
        {
            _msEnvioCorreosServicio = msEnvioCorreosServicio;
        }

        public async Task<bool> EnviarAsync(DatoWhatsAppRequest datoWhatsAppRequest) 
        {
            return true;
        }
    }
}
