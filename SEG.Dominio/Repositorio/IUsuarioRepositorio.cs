using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<SEG_Usuario?> ObtenerPorIdAsync(int id);
        Task<SEG_Usuario?> ObtenerPorUsuarioAsync(string usuario);
        Task<SEG_Usuario?> ObtenerPorEmailAsync(string email);
        Task<SEG_Usuario?> ObtenerPorIdentificacionAsync(int tipoIdentificacionId, string identificacion);
        Task ModificarAsync(SEG_Usuario usuario);
        Task<int> CrearAsync(SEG_Usuario usuario);
        IQueryable<SEG_Usuario> Listar();
    }
}
