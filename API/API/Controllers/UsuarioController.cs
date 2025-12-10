using API.Models;
using API.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioBusiness _usuarioBusiness;
        private readonly ComercioBusiness _comercioBusiness;

        public UsuarioController(UsuarioBusiness usuarioBusiness, ComercioBusiness comercioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
            _comercioBusiness = comercioBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioBusiness.GetAllUsuarios();
            return Ok(usuarios); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioBusiness.GetUsuario(id);
            if (usuario == null) return NotFound(new { error = "Usuario no encontrado" });

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UsuarioModel usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _usuarioBusiness.Add(usuario);

            if (result == null)
                return Conflict(new { error = "Ya existe un usuario con esa identificación o ocurrió un error." });

            return Ok(new { success = "Usuario registrado correctamente.", usuario = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioModel usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _usuarioBusiness.Update(usuario);

            if (result == null)
                return StatusCode(500, new { error = "Ocurrió un error al editar el usuario." });

            return Ok(new { success = "Usuario editado correctamente.", usuario = result });
        }
    }
}

