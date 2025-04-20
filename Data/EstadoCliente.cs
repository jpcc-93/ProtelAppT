using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtelAppT.Data
{
    public class EstadoCliente
    {
        [Key]
        [Column("ID_ESTADO_CLIENTE")]
        public int IdEstadoCliente { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOMBRE")]
        public string Nombre { get; set; }

        // Propiedad de navegación para la relación con la tabla Cliente.
        public ICollection<Cliente> Clientes { get; set; }

    }
}
