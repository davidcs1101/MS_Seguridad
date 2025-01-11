﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dtos
{
    public class GrupoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public int UsuarioCreadorId { get; set; }
        public string NombreUsuarioCreador { get; set; } = null!;
        public DateTime FechaCreado { get; set; }
        public int? UsuarioModificadorId { get; set; }
        public string? NombreUsuarioModificador { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
