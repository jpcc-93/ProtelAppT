using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace ProtelAppT.Data
{
    public class Usuario
    {

        [Key]
        [Parameter]
        [Column("ID_USUARIO")]
        public int Id { get; set; }
        
        [StringLength(100)]
        [Column("CORREO")]
        public string Correo { get; set; }


        [Required]
        [StringLength(100)]
        [Column("NOMBRE")]
        public string Nombre { get; set; }

        
        [StringLength(255)]
        [Column("PASSWORD_HASH")]
        public string PasswordHash { get; set; }

        [Required]
        [Column("ID_ROL")]
        [ForeignKey("Rol")]  // Especifica la columna y tabla de la clave externa.
        public int IdRol { get; set; }

        // Propiedad de navegación para la relación con la tabla Rol.
        public Rol Rol { get; set; }

        [NotMapped] // Indica que esta propiedad no se mapea a la base de datos
        public bool Seleccionado { get; set; }


    }



}

