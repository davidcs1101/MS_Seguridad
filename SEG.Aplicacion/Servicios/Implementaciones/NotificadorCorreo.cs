using SEG.Dtos;
using Utilidades;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using System.Net.Http.Json;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Entidades;
using SEG.Dominio.Servicios.Interfaces;
using System.Security.AccessControl;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dominio.Enumeraciones;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class NotificadorCorreo : INotificadorCorreo
    {
        private readonly IMSEnvioCorreosBackgroundServicio _msEnvioCorreosServicio;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly IColaSolicitudValidador _colaSolicitudValidador;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public NotificadorCorreo(IMSEnvioCorreosBackgroundServicio msEnvioCorreosServicio, IConstructorTextosNotificacion constructorTextosNotificacion, IColaSolicitudRepositorio colaSolicitudRepositorio, IColaSolicitudValidador colaSolicitudValidador, ISerializadorJsonServicio serializadorJsonServicio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _msEnvioCorreosServicio = msEnvioCorreosServicio;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _colaSolicitudValidador = colaSolicitudValidador;
            _serializadorJsonServicio = serializadorJsonServicio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<bool> EnviarAsync(DatoCorreoRequest datoCorreoRequest) 
        {
            var respuesta = await _msEnvioCorreosServicio.EnviarCorreoAsync(datoCorreoRequest);
            respuesta.EnsureSuccessStatusCode();

            return true;
        }
    }
}
