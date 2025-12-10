using API.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {
        private readonly BitacoraBusiness _bitacora;

        public BitacoraController(BitacoraBusiness bitacora)
        {
            _bitacora = bitacora;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bitacoras = await _bitacora.GetBitacoras();
            return Ok(bitacoras); 
        }
    }
}

