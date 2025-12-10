using API.Models;
using API.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComercioController : ControllerBase
    {
        private readonly ComercioBusiness _comercioBusiness;
        private readonly CajaBusiness _cajaBusiness;

        public ComercioController(ComercioBusiness comercioBusiness, CajaBusiness cajaBusiness)
        {
            _comercioBusiness = comercioBusiness;
            _cajaBusiness = cajaBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comercios = await _comercioBusiness.GetAllComercio();
            return Ok(comercios); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comercio = await _comercioBusiness.GetComercio(id);
            if (comercio == null) return NotFound(new { error = "El comercio no existe" });

            return Ok(comercio);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ComercioModel comercio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comercio_temp = await _comercioBusiness.Add(comercio);

            if (comercio_temp == null)
                return StatusCode(500, new { error = "Error al insertar el registro en la base de datos" });

            if (comercio_temp.IdComercio == -1)
                return Conflict(new { error = "El identificador del comercio ya está siendo utilizado" });

            return Ok(new { success = "El comercio fue guardado con éxito", comercio = comercio_temp });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComercioModel comercio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _comercioBusiness.Update(comercio);

            if (result == null)
                return StatusCode(500, new { error = "Error al actualizar los datos" });

            return Ok(new { success = "Los datos fueron editados correctamente", comercio = result });
        }

        [HttpGet("{id}/cajas")]
        public async Task<IActionResult> GetCajasByComercio(int id)
        {
            var cajas = await _cajaBusiness.GetCajasByComercio(id);
            return Ok(cajas);
        }
    }
}

