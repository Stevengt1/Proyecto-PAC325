using Microsoft.AspNetCore.Mvc;
using WebSimuladaAPI.Models;
using System.Threading.Tasks;

namespace WebSimuladaAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string clientName = "Api";

        public AuthController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(int idComercio)
        {
            var cliente = _clientFactory.CreateClient(clientName);
            var data = new { idComercio = idComercio };
            var respuesta = await cliente.PostAsync($"auth?idComercio={idComercio}", null);


            if (!respuesta.IsSuccessStatusCode)
            {
                TempData["error"] = "Error: el comercio no esta registrado o no posee configuraciones externas";
                return RedirectToAction("Index");
            }
            var result = await respuesta.Content.ReadFromJsonAsync<TokenResponse>();
            HttpContext.Session.SetString("JWT_TOKEN", result.token);

            TempData["success"] = "Se inició sesión correctamente en la plataforma";
            return RedirectToAction("Index", "Home");
        }
    }
}
