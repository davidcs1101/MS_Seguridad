using SEG.Dtos;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;

namespace SEG.Aplicacion.Servicios.Implementaciones
{
    public class MSEmpresas : IMSEmpresas
    {
        private readonly IMSEmpresasContextoWebServicio _msEmpresasContextoWebServicio;

        public MSEmpresas(IMSEmpresasContextoWebServicio msEmpresasContextoWebServicio)
        {
            _msEmpresasContextoWebServicio = msEmpresasContextoWebServicio;
        }

        public async Task<bool> ValidarSedeExisteAsync(int id) 
        {
            await _msEmpresasContextoWebServicio.ObtenerSedePorIdAsync(id);
            return true;
        }
    }
}
