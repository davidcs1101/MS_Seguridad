using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSDatosComunes : IMSDatosComunes
    {
        private readonly IMSDatosComunesBackgroundServicio _msDatosComunesBackgroundServicio;
        private readonly IServicioComun _servicioComun;

        public MSDatosComunes(IMSDatosComunesBackgroundServicio msDatosComunesBackgroundServicio, IServicioComun servicioComun)
        {
            _msDatosComunesBackgroundServicio = msDatosComunesBackgroundServicio;
            _servicioComun = servicioComun;
        }

        public async Task<List<ListaDetalleDto?>> ListarListasDetallePorCodigosConstanteAsync(List<string> codigosConstante)
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<List<ListaDetalleDto?>>(
                funcionEjecutar: () => _msDatosComunesBackgroundServicio.ListarListasDetallePorCodigosConstanteAsync(codigosConstante));
        }

    }
}
