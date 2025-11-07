using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Dtos;
using SEG.Aplicacion.CasosUso.Interfaces;

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
        public async Task<ActionResult<ApiResponse<AutenticacionResponse>>> Autenticar(AutenticacionRequest autenticacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _autenticacionServicio.AutenticarUsuarioAsync(autenticacionRequest);
        }

        [HttpPost("autenticarSede")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<AutenticacionResponse>>> Autenticar(int sedeId)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return await _autenticacionServicio.AutenticarSedeAsync(sedeId);
        }

    }
}
