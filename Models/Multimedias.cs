using System;
using System.ComponentModel.DataAnnotations;

namespace Kempery.Models
{
     public class Multimedia
    {
        [Key]
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Ubicacion { get; set; }

        public string? Link { get; set; }

        public string? LinkDetallado { get; set; }
    }
}