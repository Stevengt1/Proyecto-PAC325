using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Repository;
using System.Globalization;

namespace Proyecto_PAC325.Controllers
{
    public class AuthController : Controller
    {

        private readonly AuthBusiness _authBusiness;

        public AuthController(AuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string correo, string password, string rol)
        {

            string mensaje = await _authBusiness.Register(correo,password,rol);
            if (mensaje.Contains("Error"))
            {
                TempData["error"] = mensaje;
                return RedirectToAction("Registro");
            }
            TempData["success"] = mensaje;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {
            string mensaje = await _authBusiness.Login(correo, password);
            if (mensaje.Contains("Error"))
            {
                TempData["error"] = mensaje;
                return RedirectToAction("Index");
            }
            TempData["success"] = mensaje;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authBusiness.Logout();
            return RedirectToAction("Login", "Auth");
        }


    }
}
