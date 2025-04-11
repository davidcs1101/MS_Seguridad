using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MySqlConnector;
using Newtonsoft.Json;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System;
using System.Net;
using Utilidades;

namespace SEG.Api.Seguridad.Infraestructura
{
    public class MiddlewareExcepcionesGlobales
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IServiceProvider _serviceProvider;

        public MiddlewareExcepcionesGlobales(RequestDelegate requestDelegate, IServiceProvider serviceProvider = null)
        {
            _requestDelegate = requestDelegate;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                //Llamamos al siguiente MiddleWare en la cadena de ejecución
                await _requestDelegate(httpContext);
            }
            catch (Exception e)
            {
                await ManejarExcepcionesAsync(httpContext, e);
            }
        }

        private Task ManejarExcepcionesAsync(HttpContext contexto, Exception e) 
        {
            contexto.Response.ContentType = "application/json";
            using (var scope = _serviceProvider.CreateScope()) 
            { 
                var _apiResponseServicio = scope.ServiceProvider.GetRequiredService<IApiResponseServicio>();
                var respuesta = _apiResponseServicio.CrearRespuesta(false, Textos.Generales.MENSAJE_ERROR_SERVIDOR, "");

                if (e is KeyNotFoundException)
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    respuesta.Mensaje = e.Message;
                }
                else if (e is DbUpdateException)
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    respuesta.Mensaje = e.Message;
                }
                else
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Logs.EscribirLog("e", "", e);
                }

                // Si es desarrollo, incluir el detalle del error
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    //respuesta.Mensaje = e.Message;
                }

                var respuestaJson = JsonConvert.SerializeObject(respuesta);
                return contexto.Response.WriteAsync(respuestaJson);
            }
        }
    }
}
