using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Aplicacion.Servicio.Implementaciones
{
    public class ProgramaValidador : IProgramaValidador
    {
        public void ValidarDatoYaExiste(SEG_Programa? programa, string mensaje)
        {
            if (programa != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Programa? programa, string mensaje)
        {
            if (programa == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
