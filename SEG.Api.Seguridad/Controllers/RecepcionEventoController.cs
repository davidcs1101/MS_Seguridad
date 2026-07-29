using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Aplicacion.CasosUso.Interfaces;
using Utilidades.Dtos;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/recepcionEventos")]
    [Authorize]
    public class RecepcionEventoController : Controller
    {
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        public RecepcionEventoController(IColaSolicitudServicio colaSolicitudServicio)
        {
            _colaSolicitudServicio = colaSolicitudServicio; 
        }

        [HttpPost("recibirEvento")]
        //[Permiso(CodigosPermisos.Acciones.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> RecibirEvento(ColaSolicitudCreacionRequest colaSolicitudCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _colaSolicitudServicio.CrearAsync(colaSolicitudCreacionRequest);
        }
    }
}
