namespace SEG.Aplicacion.ServiciosExternos.config
{
    public interface IConfiguracionesJwt
    {
        string ObtenerEmisor();
        string ObtenerAudienciasDestinoTexto();
        List<string?> ObtenerAudienciasDestino();
        string ObtenerLlave();
        int ObtenerMinutosDuracionTokenAutenticacionUsuario();
        int ObtenerMinutosDuracionTokenAutenticacionSede();
    }
}
