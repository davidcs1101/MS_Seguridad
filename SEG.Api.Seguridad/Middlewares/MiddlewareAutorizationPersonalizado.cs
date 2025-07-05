using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using Utilidades;

namespace SEG.Api.Seguridad.Middlewares
{
    public class MiddlewareAutorizationPersonalizado
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        public MiddlewareAutorizationPersonalizado(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
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

                using (var scope = _serviceProvider.CreateScope()) 
                {
                    var _apiResponse = scope.ServiceProvider.GetRequiredService<IApiResponse>();
                    var _serializadorJson = scope.ServiceProvider.GetRequiredService<ISerializadorJsonServicio>();
                    //Si No contiene SedeId, Es un Token de usuario (Login Inicial)
                    if (!contexto.User.HasClaim(c => c.Type == "SedeId"))
                    {
                        //Si el usuario no ha realizado su cambio de clave, DENEGAMOS el acceso
                        if (!contexto.User.HasClaim(c => c.Type == "Accion" && c.Value == "CAMBIOCLAVEOK"))
                        {
                            contexto.Response.ContentType = "application/json";

                            var respuestaJson = _serializadorJson.Serializar(_apiResponse.CrearRespuesta(false, Textos.Usuarios.MENSAJE_USUARIO_NO_CAMBIO_CLAVE, ""));
                            contexto.Response.StatusCode = 403; //Forbidden
                            await contexto.Response.WriteAsync(respuestaJson);
                            return;
                        }
                    }
                }
            }
            await _next(contexto);
        }
    }
}
