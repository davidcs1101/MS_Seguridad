using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ParametrosConsultas;
namespace SEG.Dominio.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<SEG_Usuario?> ObtenerPorIdAsync(int id);
        Task<SEG_Usuario?> ObtenerPorUsuarioAsync(string usuario);
        Task<SEG_Usuario?> ObtenerPorEmailAsync(string email);
        Task<SEG_Usuario?> ObtenerPorIdentificacionAsync(int tipoIdentificacionId, string identificacion);
        Task ModificarAsync(SEG_Usuario usuario);
        void MarcarModificar(SEG_Usuario usuario);
        Task<int> CrearAsync(SEG_Usuario usuario);
        void MarcarCrear(SEG_Usuario usuario);
        Task<List<SEG_Usuario>> ListarAsync();
        Task<List<SEG_Usuario>> ListarAsync(UsuarioConsulta consulta);
        Task<int> ContarRegistrosAsync(UsuarioConsulta consulta);
    }
}
