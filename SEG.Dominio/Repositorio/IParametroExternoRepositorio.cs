using SEG.Dominio.Entidades;

namespace SEG.Dominio.Repositorio
{
    public interface IParametroExternoRepositorio
    {
        Task<int> CrearAsync(SEG_ParametroExterno parametroExterno);
        Task ModificarAsync(SEG_ParametroExterno parametroExterno);
        IQueryable<SEG_ParametroExterno> Listar();
    }
}
