using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface IApiResponseServicio
    {
        ApiResponse<T> CrearRespuesta<T>(bool correcto, string mensaje, T? data = default);
    }
}
