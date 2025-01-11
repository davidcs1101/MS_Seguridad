using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dtos
{
    public class UsuarioOtrosDatosDto
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public bool NotificadoPorCorreo { get; set; } = false;
    }
}
