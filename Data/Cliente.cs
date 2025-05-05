using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace ProtelAppT.Data
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Key]
        [Parameter]
        [Column("ID_CLIENTE")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        [Column("DIRECCION")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TELEFONO")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required]
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }

        
        [Column("FECHA_ACTUALIZACION")]
        public DateTime? FechaActualizacion { get; set; }


        [Required]
        [Column("ID_ESTADO_CLIENTE")]
        [ForeignKey("EstadoCliente")]
        public int IdEstadoCliente { get; set; }



        // Propiedades de navegación para las relaciones con otras tablas.
        public EstadoCliente EstadoCliente { get; set; }
        public ICollection<Factibilidad> Factibilidades { get; set; }

        [NotMapped] // Indica que esta propiedad no se mapea a la base de datos
        public bool Seleccionado { get; set; }


    }
}
