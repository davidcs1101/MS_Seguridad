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
        public List<SEG_GrupoPermiso> GruposPermisos { get; set; } = new List<SEG_GrupoPermiso>();
        public List<SEG_UsuarioSedeGrupo> UsuariosSedes { get; set; } = new List<SEG_UsuarioSedeGrupo>();
        public List<SEG_Usuario> Usuarios { get; set; } = new List<SEG_Usuario>();
        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
