using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Api.Seguridad.Middlewares.Permisos;
using Utilidades.AtributosValidaciones.Seguridad;

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
        [Permiso(Permisos.Grupos.CONSULTAR)]
        [HttpGet("obtenerPorId")]
        public async Task<ActionResult<ApiResponse<GrupoDto?>>> ObtenerPorId(int id)
        {
            return await _grupoServicio.ObtenerPorIdAsync(id);
        }

        [Permiso(Permisos.Grupos.CONSULTAR)]
        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<GrupoDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _grupoServicio.ObtenerPorCodigoAsync(codigo);
            
        }

        [Permiso(Permisos.Grupos.LISTAR)]
        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<GrupoDto>?>>> Listar()
        {
            return await _grupoServicio.ListarAsync();
            
        }
        
        [Permiso(Permisos.Grupos.CREAR)]
        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(GrupoCreacionRequest grupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoServicio.CrearAsync(grupoCreacionRequest);
            
        }

        [Permiso(Permisos.Grupos.MODIFICAR)]
        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(GrupoModificacionRequest grupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _grupoServicio.ModificarAsync(grupoModificacionRequest);
        }

        [Permiso(Permisos.Grupos.ELIMINAR)]
        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id) 
        {
            return await _grupoServicio.EliminarAsync(id);
        }
    }
}
