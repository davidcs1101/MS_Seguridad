using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorMaestrosExternos : IProcesadorMaestrosExternos
    {
        private readonly IProcesadorDatosComunes _procesadorDatosComunes;

        public ProcesadorMaestrosExternos(IProcesadorDatosComunes procesadorDatosComunes)
        {
            _procesadorDatosComunes = procesadorDatosComunes;
        }

        public async Task ProcesarAsync()
        {
            await _procesadorDatosComunes.ProcesarDatosConstantesAsync();
        }
    }
}
