using API.Models;
using API.Services.Business;
using API.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SinpeController : ControllerBase
    {
        private readonly SinpeBusiness _sinpeBusiness;
        private readonly CajaRepository _cajaRepository;

        public SinpeController(SinpeBusiness sinpeBusiness, CajaRepository cajaRepository)
        {
            _sinpeBusiness = sinpeBusiness;
            _cajaRepository = cajaRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sinpes = await _sinpeBusiness.ObtenerSinpesAsync();
            return Ok(sinpes);
        }

        [HttpPost("txse")]
        public async Task<IActionResult> TXSE([FromBody] SinpeModel sinpe)
        {
            var caja = await _cajaRepository.GetCaja(sinpe.IdCaja);
            if (caja == null || caja.Estado != 1)
                return BadRequest(new { error = "La caja seleccionada no es válida o está inactiva." });

            sinpe.TelefonoDestinatario = caja.TelefonoSINPE;
            sinpe.NombreDestinatario = caja.Nombre;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (exito, mensaje) = await _sinpeBusiness.HacerSinpeAsync(sinpe);

            if (!exito)
                return StatusCode(500, new { error = mensaje });

            return Ok(new { success = mensaje, sinpe });
        }
        [HttpGet("cajas")]
        public async Task<IActionResult> GetCajasActivas()
        {
            var cajas = await _cajaRepository.ObtenerCajasActivasAsync();
            var cajasJson = cajas.Select(c => new
            {
                c.IdCaja,
                c.Nombre,
                c.TelefonoSINPE
            });

            return Ok(cajasJson);
        }

        [HttpPut("sincronizar/{idSinpe}")]
        public async Task<IActionResult> Sincronizar(int idSinpe)
        {
            var resultado = await _sinpeBusiness.SincronizarSinpe(idSinpe);

            if (!resultado)
                return StatusCode(500, new { error = "Ocurrió un error al realizar la sincronización" });

            return Ok(new { success = "Sincronización realizada con éxito" });
        }
    }
}
