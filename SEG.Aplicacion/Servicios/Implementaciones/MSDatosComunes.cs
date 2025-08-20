using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSDatosComunes : IMSDatosComunes
    {
        private readonly IMSDatosComunesBackgroundServicio _msDatosComunesBackgroundServicio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public MSDatosComunes(IMSDatosComunesBackgroundServicio msDatosComunesBackgroundServicio, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _msDatosComunesBackgroundServicio = msDatosComunesBackgroundServicio;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<int> ObtenerIdListaDetallePorCodigoListaYCodigoListaDetalleAsync(CodigoDetalleRequest codigoListaIdDetalleRequest)
        {
            var respuesta = await _msDatosComunesBackgroundServicio.ObtenerListaDetallePorCodigoListaYCodigoListaDetalleAsync(codigoListaIdDetalleRequest);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<ListaDetalleDto?>>(contenidoJson);
            if (resultado == null || resultado.Data == null)
                return 0;

            return resultado.Data.Id;
        }
        public async Task<int> ObtenerIdListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoListaIdDetalleRequest) 
        {
            var respuesta = await _msDatosComunesBackgroundServicio.ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(codigoListaIdDetalleRequest);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado =  _serializadorJsonServicio.Deserializar<ApiResponse<ListaDetalleDto?>>(contenidoJson);
            if (resultado == null || resultado.Data == null)
                return 0;

            return resultado.Data.Id;
        }

        public async Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoListaAsync(string codigoLista) 
        {
            var respuesta = await _msDatosComunesBackgroundServicio.ListarListasDetallePorCodigoListaAsync(codigoLista);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<List<ListaDetalleDto?>>>(contenidoJson);

            return resultado.Data;
        }

        public async Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante)
        {
            var respuesta = await _msDatosComunesBackgroundServicio.ListarListasDetallePorCodigoConstanteAsync(codigoConstante);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<List<ListaDetalleDto?>>>(contenidoJson);

            return resultado.Data;
        }
    }
}
