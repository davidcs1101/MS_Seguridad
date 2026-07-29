using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Dtos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones.Cache
{
    public class DatosComunesListasCache
    {
        //    private readonly object _lock = new();

        //    private Dictionary<string, List<ListaDetalleDto>> _parametros = new Dictionary<string, List<ListaDetalleDto>>();

        //    private readonly IMSDatosComunes _msDatosComunes;
        //    private readonly IApiResponse _apiResponse;

        //    public DatosComunesListasCache(IMSDatosComunes msDatosComunes, IApiResponse apiResponse)
        //    {
        //        _msDatosComunes = msDatosComunes;
        //        _apiResponse = apiResponse;
        //    }

        //    public async Task InicializarAsync()
        //    {
        //        await InicializarListasAsync();
        //    }

        //    public ApiResponseDto<string> Actualizar(List<ListaDetalleDto> listasDetalle)
        //    {
        //        if (!listasDetalle.Any())
        //            return _apiResponse.CrearRespuesta(false,"La lista está vacía.","");

        //        var codigoLista = listasDetalle.First().CodigoLista;

        //        lock (_lock)
        //        {
        //            _parametros[codigoLista] = listasDetalle;
        //        }

        //        Logs.EscribirLog("i", $"{Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA}: {codigoLista}");

        //        return _apiResponse.CrearRespuesta(
        //            true,
        //            Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA,
        //            "");
        //    }

        //    public IReadOnlyList<ListaDetalleDto> ListarPorCodigoLista(string codigoLista)
        //    {
        //        lock (_lock)
        //        {
        //            if (_parametros.TryGetValue(codigoLista, out var lista))
        //                return lista.AsReadOnly();

        //            return Array.Empty<ListaDetalleDto>();
        //        }
        //    }

        //    public ListaDetalleDto? ObtenerPorCodigoListaYId(string codigoLista, int id)
        //    {
        //        lock (_lock)
        //        {
        //            if (_parametros.TryGetValue(codigoLista, out var lista))
        //                return lista.FirstOrDefault(x => x.Id == id);

        //            return null;
        //        }
        //    }

        //    public ListaDetalleDto? ObtenerPorCodigoListaYCodigoListaDetalle(string codigoLista, string codigoDetalle)
        //    {
        //        lock (_lock)
        //        {
        //            if (_parametros.TryGetValue(codigoLista, out var lista))
        //                return lista.FirstOrDefault(x => x.Codigo == codigoDetalle);

        //            return null;
        //        }
        //    }



        //    private async Task InicializarListasAsync()
        //    {
        //        lock (_lock)
        //        {
        //            if (_parametros.Count > 0)
        //                return;
        //        }

        //        var listas = await _msDatosComunes.ListarListasDetalleAsync();

        //        var agrupadas = listas
        //            .GroupBy(x => x.CodigoLista!)
        //            .ToDictionary(
        //                g => g.Key,
        //                g => g.ToList());

        //        lock (_lock)
        //        {
        //            if (_parametros.Count == 0)
        //                _parametros = agrupadas;
        //        }

        //        Logs.EscribirLog("i", Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_INICIALIZADA);
        //    }
        //}
    }
}