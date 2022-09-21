using System.ComponentModel.DataAnnotations;
namespace universityApiBakend.Models.DataModels
{
    public class Curso: BaseEntity 
    {
        [Required(ErrorMessage = "Nombre del curso"), StringLength(20)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descripción del curso"), StringLength(100)]
        public string DescripcionCorta { get; set; } = string.Empty;

        [StringLength(250)]
        public string DescripcionLarga { get; set; } = string.Empty;

        [Required(ErrorMessage = "Público al que va dirigido")]
        public string PúblicoObjetivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Requisitos necesarios")]
        public string Requisitos { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Es necesario seleccionar un Nivel")]
        public Nivel Nivel { get; set; } 
    }

    public enum Nivel
    {
        Básico,
        Intermedio,
        Avanzado,
    }
}
