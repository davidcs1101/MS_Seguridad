using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.Aplicacion.Servicios.Implementaciones;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Dtos;

namespace SEG.Api.Seguridad.Controllers
{
    [ApiController]
    [Route("api/cache")]
    [Authorize]
    public class CacheController : Controller
    {
        private readonly IDatosComunesListasCache _datosComunesListasCache;

        public CacheController(IDatosComunesListasCache datosComunesListasCache)
        {
            _datosComunesListasCache = datosComunesListasCache;
        }

        [HttpPost("actualizarDatosComunesListas")]
        public ActionResult<ApiResponse<string>> ActualizarDatosComunesListas(List<ListaDetalleDto> listaDetalle)
        {
            if (listaDetalle == null)
                return BadRequest();

            return _datosComunesListasCache.Actualizar(listaDetalle);
        }
    }
}
