using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Api.Seguridad.Middlewares.Permisos;
using Utilidades.Seguridad;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/grupos")]
    [Authorize]
    public class GrupoController : Controller
    {
        private readonly IGrupoServicio _grupoServicio;
        public GrupoController(IGrupoServicio grupoServicio)
        {
            _grupoServicio = grupoServicio; 
        }

        [HttpGet("obtenerPorCodigo")]
        [Permiso(CodigosPermisos.Grupos.CONSULTAR)]
        public async Task<ActionResult<ApiResponse<GrupoDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _grupoServicio.ObtenerPorCodigoAsync(codigo);            
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Grupos.LISTAR)]
        public async Task<ActionResult<ApiResponse<List<GrupoDto>?>>> Listar()
        {
            return await _grupoServicio.ListarAsync();
            
        }
        
        [HttpPost("crear")]
        [Permiso(CodigosPermisos.Grupos.CREAR)]
        public async Task<ActionResult<ApiResponse<int>>> Crear(GrupoCreacionRequest grupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoServicio.CrearAsync(grupoCreacionRequest);
            
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.Grupos.MODIFICAR)]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(GrupoModificacionRequest grupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _grupoServicio.ModificarAsync(grupoModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.Grupos.ELIMINAR)]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id) 
        {
            return await _grupoServicio.EliminarAsync(id);
        }
    }
}
