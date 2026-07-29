using SEG.Dtos;
using Utilidades.Dtos;
namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<ApiResponseDto<UsuarioOtrosDatosDto>> CrearAsync(UsuarioCreacionRequest usuarioCreacionRequest);
        Task<ApiResponseDto<UsuarioOtrosDatosDto>> CrearConSedeAsync(UsuarioSedeCreacionRequest usuarioSedeCreacionRequest);
        Task<ApiResponseDto<UsuarioOtrosDatosDto>> ModificarClaveAsync(string clave);
        Task<ApiResponseDto<UsuarioOtrosDatosDto>> RestablecerClavePorUsuarioAsync(string nombreUsuario);
        Task<ApiResponseDto<string>> ModificarEmailAsync(string email);
        Task<ApiResponseDto<string>> ObtenerNombreUsuarioPorIdAsync(int id);
        Task<ApiResponseDto<List<UsuarioDto>?>> ListarAsync(IdsListadoDto ids);
    }
}
