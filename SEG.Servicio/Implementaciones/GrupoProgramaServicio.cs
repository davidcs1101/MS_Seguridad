using SEG.Dominio.Entidades;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Implementaciones
{
    public class GrupoProgramaServicio : IGrupoProgramaServicio
    {
        public readonly IGrupoProgramaServicio _grupoServicio;
        public GrupoProgramaServicio() 
        {

        }

        public async Task<List<SEG_Programa>> ListarProgramasPorGrupo() 
        {
            return null;
        }
    }
}
