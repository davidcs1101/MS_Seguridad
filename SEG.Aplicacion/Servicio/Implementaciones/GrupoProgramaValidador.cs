using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class GrupoProgramaValidador : IGrupoProgramaValidador
    {
        public void ValidarDatoYaExiste(SEG_GrupoPrograma? grupoPrograma, string mensaje)
        {
            if (grupoPrograma != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_GrupoPrograma? grupoPrograma, string mensaje)
        {
            if (grupoPrograma == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
