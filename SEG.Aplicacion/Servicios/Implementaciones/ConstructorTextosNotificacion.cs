using SEG.Dominio.Entidades;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class ConstructorTextosNotificacion : IConstructorTextosNotificacion
    {
        public string ConstruirTextoCreacionUsuario(SEG_Usuario usuario, string nuevaClave) {
            
            return "Bienvenido " + usuario.Nombre1 + " " + usuario.Apellido1 + 
                   ", se ha registrado correctamente." + "\n\n" +
                   "Nuevo usuario registrado: " + usuario.NombreUsuario + "\n" +
                   "Clave de primer acceso: " + nuevaClave;
        }

        public string ConstruirTextoModificacionClaveUsuario(SEG_Usuario usuario) {
            return "Hola " + usuario.Nombre1 + " " + usuario.Apellido1 +
                   ", se ha realizado su cambio de clave correctamente." + "\n\n";
        }

        public string ConstruirTextoRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave) {
            return "Hola " + usuario.Nombre1 + " " + usuario.Apellido1 +
                   ", se ha restablecido su clave correctamente." + "\n\n" +
                   "Clave de primer acceso: " + nuevaClave;
        }
    }
}
