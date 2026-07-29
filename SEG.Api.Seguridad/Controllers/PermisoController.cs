using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Api.Seguridad.Middlewares.Permisos;
using SEG.Aplicacion.CasosUso.Implementaciones;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dtos;
using Utilidades.Seguridad;
using Utilidades.Dtos;

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

        [HttpGet("obtenerPorCodigo")]
        [Permiso(CodigosPermisos.Permisos.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<PermisoDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _permisoServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Permisos.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<PermisoDto>?>>> Listar()
        {
            return await _permisoServicio.ListarAsync();

        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.Permisos.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(PermisoModificacionRequest permisoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _permisoServicio.ModificarAsync(permisoModificacionRequest);
        }
    }
}
