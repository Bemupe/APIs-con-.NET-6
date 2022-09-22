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
        public Nivel Nivel { get; set; } = Nivel.Basico;

        [Required]
        public Temario Temario { get; set; } = new Temario();//En cada curso existirá temarios
        
        [Required]
        public ICollection<Categoria> Categorias { get; set; }= new List<Categoria>();//Como categorías puede estar en varios cursos, tenemos que hacer la situación inversa

        [Required]
        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();//En cada curso estará los estudiantes

        

    }

    public enum Nivel
    {
        Basico,
        Intermedio,
        Avanzado,
        Experto
    }
}
