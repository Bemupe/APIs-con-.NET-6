using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace universityApiBakend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;//Variable privada de log

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;//Inicialización de la variable logger para los log en el constructor del controlador
        }

        [HttpGet(Name = "GetWeatherForecast")]

       // [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator,User")]



        public IEnumerable<WeatherForecast> Get()//Cuando hagamos una petición de get los _logger se almacenarán en la carpeta "logs"
        {

            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Trace Level Log"); //Log de trazabilidad de forma concatenada, en el cual obtendríamos, el nombre del controlador y el metododo y luego el nivel. Esa debería de ser la sintaxis correcta para tener mucha información 
            _logger.LogDebug($"{nameof(WeatherForecastController)} - {nameof(Get)} - Debug Level Log");//Log de debug
            _logger.LogInformation($"{nameof(WeatherForecastController)} - {nameof(Get)} - Information Level Log");//Log informativo
            _logger.LogWarning($"{nameof(WeatherForecastController)} - {nameof(Get)} - Warning Level Log");//Log warning
            _logger.LogError($"{nameof(WeatherForecastController)} - {nameof(Get)} - Error Level Log");
            _logger.LogCritical($"{nameof(WeatherForecastController)} - {nameof(Get)} - Critical Level Log");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}