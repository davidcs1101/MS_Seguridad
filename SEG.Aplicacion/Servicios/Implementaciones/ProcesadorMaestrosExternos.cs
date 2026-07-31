using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using System.Text.RegularExpressions;
using Utilidades;
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorMaestrosExternos : IProcesadorMaestrosExternos
    {
        private readonly IMSDatosComunes _msDatosComunes;
        private readonly IMaestroExternoRepositorio _catalogoExternoRepositorio;
        private readonly IMaestroExternoCache _catalogoExternoCache;

        public ProcesadorMaestrosExternos(IMSDatosComunes msDatosComunes, IMaestroExternoRepositorio catalogoExternoRepositorio, IMaestroExternoCache catalogoExternoCache)
        {
            _msDatosComunes = msDatosComunes;
            _catalogoExternoRepositorio = catalogoExternoRepositorio;
            _catalogoExternoCache = catalogoExternoCache;
        }

        public async Task ProcesarDatosConstantesAsync()
        {
            // Obtener del appsettings la lista de catálogos a procesar
            var constantesRequeridas = ObtenerCodigosConstantesRequeridos();
            await ProcesarConstantesAsync(constantesRequeridas!);
        }

        public async Task ProcesarDatosConstantesAsync(MaestroActualizadoEventoDto codigosConstantes)
        {
            if (codigosConstantes is null || !codigosConstantes.CodigosMaestro.Any())
                return; // Si no se proporcionan códigos, no se hace nada

            // Obtener la lista de constantes a procesar
            var constantesRequeridas = ObtenerCodigosConstantesRequeridos();

            // Solo procesar los catálogos que realmente consume este micro
            var constantesValidas = codigosConstantes.CodigosMaestro
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Intersect(constantesRequeridas, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (!constantesValidas.Any())
                return;

            await ProcesarConstantesAsync(constantesValidas!);
        }



        private async Task ProcesarConstantesAsync(List<string?>? codigosMaestros)
        {
            if (codigosMaestros == null || !codigosMaestros.Any())
                throw new Exception("La lista de catálogos (DatosConstantes) a consultar se encuentra vacía.");

            // Obtener los registros desde Datos Comunes
            var catalogos = await _msDatosComunes.ListarListasDetallePorCodigosConstanteAsync(codigosMaestros!);

            // Agrupar por catálogo
            var catalogosAgrupados = catalogos.GroupBy(x => x.CodigoDatoConstante);

            foreach (var grupo in catalogosAgrupados)
            {
                var parametros = grupo.Select(x => new SEG_MaestroExterno
                {
                    ServicioOrigen = "ms_datoscomunes",
                    CodigoMaestro = x.CodigoDatoConstante!,
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

        //Acá se adicionará las difreentes constantes que se requieran para el microservicio.
        private List<string> ObtenerCodigosConstantesRequeridos() 
        {
            return new List<string>() 
            {
                CodigosConstantes.TIPOIDENTIREGISTROUSUARIO
            };
        }

    }
}
