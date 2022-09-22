using System.ComponentModel.DataAnnotations;
namespace universityApiBakend.Models.DataModels
{
    public class Temario: BaseEntity
    {
        public int CursoId { get; set; }
        public virtual Curso Curso{ get; set; } = new Curso();
        
        
        [Required]
        public string Capitulos = string.Empty;
    }
}
