using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSDatosComunes : IMSDatosComunes
    {
        private readonly IMSDatosComunesBackgroundServicio _msDatosComunesBackgroundServicio;
        private readonly IServicioComun _servicioComun;

        public MSDatosComunes(IMSDatosComunesBackgroundServicio msDatosComunesBackgroundServicio, IRespuestaHttpValidador respuestaHttpValidador, IServicioComun servicioComun)
        {
            _msDatosComunesBackgroundServicio = msDatosComunesBackgroundServicio;
            _servicioComun = servicioComun;
        }

        public async Task<List<ListaDetalleDto?>> ListarListasDetalleAsync()
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<List<ListaDetalleDto?>>(
                funcionEjecutar: _msDatosComunesBackgroundServicio.ListarListasDetalleAsync);
        }

    }
}
