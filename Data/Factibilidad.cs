using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProtelAppT.Data
{
    public class Factibilidad
    {
        [Key]
        [Column("ID_FACTIBILIDAD")]
        public int IdFactibilidad { get; set; }

        [Required]
        [Column("ID_CLIENTE")]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(200)]
        [Column("NOMBRE_PROYECTO")]
        public string NombreProyecto { get; set; }

        [StringLength(255)]
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(255)]
        [Column("UBICACION")]
        public string Ubicacion { get; set; }

        [Column("FECHA_SOLICITUD")]
        public DateTime FechaSolicitud { get; set; }

        [Column("FECHA_RESPUESTA")]
        public DateTime? FechaRespuesta { get; set; }

        [Required]
        [Column("ID_ESTADO_FACTIBILIDAD")]
        [ForeignKey("EstadoFactibilidad")]
        public int IdEstadoFactibilidad { get; set; }

        // Propiedades de navegación para las relaciones con otras tablas
        public Cliente Cliente { get; set; }
        public EstadoFactibilidad EstadoFactibilidad { get; set; }

    }
}
