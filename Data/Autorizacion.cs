using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProtelAppT.Data
{
    public class Autorizacion
    {
        [Key]
        [Column("ID_AUTORIZACION")]
        public int IdAutorizacion { get; set; }

        [Required]
        [StringLength(50)]
        [Column("NOMBRE_AUTORIZACION")]
        public string NombreAutorizacion { get; set; }

        [StringLength(255)]
        [Column("DESCRIPCION_AUTORIZACION")]
        public string DescripcionAutorizacion { get; set; }

        // Propiedades de navegación para las relaciones con otras tablas
        public ICollection<AsignacionRolAutorizacion> AsignacionesRolAutorizacion { get; set; }

    }
}
