using SEG.Infraestructura.Servicios.Interfaces;
using SEG.Dominio.Excepciones;
using SEG.Dtos;
using System.Net.Http.Json;

namespace SEG.Infraestructura.Servicios.Implementaciones
{
    public class RespuestaHttpValidador : IRespuestaHttpValidador
    {
        public async Task ValidarRespuesta(HttpResponseMessage respuesta, string mensaje) {
            var detalleError = "";
            if (!respuesta.IsSuccessStatusCode)
            {
                detalleError = $"{mensaje} {respuesta.ReasonPhrase}. ";
                var error = await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
                if (error is not null && !string.IsNullOrWhiteSpace(error.Mensaje))
                    detalleError += error.Mensaje;

                throw new SolicitudHttpException(detalleError);
            }
        }
    }
}
