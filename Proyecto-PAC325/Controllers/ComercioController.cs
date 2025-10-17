using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;

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
    }
}
