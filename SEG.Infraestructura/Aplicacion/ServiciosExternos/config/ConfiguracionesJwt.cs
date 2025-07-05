using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dtos.AppSettings;
using System.Reflection;
namespace SEG.Infraestructura.Aplicacion.ServiciosExternos.config
{
    public class ConfiguracionesJwt: IConfiguracionesJwt
    {
        private readonly JWTSettings _opciones;
        private readonly IServiceProvider _serviceProvider;

        public ConfiguracionesJwt(IOptions<JWTSettings> opciones, IServiceProvider serviceProvider)
        {
            _opciones = opciones.Value;
            _serviceProvider = serviceProvider;
        }

        public string ObtenerIssuer()
        {
            return string.IsNullOrWhiteSpace(_opciones.Issuer) ? "" : _opciones.Issuer;
        }

        public string ObtenerAudienceTexto()
        {
            var audiencetexto = "";
            using (var scope = _serviceProvider.CreateScope()) 
            {
                var _serializadorJson = scope.ServiceProvider.GetRequiredService<ISerializadorJsonServicio>();
                audiencetexto = _serializadorJson.Serializar(_opciones.Audience);
            }
            return string.IsNullOrWhiteSpace(audiencetexto) ? "" : audiencetexto;
        }

        public List<string?> ObtenerAudience()
        {
            var listaAudience = new List<string>();

            if (_opciones.Audience != null)
            {
                // Usa reflexión para obtener todas las propiedades del objeto Audience
                PropertyInfo[] propiedades = typeof(Audience).GetProperties();

                foreach (PropertyInfo propiedad in propiedades)
                {
                    if (propiedad.PropertyType == typeof(string))
                    {
                        // Obtiene el valor de la propiedad del objeto Audience
                        string? valorAudience = propiedad.GetValue(_opciones.Audience) as string;

                        // Si el valor no es nulo o vacío, lo añade a la lista
                        if (!string.IsNullOrWhiteSpace(valorAudience))
                            listaAudience.Add(valorAudience);
                    }
                }
            }
            return listaAudience;
        }

        public string ObtenerKey()
        {
            return string.IsNullOrWhiteSpace(_opciones.Key) ? "" : _opciones.Key;
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
