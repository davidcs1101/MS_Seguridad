using SEG.Infraestructura.Servicios.Interfaces;
using SEG.Dominio.Excepciones;

namespace SEG.Infraestructura.Servicios.Implementaciones
{
    public class RespuestaHttpValidador : IRespuestaHttpValidador
    {
        public void ValidarRespuesta(HttpResponseMessage respuesta, string mensaje) {
            if (!respuesta.IsSuccessStatusCode)
                throw new SolicitudHttpException($"{mensaje} : {respuesta.ReasonPhrase}");
        }
    }
}
