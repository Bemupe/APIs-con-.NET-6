//Aquí se establece todos los servicios o funcionalidades que va utilizar el controlador de nuestros estudiantes. Lo realizaremos a través de una interfaz
//La interfaz, servirá para ir añadiendo la lógica que tendremos que ir implementando poco a poco, nos definirá las lógicas que hay que desarrollar.
using universityApiBakend.Models.DataModels;

namespace universityApiBakend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Estudiante> GetEstudiantesWithCourses();//Funcionalidad que tendríamos que implementar
        IEnumerable<Estudiante> GetEstudiantesWithNoCourses();

    }

    //Una vez creado los servicios tenemos que hacer la implementación.
}
