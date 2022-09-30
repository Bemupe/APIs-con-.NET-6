//Contexto de nuestra base de datos
using Microsoft.EntityFrameworkCore;//Este contexto utilizará EntityFrameworkCore
using universityApiBakend.Models.DataModels;
namespace universityApiBakend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        
        private readonly ILoggerFactory _loggerFactory;
        
        
        public UniversityDBContext()
        {//Creamos un constructor

        }
        public UniversityDBContext (DbContextOptions<UniversityDBContext> options, ILoggerFactory loggerFactory): base(options)
        {//Creamos un constructor
            _loggerFactory = loggerFactory;

        }

        //TO DO: Add DbSets (Tables of our Data base )
        public DbSet<User>? Users { get; set; }//Esto creará la base de datos UniversityDB, con las tablas User (con sus campos establecidos y los campos de BaseEntity)
        public DbSet<Curso>? Curso { get; set; }//Tabla cursos
        public DbSet<Categoria>? Categoria { get; set; } //Tabla categoria
        public DbSet<Estudiante>? Estudiantes { get; set; } //Tabla estudiante
        public DbSet<Temario>? Temarios { get; set; } //Tabla categoria



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//Lo que hacemos es para que guarde los logins en la base de datos de forma persistida. Cada vez que hagamos una operación quedará registrada todo, aunque nos puede generar logs muy largos
        {
            var logger = _loggerFactory.CreateLogger<UniversityDBContext>();
           // optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }));
           //optionsBuilder.EnableSensitiveDataLogging();
        
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] {DbLoggerCategory.Database.Name }), LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

    }
}
