using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Interfaces
{
    public interface IGrupoRepositorio
    {
        Task<int> CrearAsync(SEG_Grupo grupo);
        Task ModificarAsync(SEG_Grupo grupo);
        Task<bool> EliminarAsync(int id);
        Task<SEG_Grupo?> ObtenerPorIdAsync(int id);
        Task<SEG_Grupo?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<GrupoDto> Listar();
    }
}
