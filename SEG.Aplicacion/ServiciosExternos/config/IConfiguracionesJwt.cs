namespace SEG.Aplicacion.ServiciosExternos.config
{
    public interface IConfiguracionesJwt
    {
        string ObtenerIssuer();
        string ObtenerAudienceTexto();
        List<string?> ObtenerAudience();
        string ObtenerKey();
        int ObtenerMinutosDuracionTokenAutenticacionUsuario();
        int ObtenerMinutosDuracionTokenAutenticacionSede();
    }
}
