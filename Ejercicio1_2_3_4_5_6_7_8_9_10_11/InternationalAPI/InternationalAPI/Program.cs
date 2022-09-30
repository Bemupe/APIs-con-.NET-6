var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//1. LOCALIZACI�N
builder.Services.AddLocalization(options =>options.ResourcesPath="Resources");//Establecemos el lugar donde est� el archivo o carpeta donde estar� los archivos de traducciones


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//2.Culturas o idiomas que se van a soportar.
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR" };

//Opciones de localizaci�n
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])//Seleccionamos como idioma por defecto el "en-US"
    .AddSupportedCultures(supportedCultures)//A�adimos todas las culturas soportadas
    .AddSupportedUICultures(supportedCultures); //A�adimos culturas al UI

//3.Add localization to app
app.UseRequestLocalization(localizationOptions);//De esta manera nuestra aplicaci�n puede recibir parametros de consultas que idiquen el idioma en el cual nos tienen que devolver la informaci�n   


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
