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

        public DatosComunesListasCache(IMSDatosComunes msDatosComunes)
        {
            _msDatosComunes = msDatosComunes;
        }

        public async Task InicializarAsync()
        {
            await InicializarListasTiposIdentificacionAsync();
        }

        public Task ActualizarAsync(List<ListaDetalleDto> listasDetalle)
        {
            switch (listasDetalle.FirstOrDefault()?.CodigoLista)
            {
                case CodigosListas.TIPOSIDENTIFICACION:
                    lock (_lock)
                        _listaTiposIdentificacion = listasDetalle.ToList();
                    break;
            }
            return Task.CompletedTask;
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

        public ListaDetalleDto? ObtenerPorId(int id)
        {
            lock (_lock)
                return _listaTiposIdentificacion.FirstOrDefault(t => t.Id == id);
        }

        public ListaDetalleDto? ObtenerPorCodigoDetalle(string codigoDetalle)
        {
            lock (_lock)
                return _listaTiposIdentificacion.FirstOrDefault(t => t.Codigo == codigoDetalle);
        }



        private async Task InicializarListasTiposIdentificacionAsync()
        {
            try
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

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<ListaDetalleDto?>> ObtenerListasDetallePorCodigoListaAsync(string codigoLista)
        {
            return await _msDatosComunes.ListarListasDetallePorCodigoListaAsync(codigoLista);
        }
    }
}
