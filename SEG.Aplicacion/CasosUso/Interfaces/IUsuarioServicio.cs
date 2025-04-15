using SEG.Dtos;

namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<ApiResponse<UsuarioOtrosDatosDto>> CrearAsync(UsuarioCreacionRequest usuarioCreacionRequest);
        Task<ApiResponse<UsuarioOtrosDatosDto>> ModificarClaveAsync(string clave);
        Task<ApiResponse<UsuarioOtrosDatosDto>> RestablecerClavePorUsuarioAsync(string nombreUsuario);
        Task<ApiResponse<string>> ModificarEmailAsync(string email);
        Task<ApiResponse<string>> ObtenerNombreUsuarioPorIdAsync(int id);
        Task<ApiResponse<List<UsuarioDto>?>> ListarAsync(IdsListadoDto ids);
    }
}
