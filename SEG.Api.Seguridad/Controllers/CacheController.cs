using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Dtos;
using Utilidades.Seguridad;
using Utilidades.Dtos;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/cache")]
    [Authorize(Policy = Politicas.Sistema)]
    public class CacheController : Controller
    {
        private readonly ISeguridadPermisosCache _seguridadPermisosCache;

        public CacheController(ISeguridadPermisosCache seguridadPermisosCache)
        {
            _seguridadPermisosCache = seguridadPermisosCache;
        }

        //[HttpPost("actualizarDatosComunesListas")]
        //public ActionResult<ApiResponseDto<string>> ActualizarDatosComunesListas(List<ListaDetalleDto> listaDetalle)
        //{
        //    if (listaDetalle == null)
        //        return BadRequest();

        //    return _datosComunesListasCache.Actualizar(listaDetalle);
        //}

        [HttpPost("refrescarPermisos")]
        public async Task<IActionResult> RefrescarPermisos()
        {
            await _seguridadPermisosCache.RefrescarAsync();
            return Ok();
        }
    }
}
