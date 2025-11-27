using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public interface ISinpeRepository
    {
        Task HacerSinpeAsync(SinpeModel sinpe);
        Task<List<SinpeModel>> ObtenerSinpesAsync();

        Task<List<SinpeModel>> GetSinpesByTelefono(string telefono);

    }
    public class SinpeRepository : ISinpeRepository
    {
        private readonly AppDbContext _context;
        private IBitacora _bitacora;
        public SinpeRepository(AppDbContext context, BitacoraRepository bitacora)
        {
            _context = context;
            _bitacora = bitacora;
        }
        public async Task HacerSinpeAsync(SinpeModel sinpe)
        {
            try
            {
                _context.SINPE.Add(sinpe);
                if(await _context.SaveChangesAsync() > 0)
                {
                    await _bitacora.RegistrarEvento("SINPE", "Registrar", "Se realizo un SINPE", sinpe);
                }
            }
            catch(Exception ex)
            {
                await _bitacora.RegistrarEvento("SINPE", "Error", "Error", ex);
            }
        }
        public async Task<List<SinpeModel>> ObtenerSinpesAsync()
        {
            return await _context.SINPE
                .Select(s => new SinpeModel
                {
                    IdSinpe = s.IdSinpe,
                    TelefonoOrigen = s.TelefonoOrigen,
                    NombreOrigen = s.NombreOrigen,
                    TelefonoDestinatario = s.TelefonoDestinatario,
                    NombreDestinatario = s.NombreDestinatario,
                    Monto = s.Monto,
                    FechaDeRegistro = s.FechaDeRegistro,
                    Descripcion = s.Descripcion,
                    Estado = s.Estado
                })
                .ToListAsync();
        }

        public async Task<List<SinpeModel>> GetSinpesByTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono)) return new List<SinpeModel>();

            return await _context.SINPE
                .Where(s => s.TelefonoDestinatario == telefono)
                .OrderByDescending(s => s.FechaDeRegistro)
                .ToListAsync();
        }

        public async Task<decimal> GetMontoSinpesByDate(int idComercio, DateTime fecha)
        {
            var inicioMes = new DateTime(fecha.Year, fecha.Month, 1);
            var fin = fecha.Date;

            return await (
                from caja in _context.CAJAS
                join sinpe in _context.SINPE
                    on caja.TelefonoSINPE equals sinpe.TelefonoDestinatario
                where caja.IdComercio == idComercio
                      && sinpe.FechaDeRegistro >= inicioMes
                      && sinpe.FechaDeRegistro <= fin
                select sinpe.Monto
            ).SumAsync();
            //Eso de arriba es lo mismo que hacer esta consulta en el sql para que no se confundan (Solo para optimizar):
            //SELECT SUM(s.Monto)
            //FROM Cajas c
            //INNER JOIN Sinpes s ON c.TelefonoSINPE = s.Telefono
            //WHERE c.IdComercio = @idComercio
            //  AND s.FechaDeRegistro >= @inicioMes
            //  AND s.FechaDeRegistro <= @fin
        }

        public async Task<int> GetCantidadSinpes(int idComercio, DateTime fecha)
        {
            var inicioMes = new DateTime(fecha.Year, fecha.Month, 1);
            var fin = fecha.Date;

            return await (
                from caja in _context.CAJAS
                join sinpe in _context.SINPE
                    on caja.TelefonoSINPE equals sinpe.TelefonoDestinatario
                where caja.IdComercio == idComercio
                      && sinpe.FechaDeRegistro >= inicioMes
                      && sinpe.FechaDeRegistro <= fin
                select sinpe
                ).CountAsync();
        }

    }
}