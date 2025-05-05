using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtelAppT.Data
{
    public class Rol
    {
        [Key] //  Indica que esta propiedad es la clave primaria en la tabla.
        [Parameter]
        [Column("ID_ROL")]  // Especifica el nombre de la columna en la base de datos.
        public int IdRol { get; set; }


        [Required]  //  Indica que esta propiedad no puede ser nula en la base de datos.
        [StringLength(20)]  //  Especifica la longitud máxima de la cadena para la columna.
        [Column("NOMBRE")]
        public string Nombre { get; set; }


        // Propiedades de navegación para relaciones con otras tablas.  Permiten acceder a datos relacionados.
        public ICollection<Usuario> Usuarios { get; set; }

        [NotMapped] // Indica que esta propiedad no se mapea a la base de datos
        public bool Seleccionado { get; set; }






    }
}
