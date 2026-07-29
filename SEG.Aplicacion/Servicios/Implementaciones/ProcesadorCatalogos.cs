using SEG.Dominio.Repositorio;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dominio.Entidades;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorCatalogos : IProcesadorCatalogos
    {
        private readonly IMSDatosComunes _msDatosComunes;
        private readonly ICatalogoExternoRepositorio _catalogoExternoRepositorio;
        private readonly ICatalogoExternoCache _catalogoExternoCache;
        private readonly IAppSettings _appSettings;

        public ProcesadorCatalogos(IMSDatosComunes msDatosComunes, ICatalogoExternoRepositorio catalogoExternoRepositorio, ICatalogoExternoCache catalogoExternoCache, IAppSettings appSettings)
        {
            _msDatosComunes = msDatosComunes;
            _catalogoExternoRepositorio = catalogoExternoRepositorio;
            _catalogoExternoCache = catalogoExternoCache;
            _appSettings = appSettings;
        }

        public async Task ProcesarAsync()
        {
            // Obtener del appsettings la lista de catálogos a procesar
            var codigosCatalogos = _appSettings.ObtenerConsultasDatosComunesCodigosConstantes();
            if (codigosCatalogos == null || !codigosCatalogos.Any())
                throw new Exception("No se han configurado los códigos de catálogos a procesar en el appsettings.");

            // Obtener los registros desde Datos Comunes
            var catalogos = await _msDatosComunes.ListarListasDetallePorCodigosConstanteAsync(codigosCatalogos!);

            // Agrupar por catálogo
            var catalogosAgrupados = catalogos.GroupBy(x => x.CodigoDatoConstante);

            foreach (var grupo in catalogosAgrupados)
            {
                var parametros = grupo.Select(x => new SEG_CatalogoExterno
                    {
                        ServicioOrigen = "ms_datoscomunes",
                        CodigoCatalogo = x.CodigoDatoConstante!,
                        OrigenId = x.Id,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        EstadoActivo = x.EstadoActivo,
                        UsuarioCreadorId = x.UsuarioCreadorId,
                }).ToList();

                await _catalogoExternoRepositorio.SincronizarCatalogoAsync(
                    "ms_datoscomunes",
                    grupo.Key!,
                    parametros);
            }

            // Refrescar la caché local
            await _catalogoExternoCache.RefrescarAsync();
        }
    }
}
