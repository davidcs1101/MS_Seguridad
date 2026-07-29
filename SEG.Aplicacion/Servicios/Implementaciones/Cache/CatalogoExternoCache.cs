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
    public class CatalogoExternoCache : ICatalogoExternoCache
    {
        private readonly object _lock = new();

        private Dictionary<string, List<CatalogoExternoDto>> _catalogos = new Dictionary<string, List<CatalogoExternoDto>>();

        private readonly IApiResponse _apiResponse;
        private readonly IServiceScopeFactory _scopeFactory;

        public CatalogoExternoCache(IApiResponse apiResponse, IServiceScopeFactory scopeFactory)
        {
            _apiResponse = apiResponse;
            _scopeFactory = scopeFactory;
        }

        public async Task InicializarAsync()
        {
            await InicializarCatalogosAsync();
        }

        public ApiResponseDto<string> Actualizar(List<CatalogoExternoDto> parametrosExternos)
        {
            var parametros = parametrosExternos.GroupBy(x => x.CodigoCatalogo)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList());

            lock (_lock)
                _catalogos = parametros;

            Logs.EscribirLog("i", Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA);

            return _apiResponse.CrearRespuesta(
                true,
                Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA,
                "");
        }

        public IReadOnlyList<CatalogoExternoDto> ListarPorCodigoCatalogo(string codigoCatalogo)
        {
            lock (_lock)
            {
                if (_catalogos.TryGetValue(codigoCatalogo, out var lista))
                    return lista.AsReadOnly();

                return Array.Empty<CatalogoExternoDto>();
            }
        }

        public CatalogoExternoDto? ObtenerPorCodigoCatalogoYOrigenId(string codigoCatalogo, int origenId)
        {
            lock (_lock)
            {
                if (_catalogos.TryGetValue(codigoCatalogo, out var lista))
                    return lista.FirstOrDefault(x => x.OrigenId == origenId);

                return null;
            }
        }

        public CatalogoExternoDto? ObtenerPorCodigoCatalogoYCodigo(string codigoCatalogo, string codigo)
        {
            lock (_lock)
            {
                if (_catalogos.TryGetValue(codigoCatalogo, out var lista))
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
                if (_catalogos.Count > 0)
                    return;
            }

            await ObtenerListaCatalogosAsync();
        }

        private async Task ObtenerListaCatalogosAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var catalogoExternoRepositorio = scope.ServiceProvider
                .GetRequiredService<ICatalogoExternoRepositorio>();

            var listas = await catalogoExternoRepositorio
                .Listar()
                .Select(x => new CatalogoExternoDto
                {
                    Id = x.Id,
                    ServicioOrigen = x.ServicioOrigen,
                    CodigoCatalogo = x.CodigoCatalogo,
                    OrigenId = x.OrigenId,
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