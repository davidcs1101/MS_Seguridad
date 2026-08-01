using Hangfire;
using SEG.Aplicacion.Servicios.Interfaces;
namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class ProcesadorMaestrosExternosJob
    {
        private readonly IProcesadorMaestrosExternos _procesadorMaestrosExternos;

        public ProcesadorMaestrosExternosJob(IProcesadorMaestrosExternos procesador)
        {
            _procesadorMaestrosExternos = procesador;
        }


        [AutomaticRetry(Attempts = 100, LogEvents = true)]
        public Task ProcesarAsync()
        {
            return _procesadorMaestrosExternos.ProcesarAsync();
        }
    }
}
