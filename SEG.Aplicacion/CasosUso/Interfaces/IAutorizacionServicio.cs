namespace SEG.Aplicacion.CasosUso.Interfaces
{
    public interface IAutorizacionServicio
    {
        Task<bool> TienePermisoAsync(int grupoId, string codigoPermiso);
    }
}
