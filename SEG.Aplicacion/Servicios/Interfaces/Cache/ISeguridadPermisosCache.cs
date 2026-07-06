using SEG.Dtos;
namespace SEG.Aplicacion.Servicios.Interfaces.Cache
{
    public interface ISeguridadPermisosCache
    {
        Task InicializarAsync();
        ApiResponse<string> Actualizar(List<AutorizacionDto> autorizaciones);
        bool TienePermiso(string codigoGrupo, string codigoPermiso);
    }
}
