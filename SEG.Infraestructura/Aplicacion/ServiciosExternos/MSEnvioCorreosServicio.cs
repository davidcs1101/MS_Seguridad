using SEG.Dtos;
using System.Net.Http.Json;
using Utilidades;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Dominio.Excepciones;
using SEG.Infraestructura.Servicios.Interfaces;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSEnvioCorreosServicio : IMSEnvioCorreosServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSEnvioCorreosServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador = null)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            var url = "api/correos/enviarCorreo";
            var respuesta = await _httpClient.PostAsJsonAsync(url, datoCorreoRequest);

            _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
