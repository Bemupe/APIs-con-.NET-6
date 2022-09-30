using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using universityApiBakend.DataAccess;
using universityApiBakend.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace universityApiBakend
{
    public class Services2
    {   
        //Buscar usuarios por email
        public async Task<ActionResult<User>> ConsUsurPorEmail(string emailUsuario, UniversityDBContext context)
        {
            var user = await context.Users.FindAsync(emailUsuario);
            if (user == null)
            {
                Console.WriteLine("No existe");
            }

            return user;
        }

        //Buscar alumnos mayores de edad
        static public List<Estudiante> EstudianteMayorEdad (UniversityDBContext context)
        {
            Estudiante estu = new Estudiante();
            var resultado = (from estuMayorEdad in context.Estudiantes where estu.Edad >= 18 select estuMayorEdad).ToList();

            return resultado;

        }

        //Buscar alumnos que tengan al menos un curso
        static public List<Estudiante> EstudiantesMinimoUnCurso (UniversityDBContext context)
        {
            Estudiante estu = new Estudiante();
            var resultado = (from minimoUnCurso in context.Estudiantes where estu.Cursos.Count >= 1 select minimoUnCurso).ToList();

            return resultado;


        }

        //Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito
        static public List<Curso> CurMiniUnEstuNivDeter(UniversityDBContext context, Nivel nivel)
        {
            
            Curso cur = new Curso();
            
            var resultado = (from curMiniEstuNivDeter in context.Curso where cur.Nivel==nivel &&  cur.Estudiantes.Count>=1 select curMiniEstuNivDeter).ToList();

            return resultado;


        }

        //Buscar cursos de un nivel determinado que sean de una categoría determinada
        static public List<Curso> CurNivDeterCateDeter(UniversityDBContext context, Nivel nivel, string categoria)
        {

            Curso cur = new Curso();

            var resultado = (from curNivDetCateDet in context.Curso where cur.Nivel == nivel && cur.Categorias.Equals(categoria) select curNivDetCateDet).ToList();

            return resultado;


        }

        //Buscar cursos sin alumnos
        static public List<Curso> BuscarCurSinEstu(UniversityDBContext context)
        {

            Curso cur = new Curso();

            var resultado = (from buscarCurSinAlu in context.Curso where cur.Estudiantes.Count==0 select buscarCurSinAlu).ToList();

            return resultado;


        }
    }
}
