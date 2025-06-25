using Hangfire;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using Utilidades;

public class JobEncoladorServicio: IJobEncoladorServicio
{
    public Task EncolarPorColaSolicitudIdAsync(int Id, bool validarEstadoPendiente = false)
    {
        try{
            BackgroundJob.Enqueue<IColaSolicitudServicio>(x => x.ProcesarPorColaSolicitudIdAsync(Id, validarEstadoPendiente));
        }
        catch (Exception e){
            Logs.EscribirLog("", "Error al tratar de encolar en HangFire: ", e);
        }
        return Task.CompletedTask;
    }
}