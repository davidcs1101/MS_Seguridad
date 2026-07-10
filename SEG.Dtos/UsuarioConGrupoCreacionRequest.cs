using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace SEG.Dtos
{
    public class UsuarioConGrupoCreacionRequest : UsuarioCreacionRequest
    {
        [MaxLength(30, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string? CodigoGrupo { get; set; }
    }
}
