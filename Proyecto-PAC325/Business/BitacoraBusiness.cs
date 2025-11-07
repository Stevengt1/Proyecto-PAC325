using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class BitacoraBusiness
    {

        private readonly BitacoraRepository _repository;

        public BitacoraBusiness (BitacoraRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BitacoraModel>> GetBitacoras()
        {
            return await _repository.GetBitacoras();
        }

    }
}
