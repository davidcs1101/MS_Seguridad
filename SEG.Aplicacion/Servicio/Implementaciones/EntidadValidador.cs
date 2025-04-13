using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class EntidadValidador<TEntity> : IEntidadValidador<TEntity>
    {
        public void ValidarDatoYaExiste(TEntity? entidad, string mensaje)
        {
            if (entidad != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(TEntity? entidad, string mensaje)
        {
            if (entidad == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
