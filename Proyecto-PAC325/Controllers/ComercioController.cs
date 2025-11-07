using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Controllers
{
    public class ComercioController : Controller
    {

        private readonly ComercioBusiness _comercioBusiness;
        private readonly CajaBusiness _cajaBusiness;

        public ComercioController (ComercioBusiness comercioBusiness, CajaBusiness cajaBusiness)
        {
            _comercioBusiness = comercioBusiness;
            _cajaBusiness = cajaBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _comercioBusiness.GetAllComercio());
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
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
                return RedirectToAction("Registro");
        }

        public async Task<IActionResult> Editar(int idComercio)
        {
            return View(await _comercioBusiness.GetComercio(idComercio));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ComercioModel comercio)
        {
            var result = await _comercioBusiness.Update(comercio);
            if (result == null)
            {
                TempData["error"] = "Ocurrio un error a la hora de actualizar los datos";
                return RedirectToAction("Editar", comercio);
            }
            TempData["success"] = "Los datos fueron editados de manera correcta";
            return RedirectToAction("Editar", result);
        }

        public async Task<IActionResult> Detalle(int idComercio)
        {
            return View(await _comercioBusiness.GetComercio(idComercio));
        }

        public async Task<IActionResult> CajasComercio(int idComercio)
        {
            return View(await _cajaBusiness.GetCajasByComercio(idComercio));
        }
    }
}
