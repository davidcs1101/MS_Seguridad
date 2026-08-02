namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorMaestrosExternos : IProcesadorMaestrosExternos
    {
        private readonly IProcesadorDatosComunes _procesadorDatosComunes;
        private readonly IProcesadorEmpresas _procesadorEmpresas;

        public ProcesadorMaestrosExternos(IProcesadorDatosComunes procesadorDatosComunes, IProcesadorEmpresas procesadorEmpresas)
        {
            _procesadorDatosComunes = procesadorDatosComunes;
            _procesadorEmpresas = procesadorEmpresas;
        }

        public async Task ProcesarAsync()
        {
            await _procesadorDatosComunes.ProcesarDatosConstantesAsync();
            await _procesadorEmpresas.ProcesarSedesAsync();
        }
    }
}
