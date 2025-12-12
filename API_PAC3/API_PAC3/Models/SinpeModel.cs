using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PAC3.Models
{
    public class SinpeModel
    {
        [Key]
        public int IdSinpe { get; set; }

        [Required(ErrorMessage = "El número telefónico debe debe tener 10 dígitos"), StringLength(10)]
        public string TelefonoOrigen { get; set; }

        [Required, StringLength(200)]
        public string NombreOrigen { get; set; }
        [StringLength(10)]
        public string? TelefonoDestinatario { get; set; }

        [StringLength(200)]
        public string? NombreDestinatario { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; } = false;
    }
}
