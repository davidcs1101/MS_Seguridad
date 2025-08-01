using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSDatosComunes : IMSDatosComunes
    {
        private readonly IMSDatosComunesContextoWebServicio _msDatosComunesContextoWebServicio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public MSDatosComunes(IMSDatosComunesContextoWebServicio msDatosComunesContextoWebServicio, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _msDatosComunesContextoWebServicio = msDatosComunesContextoWebServicio;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<int> ObtenerIdListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(CodigoDetalleRequest codigoListaIdDetalleRequest) 
        {
            var respuesta = await _msDatosComunesContextoWebServicio.ObtenerListaDetallePorCodigoConstanteYCodigoListaDetalleAsync(codigoListaIdDetalleRequest);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado =  _serializadorJsonServicio.Deserializar<ApiResponse<ListaDetalleDto?>>(contenidoJson);
            if (resultado == null || resultado.Data == null)
                return 0;

            return resultado.Data.Id;
        }
    }
}
