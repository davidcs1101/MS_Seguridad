using SEG.Dominio.Entidades;
using SEG.Dominio.Servicios;
using SEG.Dominio.Excepciones;

namespace SEG.Infraestructura.Dominio.Servicios
{
    public class UsuarioValidador : IUsuarioValidador
    {
        public void ValidarDatoYaExiste(SEG_Usuario? usuario, string mensaje)
        {
            if (usuario != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Usuario? usuario, string mensaje)
        {
            if (usuario == null)
                throw new DatoNoEncontradoException(mensaje);
        }
        public void ValidarEmailTieneOtroUsuario(SEG_Usuario? usuarioEmail, int idUsuario, string mensaje)
        {
            if (usuarioEmail != null)
                if (idUsuario != usuarioEmail.Id)
                    throw new DatoYaExisteException(mensaje);
        }
    }
}
