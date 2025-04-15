using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios;

namespace SEG.Infraestructura.Dominio.Servicios
{
    public class UsuarioSedeGrupoValidador : IUsuarioSedeGrupoValidador
    {
        public void ValidarDatoYaExiste(SEG_UsuarioSedeGrupo? usuarioSedeGrupo, string mensaje)
        {
            if (usuarioSedeGrupo != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_UsuarioSedeGrupo? usuarioSedeGrupo, string mensaje)
        {
            if (usuarioSedeGrupo == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
