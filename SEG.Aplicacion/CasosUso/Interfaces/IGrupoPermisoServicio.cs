using SEG.Dtos;
using Utilidades.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IGrupoPermisoServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(GrupoPermisoCreacionRequest grupoPermisoCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(GrupoPermisoModificacionRequest grupoPermisoModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<GrupoPermisoDto?>> ObtenerGrupoPermisoAsync(int grupoId, int permisoId);
        Task<ApiResponseDto<List<GrupoPermisoDto>?>> ListarAsync();
    }
}
