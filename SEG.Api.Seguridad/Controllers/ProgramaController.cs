using log4net.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEG.Dtos;
using SEG.Servicio.Implementaciones;
using SEG.Servicio.Interfaces;
using Utilidades;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/programas")]
    [Authorize(policy: "ProgramasPermiso")]
    public class ProgramaController : Controller
    {
        private readonly IProgramaServicio _programaServicio;
        public ProgramaController(IProgramaServicio programaServicio)
        {
            _programaServicio = programaServicio; 
        }

        [HttpGet("obtenerPorId")]
        public async Task<ActionResult<ApiResponse<ProgramaDto?>>> ObtenerPorId(int id)
        {
            return await _programaServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<ProgramaDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _programaServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(ProgramaCreacionRequest programaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _programaServicio.CrearAsync(programaCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(ProgramaModificacionRequest programaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _programaServicio.ModificarAsync(programaModificacionRequest);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            return await _programaServicio.EliminarAsync(id);
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<ProgramaDto>?>>> Listar()
        {
            return await _programaServicio.ListarAsync();
        }
    }
}
