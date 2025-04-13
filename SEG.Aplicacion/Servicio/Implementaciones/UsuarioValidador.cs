using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using Utilidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class UsuarioValidador : IUsuarioValidador
    {
        public void ValidarDatoYaExiste(SEG_Usuario? usuario, string mensaje)
        {
            if (usuario != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Usuario? usuario, string mensaje)
        {
            if (usuario == null)
                throw new KeyNotFoundException(mensaje);
        }
        public void ValidarEmailTieneOtroUsuario(SEG_Usuario? usuarioEmail, int idUsuario, string mensaje)
        {
            if (usuarioEmail != null)
                if (idUsuario != usuarioEmail.Id)
                    throw new DbUpdateException(Textos.Usuarios.MENSAJE_USUARIO_EMAIL_EXISTE);
        }
    }
}
