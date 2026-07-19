using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Api.Seguridad.Middlewares.Permisos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dtos;
using Utilidades.Seguridad;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/acciones")]
    [Authorize]
    public class AccionController : Controller
    {
        private readonly IAccionServicio _accionServicio;
        public AccionController(IAccionServicio accionServicio)
        {
            _accionServicio = accionServicio; 
        }

        [HttpGet("obtenerPorCodigo")]
        [Permiso(CodigosPermisos.Acciones.CONSULTAR)]
        public async Task<ActionResult<ApiResponse<AccionDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _accionServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Acciones.LISTAR)]
        public async Task<ActionResult<ApiResponse<List<AccionDto>?>>> Listar()
        {
            return await _accionServicio.ListarAsync();
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.Acciones.CREAR)]
        public async Task<ActionResult<ApiResponse<int>>> Crear(AccionCreacionRequest accionCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _accionServicio.CrearAsync(accionCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.Acciones.MODIFICAR)]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(AccionModificacionRequest accionModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _accionServicio.ModificarAsync(accionModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.Acciones.ELIMINAR)]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            return await _accionServicio.EliminarAsync(id);
        }
    }
}
