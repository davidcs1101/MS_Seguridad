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
            var respuesta = await _grupoServicio.ObtenerPorIdAsync(id);
            return respuesta;
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<GrupoDto?>>> ObtenerPorCodigo(string codigo)
        {
            var respuesta = await _grupoServicio.ObtenerPorCodigoAsync(codigo);
            return respuesta;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<GrupoDto>?>>> Listar()
        {
            var grupos = await _grupoServicio.ListarAsync();
            return grupos;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(GrupoCreacionRequest grupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _grupoServicio.CrearAsync(grupoCreacionRequest);
            return respuesta;
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(GrupoModificacionRequest grupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var respuesta = await _grupoServicio.ModificarAsync(grupoModificacionRequest);
            return respuesta;
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id) 
        {
            var respuesta = await _grupoServicio.EliminarAsync(id);
            return respuesta;
        }
    }
}
