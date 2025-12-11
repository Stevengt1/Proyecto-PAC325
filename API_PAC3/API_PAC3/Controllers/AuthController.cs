using API_PAC3.Models;
using API_PAC3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_PAC3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ComercioServices _comercioServices;
        private readonly ConfigComercioServices _configComercioServices;

        public AuthController(IConfiguration config, ComercioServices comercioServices, ConfigComercioServices configComercioServices)
        {
            _config = config;
            _comercioServices = comercioServices;
            _configComercioServices = configComercioServices;
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerToken(ComercioModel comercio)
        {
            comercio = await _comercioServices.GetComercio(comercio.IdComercio);
            if(comercio == null)
            {
                return Unauthorized();
            }
            ConfigComercioModel configuracion = await _configComercioServices.GetConfiguracionPorComercio(comercio.IdComercio);
            if (configuracion == null || configuracion.TipoConfiguracion == 1)
            {
                return Unauthorized();
            }

            var token = GenerarJwt(comercio);
            return Ok(new { token });
        }

        private string GenerarJwt(ComercioModel comercio)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("IdComercio", comercio.IdComercio.ToString()),
            new Claim("Nombre", comercio.Nombre)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
