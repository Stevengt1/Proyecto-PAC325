using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Controllers
{
    public class SinpeController : Controller
    {
        private readonly SinpeBusiness _sinpeBusiness;
        public SinpeController(SinpeBusiness sinpeBusiness)
        {
            _sinpeBusiness = sinpeBusiness;
        }
        public async Task<IActionResult> Index()
        {
            var sinpes = await _sinpeBusiness.ObtenerSinpesAsync();
            return View(sinpes);
        }
        [HttpGet]
        public IActionResult TXSE()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TXSE(SinpeModel sinpe)
        {
            
            if (!ModelState.IsValid) {
                return View(sinpe);
            }
            try
            {
                await _sinpeBusiness.HacerSinpeAsync(sinpe);
                TempData["Exito"] = "Sinpe realizado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(sinpe);
            }

        }
    }
}