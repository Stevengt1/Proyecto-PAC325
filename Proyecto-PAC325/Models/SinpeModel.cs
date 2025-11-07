using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_PAC325.Models
{
    /*
    IdSinpe INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    TelefonoOrigen VARCHAR(10) NOT NULL,
    NombreOrigen VARCHAR (200) NOT NULL,
    TelefonoDestinatario VARCHAR(10) NOT NULL,
    NombreDestinatario VARCHAR(200) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    Descripcion VARCHAR(50),
    Estado BIT NOT NULL
     */
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
        [NotMapped]
        [Required]
        public int IdCaja { get; set; }
    }
}
