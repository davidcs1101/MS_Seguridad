using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using Utilidades;

namespace SEG.Api.Seguridad.Middlewares
{
    public class MiddlewareAutorizationPersonalizado
    {
        private readonly RequestDelegate _next;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IApiResponse _apiResponse;
        public MiddlewareAutorizationPersonalizado(RequestDelegate next, IServiceProvider serviceProvider, ISerializadorJsonServicio serializadorJsonServicio, IApiResponse apiResponse)
        {
            _next = next;
            _serializadorJsonServicio = serializadorJsonServicio;
            _apiResponse = apiResponse;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            if (contexto.User.Identity.IsAuthenticated)
            {
                //El EndPoint de modificarClave NO requiere validar el Claim de CAMBIOCLAVEOK ya que apenas se va a ralizar
                contexto.GetEndpoint();
                var path = contexto.Request.Path.ToString().ToLower();
                if (path.Contains("modificarclave"))
                {
                    await _next(contexto);
                    return;
                }

                //Si No contiene SedeId, Es un Token de usuario (Login Inicial)
                if (!contexto.User.HasClaim(c => c.Type == "SedeId"))
                {
                    //Si el usuario no ha realizado su cambio de clave, DENEGAMOS el acceso
                    if (!contexto.User.HasClaim(c => c.Type == "Accion" && c.Value == "CAMBIOCLAVEOK"))
                    {
                        contexto.Response.ContentType = "application/json";

                        var respuestaJson = _serializadorJsonServicio.Serializar(_apiResponse.CrearRespuesta(false, Textos.Usuarios.MENSAJE_USUARIO_NO_CAMBIO_CLAVE, ""));
                        contexto.Response.StatusCode = 403; //Forbidden
                        await contexto.Response.WriteAsync(respuestaJson);
                        return;
                    }
                }
            }
            await _next(contexto);
        }
    }
}
