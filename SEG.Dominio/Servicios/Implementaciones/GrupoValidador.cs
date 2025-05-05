using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
{
    public class GrupoValidador : IGrupoValidador
    {
        public void ValidarDatoYaExiste(SEG_Grupo? grupo, string mensaje)
        {
            if (grupo != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Grupo? grupo, string mensaje)
        {
            if (grupo == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
