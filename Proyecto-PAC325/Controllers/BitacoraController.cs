using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_PAC325.Business;
using System.Net.Http;

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
