using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Dtos
{
    public class UsuarioSedeGrupoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SedeId { get; set; }
        public int GrupoId { get; set; }

        public int UsuarioCreadorId { get; set; }
        public string NombreUsuarioCreador { get; set; } = null!;
        public DateTime FechaCreado { get; set; }
        public int? UsuarioModificadorId { get; set; }
        public string? NombreUsuarioModificador { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
