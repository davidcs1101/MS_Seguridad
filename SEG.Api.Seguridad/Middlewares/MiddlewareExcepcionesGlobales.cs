//using MySqlConnector; //Si queremos poner excepciones del tipo de base de datos especifico
using System.Net;
using Utilidades;
using SEG.Dominio.Excepciones;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos;

namespace SEG.Api.Seguridad.Middlewares
{
    public class MiddlewareExcepcionesGlobales
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IApiResponse _apiResponse;

        public MiddlewareExcepcionesGlobales(RequestDelegate requestDelegate, ISerializadorJsonServicio serializadorJsonServicio, IApiResponse apiResponse)
        {
            _requestDelegate = requestDelegate;
            _serializadorJsonServicio = serializadorJsonServicio;
            _apiResponse = apiResponse;
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
            var respuesta = _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_ERROR_SERVIDOR, "");

            if (e is DatoNoEncontradoException)
            {
                contexto.Response.StatusCode = (int)HttpStatusCode.NotFound;
                respuesta.Mensaje = e.Message;
            }
            else if (e is DatoYaExisteException)
            {
                contexto.Response.StatusCode = (int)HttpStatusCode.Conflict;
                respuesta.Mensaje = e.Message;
            }
            else if (e is SolicitudHttpException)
            {
                contexto.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                respuesta.Mensaje = e.Message;
            }
            else if (e is LoguinException)
            {
                contexto.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                respuesta.Mensaje = e.Message;
            }
            else
            {
                contexto.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            //Siempre escribimos en los logs las diferentes Excepciones
            Logs.EscribirLog("e", "", e);

            // Si es desarrollo, incluir el detalle del error
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                //respuesta.Mensaje = e.Message;
            }

            var respuestaJson = _serializadorJsonServicio.Serializar(respuesta);
            return contexto.Response.WriteAsync(respuestaJson);
        }
    }
}
