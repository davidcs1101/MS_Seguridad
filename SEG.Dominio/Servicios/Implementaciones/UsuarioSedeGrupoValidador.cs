using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
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
