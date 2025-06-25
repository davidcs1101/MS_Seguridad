namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IConfiguracionesTrabajosColas
    {
        int ObtenerCantidadIntentosPorRegistroEnCola();
        string ObtenerProcesarColaSolicitudesCron();
    }
}
