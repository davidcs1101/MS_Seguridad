using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dominio.Entidades;
using SEG.Dominio.Enumeraciones;
using SEG.Dominio.Repositorio;
using Utilidades;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class SincronizadorAutorizacion : ISincronizadorAutorizacion
    {
        /// <inheritdoc/>
        private readonly IAutorizacionServicio _autorizacionServicio;
        private readonly ISeguridadPermisosCache _permisosCache;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly IAppSettings _appSettings;
        private readonly IJobEncoladorServicio _jobEncoladorServicio;

        public SincronizadorAutorizacion(IAutorizacionServicio autorizacionServicio,
            ISeguridadPermisosCache permisosCache, IColaSolicitudRepositorio colaSolicitudRepositorio, IAppSettings appSettings, IJobEncoladorServicio jobEncoladorServicio)
        {
            _autorizacionServicio = autorizacionServicio;
            _permisosCache = permisosCache;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _appSettings = appSettings;
            _jobEncoladorServicio = jobEncoladorServicio;
        }

        public async Task SincronizarPermisosAsync()
        {
            var permisos = await _autorizacionServicio.ListarCatalogoAutorizacionAsync();

            _permisosCache.Actualizar(permisos);

            var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
            if (urls.Count > 0)
                await this.AgregarColaSolicitud(urls!);
        }

        private async Task AgregarColaSolicitud(List<string> urls)
        {
            var colas = new List<SEG_ColaSolicitud>();
            foreach (var url in urls)
            {
                var solicitud = new SEG_ColaSolicitud
                {
                    Tipo = EventosColas.PERMISOSACTUALIZADOS,
                    UrlDestino = url,
                    Estado = EstadoCola.Pendiente,
                };
                await _colaSolicitudRepositorio.CrearAsync(solicitud);
                _ = _jobEncoladorServicio.EncolarPorColaSolicitudId(solicitud.Id, true);
            }
        }
    }
}
