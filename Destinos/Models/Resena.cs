using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Destinos.Models
{
    [Table("Resenas")]
    public class Resena
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("destino"), Required]
        public Guid IdDestino { get; set; }
        public virtual Destino destino { get; set; }

        [ForeignKey("User")]
        public string IdUser { get; set; }
        public virtual ApplicationUser User { get; set; }       

        [MaxLength(45)]
        public string Titulo { get; set; }

        [Range(1,10)]
        public int Puntuacion { get; set; }

        public DateTime FechaResena { get; set; } = DateTime.Now;

        [MaxLength(500), Required]
        public string Comentario { get; set; }

        [MaxLength(900)]
        public string UrlFoto { get; set; }

        public int Likes { get; set; } // Agregar esta propiedad para manejar los likes


        public virtual List<Foto> Fotos { get; set; }

    }
}