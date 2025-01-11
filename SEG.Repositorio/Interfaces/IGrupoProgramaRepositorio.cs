using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Interfaces
{
    public interface IGrupoProgramaRepositorio
    {
        IQueryable<ProgramaDto> ListarProgramasPorGrupo(int grupoId);
    }
}
