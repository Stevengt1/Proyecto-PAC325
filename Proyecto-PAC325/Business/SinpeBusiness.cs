using Proyecto_PAC325.Models;
using Proyecto_PAC325.Repository;

namespace Proyecto_PAC325.Business
{
    public class SinpeBusiness
    {
        private readonly SinpeRepository _sinpeRepository;
        private readonly CajaRepository _cajaRepository;

        public SinpeBusiness(SinpeRepository sinpeRepository, CajaRepository cajaRepository)
        {
            _sinpeRepository = sinpeRepository;
            _cajaRepository = cajaRepository;
        }

        public async Task<(bool Exito, string Mensaje)> HacerSinpeAsync(SinpeModel sinpe)
        {
            // Validar existencia y estado de la caja destinataria
            var caja = await _cajaRepository.GetCaja(sinpe.IdCaja);
            if (caja == null || caja.Estado != 1)
                return (false, "La caja seleccionada no es válida o está inactiva.");

            // Asignar datos del destinatario desde la caja
            sinpe.TelefonoDestinatario = caja.TelefonoSINPE;
            sinpe.NombreDestinatario = caja.Nombre;
            sinpe.FechaDeRegistro = DateTime.Now;
            sinpe.Estado = false;

            // Guardar la transacción
            await _sinpeRepository.HacerSinpeAsync(sinpe);
            return (true, "Sinpe realizado correctamente.");
        }

        public async Task<List<SinpeModel>> ObtenerSinpesAsync()
        {
            return await _sinpeRepository.ObtenerSinpesAsync();
        }

        public async Task<List<SinpeModel>> GetSinpesByTelefono(string telefono)
        {
            return await _sinpeRepository.GetSinpesByTelefono(telefono);
        }

        public async Task<bool> SincronizarSinpe(int idSinpe)
        {
            return await _sinpeRepository.SincronizarSinpe(idSinpe);
        }
    }
}