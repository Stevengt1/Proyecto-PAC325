using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ReporteMensualModel
    {
        [Key]
        public int IdReporte { get; set; }

        [Required]
        public int IdComercio { get; set; }

        [Required]
        public int CantidadDeCajas { get; set; }

        [Required]
        public decimal MontoTotalRecaudado { get; set; }

        [Required]
        public int CantidadDeSINPES { get; set; }

        [Required]
        public decimal MontoTotalComision { get; set; }

        [Required]
        public DateTime FechaDelReporte { get; set; }

        [NotMapped]
        public string? Nombre { get; set; }
    }
}
