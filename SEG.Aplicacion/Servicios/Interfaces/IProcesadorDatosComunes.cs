
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IProcesadorDatosComunes
    {
        Task ProcesarDatosConstantesAsync();
        Task ProcesarDatosConstantesAsync(MaestroActualizadoEventoDto codigosMaestros);
    }
}
