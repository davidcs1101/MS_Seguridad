using SEG.Dominio.Entidades;
using SEG.Dtos;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface IConstructorMensajesNotificacionCorreo
    {
        DatoCorreoRequest ConstruirMensajeCreacionUsuario(SEG_Usuario usuario, string nuevaClave);
        DatoCorreoRequest ConstruirMensajeModificacionClaveUsuario(SEG_Usuario usuario);
        DatoCorreoRequest ConstruirMensajeRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave);
    }
}
