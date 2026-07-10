using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Api.Seguridad.Middlewares.Permisos;
using Utilidades.Seguridad;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;
        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("crear")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> Crear(UsuarioCreacionRequest usuarioCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.CrearAsync(usuarioCreacionRequest);
        }

        [HttpPost("crearConGrupo")]
        [Permiso(Permisos.Usuarios.CREARCONGRUPO)]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> CrearConGrupo(UsuarioConGrupoCreacionRequest usuarioConGrupoCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.CrearAsync(usuarioConGrupoCreacionRequest);
        }

        [HttpPost("crearConSede")]
        [Permiso(Permisos.Usuarios.CREARCONSEDE)]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> CrearConSede(UsuarioSedeCreacionRequest usuarioSedeCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.CrearConSedeAsync(usuarioSedeCreacionRequest);
        }

        [HttpPut("modificarClave")]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> ModificarClave(string clave)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.ModificarClaveAsync(clave);
        }

        [HttpPut("restablecerClave")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> RestablecerClave(string nombreUsuario)
        {
            return await _usuarioServicio.RestablecerClavePorUsuarioAsync(nombreUsuario);
        }

        [HttpPut("modificarEmail")]
        public async Task<ActionResult<ApiResponse<string>>> ModificarEmail(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.ModificarEmailAsync(email);
        }

        [HttpGet("obtenerNombreUsuarioPorId")]
        [Permiso(Permisos.Usuarios.CONSULTAR)]
        public async Task<ActionResult<ApiResponse<string>>> ObtenerNombreUsuarioPorIdAsync(int id)
        {
            return await _usuarioServicio.ObtenerNombreUsuarioPorIdAsync(id);
        }

        [HttpPost("listar")]
        [Permiso(Permisos.Usuarios.LISTAR)]
        public async Task<ActionResult<ApiResponse<List<UsuarioDto>?>>> ListarAsync(IdsListadoDto ids)
        {
            return await _usuarioServicio.ListarAsync(ids);
        }

    }
}
