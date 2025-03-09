using log4net.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System.Reflection.Metadata;
using Utilidades;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/grupos")]
    [Authorize(policy: "GruposPermiso")]
    public class GrupoController : Controller
    {
        private readonly IGrupoServicio _grupoServicio;
        public GrupoController(IGrupoServicio grupoServicio)
        {
            _grupoServicio = grupoServicio; 
        }

        [HttpGet("obtenerPorId")]
        public async Task<ActionResult<ApiResponse<GrupoDto?>>> ObtenerPorId(int id)
        {
            return await _grupoServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<GrupoDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _grupoServicio.ObtenerPorCodigoAsync(codigo);
            
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<GrupoDto>?>>> Listar()
        {
            return await _grupoServicio.ListarAsync();
            
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(GrupoCreacionRequest grupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoServicio.CrearAsync(grupoCreacionRequest);
            
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(GrupoModificacionRequest grupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _grupoServicio.ModificarAsync(grupoModificacionRequest);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id) 
        {
            return await _grupoServicio.EliminarAsync(id);
        }
    }
}
