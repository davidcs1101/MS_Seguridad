using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IGrupoProgramaServicio
    {
        Task<ApiResponse<int>> CrearAsync(GrupoProgramaCreacionRequest grupoProgramaCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(GrupoProgramaModificacionRequest grupoProgramaModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<GrupoProgramaDto?>> ObtenerGrupoProgramaAsync(int grupoId, int programaId);
        //Task<List<SEG_Programa>> ListarProgramasPorGrupo();
    }
}
