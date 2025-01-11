using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades
{
    public class SEG_UsuarioSedeGrupo : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public SEG_Usuario Usuario { get; set; } = null!;
        public int UsuarioId { get; set; }
        public int SedeId { get; set; }
        public SEG_Grupo Grupo { get; set; } = null!;
        public int GrupoId { get; set; }
        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
