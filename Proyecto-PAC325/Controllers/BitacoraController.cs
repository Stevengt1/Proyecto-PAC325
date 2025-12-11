using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;

namespace Proyecto_PAC325.Controllers
{
    public class BitacoraController : Controller
    {

        private readonly BitacoraBusiness _bitacora;

        public BitacoraController(BitacoraBusiness bitacora)
        {
            _bitacora = bitacora;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _bitacora.GetBitacoras());
        }
    }
}
