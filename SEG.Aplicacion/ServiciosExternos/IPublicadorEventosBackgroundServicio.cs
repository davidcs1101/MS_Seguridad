using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IPublicadorEventosBackgroundServicio
    {
        Task<HttpResponseMessage> PublicarActualizacionPermisos(string url);
    }
}
