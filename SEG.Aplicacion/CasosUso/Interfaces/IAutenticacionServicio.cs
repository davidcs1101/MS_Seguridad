using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task<ApiResponse<AutenticacionResponse>> AutenticarUsuarioAsync(AutenticacionRequest autenticacionDto);
        Task<ApiResponse<AutenticacionResponse>> AutenticarSedeAsync(int sedeId);
    }
}
