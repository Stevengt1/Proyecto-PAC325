using Microsoft.AspNetCore.Mvc;
using Proyecto_PAC325.Business;

namespace Proyecto_PAC325.Controllers
{
    public class ReporteController : Controller
    {

        private readonly ReporteBusiness _reporteBusiness;

        public ReporteController(ReporteBusiness reporteBusiness)
        {
            _reporteBusiness = reporteBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var reportes = await _reporteBusiness.GetAllReportesAsync();
            return View(reportes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerarReportes(DateTime fecha)
        {
            fecha = fecha.Date.AddDays(1).AddTicks(-1); //Con esto hacemos que la fecha le agreguemos un día pero al quitarle un tick pasa al fin exacto
            //del día actual
            await _reporteBusiness.GenerarReportes(fecha);
            TempData["success"] = $"Reportes generados/actualizados ({fecha:yyyy-MM-dd}).";
            return RedirectToAction("Index");
        }
        


    }
}
