using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProtelAppT.Data
{
    public class AsignacionRolAutorizacion
    {
        [Key]
        [Column("ID_ASIGNACION")]
        public int IdAsignacion { get; set; }

        [Required]
        [Column("ID_ROL")]
        [ForeignKey("Rol")]
        public int IdRol { get; set; }

        [Required]
        [Column("ID_AUTORIZACION")]
        [ForeignKey("Autorizacion")]
        public int IdAutorizacion { get; set; }

        // Propiedades de navegación para las relaciones con otras tablas
        public Rol Rol { get; set; }
        public Autorizacion Autorizacion { get; set; }

    }
}
