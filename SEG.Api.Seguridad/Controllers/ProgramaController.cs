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
    [Route("api/programas")]
    [Authorize]
    public class ProgramaController : Controller
    {
        private readonly IProgramaServicio _programaServicio;
        public ProgramaController(IProgramaServicio programaServicio)
        {
            _programaServicio = programaServicio; 
        }

        [HttpGet("obtenerPorCodigo")]
        [Permiso(CodigosPermisos.Programas.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<ProgramaDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _programaServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Programas.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<ProgramaDto>?>>> Listar()
        {
            return await _programaServicio.ListarAsync();
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.Programas.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(ProgramaCreacionRequest programaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _programaServicio.CrearAsync(programaCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.Programas.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(ProgramaModificacionRequest programaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _programaServicio.ModificarAsync(programaModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.Programas.ELIMINAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id)
        {
            return await _programaServicio.EliminarAsync(id);
        }
    }
}
