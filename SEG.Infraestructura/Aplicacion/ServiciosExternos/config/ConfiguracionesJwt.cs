using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dtos.AppSettings;
namespace SEG.Infraestructura.Aplicacion.ServiciosExternos.Config
{
    public class ConfiguracionesJwt: IConfiguracionesJwt
    {
        private readonly JWTSettings _opciones;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public ConfiguracionesJwt(IOptions<JWTSettings> opciones, IServiceProvider serviceProvider, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _opciones = opciones.Value;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public string ObtenerEmisor()
        {
            return string.IsNullOrWhiteSpace(_opciones.Emisor) ? "" : _opciones.Emisor;
        }

        public string ObtenerAudienciasDestinoTexto()
        {
            var audienciasDestinotexto = "";
            audienciasDestinotexto = _serializadorJsonServicio.Serializar(_opciones.AudienciasDestino);
            return string.IsNullOrWhiteSpace(audienciasDestinotexto) ? "" : audienciasDestinotexto;
        }

        public List<string?> ObtenerAudienciasDestino()
        {
            return _opciones.AudienciasDestino ?? new List<string?>();
        }

        public string ObtenerLlave()
        {
            return string.IsNullOrWhiteSpace(_opciones.Llave) ? "" : _opciones.Llave;
        }

        public int ObtenerMinutosDuracionTokenAutenticacionUsuario()
        {
            if (int.TryParse(_opciones.MinutosDuracionTokenAutenticacionUsuario, out int dato))
                return dato;
            return 0;
        }

        public int ObtenerMinutosDuracionTokenAutenticacionSede()
        {
            if (int.TryParse(_opciones.MinutosDuracionTokenAutenticacionSede, out int dato))
                return dato;
            return 0;
        }
    }
}
