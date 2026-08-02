using SEG.Aplicacion.ServiciosExternos;
using SEG.Dtos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Serializacion.Interfaces;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorEventos : IProcesadorEventos
    {
        private readonly IMSEnvioCorreos _msEnvioCorreos;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IPublicadorEventosBackgroundServicio _publicadorEventosBackgroundServicio;
        private readonly IProcesadorDatosComunes _procesadorDatosComunes;
        private readonly IProcesadorEmpresas _procesadorEmpresas;

        public ProcesadorEventos(IMSEnvioCorreos msEnvioCorreos, ISerializadorJsonServicio serializadorJsonServicio, IPublicadorEventosBackgroundServicio publicadorEventosBackgroundServicio, IProcesadorDatosComunes procesadorDatosComunes, IProcesadorEmpresas procesadorEmpresas)
        {
            _msEnvioCorreos = msEnvioCorreos;
            _serializadorJsonServicio = serializadorJsonServicio;
            _publicadorEventosBackgroundServicio = publicadorEventosBackgroundServicio;
            _procesadorDatosComunes = procesadorDatosComunes;
            _procesadorEmpresas = procesadorEmpresas;
        }

        public async Task ProcesarAsync(string evento, string payload = "", string UrlDestino = "")
        {
            switch (evento)
            {
                case EventosColas.ENVIARCORREO:
                    await _msEnvioCorreos.EnviarAsync(_serializadorJsonServicio.Deserializar<DatoCorreoRequest>(payload));
                    break;
                case EventosColas.PERMISOSACTUALIZADOS:
                    await _publicadorEventosBackgroundServicio.PublicarActualizacionPermisos(UrlDestino);
                    break;
                case EventosColas.CONSTANTESDETALLEACTUALIZADO:
                    await _procesadorDatosComunes.ProcesarDatosConstantesAsync(_serializadorJsonServicio.Deserializar<MaestroActualizadoEventoDto>(payload));
                    break;
                case EventosColas.SEDESACTUALIZADAS:
                    await _procesadorEmpresas.ProcesarSedesAsync(Convert.ToInt32(payload));
                    break;
            }
        }

    }
}
