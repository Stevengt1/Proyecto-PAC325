using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Controllers
{
    public class ComercioController : Controller
    {

        private readonly ComercioBusiness _comercioBusiness;

        public ComercioController (ComercioBusiness comercioBusiness)
        {
            _comercioBusiness = comercioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _comercioBusiness.GetAllComercio());
        }

        public IActionResult Registro()
        {
            return View();
        }

        public async Task<IActionResult> Add(ComercioModel comercio)
        {
            if (await _comercioBusiness.Add(comercio) == null)
            {
                return RedirectToAction("Registro");
            }
            return RedirectToAction("Index");
        }
    }
}
