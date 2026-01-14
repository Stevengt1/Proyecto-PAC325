using API_PAC3.Data;
using API_PAC3.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PAC3.Services
{
    public class SinpeServices
    {

        private readonly AppDbContext _context;
        private readonly CajaServices _cajaServices;

        public SinpeServices(AppDbContext contex, CajaServices cajaServices)
        {
            _context = contex;
            _cajaServices = cajaServices;
        }

        public async Task<List<SinpeModel>> GetSinpesByTelefono(string telefono, string idComercio)
        {
            if (string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(idComercio))
                return new List<SinpeModel>();
            int id = int.Parse(idComercio);
            CajaModel caja = await _cajaServices.GetCajaByTelefono(telefono);
            if (caja != null && caja.IdComercio == id)
            {
                return await _context.SINPE
                            .Where(s => s.TelefonoDestinatario == telefono)
                            .OrderByDescending(s => s.FechaDeRegistro)
                            .ToListAsync();
            }
            else
            {
                return new List<SinpeModel>();
            }
        }

        public async Task<bool> Sincronizar(int idSinpe) 
        {
            try
            {
                SinpeModel sinpe = await GetSinpeById(idSinpe);
                if(sinpe == null) return false;

                sinpe.Estado = true;
                _context.SINPE.Update(sinpe);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<SinpeModel?> GetSinpeById(int idSinpe)
        {
            try
            {
                return await _context.SINPE
                    .FirstOrDefaultAsync(s => s.IdSinpe == idSinpe);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
