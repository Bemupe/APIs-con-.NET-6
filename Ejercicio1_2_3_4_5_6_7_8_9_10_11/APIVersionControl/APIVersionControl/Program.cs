using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using APIVersionControl;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//1.Inyectamos el HttpClient para mandar o poder hacer las peticiones a HttRequest
builder.Services.AddHttpClient();
//2. A�adimos el control de versiones del App
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);//Le decimos al setup que seleciones como versi�n por defecto la 1.0
    setup.AssumeDefaultVersionWhenUnspecified=true;//Le decimos al setup que asuma por defecto la versi�n cuando no est� especificada
    setup.ReportApiVersions = true;
});
//3. �C�mo queremos documentar nuestras versiones?. A�air la configuraci�n al documento de versiones. Formato de visualizaci�n de las versiones
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";//1.0.0, 1.1..0
    setup.SubstituteApiVersionInUrl = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//4. Configurar las opciones 
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

//5. Configurar los puntos de final de los documentos de swagger de cada versi�n de nuestro API
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Configurar los Swagger Docs o los documentos de Swagger.
    app.UseSwaggerUI(options=>
    {
        //Por cada una de las versiones que tenemos, tenemos que a�adir una versi�n distinta a nuestro Swagger para que se desplieguen distintas opciones seg�n las versiones
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",//Cuando vemos Swagger, es una interpretaci�n de json
                description.GroupName.ToUpperInvariant()                                                
                );//Lo que tendr�amos ser�a /swagger/v2/swagger.json seg�n las versiones
                    
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
