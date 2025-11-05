using Proyecto_PAC325.Data;
using Proyecto_PAC325.Models;

namespace Proyecto_PAC325.Repository
{
    public interface ISinpeRepository
    {
        Task HacerSinpeAsync(SinpeModel sinpe);
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
    }
}
