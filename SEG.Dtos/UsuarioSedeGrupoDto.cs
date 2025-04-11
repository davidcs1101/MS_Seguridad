using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Dtos
{
    public class UsuarioSedeGrupoDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SedeId { get; set; }
        public int GrupoId { get; set; }
    }
}
