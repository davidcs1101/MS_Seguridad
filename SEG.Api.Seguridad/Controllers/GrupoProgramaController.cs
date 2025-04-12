using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Servicio.Implementaciones;
using SEG.Servicio.Interfaces;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/gruposProgramas")]
    [Authorize(policy: "GruposProgramasPermiso")]
    public class GrupoProgramaController: Controller
    {
        private readonly IGrupoProgramaServicio _grupoProgramaServicio;
        public GrupoProgramaController(IGrupoProgramaServicio grupoProgramaServicio)
        {
            _grupoProgramaServicio = grupoProgramaServicio;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(GrupoProgramaCreacionRequest grupoProgramaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoProgramaServicio.CrearAsync(grupoProgramaCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(GrupoProgramaModificacionRequest grupoProgramaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _grupoProgramaServicio.ModificarAsync(grupoProgramaModificacionRequest);
        }

        [HttpDelete("eliminar")]
        public async Task<ApiResponse<string>> Eliminar(int id) 
        { 
            return await _grupoProgramaServicio.EliminarAsync(id);
        }

        [HttpGet("obtenerPorGrupoPrograma")]
        public async Task<ApiResponse<GrupoProgramaDto?>> ObtenerPorGrupoPrograma(int grupoId, int programaId) 
        {
            return await _grupoProgramaServicio.ObtenerGrupoProgramaAsync(grupoId, programaId);
        }
    }
}
