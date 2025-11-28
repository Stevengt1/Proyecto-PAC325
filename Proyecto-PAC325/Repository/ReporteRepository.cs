using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;
namespace Proyecto_PAC325.Repository
{
    public class ReporteRepository
    {
        private readonly AppDbContext _context;
        private readonly CajaRepository _cajaRepository;
        private SinpeRepository _sinpeRepository;
        private ComercioRepository _comercioRepository;
        private ConfigComercioRepository _configComercioRepository;
        private readonly IBitacora _bitacora;

        public ReporteRepository(AppDbContext context, IBitacora bitacora, CajaRepository cajaRepository,
            SinpeRepository sinpeRepository, ComercioRepository comercioRepository, ConfigComercioRepository configComercioRepository)
        {
            _context = context;
            _cajaRepository = cajaRepository;
            _sinpeRepository = sinpeRepository;
            _comercioRepository = comercioRepository;
            _configComercioRepository = configComercioRepository;
            _bitacora = bitacora;
        }

        //obtiene los reportes para pasarlos a la vista
        public async Task<List<ReporteMensualModel>> GetAllReportesAsync()
        {
            return await _context.REPORTES_MENSUALES
               .OrderByDescending(r => r.FechaDelReporte)
               .ToListAsync();
        }


        private async Task<int> CantidadCajas(int idComercio)
        {
            List<CajaModel> cajas = await _cajaRepository.GetCajasByComercio(idComercio); 
            return cajas.Count;
        }

        private async Task<ReporteMensualModel> ExistReporteComercio(int idComercio)
        {
            return await _context.REPORTES_MENSUALES.FirstOrDefaultAsync(r => r.IdComercio == idComercio);
        }

        public async Task GenerarReporte(DateTime fecha)
        {
            try
            {
                List<ComercioModel> comercios = await _comercioRepository.GetAllComercio();
                foreach(var comercio in comercios)
                {

                    ConfigComercioModel config = await _configComercioRepository.GetConfigActiva(comercio.IdComercio);
                    if (config != null)
                    {
                        var reporteAnterior = await ExistReporteComercio(comercio.IdComercio);
                        var reporte = reporteAnterior ?? new ReporteMensualModel();

                        reporte.CantidadDeCajas = await CantidadCajas(comercio.IdComercio);
                        reporte.MontoTotalRecaudado = await _sinpeRepository.GetMontoSinpesByDate(comercio.IdComercio, fecha);
                        reporte.CantidadDeSINPES = await _sinpeRepository.GetCantidadSinpes(comercio.IdComercio, fecha);
                        decimal comision = (decimal)(config.Comision * 0.01);
                        reporte.MontoTotalComision = reporte.MontoTotalRecaudado*comision;
                        reporte.IdComercio = comercio.IdComercio;
                        reporte.FechaDelReporte = fecha;

                        if (reporteAnterior == null)
                        {
                            _context.REPORTES_MENSUALES.Add(reporte);
                            await _bitacora.RegistrarEvento("REPORTE", "Registro", "Se registro un reporte para la fecha " + fecha, reporte);
                        }
                        else
                        {
                            await _bitacora.RegistrarEvento("REPORTE", "Actualización", "Se actualizo un reporte para la fecha " + fecha,
                                reporteAnterior, reporte);
                        }
                        _context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                //await _bitacora.RegistrarEvento("REPORTE", "Error", "Error", ex);
            }
        }
    }
}