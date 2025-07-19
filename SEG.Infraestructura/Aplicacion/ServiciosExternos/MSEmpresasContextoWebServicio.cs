using System.Net.Http.Json;
using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using Utilidades;
using SEG.Infraestructura.Servicios.Interfaces;
using System.Net.Http;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSEmpresasContextoWebServicio : IMSEmpresasContextoWebServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSEmpresasContextoWebServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> ObtenerSedePorIdAsync(int id)
        {
            var url = $"emp/sedes/obtenerPorId?id={id}";
            var respuesta = await _httpClient.GetAsync(url);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
