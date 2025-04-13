using SEG.Dtos;
using Utilidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class NotificadorCorreo : INotificadorCorreo
    {
        private readonly IMSEnvioCorreosServicio _msEnvioCorreosServicio;

        public NotificadorCorreo(IMSEnvioCorreosServicio msEnvioCorreosServicio, IConstructorTextosNotificacion constructorTextosNotificacion)
        {
            _msEnvioCorreosServicio = msEnvioCorreosServicio;
        }

        public async Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            try
            {
                var respuesta = await _msEnvioCorreosServicio.EnviarCorreoAsync(datoCorreoRequest);
                return true;
            }
            catch (Exception e)
            {
                Logs.EscribirLog("e", e.Message);
                return false;
            }
        }
    }
}
