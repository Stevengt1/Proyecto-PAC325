using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PAC3.Models
{
    public class ConfigComercioModel
    {
        [Key]
        public int IdConfiguracion { get; set; }
        public int IdComercio { get; set; }
        public int TipoConfiguracion { get; set; }
        public int Comision { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public int Estado { get; set; }
        [ForeignKey(nameof(IdComercio))]
        public ComercioModel Comercio { get; set; }
    }
}
