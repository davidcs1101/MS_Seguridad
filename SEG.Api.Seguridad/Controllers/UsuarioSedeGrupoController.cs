using log4net.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using Utilidades;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/usuariosSedesGrupos")]
    public class UsuarioSedeGrupoController : Controller
    {
        private readonly IUsuarioSedeGrupoServicio _usuarioSedeGrupoServicio;
        public UsuarioSedeGrupoController(IUsuarioSedeGrupoServicio usuarioSedeGrupoServicio)
        {
            _usuarioSedeGrupoServicio = usuarioSedeGrupoServicio; 
        }

        [HttpGet("listarPorUsuarioIdLogueado")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<UsuarioSedeGrupoDto>?>>> ListarPorUsuarioIdLogueado()
        {
            var respuesta = await _usuarioSedeGrupoServicio.ListarPorUsuarioIdLogueadoAsync();
            if (!respuesta.Correcto)
                return NotFound(respuesta);

            return respuesta;
        }

        [HttpPost("crear")]
        [Authorize(policy: "UsuarioSedesGruposPermiso")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _usuarioSedeGrupoServicio.CrearAsync(usuarioSedeGrupoCreacionRequest);
            return respuesta;
        }

        [HttpPut("modificar")]
        [Authorize(policy: "UsuarioSedesGruposPermiso")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var respuesta = await _usuarioSedeGrupoServicio.ModificarAsync(usuarioSedeGrupoModificacionRequest);
            return respuesta;
        }

        [HttpDelete("eliminar")]
        [Authorize(policy: "UsuarioSedesGruposPermiso")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id) 
        {
            var respuesta = await _usuarioSedeGrupoServicio.EliminarAsync(id);
            return respuesta;
        }
    }
}
