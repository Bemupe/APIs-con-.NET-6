using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploFlujoAsincrono
{
    internal class CalculadoraHipotecaAsincrona
    {
        public static async Task<int> ObtenerAniosVidaLaboral()//En los métodos asincronos tenemos que añadir la palabra async y devolver una TASK, una tarea que en algún momento se ejecutará
        {
            Console.WriteLine("\nObteniendo años de vida laboral...");
            await Task.Delay(5000);//Le quitamos el (Wait) y añadimos await, esto implicará que hará una espera en el Task (tarea) de 5 segundos pero de forma asincrona. Si no le ponemos await se ejecutará de forma sincrona
            return new Random().Next(1, 35);//Devolvemos un valor aleatorio del 1 al 35

        }

        public static async Task<bool> EsTipoContratoIndefinido()
        {
            Console.WriteLine("\nVerificando si el tipo de contrato es indefinido");
            await Task.Delay(5000);
            return (new Random().Next(1, 10)) % 2 == 0;//Devolvemos true si el valor aleatorio es par / impar
        }

        public static async Task<int> ObtenerSueldoNeto()
        {
            Console.WriteLine("\nObteniendo sueldo neto...");
            await Task.Delay(5000);//esperamos 5 segundos
            return new Random().Next(800, 6000);//Devolvemos true si el valor aleatorio entre 800 y 6000
        }

        public static async Task<int> ObtenerGastosMensuales()
        {
            Console.WriteLine("\nObteniendo gastos mensuales del usuario...");
            await Task.Delay(5000);//esperamos 5 segundos
            return new Random().Next(200, 1000);//Devolvemos true si el valor aleatorio entre 200 y 1000
        }


        public static bool AnalizarInformaionParaConcederHipoteca(
            int aniosVidaLaboral,
            bool tipoContratoEsIndefinido,
            int sueldoNeto, int gastosMensuales,
            int cantidadSolicitada,
            int aniosPagar)
        {
            Console.WriteLine("\nAnalizando información para conceder hipoteca");

            //Primer filtro
            if (aniosVidaLaboral < 2)
            {
                return false;
            }

            //Obtener la cuota mensual a pagar
            var cuota = (cantidadSolicitada / aniosPagar) / 12;

            if (cuota >= sueldoNeto || cuota > (sueldoNeto / 2))
            {
                return false;
            }

            //Obtener porcentaje de Gastos sobre el sueldo neto del usuario
            var porcentajeGastosSobresueldo = ((gastosMensuales * 100) / sueldoNeto);

            if (porcentajeGastosSobresueldo > 30)
            {
                return false;
            }
            if ((cuota + gastosMensuales) >= sueldoNeto)
            {
                return false;
            }
            if (!tipoContratoEsIndefinido)
            {
                if (((cuota + gastosMensuales) > (sueldoNeto / 3)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //Si no entra en ninguna de las condiciones, si que la concedemos
            return true;



        }
    }
}
