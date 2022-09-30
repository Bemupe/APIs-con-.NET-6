using InternationalAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InternationalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //Especificamos el localizar del texto que vamos a traducir
        private readonly IStringLocalizer<PostsController> _stringLocalizer;
        //Especificar donde esta el recurso compartido con las traducciones
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer; 
    
    
        //Lo inicializamos
        public PostsController (IStringLocalizer<PostsController> stringLocalizer, IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        //Generar un Http get
        [HttpGet]
        [Route("PostControllerResource")]
        public IActionResult GetUsingPostControlResource()
        {
            //Encontrar texto
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;

            return Ok(new
            {
                PostType = article.Value,//Obtener el valor de article
                PostName = postName,
            });
        }

        //Establecer formatos según culturas
        [HttpGet]
        [Route("SharedResource")]
        public IActionResult GetUsingSharedResource()
        {
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;
            var todayIs = string.Format(_sharedResourceLocalizer.GetString("TodayIs"), DateTime.Now.ToLongDateString());

            return Ok(new
            {
                PostType = article.Value,
                PostName = postName,
                TodayIs=todayIs,
            });
        }
    }
}
