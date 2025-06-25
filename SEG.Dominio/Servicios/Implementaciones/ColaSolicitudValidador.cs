using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
{
    public class ColaSolicitudValidador : IColaSolicitudValidador
    {
        public void ValidarDatoYaExiste(SEG_ColaSolicitud? cola, string mensaje)
        {
            if (cola != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_ColaSolicitud? cola, string mensaje)
        {
            if (cola == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
