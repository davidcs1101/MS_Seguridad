using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace SEG.Dtos
{
    public class DatoCorreoRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [CustomValidation(typeof(DatoCorreoRequest),nameof(ValidarCorreos))]
        public List<string> Destinatarios { get; set; } = new List<string>();

        [EmailAddress(ErrorMessage = Textos.Generales.VALIDA_CORREO_NO_VALIDO)]
        public string? CorreoRespuesta { get; set; }
        
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public string Asunto { get; set; } = null!;

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public string Cuerpo { get; set; } = null!;

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public bool EsCuerpoHtml { get; set; }


        public static ValidationResult? ValidarCorreos(List<string> correos, ValidationContext contexto) 
        {
            var atributosCorreo = new EmailAddressAttribute();
            foreach (var correo in correos)
            {
                if (!atributosCorreo.IsValid(correo))
                    return new ValidationResult(Textos.Generales.VALIDA_CORREO_NO_VALIDO + " _ " + correo);
            }
            return ValidationResult.Success;
        }
    }
}
