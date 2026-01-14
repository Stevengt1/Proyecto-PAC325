using API_PAC3.Models;
using API_PAC3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_PAC3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinpeController : ControllerBase
    {
        private readonly SinpeServices _sinpeServices;

        public SinpeController(SinpeServices sinpeServices)
        {
            _sinpeServices = sinpeServices;
        }

        [Authorize]
        [HttpGet("{telefono}")]
        public async Task<ActionResult<IEnumerable<SinpeModel>>> GetSinpesCaja(string telefono)
        {
            var idComercio = User.FindFirst("IdComercio")?.Value;
            return await _sinpeServices.GetSinpesByTelefono(telefono, idComercio);
        }

        [Authorize]
        [HttpPost("{idSinpe}")]
        public async Task<ActionResult> Sincronizar(int idSinpe)
        {
            bool respuesta = await _sinpeServices.Sincronizar(idSinpe);
            return Ok(new
            {
                EsValido = respuesta,
                Mensaje = respuesta
                            ? "Success: El SINPE se sincronizó correctamente."
                            : "Error: No se pudo sincronizar el SINPE."
            });

        }
    }
}
