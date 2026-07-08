using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Api.Seguridad.Middlewares.Permisos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dtos;
using Utilidades.Seguridad;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/programas")]
    [Authorize]
    public class ProgramaController : Controller
    {
        private readonly IProgramaServicio _programaServicio;
        public ProgramaController(IProgramaServicio programaServicio)
        {
            _programaServicio = programaServicio; 
        }

        [HttpGet("obtenerPorId")]
        [Permiso(Permisos.Programas.CONSULTAR)]
        public async Task<ActionResult<ApiResponse<ProgramaDto?>>> ObtenerPorId(int id)
        {
            return await _programaServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        [Permiso(Permisos.Programas.CONSULTAR)]
        public async Task<ActionResult<ApiResponse<ProgramaDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _programaServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpPost("crear")]
        [Permiso(Permisos.Programas.CREAR)]
        public async Task<ActionResult<ApiResponse<int>>> Crear(ProgramaCreacionRequest programaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _programaServicio.CrearAsync(programaCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(Permisos.Programas.MODIFICAR)]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(ProgramaModificacionRequest programaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _programaServicio.ModificarAsync(programaModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(Permisos.Programas.ELIMINAR)]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            return await _programaServicio.EliminarAsync(id);
        }

        [HttpGet("listar")]
        [Permiso(Permisos.Programas.LISTAR)]
        public async Task<ActionResult<ApiResponse<List<ProgramaDto>?>>> Listar()
        {
            return await _programaServicio.ListarAsync();
        }
    }
}
