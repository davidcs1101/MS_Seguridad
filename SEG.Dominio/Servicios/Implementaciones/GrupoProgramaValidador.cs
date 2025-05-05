using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
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
