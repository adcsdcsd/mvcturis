using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using kempery.Models;

namespace Kempery.Models
{
    public class ReservaCopropiedad
    {
        [Key]
        public int Id { get; set; }

        // ---- Llave foránea a Usuario ----
        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        [JsonIgnore]
         public virtual Usuario Usuario { get; set; } 


        // ---- Llave foránea a Copropiedad ----
        [Required]
        [ForeignKey("Copropiedad")]
        public int CopropiedadId { get; set; }
        [JsonIgnore]
        public virtual Copropiedad Copropiedad { get; set; }
        
        

        // ---- Campos propios de la reserva ----
        [Required]
        public DateTime FechaEntrada { get; set; }

        [Required]
        public DateTime FechaSalida { get; set; }

        [Required]
        public int HabSencillas { get; set; }

        [Required]
        public int HabDobles { get; set; }

        [Required]
        public int HabTriples { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostoTotal { get; set; }
    }
}
