namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IJobEncoladorServicio
    {
        Task EncolarPorColaSolicitudIdAsync(int Id, bool validarEstadoPendiente = false);
    }
}
