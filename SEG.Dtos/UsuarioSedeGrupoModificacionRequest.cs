using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace SEG.Dtos
{
    public class UsuarioSedeGrupoModificacionRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public int Id { get; set; }

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public int GrupoId { get; set; }
        
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public bool EstadoActivo { get; set; } = true;
    }
}
