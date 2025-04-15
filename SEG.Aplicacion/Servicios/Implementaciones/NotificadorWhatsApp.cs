using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
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
