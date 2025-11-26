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
        public SinpeRepository(AppDbContext context, IBitacora bitacora)
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
    }
}