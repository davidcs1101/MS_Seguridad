using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSDatosComunes : IMSDatosComunes
    {
        private readonly IMSDatosComunesContextoWebServicio _msDatosComunesContextoWebServicio;

        public MSDatosComunes(IMSDatosComunesContextoWebServicio msDatosComunesContextoWebServicio)
        {
            _msDatosComunesContextoWebServicio = msDatosComunesContextoWebServicio;
        }

        public async Task<bool> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoListaIdDetalleRequest codigoListaIdDetalleRequest) 
        {
            await _msDatosComunesContextoWebServicio.ValidarIdDetalleExisteEnCodigoListaAsync(codigoListaIdDetalleRequest);
            return true;
        }
    }
}
