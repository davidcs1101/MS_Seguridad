using log4net.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEG.Api.Seguridad.Infraestructura;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using Utilidades;

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

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> Crear(UsuarioCreacionRequest usuarioCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _usuarioServicio.CrearAsync(usuarioCreacionRequest);
            return respuesta;
        }

        [HttpPut("modificarClave")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> ModificarClave(string clave)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _usuarioServicio.ModificarClaveAsync(clave);
            if (!respuesta.Correcto)
                return NotFound(respuesta);

            return respuesta;
        }

        [HttpPut("restablecerClave")]
        public async Task<ActionResult<ApiResponse<UsuarioOtrosDatosDto>>> RestablecerClave(string nombreUsuario)
        {
            var respuesta = await _usuarioServicio.RestablecerClavePorUsuarioAsync(nombreUsuario);
            if (!respuesta.Correcto)
                return NotFound(respuesta);

            return respuesta;
        }

        [HttpPut("modificarEmail")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<string>>> ModificarEmail(string email) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _usuarioServicio.ModificarEmailAsync(email);
            return respuesta;
        }

        [HttpGet("obtenerNombreUsuarioPorId")]
        [Authorize]//VALIDAR POSTERIORMENTE SI SE REQUIERE UN PERMISO ESPECIFICO
        public async Task<ActionResult<ApiResponse<string>>> ObtenerNombreUsuarioPorIdAsync(int id) 
        {
            var respuesta = await _usuarioServicio.ObtenerNombreUsuarioPorIdAsync(id);
            return respuesta;
        }

        [HttpPost("listar")]
        [Authorize]//VALIDAR POSTERIORMENTE SI SE REQUIERE UN PERMISO ESPECIFICO
        public async Task<ActionResult<ApiResponse<List<UsuarioDto>?>>> ListarAsync(IdsListadoDto ids)
        {
            var usuarios = await _usuarioServicio.ListarAsync(ids);
            return usuarios;
        }

    }
}
