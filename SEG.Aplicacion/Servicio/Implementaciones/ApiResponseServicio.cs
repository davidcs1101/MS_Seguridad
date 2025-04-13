using SEG.Dtos;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class ApiResponseServicio : IApiResponseServicio
    {
        public ApiResponse<T> CrearRespuesta<T>(bool correcto, string mensaje, T? data = default)
        {
            return new ApiResponse<T>
            {
                Correcto = correcto,
                Mensaje = mensaje,
                Data = data  // Si data es nulo o no se pasa, se usa default(T)
            };
        }
    }
}
