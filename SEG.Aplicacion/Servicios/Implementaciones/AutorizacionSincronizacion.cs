using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dominio.Entidades;
using SEG.Dominio.Enumeraciones;
using SEG.Dominio.Repositorio;
using SEG.Dtos;
using System.Runtime.InteropServices;
using Utilidades;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class AutorizacionSincronizacion : IAutorizacionSincronizacion
    {
        /// <inheritdoc/>
        private readonly IAutorizacionServicio _autorizacionServicio;
        private readonly ISeguridadPermisosCache _permisosCache;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IAppSettings _appSettings;

        public AutorizacionSincronizacion(IAutorizacionServicio autorizacionServicio,
            ISeguridadPermisosCache permisosCache, IColaSolicitudRepositorio colaSolicitudRepositorio, ISerializadorJsonServicio serializadorJsonServicio, IAppSettings appSettings)
        {
            _autorizacionServicio = autorizacionServicio;
            _permisosCache = permisosCache;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _serializadorJsonServicio = serializadorJsonServicio;
            _appSettings = appSettings;
        }

        public async Task SincronizarPermisosAsync()
        {
            var permisos = await _autorizacionServicio.ListarCatalogoAutorizacionAsync();

            _permisosCache.Actualizar(permisos);

            var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
            if (urls.Count > 0)
                await this.AgregarColaSolicitud(urls!);
        }

        private async Task<List<SEG_ColaSolicitud>> AgregarColaSolicitud(List<string> urls)
        {
            var colas = new List<SEG_ColaSolicitud>();
            foreach (var url in urls)
            {
                var solicitud = new SEG_ColaSolicitud
                {
                    Tipo = EventosColas.PERMISOSACTUALIZADOS,
                    UrlDestino = url,
                    Payload = "",
                    Estado = EstadoCola.Pendiente,
                    Intentos = 0
                };
                await _colaSolicitudRepositorio.CrearAsync(solicitud);
                colas.Add(solicitud);
            }
            return colas;
        }
    }
}
