using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System.Net.Http;
using Utilidades;

namespace SEG.Api.Seguridad.Infraestructura
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
                    var _apiResponseServicio = scope.ServiceProvider.GetRequiredService<IApiResponseServicio>();
                    //Si No contiene SedeId, Es un Token de usuario (Login Inicial)
                    if (!contexto.User.HasClaim(c => c.Type == "SedeId"))
                    {
                        //Si el usuario no ha realizado su cambio de clave, DENEGAMOS el acceso
                        if (!contexto.User.HasClaim(c => c.Type == "Accion" && c.Value == "CAMBIOCLAVEOK"))
                        {
                            contexto.Response.ContentType = "application/json";

                            var respuestaJson = JsonConvert.SerializeObject(_apiResponseServicio.CrearRespuesta(false, "El usuario no ha realizado el cambio de clave. No tienes permiso para realizar la acción.", ""));
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
