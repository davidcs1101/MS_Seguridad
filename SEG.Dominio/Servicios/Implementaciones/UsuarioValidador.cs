using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
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
