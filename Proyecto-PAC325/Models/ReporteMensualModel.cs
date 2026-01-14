using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Proyecto_PAC325.Models
{

    //▪ IdReporte – int, primary key, not null (identity) 
    //▪ IdComercio – int, not null, llave foránea de la tabla comercio.
    //▪ CantidadDeCajas – int, not null 
    //▪ MontoTotalRecaudado – decimal, not null 
    //▪ CantidadDeSINPES – int, not null 
    //▪ MontoTotalComision – decimal, not null. 
    //▪ FechaDelReporte – DateTime, not null 
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
