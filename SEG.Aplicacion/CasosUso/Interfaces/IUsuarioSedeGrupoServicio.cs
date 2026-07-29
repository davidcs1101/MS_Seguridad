using SEG.Dtos;
using Utilidades.Dtos;
namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IUsuarioSedeGrupoServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacion);
        Task<ApiResponseDto<string>> ModificarAsync(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<UsuarioSedeGrupoDto?>> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId);
        Task<ApiResponseDto<List<UsuarioSedeGrupoDto>?>> ListarPorUsuarioIdLogueadoAsync();
    }
}
