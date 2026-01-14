using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_PAC325.Business;
using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Controllers
{
    public class SinpeController : Controller
    {
        private readonly SinpeBusiness _sinpeBusiness;
        private readonly CajaRepository _cajaRepository;

        public SinpeController(SinpeBusiness sinpeBusiness, CajaRepository cajaRepository)
        {
            _sinpeBusiness = sinpeBusiness;
            _cajaRepository = cajaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sinpes = await _sinpeBusiness.ObtenerSinpesAsync();
            return View(sinpes);
        }

        [HttpGet]
        public async Task<IActionResult> TXSE()
        {
            await CargarCajasAsync();
            return View(new SinpeModel());
        }

        [HttpPost]
        public async Task<IActionResult> TXSE(SinpeModel sinpe)
        {
            var caja = await _cajaRepository.GetCaja(sinpe.IdCaja);
            if (caja == null || caja.Estado != 1)
            {
                ModelState.AddModelError("IdCaja", "La caja seleccionada no es válida o está inactiva.");
                await CargarCajasAsync();
                return View(sinpe);
            }

            // Asignar los valores del destinatario con anterioridad
            sinpe.TelefonoDestinatario = caja.TelefonoSINPE;
            sinpe.NombreDestinatario = caja.Nombre;

            // Remover Telefono y Nombre del ModelState porque tomamos estos valores de otra tabla
            ModelState.Remove(nameof(sinpe.TelefonoDestinatario));
            ModelState.Remove(nameof(sinpe.NombreDestinatario));

            if (!ModelState.IsValid)
            {
                await CargarCajasAsync();
               

                return View(sinpe);
            }

            var (exito, mensaje) = await _sinpeBusiness.HacerSinpeAsync(sinpe);

            if (!exito)
            {
                ModelState.AddModelError("", mensaje);
                await CargarCajasAsync();
                return View(sinpe);
            }

            TempData["success"] = mensaje;
            return RedirectToAction("Index");
        }
        private async Task CargarCajasAsync()
        {
            var cajas = await _cajaRepository.ObtenerCajasActivasAsync();

            ViewBag.Cajas = new SelectList(cajas, "IdCaja", "Nombre"); // Esto sirve para jalar info de cajas

            ViewBag.CajasJson = System.Text.Json.JsonSerializer.Serialize(cajas.Select(c => new {
                c.IdCaja,
                c.Nombre,
                c.TelefonoSINPE
            }));
        }

        public async Task<IActionResult> Sincronizar(int idSinpe)
        {
            if (await _sinpeBusiness.SincronizarSinpe(idSinpe))
            {
                TempData["success"] = "Sincronización realizada con exito";
                return RedirectToAction("Index", "Caja");
            }
            else
            {
                TempData["error"] = "Ocurrio un error al realizar la sincronización";
                return RedirectToAction("Index", "Caja");
            }
        }

    }
}
