using Microsoft.Extensions.Options;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dtos.AppSettings;
using Utilidades.Serializacion.Interfaces;
namespace SEG.Infraestructura.Aplicacion.ServiciosExternos.Config
{
    public class AppSettings : IAppSettings
    {
        private readonly TrabajosColasSettings _trabajosColas;
        private readonly EventosNotificarSettings _eventosNotificar;
        private readonly JWTSettings _jwt;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public AppSettings(
            IOptions<TrabajosColasSettings> opcionesTrabajosColas, IOptions<EventosNotificarSettings> eventosNotificar, IOptions<JWTSettings> jwt, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _trabajosColas = opcionesTrabajosColas.Value;
            _eventosNotificar = eventosNotificar.Value;
            _jwt = jwt.Value;
            _serializadorJsonServicio = serializadorJsonServicio;
        }


        //TrabajosColas
        public TrabajosColasSettings ObtenerTrabajosColasSettings() 
        {
            return new TrabajosColasSettings
            {
                CantidadIntentosPorRegistroEnCola = _trabajosColas.CantidadIntentosPorRegistroEnCola,

                CantidadRegistrosProcesarIteracion = _trabajosColas.CantidadRegistrosProcesarIteracion,

                ProcesarColaSolicitudesCron =
                    string.IsNullOrWhiteSpace(_trabajosColas.ProcesarColaSolicitudesCron)
                        ? "*/5 * * * *"
                        : _trabajosColas.ProcesarColaSolicitudesCron,

                UsuarioIntegracion = _trabajosColas.UsuarioIntegracion,

                ClaveIntegracion = _trabajosColas.ClaveIntegracion
            };
        }

        //EventosNotificar/ActualizarPermisos
        public List<string?> ObtenerEventosNotificarActualizarPermisos()
        {
            var urls = _eventosNotificar.ActualizarPermisos;
            return ObtenerListasUrls(urls);
        }

        // JWT
        public JWTSettings ObtenerJWT()
        {
            return new JWTSettings
            {
                Emisor = string.IsNullOrWhiteSpace(_jwt.Emisor)
                    ? ""
                    : _jwt.Emisor,

                Audiencia = string.IsNullOrWhiteSpace(_jwt.Audiencia)
                    ? ""
                    : _jwt.Audiencia,

                Llave = string.IsNullOrWhiteSpace(_jwt.Llave)
                    ? ""
                    : _jwt.Llave,

                MinutosDuracionTokenAutenticacionUsuario = _jwt.MinutosDuracionTokenAutenticacionUsuario,

                MinutosDuracionTokenAutenticacionSede = _jwt.MinutosDuracionTokenAutenticacionSede,

                AudienciasDestino = _jwt.AudienciasDestino?.ToList() ?? new List<string>()
            };
        }

        // AudienciasDestino en formato de texto JSON
        public string ObtenerAudienciasDestinoTexto()
        {
            var audienciasDestinotexto = "";
            audienciasDestinotexto = _serializadorJsonServicio.Serializar(_jwt.AudienciasDestino);
            return string.IsNullOrWhiteSpace(audienciasDestinotexto) ? "" : audienciasDestinotexto;
        }



        private List<string?> ObtenerListasUrls(List<string?> urls)
        {
            var urlsCompletas = new List<string?>();
            foreach (var url in urls)
                urlsCompletas.Add(url);

            return urlsCompletas ?? new List<string?>();
        }
    }
}
