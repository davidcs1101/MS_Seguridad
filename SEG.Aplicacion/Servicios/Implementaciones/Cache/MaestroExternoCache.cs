using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Dominio.Repositorio;
using SEG.Dtos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones.Cache
{
    public class MaestroExternoCache : IMaestroExternoCache
    {
        private readonly object _lock = new();

        private Dictionary<string, List<MaestroExternoDto>> _maestros = new Dictionary<string, List<MaestroExternoDto>>();

        private readonly IApiResponse _apiResponse;
        private readonly IServiceScopeFactory _scopeFactory;

        public MaestroExternoCache(IApiResponse apiResponse, IServiceScopeFactory scopeFactory)
        {
            _apiResponse = apiResponse;
            _scopeFactory = scopeFactory;
        }

        public async Task InicializarAsync()
        {
            await InicializarCatalogosAsync();
        }

        public ApiResponseDto<string> Actualizar(List<MaestroExternoDto> parametrosExternos)
        {
            var parametros = parametrosExternos.GroupBy(x => x.CodigoMaestro)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList());

            lock (_lock)
                _maestros = parametros;

            Logs.EscribirLog("i", Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA);

            return _apiResponse.CrearRespuesta(
                true,
                Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA,
                "");
        }

        public IReadOnlyList<MaestroExternoDto> ListarPorCodigoMaestro(string codigoMaestro)
        {
            lock (_lock)
            {
                if (_maestros.TryGetValue(codigoMaestro, out var lista))
                    return lista.AsReadOnly();

                return Array.Empty<MaestroExternoDto>();
            }
        }

        public MaestroExternoDto? ObtenerPorCodigoMaestroYOrigenId(string codigoMaestro, int origenId)
        {
            lock (_lock)
            {
                if (_maestros.TryGetValue(codigoMaestro, out var lista))
                    return lista.FirstOrDefault(x => x.OrigenId == origenId);

                return null;
            }
        }

        public MaestroExternoDto? ObtenerPorCodigoMaestroYCodigo(string codigoMaestro, string codigo)
        {
            lock (_lock)
            {
                if (_maestros.TryGetValue(codigoMaestro, out var lista))
                    return lista.FirstOrDefault(x => x.Codigo == codigo);

                return null;
            }
        }

        public async Task RefrescarAsync()
        {
            await ObtenerListaCatalogosAsync();
        }



        private async Task InicializarCatalogosAsync()
        {
            lock (_lock)
            {
                if (_maestros.Count > 0)
                    return;
            }

            await ObtenerListaCatalogosAsync();
        }

        private async Task ObtenerListaCatalogosAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var catalogoExternoRepositorio = scope.ServiceProvider
                .GetRequiredService<IMaestroExternoRepositorio>();

            var listas = await catalogoExternoRepositorio
                .Listar()
                .Select(x => new MaestroExternoDto
                {
                    Id = x.Id,
                    ServicioOrigen = x.ServicioOrigen,
                    CodigoMaestro = x.CodigoMaestro,
                    OrigenId = x.OrigenId,
                    OrigenPadreId = x.OrigenPadreId,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    EstadoActivo = x.EstadoActivo,
                })
                .ToListAsync();

            Actualizar(listas);

            Logs.EscribirLog("i", Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_INICIALIZADA);
        }
    }
}