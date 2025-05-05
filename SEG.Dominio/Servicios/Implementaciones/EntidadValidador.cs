using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
{
    public class EntidadValidador<TEntity> : IEntidadValidador<TEntity>
    {
        public void ValidarDatoYaExiste(TEntity? entidad, string mensaje)
        {
            if (entidad != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(TEntity? entidad, string mensaje)
        {
            if (entidad == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
