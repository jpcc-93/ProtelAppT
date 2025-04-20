using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProtelAppT.Data
{
    public class EstadoFactibilidad
    {
        [Key]
        [Column("ID_ESTADO_FACTIBILIDAD")]
        public int IdEstadoFactibilidad { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOMBRE")]
        public string Nombre { get; set; }

        // Propiedades de navegación para las relaciones con otras tablas
        public ICollection<Factibilidad> Factibilidades { get; set; }

    }
}
