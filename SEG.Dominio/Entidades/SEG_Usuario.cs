using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades
{
    public class SEG_Usuario : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre1 { get; set; } = null!;
        public string? Nombre2 { get; set; }
        public string Apellido1 { get; set; } = null!;
        public string? Apellido2 { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool CambiarClave { get; set;}
        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
        public List<SEG_UsuarioSedeGrupo> UsuariosSedes { get; set; } = new List<SEG_UsuarioSedeGrupo>();
    }
}
