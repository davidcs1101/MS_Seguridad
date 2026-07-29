using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task<ApiResponseDto<AutenticacionResponse>> AutenticarUsuarioAsync(AutenticacionRequest autenticacionDto);
        Task<ApiResponseDto<AutenticacionResponse>> AutenticarUsuarioConGrupoAsync(AutenticacionRequest autenticacionRequest);
        Task<ApiResponseDto<AutenticacionResponse>> AutenticarSedeAsync(int sedeId);
    }
}
