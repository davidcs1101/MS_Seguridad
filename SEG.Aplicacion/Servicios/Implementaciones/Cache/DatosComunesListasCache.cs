using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Dtos;
using Utilidades;

namespace SEG.Aplicacion.Servicios.Implementaciones.Cache
{
    public class DatosComunesListasCache : IDatosComunesListasCache
    {
        private readonly object _lock = new();
        private List<ListaDetalleDto> _listaTiposIdentificacion = new();
        private readonly IMSDatosComunes _msDatosComunes;
        private IApiResponse _apiResponse;

        public DatosComunesListasCache(IMSDatosComunes msDatosComunes, IApiResponse apiResponse)
        {
            _msDatosComunes = msDatosComunes;
            _apiResponse = apiResponse;
        }

        public async Task InicializarAsync()
        {
            await InicializarListasTiposIdentificacionAsync();
        }

        public ApiResponse<string> Actualizar(List<ListaDetalleDto> listasDetalle)
        {
            var codigoLista = listasDetalle.FirstOrDefault()?.CodigoLista;
            switch (codigoLista)
            {
                case CodigosListas.TIPOSIDENTIFICACION:
                    lock (_lock)
                        _listaTiposIdentificacion = listasDetalle.ToList();
                    break;
            }

            var mensaje = Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_ACTUALIZADA;
            Logs.EscribirLog("i", $"{mensaje}: {codigoLista}");
            return _apiResponse.CrearRespuesta(true, mensaje, "");
        }

        public IReadOnlyList<ListaDetalleDto> ListarPorCodigoLista(string codigoLista)
        {
            lock (_lock)
            {
                return codigoLista switch
                {
                    CodigosListas.TIPOSIDENTIFICACION => _listaTiposIdentificacion.AsReadOnly(),
                    _ => Array.Empty<ListaDetalleDto>()
                };
            }
        }

        public ListaDetalleDto? ObtenerPorCodigoListaYId(string codigoLista,int id)
        {
            switch (codigoLista)
            {
                case CodigosListas.TIPOSIDENTIFICACION:
                    lock (_lock)
                        return _listaTiposIdentificacion.FirstOrDefault(t => t.Id == id);
                default:
                    return null;
            }
        }

        public ListaDetalleDto? ObtenerPorCodigoListaYCodigoListaDetalle(string codigoLista,string codigoDetalle)
        {
            switch (codigoLista) {
                case CodigosListas.TIPOSIDENTIFICACION:
                    lock (_lock)
                        return _listaTiposIdentificacion.FirstOrDefault(t => t.Codigo == codigoDetalle);
                default:
                    return null;
            }
        }



        private async Task InicializarListasTiposIdentificacionAsync()
        {
            lock (_lock)
            {
                if (_listaTiposIdentificacion.Count > 0)
                    return;
            }
            var lista = await ObtenerListasDetallePorCodigoListaAsync(CodigosListas.TIPOSIDENTIFICACION);
            lock (_lock)
            {
                if (_listaTiposIdentificacion.Count == 0) // segundo chequeo
                    _listaTiposIdentificacion = lista;
            }
            Logs.EscribirLog("i", $"{Textos.CacheDatos.MENSAJE_CACHE_DATOSCOMUNES_INICIALIZADA}: {CodigosListas.TIPOSIDENTIFICACION}");
        }

        private async Task<List<ListaDetalleDto?>> ObtenerListasDetallePorCodigoListaAsync(string codigoLista)
        {
            return await _msDatosComunes.ListarListasDetallePorCodigoListaAsync(codigoLista);
        }
    }
}
