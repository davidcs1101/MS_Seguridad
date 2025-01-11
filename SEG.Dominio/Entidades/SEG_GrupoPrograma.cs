using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades
{
    public class SEG_GrupoPrograma : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public SEG_Grupo Grupo { get; set; } = null!; //new SEG_Grupos();
        public int GrupoId { get; set; }
        public SEG_Programa Programa { get; set; } = null!; //new SEG_Programas();
        public int ProgramaId { get; set; }
        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
