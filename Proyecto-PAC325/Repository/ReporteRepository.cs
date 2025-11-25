using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;
namespace Proyecto_PAC325.Repository
{
    public class ReporteRepository
    {
        private readonly AppDbContext _context;
        private readonly BitacoraRepository _bitacora;

        public ReporteRepository(AppDbContext context, BitacoraRepository bitacora)
        {
            _context = context;
            _bitacora = bitacora;
        }

        //obtiene los reportes para pasarlos a la vista
        public async Task<List<ReporteMensualModel>> GetAllReportesAsync()
        {
            return await _context.REPORTES_MENSUALES
               .OrderByDescending(r => r.FechaDelReporte)
               .ToListAsync();
        }

        // Generay actualiza reportes para el mes indicado
        // Genera/actualiza reportes para el mes indicado (monthDate -> se usa el primer día del mes)
        public async Task ReportesPorMesAsync(DateTime monthDate, Func<int, Task<decimal>> obtenerPorcentajeComisionAsync)
        {
            var monthStart = new DateTime(monthDate.Year, monthDate.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            //obtener sinpes por comercio y agruparlos
            var agrupados = await (
                from caja in _context.CAJAS
                join sinpe in _context.SINPE on caja.TelefonoSINPE equals sinpe.TelefonoDestinatario
                where sinpe.FechaDeRegistro >= monthStart && sinpe.FechaDeRegistro < monthEnd
                group sinpe by caja.IdComercio into g
                select new
                {
                    IdComercio = g.Key,
                    CantidadDeSINPES = g.Count(),
                    MontoTotalRecaudado = g.Sum(x => x.Monto)
                }).ToListAsync();

            //obtener todos los comercios
            var comercios = await _context.COMERCIOS.ToListAsync();

            foreach (var comercio in comercios)
            {
                var agreg = agrupados.FirstOrDefault(a => a.IdComercio == comercio.IdComercio);
                var cantidadSinpes = agreg?.CantidadDeSINPES ?? 0;
                var montoRecaudado = agreg?.MontoTotalRecaudado ?? 0m;

                //cantidad de cajas del comercio
                var cantidadCajas = await _context.CAJAS.CountAsync(c => c.IdComercio == comercio.IdComercio);

                //obtener porcentaje de comisión (usa el callback para que la lógica quede fuera del repo)
                decimal porcentaje = await obtenerPorcentajeComisionAsync(comercio.IdComercio); // devuelve 20 para 20%
                var montoComision = Math.Round(montoRecaudado * (porcentaje / 100m), 2);

                // Upsert/busca si ya existe reporte para ese comercio y mes
                var existing = await _context.REPORTES_MENSUALES
                    .FirstOrDefaultAsync(r => r.IdComercio == comercio.IdComercio && r.FechaDelReporte == monthStart);

                //datos
                if (existing == null)
                {
                    var nuevo = new ReporteMensualModel
                    {
                        IdComercio = comercio.IdComercio,
                        CantidadDeCajas = cantidadCajas,
                        MontoTotalRecaudado = montoRecaudado,
                        CantidadDeSINPES = cantidadSinpes,
                        MontoTotalComision = montoComision,
                        FechaDelReporte = monthStart
                    };
                    _context.REPORTES_MENSUALES.Add(nuevo);
                }
                else
                {
                    existing.CantidadDeCajas = cantidadCajas;
                    existing.MontoTotalRecaudado = montoRecaudado;
                    existing.CantidadDeSINPES = cantidadSinpes;
                    existing.MontoTotalComision = montoComision;
                }
            }

            await _context.SaveChangesAsync();
            await _bitacora.RegistrarEvento("REPORTE", "Generar/Actualizar", $"Reportes del mes {monthStart:yyyy-MM} generados/actualizados", monthStart);
        }
    }
}