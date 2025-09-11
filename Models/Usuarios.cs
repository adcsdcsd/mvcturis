using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Kempery.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string? Contrato { get; set; }


        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(20)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string Apellido { get; set; }


        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string CorreoElectronico { get; set; }

        [StringLength(200)]
        public string? Cotitular { get; set; }

        [Phone(ErrorMessage = "Número de teléfono inválido")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(100)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El estado civil es obligatorio")]
        [StringLength(20)]
        public string? EstadoCivil { get; set; }

        public string? Foto { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public int? Noches { get; set; }

       

        [StringLength(100)]
        public string? RInternacional { get; set; }

        public int? Cuotas { get; set; }


        public decimal? Volumen { get; set; }


        public decimal? Cash { get; set; }

        public int? Anio { get; set; }
    }
}
