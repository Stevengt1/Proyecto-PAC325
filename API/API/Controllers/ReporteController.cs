using API.Services.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteBusiness _reporteBusiness;

        public ReporteController(ReporteBusiness reporteBusiness)
        {
            _reporteBusiness = reporteBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reportes = await _reporteBusiness.GetAllReportesAsync();
            return Ok(reportes); 
        }

        [HttpPost("generar")]
        public async Task<IActionResult> GenerarReportes([FromBody] DateTime fecha)
        {
            fecha = fecha.Date.AddDays(1).AddTicks(-1);

            await _reporteBusiness.GenerarReportes(fecha);

            return Ok(new { success = $"Reportes generados/actualizados ({fecha:yyyy-MM-dd})." });
        }
    }
}


