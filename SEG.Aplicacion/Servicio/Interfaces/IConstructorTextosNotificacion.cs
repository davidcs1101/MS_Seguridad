using SEG.Dominio.Entidades;

namespace SEG.Aplicacion.Servicio.Interfaces
{
    public interface IConstructorTextosNotificacion
    {
        string ConstruirTextoCreacionUsuario(SEG_Usuario usuario, string nuevaClave);
        string ConstruirTextoModificacionClaveUsuario(SEG_Usuario usuario);
        string ConstruirTextoRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave);
    }
}
