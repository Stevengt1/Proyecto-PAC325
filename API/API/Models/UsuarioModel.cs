using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("USUARIOS")]
    public class UsuarioModel
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        public int IdComercio { get; set; }

        public Guid? IdNetUser { get; set; }

        [Required, StringLength(100)]
        public string Nombres { get; set; }

        [Required, StringLength(100)]
        public string PrimerApellido { get; set; }

        [Required, StringLength(100)]
        public string SegundoApellido { get; set; }

        [Required, StringLength(10)]
        public string Identificacion { get; set; }

        [Required, StringLength(200), EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        [Required]
        public bool Estado { get; set; }

        [NotMapped]
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";

        // Propiedad de navegación
        [ValidateNever]
        [JsonIgnore]
        [ForeignKey("IdComercio")]
        public ComercioModel Comercio { get; set; }
    }
}
