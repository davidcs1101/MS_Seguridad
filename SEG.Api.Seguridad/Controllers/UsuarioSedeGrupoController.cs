using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;

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
            return await _usuarioSedeGrupoServicio.ListarPorUsuarioIdLogueadoAsync();
        }

        [HttpPost("crear")]
        [Authorize(policy: "UsuarioSedesGruposPermiso")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioSedeGrupoServicio.CrearAsync(usuarioSedeGrupoCreacionRequest);
        }

        [HttpPut("modificar")]
        [Authorize(policy: "UsuarioSedesGruposPermiso")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _usuarioSedeGrupoServicio.ModificarAsync(usuarioSedeGrupoModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Authorize(policy: "UsuarioSedesGruposPermiso")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id) 
        {
            return await _usuarioSedeGrupoServicio.EliminarAsync(id);
        }
    }
}
