using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using System.Net.Http.Json;
using Utilidades;
using SEG.Aplicacion.ServiciosExternos.config;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class PublicadorEventosBackgroundServicio : IPublicadorEventosBackgroundServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public PublicadorEventosBackgroundServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> PublicarActualizacionPermisos(string url) 
        {
            var requestUrl = $"{url}";
            var respuesta = await _httpClient.PostAsJsonAsync(requestUrl, "");
            await _respuestaHttpValidador.ValidarRespuesta(
                respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
