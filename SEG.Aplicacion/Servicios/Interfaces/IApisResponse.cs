using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IApisResponse
    {
        ApiResponse<T> CrearRespuesta<T>(bool correcto, string mensaje, T? data = default);
    }
}
