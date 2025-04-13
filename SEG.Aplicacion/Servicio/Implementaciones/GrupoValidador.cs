using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class GrupoValidador : IGrupoValidador
    {
        public void ValidarDatoYaExiste(SEG_Grupo? grupo, string mensaje)
        {
            if (grupo != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Grupo? grupo, string mensaje)
        {
            if (grupo == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
