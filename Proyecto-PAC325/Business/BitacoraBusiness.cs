using Microsoft.EntityFrameworkCore;
using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{    public class BitacoraBusiness
    {
        private readonly IBitacora _repository;

        public BitacoraBusiness(IBitacora repository)
        {
            _repository = repository;
        }

        public async Task<List<BitacoraModel>> GetBitacoras()
        {
            return await _repository.GetBitacoras();
        }
    }
}