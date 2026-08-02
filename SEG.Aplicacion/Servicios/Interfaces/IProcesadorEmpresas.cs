
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IProcesadorEmpresas
    {
        Task ProcesarSedesAsync();
        Task ProcesarSedesAsync(int sedeId);
    }
}
