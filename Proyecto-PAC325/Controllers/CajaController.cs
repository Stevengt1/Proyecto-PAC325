using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Controllers
{
    public class CajaController : Controller
    {
        private readonly CajaBusiness _cajaBusiness;
        private readonly ComercioBusiness _comercioBusiness;
        private readonly SinpeBusiness _sinpeBusiness;


        public CajaController(CajaBusiness cajaBusiness, ComercioBusiness comercioBusiness, SinpeBusiness sinpeBusiness)
        {
            _cajaBusiness = cajaBusiness;
            _comercioBusiness = comercioBusiness;
            _sinpeBusiness = sinpeBusiness;

        }

        public async Task<IActionResult> Index(int? idComercio)
        {
            ViewBag.IdComercio = idComercio ?? 0;

            if (idComercio.HasValue && idComercio.Value > 0)
            {
                var cajasPorComercio = await _cajaBusiness.GetCajasByComercio(idComercio.Value);
                return View(cajasPorComercio);
            }

            var todas = await _cajaBusiness.GetAllCajas();
            return View(todas);
        }

        public async Task<IActionResult> Registro(int idComercio = 0)
        {
            var model = new CajaModel { IdComercio = idComercio, Estado = 1 };

            var comercios = await _comercioBusiness.GetAllComercio();
            ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", idComercio);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CajaModel caja)
        {
            if (!ModelState.IsValid)
            {
                var listInvalid = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listInvalid, "IdComercio", "Nombre", caja.IdComercio);
                return View("Registro", caja);
            }

            var comercio = await _comercioBusiness.GetComercio(caja.IdComercio);
            if (comercio == null)
            {
                TempData["error"] = "El comercio seleccionado no existe.";
                var listFk = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listFk, "IdComercio", "Nombre", caja.IdComercio);
                return View("Registro", caja);
            }

            CajaModel caja_temp = await _cajaBusiness.Add(caja);

            if (caja_temp == null)
            {
                TempData["error"] = "Ocurrió un error al insertar el registro en la base de datos";
                var listErr = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listErr, "IdComercio", "Nombre", caja.IdComercio);
                return View("Registro", caja);
            }
            else if (caja_temp.IdCaja == -1)
            {
                TempData["error"] = "Ya existe una caja con ese nombre para el comercio seleccionado";
                var listName = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listName, "IdComercio", "Nombre", caja.IdComercio);
                return View("Registro", caja);
            }
            else if (caja_temp.IdCaja == -2)
            {
                TempData["error"] = "Ya existe una caja activa con ese teléfono SINPE";
                var listPhone = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listPhone, "IdComercio", "Nombre", caja.IdComercio);
                return View("Registro", caja);
            }

            TempData["success"] = "La caja fue guardada con éxito";
            return RedirectToAction("Index", new { idComercio = caja.IdComercio });
        }

        public async Task<IActionResult> Editar(int idCaja)
        {
            var model = await _cajaBusiness.GetCaja(idCaja);
            if (model == null) return NotFound();

            var comercios = await _comercioBusiness.GetAllComercio();
            ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", model.IdComercio);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CajaModel caja)
        {
            if (!ModelState.IsValid)
            {
                var list = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(list, "IdComercio", "Nombre", caja.IdComercio);
                return View("Editar", caja);
            }

            var comercio = await _comercioBusiness.GetComercio(caja.IdComercio);
            if (comercio == null)
            {
                TempData["error"] = "El comercio seleccionado no existe.";
                var listFk = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listFk, "IdComercio", "Nombre", caja.IdComercio);
                return View("Editar", caja);
            }

            CajaModel result = await _cajaBusiness.Update(caja);

            if (result == null)
            {
                TempData["error"] = "Ocurrió un error al actualizar los datos";
                var listErr = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listErr, "IdComercio", "Nombre", caja.IdComercio);
                return View("Editar", caja);
            }
            else if (result.IdCaja == -1)
            {
                TempData["error"] = "El nombre de la caja ya está siendo utilizado para este comercio";
                var listName = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listName, "IdComercio", "Nombre", caja.IdComercio);
                return View("Editar", caja);
            }
            else if (result.IdCaja == -2)
            {
                TempData["error"] = "Ya existe otra caja activa con ese teléfono SINPE";
                var listPhone = await _comercioBusiness.GetAllComercio();
                ViewBag.Comercios = new SelectList(listPhone, "IdComercio", "Nombre", caja.IdComercio);
                return View("Editar", caja);
            }

            TempData["success"] = "Los datos fueron editados de manera correcta";
            return RedirectToAction("Editar", new { idCaja = result.IdCaja });
        }

        public async Task<IActionResult> Detalle(int idCaja)
        {
            return View(await _cajaBusiness.GetCaja(idCaja));
        }


        [HttpGet]
        public async Task<IActionResult> SinpesPorCaja(string telefono)
        {
            return View( await _sinpeBusiness.GetSinpesByTelefono(telefono));
        }
    }
}