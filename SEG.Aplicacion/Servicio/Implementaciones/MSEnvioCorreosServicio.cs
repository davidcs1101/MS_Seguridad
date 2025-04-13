using SEG.Dtos;
using System.Net.Http.Json;
using Utilidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class MSEnvioCorreosServicio : IMSEnvioCorreosServicio
    {
        private readonly HttpClient _httpClient;
        public MSEnvioCorreosServicio(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            var url = "api/correos/enviarCorreo";
            var respuesta = await _httpClient.PostAsJsonAsync(url, datoCorreoRequest);

            if (!respuesta.IsSuccessStatusCode)
                throw new HttpRequestException($"{Textos.Generales.MENSAJE_CORREO_ENVIADO_ERROR}: {respuesta.ReasonPhrase}");

            return await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
        }
    }
}
