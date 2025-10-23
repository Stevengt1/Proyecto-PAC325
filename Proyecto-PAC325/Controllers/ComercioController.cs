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
            ComercioModel comercio_temp = await _comercioBusiness.Add(comercio);
            if (comercio_temp == null)
            {
                TempData["error"] = "Ocurrio un error a la hora de insertar el registro en la base de datos";
            }else if (comercio_temp.IdComercio == -1)
            {
                TempData["error"] = "El identificador del comercio ya esta siendo utilizado";
            }
            else
            {
                TempData["success"] = "El comercio fue guardado con exito";
            }
                return RedirectToAction("Index");
        }
    }
}
