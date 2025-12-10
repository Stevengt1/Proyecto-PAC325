using API.Models;
using API.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajaController : ControllerBase
    {
        private readonly CajaBusiness _cajaBusiness;
        private readonly ComercioBusiness _comercioBusiness;
        private readonly SinpeBusiness _sinpeBusiness;

        public CajaController(CajaBusiness cajaBusiness, ComercioBusiness comercioBusiness, SinpeBusiness sinpeBusiness)
        {
            _cajaBusiness = cajaBusiness;
            _comercioBusiness = comercioBusiness;
            _sinpeBusiness = sinpeBusiness;
        }

        [HttpGet("{idComercio?}")]
        public async Task<IActionResult> GetCajas(int? idComercio)
        {
            if (idComercio.HasValue && idComercio.Value > 0)
            {
                var cajasPorComercio = await _cajaBusiness.GetCajasByComercio(idComercio.Value);
                return Ok(cajasPorComercio);
            }

            var todas = await _cajaBusiness.GetAllCajas();
            return Ok(todas);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CajaModel caja)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comercio = await _comercioBusiness.GetComercio(caja.IdComercio);
            if (comercio == null)
                return NotFound(new { error = "El comercio seleccionado no existe." });

            CajaModel caja_temp = await _cajaBusiness.Add(caja);

            if (caja_temp == null)
                return StatusCode(500, new { error = "Error al insertar el registro en la base de datos" });

            if (caja_temp.IdCaja == -1)
                return Conflict(new { error = "Ya existe una caja con ese nombre para el comercio seleccionado" });

            if (caja_temp.IdCaja == -2)
                return Conflict(new { error = "Ya existe una caja activa con ese teléfono SINPE" });

            return Ok(new { success = "La caja fue guardada con éxito", caja = caja_temp });
        }

        [HttpPut("{idCaja}")]
        public async Task<IActionResult> Update(int idCaja, [FromBody] CajaModel caja)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comercio = await _comercioBusiness.GetComercio(caja.IdComercio);
            if (comercio == null)
                return NotFound(new { error = "El comercio seleccionado no existe." });

            CajaModel result = await _cajaBusiness.Update(caja);

            if (result == null)
                return StatusCode(500, new { error = "Error al actualizar los datos" });

            if (result.IdCaja == -1)
                return Conflict(new { error = "El nombre de la caja ya está siendo utilizado para este comercio" });

            if (result.IdCaja == -2)
                return Conflict(new { error = "Ya existe otra caja activa con ese teléfono SINPE" });

            return Ok(new { success = "Los datos fueron editados correctamente", caja = result });
        }

        [HttpGet("detalle/{idCaja}")]
        public async Task<IActionResult> Detalle(int idCaja)
        {
            var model = await _cajaBusiness.GetCaja(idCaja);
            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpGet("sinpes/{telefono}")]
        public async Task<IActionResult> SinpesPorCaja(string telefono)
        {
            var sinpes = await _sinpeBusiness.GetSinpesByTelefono(telefono);
            return Ok(sinpes);
        }
    }
}

