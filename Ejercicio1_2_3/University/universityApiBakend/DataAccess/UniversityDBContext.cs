//Contexto de nuestra base de datos
using Microsoft.EntityFrameworkCore;//Este contexto utilizará EntityFrameworkCore
using universityApiBakend.Models.DataModels;
namespace universityApiBakend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext()
        {//Creamos un constructor

        }
        public UniversityDBContext (DbContextOptions<UniversityDBContext> options): base(options)
        {//Creamos un constructor

        }

        //TO DO: Add DbSets (Tables of our Data base )
        public DbSet<User>? Users { get; set; }//Esto creará la base de datos UniversityDB, con las tablas User (con sus campos establecidos y los campos de BaseEntity)
        public DbSet<Curso>? Curso { get; set; }//Tabla cursos
        public DbSet<Categoria>? Categoria { get; set; } //Tabla categoria
        public DbSet<Estudiante>? Estudiantes { get; set; } //Tabla estudiante
        public DbSet<Temario>? Temarios { get; set; } //Tabla categoria

    }
}
