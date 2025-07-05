using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class NotificadorCorreo : INotificadorCorreo
    {
        private readonly IMSEnvioCorreosBackgroundServicio _msEnvioCorreosBackgroundServicio;

        public NotificadorCorreo(IMSEnvioCorreosBackgroundServicio msEnvioCorreosBackgroundServicio)
        {
            _msEnvioCorreosBackgroundServicio = msEnvioCorreosBackgroundServicio;
        }

        public async Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            await _msEnvioCorreosBackgroundServicio.EnviarCorreoAsync(datoCorreoRequest);
            return true;
        }
    }
}
