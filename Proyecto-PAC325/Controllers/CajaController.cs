using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;


namespace Proyecto_PAC325.Controllers
{
    public class CajaController : Controller
    {

        private readonly CajaBusiness _cajaBusiness;

        public CajaController(CajaBusiness cajaBusiness)
        {
            _cajaBusiness = cajaBusiness;
        }
        //listar
        public async Task<IActionResult> Index(int idComercio)
        {
            return View(await _cajaBusiness.GetCajasByComercio(idComercio));
        }
        //
        public IActionResult Registro(int idComercio)
        {
            var model = new CajaModel { IdComercio = idComercio, Estado = 1 };
            return View(model);
        }
        //registro post
        public async Task<IActionResult> Add(CajaModel caja)
        {
            CajaModel caja_temp = await _cajaBusiness.Registro(caja);
            if (caja_temp == null)
            {
                TempData["error"] = "no se pude insertar el registro";
            }
            else if (caja_temp.IdCaja == -1)
            {
                TempData["error"] = "el nombre de la caja esta siendo utilizado por otro comercio";
            }
            else if (caja_temp.IdCaja == -2)
            {
                TempData["error"] = "ya existe una caja activa con ese telefono SINPE";
            }
            else
            {
                TempData["success"] = "La caja fue guardada con exito";
            }

            return RedirectToAction("Registro", new { idComercio = caja.IdComercio });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _cajaBusiness.GetCaja(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CajaModel model)
        {
            var result = await _cajaBusiness.Editar(model);
            if (result == null)
            {
                TempData["error"] = "Ocurrio un error al actualizar la caja";
                return View(model);
            }
            else if (result.IdCaja == -1)
            {
                TempData["error"] = "El nombre de la caja ya esta siendo utilizado para este comercio";
                return View(model);
            }
            else if (result.IdCaja == -2)
            {
                TempData["error"] = "Ya existe otra caja activa con ese telefono SINPE";
                return View(model);
            }

            TempData["success"] = "Caja actualizada correctamente";
            return RedirectToAction("Index", new { idComercio = model.IdComercio });
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _cajaBusiness.GetCaja(id);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
