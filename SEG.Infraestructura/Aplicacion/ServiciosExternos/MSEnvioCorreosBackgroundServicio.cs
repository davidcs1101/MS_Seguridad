using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Infraestructura.Servicios.Interfaces;
using SEG.Aplicacion.CasosUso.Interfaces;
using System.Net.Http.Json;
using Utilidades;
using System.Net.Http.Headers;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSEnvioCorreosBackgroundServicio : IMSEnvioCorreosBackgroundServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;
        private readonly IAutenticacionServicio _autenticacionServicio;

        public MSEnvioCorreosBackgroundServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador, IAutenticacionServicio autenticacionServicio)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
            _autenticacionServicio = autenticacionServicio;
        }

        public async Task<HttpResponseMessage> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            await this.AutenticarUsuarioAsync(new AutenticacionRequest { NombreUsuario = "ADMINISTRADOR", Clave = "1" });

            var url = "api/correos/enviarCorreo";
            var respuesta = await _httpClient.PostAsJsonAsync(url, datoCorreoRequest);

            _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        public async Task AutenticarUsuarioAsync(AutenticacionRequest autenticacionRequest)
        {
            var respuesta = await _autenticacionServicio.AutenticarUsuarioAsync(autenticacionRequest);

            if (!respuesta.Correcto)
                throw new Exception(Textos.Usuarios.MENSAJE_LOGIN_INCORRECTO);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.Data);
        }
    }
}
