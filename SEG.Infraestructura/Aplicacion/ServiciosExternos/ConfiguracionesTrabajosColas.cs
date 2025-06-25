using Microsoft.Extensions.Options;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Dtos.AppSettings;
namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class ConfiguracionesTrabajosColas : IConfiguracionesTrabajosColas
    {
        private readonly TrabajosColasSettings _opciones;

        public ConfiguracionesTrabajosColas(IOptions<TrabajosColasSettings> opciones)
        {
            _opciones = opciones.Value;
        }

        public int ObtenerCantidadIntentosPorRegistroEnCola()
        {
            if (int.TryParse(_opciones.CantidadIntentosPorRegistroEnCola, out int intentos))
                return intentos;
            return 0;
        }
        public string ObtenerProcesarColaSolicitudesCron()
        {
            return string.IsNullOrWhiteSpace(_opciones.ProcesarColaSolicitudesCron)
                            ? "*/5 * * * *"
                            : _opciones.ProcesarColaSolicitudesCron;
        }
    }
}
