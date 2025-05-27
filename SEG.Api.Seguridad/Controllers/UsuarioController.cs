using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;
        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> Registrar(UsuarioCreacionRequest usuarioCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.RegistrarAsync(usuarioCreacionRequest);
        }

        [HttpPost("crear")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> Crear(UsuarioCreacionRequest usuarioCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.CrearAsync(usuarioCreacionRequest);
        }

        [HttpPut("modificarClave")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> ModificarClave(string clave)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.ModificarClaveAsync(clave);
        }

        [HttpPut("restablecerClave")]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> RestablecerClave(string nombreUsuario)
        {
            return await _usuarioServicio.RestablecerClavePorUsuarioAsync(nombreUsuario);
        }

        [HttpPut("modificarEmail")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<string>>> ModificarEmail(string email) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _usuarioServicio.ModificarEmailAsync(email);
        }

        [HttpGet("obtenerNombreUsuarioPorId")]
        [Authorize]//VALIDAR POSTERIORMENTE SI SE REQUIERE UN PERMISO ESPECIFICO
        public async Task<ActionResult<ApiResponse<string>>> ObtenerNombreUsuarioPorIdAsync(int id) 
        {
            return await _usuarioServicio.ObtenerNombreUsuarioPorIdAsync(id);
        }

        [HttpPost("listar")]
        [Authorize]//VALIDAR POSTERIORMENTE SI SE REQUIERE UN PERMISO ESPECIFICO
        public async Task<ActionResult<ApiResponse<List<UsuarioDto>?>>> ListarAsync(IdsListadoDto ids)
        {
            return await _usuarioServicio.ListarAsync(ids);
        }

    }
}
