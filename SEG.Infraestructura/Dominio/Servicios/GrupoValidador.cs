using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios;

namespace SEG.Infraestructura.Dominio.Servicios
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
