using SEG.Dominio.Entidades;
using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface IConstructorMensajesNotificacionWhatsApp
    {
        DatoWhatsAppRequest ConstruirMensajeCreacionUsuario(SEG_Usuario usuario, string nuevaClave);
        DatoWhatsAppRequest ConstruirMensajeModificacionClaveUsuario(SEG_Usuario usuario);
        DatoWhatsAppRequest ConstruirMensajeRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave);
    }
}
