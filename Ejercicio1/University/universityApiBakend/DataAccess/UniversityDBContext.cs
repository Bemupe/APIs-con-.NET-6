﻿//Contexto de nuestra base de datos
using Microsoft.EntityFrameworkCore;//Este contexto utilizará EntityFrameworkCore
using universityApiBakend.Models.DataModels;
namespace universityApiBakend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext (DbContextOptions<UniversityDBContext> options): base(options)
        {//Creamos un constructor

        }

        //TO DO: Add DbSets (Tables of our Data base )
        public DbSet<User>? Users { get; set; }//Esto creará la base de datos UniversityDB, con las tablas User (con sus campos establecidos y los campos de BaseEntity)
        public DbSet<Curso>? Curso { get; set; }                                       //
    }
}
