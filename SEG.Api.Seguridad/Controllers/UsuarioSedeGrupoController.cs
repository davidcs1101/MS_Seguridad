using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Api.Seguridad.Middlewares.Permisos;
using Utilidades.Seguridad;
using Utilidades.Dtos;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/usuariosSedesGrupos")]
    [Authorize]
    public class UsuarioSedeGrupoController : Controller
    {
        private readonly IUsuarioSedeGrupoServicio _usuarioSedeGrupoServicio;
        public UsuarioSedeGrupoController(IUsuarioSedeGrupoServicio usuarioSedeGrupoServicio)
        {
            _usuarioSedeGrupoServicio = usuarioSedeGrupoServicio; 
        }

        [HttpGet("listarPorUsuarioIdLogueado")]
        public async Task<ActionResult<ApiResponseDto<List<UsuarioSedeGrupoDto>?>>> ListarPorUsuarioIdLogueado()
        {
            return await _usuarioSedeGrupoServicio.ListarPorUsuarioIdLogueadoAsync();
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.UsuariosSedesGrupos.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacionRequest) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioSedeGrupoServicio.CrearAsync(usuarioSedeGrupoCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.UsuariosSedesGrupos.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _usuarioSedeGrupoServicio.ModificarAsync(usuarioSedeGrupoModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.UsuariosSedesGrupos.ELIMINAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id) 
        {
            return await _usuarioSedeGrupoServicio.EliminarAsync(id);
        }
    }
}
