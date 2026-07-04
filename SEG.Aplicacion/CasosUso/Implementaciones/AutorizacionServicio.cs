using SEG.Aplicacion.CasosUso.Interfaces;
namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class AutorizacionServicio : IAutorizacionServicio
    {
        public Task<bool> TienePermisoAsync(int grupoId, string codigoPermiso)
        {
            return Task.FromResult(false);
        }
    }
}
