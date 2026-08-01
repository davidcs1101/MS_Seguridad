using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IMaestroExternoRepositorio
    {
        IQueryable<SEG_MaestroExterno> Listar();
        Task SincronizarMaestroAsync(string microservicioOrigen, string codigoMaestro, List<SEG_MaestroExterno> registros);
    }
}
