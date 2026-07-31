using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IMaestroExternoRepositorio
    {
        IQueryable<SEG_MaestroExterno> Listar();
        Task SincronizarCatalogoAsync(string microservicioOrigen, string codigoCatalogo, List<SEG_MaestroExterno> registros);
    }
}
