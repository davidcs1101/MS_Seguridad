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
            var respuesta = await _programaServicio.ObtenerPorIdAsync(id);
            return respuesta;
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<ProgramaDto?>>> ObtenerPorCodigo(string codigo)
        {
            var respuesta = await _programaServicio.ObtenerPorCodigoAsync(codigo);
            return respuesta;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(ProgramaCreacionRequest programaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _programaServicio.CrearAsync(programaCreacionRequest);
            return respuesta;
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(ProgramaModificacionRequest programaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var respuesta = await _programaServicio.ModificarAsync(programaModificacionRequest);
            return respuesta;
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            var respuesta = await _programaServicio.EliminarAsync(id);
            return respuesta;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<ProgramaDto>?>>> Listar()
        {
            var programas = await _programaServicio.ListarAsync();
            return programas;
        }
    }
}
