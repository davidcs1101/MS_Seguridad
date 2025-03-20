using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IConstructorMensajesNotificacionWhatsApp
    {
        DatoWhatsAppRequest ConstruirMensajeCreacionUsuario(SEG_Usuario usuario, string nuevaClave);
        DatoWhatsAppRequest ConstruirMensajeModificacionClaveUsuario(SEG_Usuario usuario);
        DatoWhatsAppRequest ConstruirMensajeRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave);
    }
}
