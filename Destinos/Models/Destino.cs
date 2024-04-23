using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Destinos.Models
{
    [Table("Destinos")]
    public class Destino
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(35), Index(IsUnique = true), Required]
        public string Nombre { get; set; }

        [MaxLength(35), Required ]
        public string Ciudad { get; set; }

        [MaxLength(35)]
        public string Comunidad_Region { get; set; }

        [MaxLength(35), Required]
        public string Pais { get; set; }

        [MaxLength(35)]
        public string Idioma { get; set; }

        [MaxLength(35)]
        public string Moneda { get; set; }
        
        [NotMapped]
        public double? ValoracionMedia
        {
            get
            {
                if (Resenas != null && Resenas.Any())
                {
                    return Resenas.Average(r => r.Puntuacion);
                }
                return null;
            }
        }

        public virtual List<Resena> Resenas { get; set; }


    }
}