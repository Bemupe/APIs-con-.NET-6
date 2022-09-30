var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//1. LOCALIZACIÓN
builder.Services.AddLocalization(options =>options.ResourcesPath="Resources");//Establecemos el lugar donde está el archivo o carpeta donde estará los archivos de traducciones


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//2.Culturas o idiomas que se van a soportar.
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR" };

//Opciones de localización
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])//Seleccionamos como idioma por defecto el "en-US"
    .AddSupportedCultures(supportedCultures)//Añadimos todas las culturas soportadas
    .AddSupportedUICultures(supportedCultures); //Añadimos culturas al UI

//3.Add localization to app
app.UseRequestLocalization(localizationOptions);//De esta manera nuestra aplicación puede recibir parametros de consultas que idiquen el idioma en el cual nos tienen que devolver la información   


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
