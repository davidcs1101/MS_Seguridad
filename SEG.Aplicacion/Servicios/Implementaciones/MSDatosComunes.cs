using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using Utilidades;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSDatosComunes : IMSDatosComunes
    {
        private readonly IMSDatosComunesBackgroundServicio _msDatosComunesBackgroundServicio;
        private readonly IServicioComun _servicioComun;

        public MSDatosComunes(IMSDatosComunesBackgroundServicio msDatosComunesBackgroundServicio, ISerializadorJsonServicio serializadorJsonServicio, IRespuestaHttpValidador respuestaHttpValidador, IServicioComun servicioComun)
        {
            _msDatosComunesBackgroundServicio = msDatosComunesBackgroundServicio;
            _servicioComun = servicioComun;
        }

        public async Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoListaAsync(string codigoLista) 
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<string, List<ListaDetalleDto?>>(
                funcionEjecutar: _msDatosComunesBackgroundServicio.ListarListasDetallePorCodigoListaAsync,
                request: codigoLista);
        }

        public async Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigoConstanteAsync(string codigoConstante)
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<string, List<ListaDetalleDto?>>(
                funcionEjecutar: _msDatosComunesBackgroundServicio.ListarListasDetallePorCodigoConstanteAsync,
                request: codigoConstante);
        }
    }
}
