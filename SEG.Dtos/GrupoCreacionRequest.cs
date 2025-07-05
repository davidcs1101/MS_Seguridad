﻿using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace SEG.Dtos
{
    public class GrupoCreacionRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [MaxLength(30, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string Codigo { get; set; } = null!;

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [MaxLength(250, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string Nombre { get; set; } = null!;
    }
}
