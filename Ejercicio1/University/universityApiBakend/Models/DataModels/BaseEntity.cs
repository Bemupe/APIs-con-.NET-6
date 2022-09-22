using System.ComponentModel.DataAnnotations;//Este uso nos permitirá escribir las anotaciones del modelo de la tablas (Required, )
namespace universityApiBakend.Models.DataModels
{
    public class BaseEntity
    {
        //Estos campos estarán presentes en todas nuestras tablas de la base de dato
        [Required]
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;       
        public DateTime CreatedAt{ get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdateAt { get; set; }
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }=false;
    }
}
