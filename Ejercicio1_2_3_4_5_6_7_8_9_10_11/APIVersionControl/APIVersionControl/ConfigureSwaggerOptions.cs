using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIVersionControl
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        //Configuramos el uso IApiVersionDescriptionProvider para poder proporcionar las versiones a nuestra documentación de Swagger
        public ConfigureSwaggerOptions (IApiVersionDescriptionProvider provider) 
        {
            _provider = provider;
        }

        //Creamos un método, para devolver una información de OpenApi dependiendo la descripción que hemos tenido. Añademos la información de esa descripción específica de nuestra versión , para que el cliente pueda saber lo que tiene la versión.
        private OpenApiInfo CreateVersionInfo (ApiVersionDescription description)//Este método es para dar información para la documentación que se va a generar en este punto
        {
            var info = new OpenApiInfo()
            {
                Title = "My .Net Api restful",
                Version = description.ApiVersion.ToString(),
                Description = "This is my first API Versioning control",
                Contact=new OpenApiContact()//Esto es para información de contacto,por si hay algo roto en la API saber a quien acudir
                {
                    Name ="Martín",
                    Email="martin@gmail.com"
                }
            };

            //En caso de que este desfasada la version podemos ponerle alguna descripción
            if (description.IsDeprecated)
            {
                info.Description += "This API version has been depretcated";
            }
            return info;
        }
        public void Configure(SwaggerGenOptions options)
        {
            //Añadir una documentación de Swagger para cada una de nuestras API versiones
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }

        } 
          
        public void Configure(string name, SwaggerGenOptions options)//Con esta clase conseguimos es que en "Program.sc" Swagger utilice las opciones establecidas en esta clase y las respuestas de información  
        {
            Configure(options);
        }


    }
}
