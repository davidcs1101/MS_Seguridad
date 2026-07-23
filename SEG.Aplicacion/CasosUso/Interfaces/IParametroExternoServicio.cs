using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IParametroExternoServicio
    {
        Task<ApiResponse<int>> SincronizarDatosAsync();
        Task<ApiResponse<List<ProgramaDto>?>> ListarAsync();
    }
}
