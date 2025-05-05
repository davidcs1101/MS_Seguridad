using SEG.Dominio.Entidades;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Dominio.Servicios.Implementaciones
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
