using System.Net.Http.Json;
using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using Utilidades;
using SEG.Infraestructura.Servicios.Interfaces;
using System.Net.Http;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSDatosComunesContextoWebServicio : IMSDatosComunesContextoWebServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSDatosComunesContextoWebServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoListaIdDetalleRequest codigoListaIdDetalleRequest)
        {
            var url = $"dco/listasDetalles/validarIdDetalleExisteEnCodigoLista";
            var respuesta = await _httpClient.PostAsJsonAsync(url, codigoListaIdDetalleRequest);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
