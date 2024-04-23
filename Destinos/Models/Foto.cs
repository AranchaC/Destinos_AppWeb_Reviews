using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Destinos.Models
{
    [Table("Fotos")]
    public class Foto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("resena")]
        public Guid IdResena { get; set; }
        public virtual Resena resena { get; set; }

        [MaxLength(200)]
        public string UrlFoto { get; set; }

        public DateTime FechaSubida { get; set; } = DateTime.Now;
    }
}