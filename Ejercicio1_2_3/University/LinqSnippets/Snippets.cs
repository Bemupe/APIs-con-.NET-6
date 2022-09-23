using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinqQ()
        {

            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            //Ejemplo 1. SELECT * of cars (Ejemplo de la lista de todos los coches)//Selecciona todos los coches
            var carList = from car in cars select car;// Metemos en "carList" la lista de coche "car" con un select, utlizando SQL

            foreach (var car in carList)//Sacamos por pantalla con un foreach los coches
            {
                Console.WriteLine(car);
            }

            //Ejemplo 2. Usamos condiconales SELECT WHERE car is Audi// Selecciona sólo los Audis
            var audilist = from car in cars where car.Contains("Audi") select car;

            foreach(var audi in audilist)
            {
                Console.WriteLine(audi);
            } 

        }

        //Ejemplo con números. 
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> {1,2,3,4,5,6,7,8,9 };

            //Cada número multiplicado por 3
            //lo cogemos todos los números, menos el 9
            //y finalmente los ordenamos de forma ascendente

            var processedNumberList =
                numbers
                    .Select(num => num * 3)//Selecciona todos los numeros de "numbers" y los multiplica por 3
                    .Where(num => num != 9)//El número 9 no lo multiplica por 3
                    .OrderBy(num => num);//Y ordenalos de menor a mayor
        }
        
        //Usando Linq con consultas más avanzadas
        static public void EjemplosBusqueda ()
        {
            List<string> textlist = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            //1.Obtener el primero de los elementos de textlist
            var first=textlist.First();

            //2.Obtener el primer elementos que es igual "c"
            var cText= textlist.First(text => text.Equals("c"));

            //3. Obtener el primer elemento que contenga "j"
            var jText = textlist.First(text => text.Contains("j"));

            //4. Obtener el primer elemento que contiene la "z" y si no coger un valor por defecto
            var firstOrDefaultText = textlist.FirstOrDefault(text => text.Contains("z")); //El resultado será una lista vacía o palabra que contiene la "z"

            //5. Obtener el último elemento que contiene la "z" o si no coger valor por defecto
            var lastOrDefaultText = textlist.LastOrDefault(text => text.Contains("z"));

            //6. Obtener un valor único
            var uniqueTexts = textlist.Single();
            var uniqueOrDefaultTexts = textlist.SingleOrDefault();


            //Mas ejemplos
            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] othersEvenNumbers = { 0, 2, 6 };
                
                //1. Obtener los número 4 y 8
                var myEventNumbers = evenNumbers.Except(othersEvenNumbers);// Elimina todos los valores que estén ambos grupos repetidos y se queda con los que no lo están (4,8)
                
        }

        //SELECT MÁS COMPLICADOS
        static public void MultiplesSelecciones ()
        {
            //Seleccionar muchos
            string[] myOpinions =
            {
                "Opinión 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"

            };
            var myOpinonSelection = myOpinions.SelectMany(opinion => opinion.Split(","));//Obtenemos todas las opiniones separadas por comas


            //Creamos empresas y empleados para usar las consultas
            var Empresas = new[]
            {
                new Empresa()
                {
                    Id=1,
                    Name="Interprise 1",
                    Empleados=new[]
                    {
                        new Empleado
                        {
                            Id=1,
                            Nombre="Martín",
                            Email="martin@hotmail.com",
                            Salario=3000
                        },

                         new Empleado
                        {
                            Id=2,
                            Nombre="Pepe",
                            Email="pepe@hotmail.com",
                            Salario=1000
                        },

                        new Empleado
                        {
                            Id=3,
                            Nombre="Juanjo",
                            Email="juanjo@hotmail.com",
                            Salario=2000
                        }



                    }
                },

                new Empresa()
                {
                    Id=1,
                    Name="Interprise 1",
                    Empleados=new[]
                    {
                        new Empleado
                        {
                            Id=4,
                            Nombre="Ana",
                            Email="ana@hotmail.com",
                            Salario=3000
                        },

                         new Empleado
                        {
                            Id=5,
                            Nombre="Maria",
                            Email="maria@hotmail.com",
                            Salario=1500
                        },

                        new Empleado
                        {
                            Id=6,
                            Nombre="Marta",
                            Email="marta@hotmail.com",
                            Salario=500
                        }

                    }
                }
            };

            //1. Obtener todos los empleados de todas las empresas
            var empleadosList = Empresas.SelectMany(Empresa => Empresa.Empleados);

            //2. Ver si tenemos una lista vacía
            bool tenemosEmpresas = Empresas.Any();
            bool tenemosEmpleados = Empresas.Any(Empresa => Empresa.Empleados.Any());// Estamos diciendo que dentro de las empresas (todas), alguan de ellas ,tienen que tener algún empleado

            //3. Obtener todas las empresas que tienen algún empleado con un salario de más de 1000 euros
            bool tengaEmpleadosConAlmenos100EurosOIgual =
                Empresas.Any(Empresa =>
                    Empresas.Any(Empresa =>
                        Empresa.Empleados.Any(Empleado => Empleado.Salario > 1000)));//De toda la lista de empresas, obtener los empleados con 1000 euros o más de salarios 
        }

        //Trabajar con colecciones//Haciendo Innerjoin (Los que comparten ambos grupos (a,c)) y outer join-left (Lista inicia o lista de la izquierda, porque nos quedamos con los valores del grupo de la izquierda (inicial) que sobran de restar los elementos en común entre dos listas)
        static public void linqCollection ()
        {
            var firtsList = new List<String> {"a","b","c" };
            var secondList = new List<String> { "a","c","d" };
            
            //Inner Join (Clásico)
            var resultadosComunes = from element in firtsList// Desde "elemento" donde introducimos los datos de la lista "firsList"
                                    join secondElement in secondList//haz una unión con "seconElement", donde introducimos los valores de la lista "secondList"
                                    on element equals secondElement// y sobre la variable "element", igualaló o encuentra los elementos iguales con la variable "secondElement"
                                    select new { element, secondElement };//y finalmente selecciona los resultados 

            //Inner Join (Nuevo) 
            var commonResult2 = firtsList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new {element, secondElement}
                    );
        
            //Outer Join-Left (Lista inicia o lista de la izquierda, porque nos quedamos con los valores del grupo de la izquierda (inicial) que sobran de restar los elementos en común entre dos listas)
            var leftOuterJoin = from element in firtsList
                                join secondElement in secondList
                                on element equals secondElement//Obtenemos lo que es común en las dos listas
                                into temporalList//Lo anterior lo introducimos en "temporalList"
                                from temporalElement in temporalList.DefaultIfEmpty()// Introducimos en "temporalElement", "temporalList" aunque esté vació
                                where element != temporalElement//Obtenemos los valores de "element/firtsList" que sean distintos de los valores de "temporalList/temporalElement"
                                select new { Element = element };

            //Outer joinn-left mas simplificando
            var leftOuterJoin2 = from element in firtsList
                                 from secondElement in secondList.Where(s => s== element).DefaultIfEmpty()
                                 select new { Element = element, secondElement = secondElement };
            
            //Outer Join-Right (Al contrario de left)
            var righttOuterJoin = from secondElement in secondList
                                join element in firtsList
                                on secondElement equals element//Obtenemos lo que es común en las dos listas
                                into temporalList//Lo anterior lo introducimos en "temporalList"
                                from temporalElement in temporalList.DefaultIfEmpty()// Introducimos en "temporalElement", "temporalList" aunque esté vació
                                where secondElement != temporalElement//Obtenemos los valores de "element/firtsList" que sean distintos de los valores de "temporalList/temporalElement"
                                select new { Element = secondElement };
        }
        //Union (Uniones)
        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            //Funciones de SKIP (Saltar)
            var skipTwoFirstValues = myList.Skip(2);//Saltar los 2 primeros valores (Resultado:3,4,5,6,7,8,9,10)

            var skipLastTwoValues = myList.SkipLast(2);//Saltar los 2 últimos valores (Resultado:1,2,3,4,5,6,7,8)

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num <4);//Saltar todos los valores menores que 4 (Resultado=4,5,6,7,8,9,10)
            
            
            //Funciones TAKE (coger)
            var takeFirstTwoValues = myList.Take(2);//Coger los 2 primeros valores (Resultado 1,2)
            var takeLastTwoValues = myList.TakeLast(2);//Coger los 2 últimos valores (Resultado 9,10)
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num <4);//Coger todos los valores menores de 4 (Resultado 1,2,3)
        }
    }
    
    
}