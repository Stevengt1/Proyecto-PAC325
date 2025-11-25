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
        public async Task<IActionResult> GenerarReportes(int? year, int? month)
        {
            var now = DateTime.Now;
            var target = new DateTime(year ?? now.Year, month ?? now.Month, 1);
            await _reporteBusiness.GenerarReportesPorMesAsync(target);
            TempData["success"] = $"Reportes generados/actualizados ({target:yyyy-MM}).";
            return RedirectToAction("Index");
        }
        


    }
}
