using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios;

namespace SEG.Infraestructura.Dominio.Servicios
{
    public class ProgramaValidador : IProgramaValidador
    {
        public void ValidarDatoYaExiste(SEG_Programa? programa, string mensaje)
        {
            if (programa != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Programa? programa, string mensaje)
        {
            if (programa == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
