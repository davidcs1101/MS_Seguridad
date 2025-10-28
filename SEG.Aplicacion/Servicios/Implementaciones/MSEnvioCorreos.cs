using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSEnvioCorreos : IMSEnvioCorreos
    {
        private readonly IMSEnvioCorreosBackgroundServicio _msEnvioCorreosBackgroundServicio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSEnvioCorreos(IMSEnvioCorreosBackgroundServicio msEnvioCorreosBackgroundServicio, ISerializadorJsonServicio serializadorJsonServicio, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _msEnvioCorreosBackgroundServicio = msEnvioCorreosBackgroundServicio;
            _serializadorJsonServicio = serializadorJsonServicio;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            var respuesta = await _msEnvioCorreosBackgroundServicio.EnviarCorreoAsync(datoCorreoRequest);
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Utilidades.Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            return true;
        }
    }
}
