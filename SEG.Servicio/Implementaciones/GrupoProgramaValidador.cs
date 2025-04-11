using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Servicio.Implementaciones
{
    public class GrupoProgramaValidador : IGrupoProgramaValidador
    {
        public void ValidarDatoYaExiste(SEG_GrupoPrograma? grupoPrograma, string mensaje)
        {
            if (grupoPrograma != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_GrupoPrograma? grupoPrograma, string mensaje)
        {
            if (grupoPrograma == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
