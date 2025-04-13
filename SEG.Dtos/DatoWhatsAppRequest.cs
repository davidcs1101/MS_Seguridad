using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace SEG.Dtos
{
    public class DatoWhatsAppRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public List<string> Destinatarios { get; set; } = new List<string>();

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public string Mensaje { get; set; } = null!;
    }
}
