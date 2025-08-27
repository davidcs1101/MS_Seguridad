namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IJobEncoladorServicio
    {
        Task EncolarPorColaSolicitudId(int Id, bool validarEstadoPendiente = false);
        Task EncolarCacheListaTiposIdentificacion();
    }
}
