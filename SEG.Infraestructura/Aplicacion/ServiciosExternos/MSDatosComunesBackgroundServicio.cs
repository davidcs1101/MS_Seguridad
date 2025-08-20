using System.Net.Http.Json;
using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using Utilidades;
using SEG.Infraestructura.Servicios.Interfaces;
using System.Net.Http;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSDatosComunesBackgroundServicio : IMSDatosComunesBackgroundServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSDatosComunesBackgroundServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> ObtenerListaDetallePorCodigoListaYCodigoListaDetalleAsync(CodigoDetalleRequest codigoDetalleRequest)
        {
            var url = $"dco/listasDetalles/obtenerPorCodigoListaYCodigoListaDetalle";
            var respuesta = await _httpClient.PostAsJsonAsync(url, codigoDetalleRequest);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        public async Task<HttpResponseMessage> ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoDetalleRequest)
        {
            var url = $"dco/listasDetalles/obtenerPorCodigoConstanteYCodigoListaDetalle";
            var respuesta = await _httpClient.PostAsJsonAsync(url, codigoDetalleRequest);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        public async Task<HttpResponseMessage> ListarListasDetallePorCodigoListaAsync(string codigoLista)
        {
            var url = $"dco/listasDetalles/listarPorCodigoLista?codigoLista={codigoLista}";
            var respuesta = await _httpClient.GetAsync(url);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        public async Task<HttpResponseMessage> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante)
        {
            var url = $"dco/listasDetalles/listarPorCodigoConstante?codigoConstante={codigoConstante}";
            var respuesta = await _httpClient.GetAsync(url);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
