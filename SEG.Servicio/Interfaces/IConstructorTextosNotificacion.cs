using SEG.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IConstructorTextosNotificacion
    {
        string ConstruirTextoCreacionUsuario(SEG_Usuario usuario, string nuevaClave);
        string ConstruirTextoModificacionClaveUsuario(SEG_Usuario usuario);
        string ConstruirTextoRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave);
    }
}
