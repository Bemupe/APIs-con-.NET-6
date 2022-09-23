using System.ComponentModel.DataAnnotations;
namespace universityApiBakend.Models.DataModels
{
    public class Estudiante:BaseEntity
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }

        [Required]
        public DateTime Dob { get; set; }


        public ICollection<Curso> Cursos { get; set; }=new List<Curso>();//El estudiante puede tener varios cursos
    }
}
