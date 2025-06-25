using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class NotificadorWhatsApp : INotificadorWhatsApp
    {
        private readonly IMSEnvioCorreosBackgroundServicio _msEnvioCorreosServicio;

        public NotificadorWhatsApp(IMSEnvioCorreosBackgroundServicio msEnvioCorreosServicio, IConstructorTextosNotificacion constructorTextosNotificacion)
        {
            _msEnvioCorreosServicio = msEnvioCorreosServicio;
        }

        public async Task<bool> EnviarAsync(DatoWhatsAppRequest datoWhatsAppRequest) 
        {
            return true;
        }
    }
}
