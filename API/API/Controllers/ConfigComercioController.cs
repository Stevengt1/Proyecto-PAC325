using API.Models;
using API.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigComercioController : ControllerBase
    {
        private readonly ConfigComercioBusiness _configComercioBusiness;
        private readonly ComercioBusiness _comercioBusiness;

        public ConfigComercioController(ConfigComercioBusiness configComercioBusiness, ComercioBusiness comercioBusiness)
        {
            _configComercioBusiness = configComercioBusiness;
            _comercioBusiness = comercioBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var configuraciones = await _configComercioBusiness.GetConfiguraciones();
            return Ok(configuraciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var config = await _configComercioBusiness.GetConfiguracion(id);
            if (config == null) return NotFound(new { error = "La configuración no existe" });

            return Ok(config);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ConfigComercioModel config)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var config_temp = await _configComercioBusiness.Add(config);

            if (config_temp == null)
                return StatusCode(500, new { error = "Error al insertar el registro en la base de datos" });

            if (config_temp.IdConfiguracion == -1)
                return Conflict(new { error = "El comercio ya presenta una configuración, edita la ya registrada" });

            return Ok(new { success = "El comercio fue guardado con éxito", config = config_temp });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ConfigComercioModel config)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _configComercioBusiness.Update(config);

            if (result == null)
                return StatusCode(500, new { error = "Error al actualizar los datos" });

            return Ok(new { success = "Los datos fueron editados correctamente", config = result });
        }
    }
}

