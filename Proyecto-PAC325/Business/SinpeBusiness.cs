using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class SinpeBusiness
    {
        private readonly ISinpeRepository _sinpeRepository;
        public SinpeBusiness(ISinpeRepository sinpeRepository)
        {
            _sinpeRepository = sinpeRepository;
        }
        public async Task HacerSinpeAsync(SinpeModel sinpe)
        {
            await _sinpeRepository.HacerSinpeAsync(sinpe);
            sinpe.FechaDeRegistro = DateTime.Now;
            sinpe.Estado = false;
        }

    }
}
