using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IApiResponseServicio
    {
        ApiResponse<T> CrearRespuesta<T>(bool correcto, string mensaje, T? data = default);
    }
}
