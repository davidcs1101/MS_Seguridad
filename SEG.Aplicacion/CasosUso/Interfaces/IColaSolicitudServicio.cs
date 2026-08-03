using SEG.Dominio.Entidades;
using Utilidades.Dtos;
namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IColaSolicitudServicio
    {
        Task ProcesarColaSolicitudesAsync();
        Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false);
        Task<ApiResponseDto<int>> CrearAsync(ColaSolicitudCreacionRequest colaSolicitudCreacionRequest);
        Task<SEG_ColaSolicitud> AgregarColaSolicitud(string tipo, object payload, string urlDestino = "");
    }
}
