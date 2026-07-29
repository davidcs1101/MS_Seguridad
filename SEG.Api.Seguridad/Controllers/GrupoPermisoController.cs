using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Api.Seguridad.Middlewares.Permisos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dtos;
using Utilidades.Seguridad;
using Utilidades.Dtos;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/gruposPermisos")]
    [Authorize]
    public class GrupoPermisoController : Controller
    {
        private readonly IGrupoPermisoServicio _grupoPermisoServicio;
        public GrupoPermisoController(IGrupoPermisoServicio grupoPermisoServicio)
        {
            _grupoPermisoServicio = grupoPermisoServicio;
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.GruposPermisos.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(GrupoPermisoCreacionRequest grupoPermisoCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoPermisoServicio.CrearAsync(grupoPermisoCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.GruposPermisos.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(GrupoPermisoModificacionRequest grupoPermisoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoPermisoServicio.ModificarAsync(grupoPermisoModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.GruposPermisos.ELIMINAR)]
        public async Task<ApiResponseDto<string>> Eliminar(int id) 
        { 
            return await _grupoPermisoServicio.EliminarAsync(id);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.GruposPermisos.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<GrupoPermisoDto>?>>> Listar()
        {
            return await _grupoPermisoServicio.ListarAsync();

        }
    }
}
