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
    public class SincronizadorMicroservicios : ISincronizadorMicroservicios
    {
        /// <inheritdoc/>
        private readonly IAutorizacionServicio _autorizacionServicio;
        private readonly ISeguridadPermisosCache _permisosCache;
        private readonly IJobEncoladorServicio _jobEncoladorServicio;

        public SincronizadorMicroservicios(IAutorizacionServicio autorizacionServicio,
            ISeguridadPermisosCache permisosCache, IJobEncoladorServicio jobEncoladorServicio)
        {
            _autorizacionServicio = autorizacionServicio;
            _permisosCache = permisosCache;
            _jobEncoladorServicio = jobEncoladorServicio;
        }

        public async Task SincronizarPermisosAsync(List<int> colasSolicitudIds)
        {
            var permisos = await _autorizacionServicio.ListarCatalogoAutorizacionAsync();
            _permisosCache.Actualizar(permisos);

            _ = _jobEncoladorServicio.EncolarPorColasSolicitudesIds(colasSolicitudIds, true);
        }
    }
}
