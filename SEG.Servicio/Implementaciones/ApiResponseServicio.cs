using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Implementaciones
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
