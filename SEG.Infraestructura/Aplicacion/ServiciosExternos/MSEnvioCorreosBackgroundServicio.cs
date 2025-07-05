using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Infraestructura.Servicios.Interfaces;
using SEG.Aplicacion.CasosUso.Interfaces;
using System.Net.Http.Json;
using Utilidades;
using System.Net.Http.Headers;
using SEG.Aplicacion.ServiciosExternos.config;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSEnvioCorreosBackgroundServicio : IMSEnvioCorreosBackgroundServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;
        private readonly IAutenticacionServicio _autenticacionServicio;
        private readonly IConfiguracionesTrabajosColas _configuracionesTrabajosColas;

        public MSEnvioCorreosBackgroundServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador, IAutenticacionServicio autenticacionServicio, IConfiguracionesTrabajosColas configuracionesTrabajosColas)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
            _autenticacionServicio = autenticacionServicio;
            _configuracionesTrabajosColas = configuracionesTrabajosColas;
        }

        public async Task<HttpResponseMessage> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            await AutenticarUsuarioAsync();

            var url = "api/correos/enviarCorreo";
            var respuesta = await _httpClient.PostAsJsonAsync(url, datoCorreoRequest);

            _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        private async Task AutenticarUsuarioAsync()
        {
            AutenticacionRequest autenticacionRequest = new AutenticacionRequest()
            {
                NombreUsuario = _configuracionesTrabajosColas.ObtenerUsuarioIntegracion(),
                Clave = _configuracionesTrabajosColas.ObtenerClaveIntegracion()
            };
            var respuesta = await _autenticacionServicio.AutenticarUsuarioAsync(autenticacionRequest);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.Data);
        }
    }
}
