using SEG.Dtos.AppSettings;
namespace SEG.Aplicacion.ServiciosExternos.config
{
    public interface IAppSettings
    {
        TrabajosColasSettings ObtenerTrabajosColasSettings();
        List<string?> ObtenerEventosNotificarActualizarPermisos();
        JWTSettings ObtenerJWT();
        string ObtenerAudienciasDestinoTexto();

    }
}
