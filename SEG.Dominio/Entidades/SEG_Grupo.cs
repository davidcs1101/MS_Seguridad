using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades
{
    public class SEG_Grupo : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public List<SEG_GrupoPrograma> GruposProgramas { get; set; } = new List<SEG_GrupoPrograma>();
        public List<SEG_UsuarioSedeGrupo> UsuariosSedes { get; set; } = new List<SEG_UsuarioSedeGrupo>();
        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
