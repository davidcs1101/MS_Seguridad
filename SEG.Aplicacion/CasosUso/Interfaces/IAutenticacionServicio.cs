using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task<ApiResponse<string>> AutenticarUsuarioAsync(AutenticacionRequest autenticacionDto);
        Task<ApiResponse<string>> AutenticarSedeAsync(int sedeId);
    }
}
