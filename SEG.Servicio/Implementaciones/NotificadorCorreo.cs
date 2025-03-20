using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Servicio.Implementaciones
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
