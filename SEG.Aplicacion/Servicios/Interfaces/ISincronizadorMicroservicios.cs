namespace SEG.Aplicacion.Servicios.Interfaces
{
    public interface ISincronizadorMicroservicios
    {
        /// <summary>
        /// Este metodo se encarga de recibir los ids de las solicitudes de colas que van a ser sincronizadas y enviadas a TODOS los microservicios que requieran permisos de usuarios.
        /// Cada id representa una URL distinta que deberá ser notificada de la actualización de permisos. El método no retorna un tipo, sólo activa el Job para que se notifique a cada microservicio en cuestión.
        /// Además de esa activación de Jobs, refresca la caché local de permisos del micro de seguridad.
        /// </summary>
        /// <returns></returns>
        Task SincronizarPermisosAsync(List<int> colasSolicitudIds);
    }
}
