using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos.config;

namespace SEG.Api.Seguridad.Middlewares
{
    public class MiddlewareManejadorTokensBackground : DelegatingHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly IAutenticacionServicio _autenticacionServicio;
        private readonly IConfiguracionesTrabajosColas _configuracionesTrabajosColas;

        public MiddlewareManejadorTokensBackground(IConfiguration configuration, IMemoryCache cache, IAutenticacionServicio autenticacionServicio, IConfiguracionesTrabajosColas configuracionesTrabajosColas)
        {
            _configuration = configuration;
            _cache = cache;
            _autenticacionServicio = autenticacionServicio;
            _configuracionesTrabajosColas = configuracionesTrabajosColas;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Intenta obtener el token desde caché
            if (!_cache.TryGetValue("Token", out string token))
            {
                var datosToken = await AutenticarUsuarioAsync();
                token = datosToken.Token;

                //Calculamos el tiempo hasta la expiración
                var ahora = DateTime.UtcNow;
                var expiracion = datosToken.FechaExpiracion.ToUniversalTime();
                var duracion = expiracion - ahora;

                //Si por alguna razón la diferencia es negativa (ej. reloj del servidor), aplicamos un mínimo
                if (duracion <= TimeSpan.Zero)
                    duracion = TimeSpan.FromMinutes(1);

                //Restamos unos minutos de margen antes de que expire (por seguridad)
                var duracionConMargen = duracion - TimeSpan.FromMinutes(1);
                if (duracionConMargen < TimeSpan.Zero)
                    duracionConMargen = TimeSpan.FromMinutes(1);

                //Guardamos el token en memoria segun la duración calculada con base en la fecha de expiración obtenida desde el servicio.
                _cache.Set("Token", token, duracionConMargen);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<AutenticacionResponse> AutenticarUsuarioAsync()
        {
            AutenticacionRequest autenticacionRequest = new AutenticacionRequest()
            {
                NombreUsuario = _configuracionesTrabajosColas.ObtenerUsuarioIntegracion(),
                Clave = _configuracionesTrabajosColas.ObtenerClaveIntegracion()
            };
            return (await _autenticacionServicio.AutenticarUsuarioAsync(autenticacionRequest)).Data!;
        }

    }
}
