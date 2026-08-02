using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSEmpresas : IMSEmpresas
    {
        private readonly IMSEmpresasBackgroundServicio _msEmpresasBackgroundServicio;
        private readonly IServicioComun _servicioComun;

        public MSEmpresas(IMSEmpresasBackgroundServicio msEmpresasBackgroundServicio, IServicioComun servicioComun)
        {
            _servicioComun = servicioComun;
            _msEmpresasBackgroundServicio = msEmpresasBackgroundServicio;
        }

        public async Task<bool> ValidarSedeExisteAsync(int id) 
        {
            var sede = await ObtenerSedePorId(id);
            return sede.Id != 0;
        }

        public async Task<SedeDto> ObtenerSedePorId(int sedeId) 
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<int, SedeDto>(
                funcionEjecutar: _msEmpresasBackgroundServicio.ObtenerSedePorIdAsync,
                request: sedeId);
        }

        public async Task<List<SedeDto?>> ListarSedesAsync()
        {
            return await _servicioComun.ObtenerRespuestaHttpAsync<List<SedeDto?>>(
                funcionEjecutar: _msEmpresasBackgroundServicio.ListarSedesAsync);
        }

    }
}
