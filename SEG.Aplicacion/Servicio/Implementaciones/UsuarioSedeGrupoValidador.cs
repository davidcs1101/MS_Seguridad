using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class UsuarioSedeGrupoValidador : IUsuarioSedeGrupoValidador
    {
        public void ValidarDatoYaExiste(SEG_UsuarioSedeGrupo? usuarioSedeGrupo, string mensaje)
        {
            if (usuarioSedeGrupo != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_UsuarioSedeGrupo? usuarioSedeGrupo, string mensaje)
        {
            if (usuarioSedeGrupo == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
