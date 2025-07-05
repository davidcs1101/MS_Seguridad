using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades
{
    public class SEG_BaseAuditoria
    {
        public int UsuarioCreadorId { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public int? UsuarioModificadorId { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; } = true;
    }
}
