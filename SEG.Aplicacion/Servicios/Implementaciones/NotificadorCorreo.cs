using SEG.Dtos;
using Utilidades;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using System.Net.Http.Json;

namespace SEG.Aplicacion.Servicios.Implementaciones
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
                //var apiResponse = await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
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
