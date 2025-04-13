using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace SEG.Dtos
{
    public class GrupoModificacionRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public int Id { get; set; }

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [MaxLength(250, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public bool EstadoActivo { get; set; }
    }
}
