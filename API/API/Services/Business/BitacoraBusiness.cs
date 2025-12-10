using API.Models;

namespace API.Services.Business
{
    public class BitacoraBusiness
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
