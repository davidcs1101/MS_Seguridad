using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSEnvioCorreos : IMSEnvioCorreos
    {
        private readonly IMSEnvioCorreosBackgroundServicio _msEnvioCorreosBackgroundServicio;
        private readonly IServicioComun _servicioComun;

        public MSEnvioCorreos(IMSEnvioCorreosBackgroundServicio msEnvioCorreosBackgroundServicio,  IServicioComun servicioComun)
        {
            _msEnvioCorreosBackgroundServicio = msEnvioCorreosBackgroundServicio;
            _servicioComun = servicioComun;
        }

        public async Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<DatoCorreoRequest, bool>(
                funcionEjecutar: _msEnvioCorreosBackgroundServicio.EnviarCorreoAsync,
                request: datoCorreoRequest);
        }
    }
}
