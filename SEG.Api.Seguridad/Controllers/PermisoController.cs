using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Api.Seguridad.Middlewares.Permisos;
using SEG.Aplicacion.CasosUso.Implementaciones;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dtos;
using Utilidades.Seguridad;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/permisos")]
    [Authorize]
    public class PermisoController: Controller
    {
        private readonly IPermisoServicio _permisoServicio;
        public PermisoController(IPermisoServicio permisoServicio)
        {
            _permisoServicio = permisoServicio;
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.Permisos.MODIFICAR)]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(PermisoModificacionRequest permisoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _permisoServicio.ModificarAsync(permisoModificacionRequest);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Permisos.LISTAR)]
        public async Task<ActionResult<ApiResponse<List<PermisoDto>?>>> Listar()
        {
            return await _permisoServicio.ListarAsync();

        }
    }
}
