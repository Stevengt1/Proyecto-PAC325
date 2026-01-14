using System.ComponentModel.DataAnnotations;

namespace WebSimuladaAPI.Models
{
    public class ComercioModel
    {
        [Key]
        public int IdComercio { get; set; }
        public String Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        public String Nombre { get; set; }
        public int TipoDeComercio { get; set; }
        public String Telefono { get; set; }
        public String CorreoElectronico { get; set; }
        public String Direccion { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public int Estado { get; set; }
    }
}
