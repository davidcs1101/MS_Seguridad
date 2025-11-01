using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using Utilidades;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSEmpresas : IMSEmpresas
    {
        private readonly IMSEmpresasContextoWebServicio _msEmpresasContextoWebServicio;
        private readonly IServicioComun _servicioComun;

        public MSEmpresas(IMSEmpresasContextoWebServicio msEmpresasContextoWebServicio, IServicioComun servicioComun)
        {
            _msEmpresasContextoWebServicio = msEmpresasContextoWebServicio;
            _servicioComun = servicioComun;
        }

        public async Task<bool> ValidarSedeExisteAsync(int id) 
        {
            var sede = await _servicioComun.ObtenerRespuestaHttpAsync<int, SedeDto>(
                funcionEjecutar: _msEmpresasContextoWebServicio.ObtenerSedePorIdAsync,
                request: id);
            return sede.Id != 0;
        }
    }
}
