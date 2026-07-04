using SEG.Aplicacion.ServiciosExternos;
using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ServicioComun : IServicioComun
    {
        /// <inheritdoc/>
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public ServicioComun(IRespuestaHttpValidador respuestaHttpValidador, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _respuestaHttpValidador = respuestaHttpValidador;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<T> ObtenerRespuestaHttpAsync<TRequest, T>
            (Func<TRequest, Task<HttpResponseMessage>> funcionEjecutar, TRequest request)
        {
            var respuesta = await funcionEjecutar(request);
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Utilidades.Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<T?>>(contenidoJson);

            return resultado.Data!;
        }

        public async Task<T> ObtenerRespuestaHttpAsync<T>(
            Func<Task<HttpResponseMessage>> funcionEjecutar)
        {
            var respuesta = await funcionEjecutar();
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Utilidades.Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<T?>>(contenidoJson);

            return resultado.Data!;
        }
    }
}
