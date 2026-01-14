using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class ReporteBusiness
    {

        private readonly ReporteRepository _reporteRepository;
        private readonly ComercioRepository _comercioRepository ;
        private readonly ConfigComercioRepository _configComercioRepository;


        public ReporteBusiness(ReporteRepository reporteRepository, ComercioRepository comercioRepository, ConfigComercioRepository configComercioRepository)
        {
            _reporteRepository = reporteRepository;
            _comercioRepository = comercioRepository;
            _configComercioRepository = configComercioRepository;

        }
        public async Task GenerarReportes(DateTime fecha)
        {
            await _reporteRepository.GenerarReporte(fecha);
        }

        public async Task<List<ReporteMensualModel>> GetAllReportesAsync()
        {
            var reports = await _reporteRepository.GetAllReportesAsync();
            //se trae la lista de comercios para asignar el nombre a cada reporte
            var comercios = await _comercioRepository.GetAllComercio();
            foreach (var r in reports)
            {
                r.Nombre = comercios.FirstOrDefault(c => c.IdComercio == r.IdComercio)?.Nombre;
            }
            return reports;
        }

    }
}
