
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IProcesadorMaestrosExternos
    {
        Task ProcesarDatosConstantesAsync();
        Task ProcesarDatosConstantesAsync(MaestroActualizadoEventoDto codigosMaestros);
    }
}
