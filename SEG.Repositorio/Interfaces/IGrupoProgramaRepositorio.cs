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
        Task<int> CrearAsync(SEG_GrupoPrograma grupoPrograma);
        Task ModificarAsync(SEG_GrupoPrograma grupoPrograma);
        Task<bool> EliminarAsync(int id);
        Task<SEG_GrupoPrograma?> ObtenerGrupoProgramaAsync(int grupoId, int programaId);
        Task<SEG_GrupoPrograma?> ObtenerPorIdAsync(int id);
        IQueryable<ProgramaDto> ListarProgramasPorGrupo(int grupoId);
    }
}
