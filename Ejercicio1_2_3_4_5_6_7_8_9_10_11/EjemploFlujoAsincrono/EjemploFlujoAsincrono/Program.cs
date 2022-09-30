using EjemploFlujoAsincrono;
using System.Diagnostics;//Añadimos Diagnotics, para poder hacer análisis de tiempo del funcionamiento de la aplicación y ver cuanto tardamos en hacer operaciones

//Con esto iniciamos un contador de tiempo y lo contará todo hasta parar en el "stopwatch"

//EJECUCIÓN SÍNCRONA

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();//Iniciamos stopwatch

Console.WriteLine("\nBienvenido a la Calculadora de la Hipoteca Síncrona");
Console.WriteLine("\n---------------------------------------------------");

var aniosVidaLaboral = CalculadoraHipotecaSync.ObtenerAniosVidaLaboral();
Console.WriteLine("\nAños de vida laboral obtenidos: "+aniosVidaLaboral);

var esTipoContratoIndefinido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine("\nTipo de contrato indefinido: " + esTipoContratoIndefinido);

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine("\nSueldo neto obtenido: " + sueldoNeto+"€");

var gastosMensuales = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine("\nGastos mensuales obtenidos: " + gastosMensuales+"€");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalizarInformaionParaConcederHipoteca(
    aniosVidaLaboral, 
    esTipoContratoIndefinido,
    sueldoNeto,
    gastosMensuales,
    cantidadSolicitada: 50000,
    aniosPagar: 30
    );

var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnálisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");


stopwatch.Stop();//Paramos el stopwatch

Console.WriteLine($"\nLa operación sincrona ha durado: {stopwatch.Elapsed}");





//EJECUCIÓN ASINCRONA



/*stopwatch.Restart();//Reiniciamos el stopwatch
Console.WriteLine("\nReiniciamos el contador");

Console.WriteLine("\nBienvenido a la Calculadora de la Hipoteca Asíncrona");
Console.WriteLine("\n----------------------------------------------------");

var aniosVidaLaboralAsin = CalculadoraHipotecaAsincrona.ObtenerAniosVidaLaboral();
Console.WriteLine("\nAños de vida laboral obtenidos: " + aniosVidaLaboral);

var esTipoContratoIndefinidoAsin = CalculadoraHipotecaAsincrona.EsTipoContratoIndefinido();
Console.WriteLine("\nTipo de contrato indefinido: " + esTipoContratoIndefinido);

var sueldoNetoAsin = CalculadoraHipotecaAsincrona.ObtenerSueldoNeto();
Console.WriteLine("\nSueldo neto obtenido: " + sueldoNeto + "€");

var gastosMensualesAsin = CalculadoraHipotecaAsincrona.ObtenerGastosMensuales();
Console.WriteLine("\nGastos mensuales obtenidos: " + gastosMensuales + "€");

var hipotecaConcedidaAsin = CalculadoraHipotecaAsincrona.AnalizarInformaionParaConcederHipoteca(
    aniosVidaLaboral,
    esTipoContratoIndefinido,
    sueldoNeto,
    gastosMensuales,
    cantidadSolicitada: 50000,
    aniosPagar: 30
    );

var resultadoAsin = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnálisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");


stopwatch.Stop();//Paramos el stopwatch

Console.WriteLine($"\nLa operación ha durado: {stopwatch.Elapsed}");*/













stopwatch.Restart();//Reiniciamos el stopwatch
Console.WriteLine("\nReiniciamos el contador");

Console.WriteLine("\nBienvenido a la Calculadora de la Hipoteca Asíncrona");
Console.WriteLine("\n----------------------------------------------------");

//Establecemos las variables como tareas (task)
Task<int> aniosVidaLaboralTask = CalculadoraHipotecaAsincrona.ObtenerAniosVidaLaboral();
Task<bool>esTipoContratoIndefinidoTask = CalculadoraHipotecaAsincrona.EsTipoContratoIndefinido();
Task<int> sueldoNetoTask = CalculadoraHipotecaAsincrona.ObtenerSueldoNeto();
Task<int> gastosMensualesTask = CalculadoraHipotecaAsincrona.ObtenerGastosMensuales();


//Metemos las tareas en una variable de tipo lista de tareas
var analisisHipotecaTask = new List<Task>
{
    aniosVidaLaboralTask,
    esTipoContratoIndefinidoTask,
    sueldoNetoTask,
    gastosMensualesTask
};

while (analisisHipotecaTask.Any())//Mientras cualquiera de la lista de tarea haya terminado
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTask);
    if (tareaFinalizada == aniosVidaLaboralTask)
    {
        Console.WriteLine("\nAños de vida laboral obtenidos: " + aniosVidaLaboralTask.Result);

    }
    else if (tareaFinalizada == esTipoContratoIndefinidoTask)
    {
     Console.WriteLine("\nTipo de contrato indefinido: " + esTipoContratoIndefinidoTask.Result);
    }
    else if (tareaFinalizada== sueldoNetoTask)
    {
        Console.WriteLine("\nSueldo neto obtenido: " + sueldoNetoTask.Result + "€");

    }
    else if (tareaFinalizada== gastosMensualesTask)
    {
        Console.WriteLine("\nGastos mensuales obtenidos: " + gastosMensualesTask.Result + "€");

    }

    analisisHipotecaTask.Remove(tareaFinalizada);//eliminamos de la lista de tareas para ir vaciando y salir del while
}

var hipotecaConcedidaAsyn = CalculadoraHipotecaAsincrona.AnalizarInformaionParaConcederHipoteca(
    aniosVidaLaboralTask.Result,
    esTipoContratoIndefinidoTask.Result,
    sueldoNetoTask.Result,
    gastosMensualesTask.Result,
    cantidadSolicitada: 50000,
    aniosPagar: 30
    );

var resultadoAsyn = hipotecaConcedidaAsyn ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnálisis Finalizado. Su solicitud de hipoteca ha sido: {resultadoAsyn}");


stopwatch.Stop();//Paramos el stopwatch

Console.WriteLine($"\nLa operación asíncrona ha durado: {stopwatch.Elapsed}");

Console.Read();