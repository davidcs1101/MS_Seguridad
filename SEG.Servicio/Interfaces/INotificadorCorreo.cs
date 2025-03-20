using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface INotificadorCorreo
    {
        Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest);
    }
}
