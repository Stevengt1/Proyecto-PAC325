using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public interface ISinpeRepository
    {
        Task HacerSinpeAsync(SinpeModel sinpe);
        Task<List<SinpeModel>> ObtenerSinpesAsync();
    }
    public class SinpeRepository : ISinpeRepository
    {
        private readonly AppDbContext _context;
        public SinpeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task HacerSinpeAsync(SinpeModel sinpe)
        {
            _context.SINPE.Add(sinpe);
            await _context.SaveChangesAsync();
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
    }
}