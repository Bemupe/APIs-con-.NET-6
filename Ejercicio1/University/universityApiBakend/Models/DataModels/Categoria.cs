using System.ComponentModel.DataAnnotations;
namespace universityApiBakend.Models.DataModels
{
    public class Categoria : BaseEntity
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();//Una categoría puede tener varios cursos
    }
}
