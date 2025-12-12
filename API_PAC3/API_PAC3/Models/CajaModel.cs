using System.ComponentModel.DataAnnotations;

namespace API_PAC3.Models
{
    public class CajaModel
    {
        [Key]
        public int IdCaja { get; set; }

        public int IdComercio { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string TelefonoSINPE { get; set; } = string.Empty;

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }
        public int Estado { get; set; }
    }
}
