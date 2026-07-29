using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface ICatalogoExternoRepositorio
    {
        IQueryable<SEG_CatalogoExterno> Listar();
        Task SincronizarCatalogoAsync(string microservicioOrigen, string codigoCatalogo, List<SEG_CatalogoExterno> registros);
    }
}
