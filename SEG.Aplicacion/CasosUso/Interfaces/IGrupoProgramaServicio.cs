using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IGrupoProgramaServicio
    {
        Task<ApiResponse<int>> CrearAsync(GrupoProgramaCreacionRequest grupoProgramaCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(GrupoProgramaModificacionRequest grupoProgramaModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<GrupoProgramaDto?>> ObtenerGrupoProgramaAsync(int grupoId, int programaId);
    }
}
