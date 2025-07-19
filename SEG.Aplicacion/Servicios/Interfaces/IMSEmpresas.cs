using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IMSEmpresas
    {
        Task<bool> ValidarSedeExisteAsync(int id);
    }
}
