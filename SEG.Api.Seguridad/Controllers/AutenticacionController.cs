using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.Servicio.Interfaces;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/autenticacion")]
    public class AutenticacionController : Controller
    {
        private readonly IAutenticacionServicio _autenticacionServicio;
        public AutenticacionController(IAutenticacionServicio autenticacionServicio)
        {
            _autenticacionServicio = autenticacionServicio;
        }

        [HttpPost("autenticarUsuario")]
        public async Task<ActionResult<ApiResponse<string>>> Autenticar(AutenticacionRequest autenticacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _autenticacionServicio.AutenticarUsuarioAsync(autenticacionRequest);
            if (!respuesta.Correcto)
            {
                return Unauthorized(respuesta);
            }

            return respuesta;
        }

        [HttpPost("autenticarSede")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<string>>> Autenticar(int sedeId)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var seleccionSedeResponse = await _autenticacionServicio.AutenticarSedeAsync(sedeId);
            if (!seleccionSedeResponse.Correcto)
                return Unauthorized(seleccionSedeResponse);

            return seleccionSedeResponse;
        }

    }
}
