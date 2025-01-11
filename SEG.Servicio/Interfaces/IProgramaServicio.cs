using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IProgramaServicio
    {
        Task<ApiResponse<int>> CrearAsync(ProgramaCreacionRequest grupoCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(ProgramaModificacionRequest grupoModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<ProgramaDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<ProgramaDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<ProgramaDto>?>> ListarAsync();
    }
}
