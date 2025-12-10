using API.Models;
using API.Services.Repository;

namespace API.Services.Business
{
    public class ConfigComercioBusiness
    {
        private readonly ConfigComercioRepository _repository;

        public ConfigComercioBusiness(ConfigComercioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ConfigComercioModel>> GetConfiguraciones()
        {
            return await _repository.GetConfiguraciones();
        }

        public async Task<ConfigComercioModel> GetConfiguracion(int id)
        {
            return await _repository.GetConfiguracion(id);
        }

        public async Task<ConfigComercioModel> Add(ConfigComercioModel config)
        {
            if (await _repository.Exist(config.IdConfiguracion))
            {
                config.IdConfiguracion = -1;
                return config;
            }
            config.FechaDeRegistro = DateTime.Now;
            config.FechaDeModificacion = DateTime.Now;
            config.Estado = 1;
            return await _repository.Add(config);
        }

        public async Task<ConfigComercioModel> Update(ConfigComercioModel config)
        {
            config.FechaDeModificacion = DateTime.Now;
            return await _repository.Update(config);
        }
    }
}