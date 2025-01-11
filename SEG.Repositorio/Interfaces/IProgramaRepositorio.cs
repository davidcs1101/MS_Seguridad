using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Interfaces
{
    public interface IProgramaRepositorio
    {
        Task<int> CrearAsync(SEG_Programa programa);
        Task ModificarAsync(SEG_Programa programa);
        Task<bool> EliminarAsync(int id);
        Task<SEG_Programa?> ObtenerPorIdAsync(int id);
        Task<SEG_Programa?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<ProgramaDto> Listar();
    }
}
