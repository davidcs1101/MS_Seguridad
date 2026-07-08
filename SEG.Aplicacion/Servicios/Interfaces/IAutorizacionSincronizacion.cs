namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface IAutorizacionSincronizacion
    {
        /// <summary>
        /// Este metodo se encarga crear las colas de sincronización para enviar los permisos de los usuarios a cada microservicio.
        /// </summary>
        /// <returns>No retorna un tipo, sólo crea la cola de solicitudes</returns>
        Task SincronizarPermisosAsync();
    }
}
