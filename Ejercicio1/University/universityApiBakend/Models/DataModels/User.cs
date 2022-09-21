//La clase User, extenderá nuestra clase "BaseEntity"

using System.ComponentModel.DataAnnotations; //Establecemos el uso de esto para poder autocompletar para crear los campos de la tabla User
namespace universityApiBakend.Models.DataModels
{
    public class User:BaseEntity //De esta forma (:BaseEntity) conseguimos que la clase User tenga los campos de BaseEntity
    {
        
        //Establecemos los campos de la tabla  User
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; }=string.Empty;

        [Required]
        public string Password { get; set; }=string.Empty;
    }
}
