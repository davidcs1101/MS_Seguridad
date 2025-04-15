using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios;

namespace SEG.Infraestructura.Dominio.Servicios
{
    public class GrupoProgramaValidador : IGrupoProgramaValidador
    {
        public void ValidarDatoYaExiste(SEG_GrupoPrograma? grupoPrograma, string mensaje)
        {
            if (grupoPrograma != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_GrupoPrograma? grupoPrograma, string mensaje)
        {
            if (grupoPrograma == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
