//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using universityApiBakend;
using universityApiBakend.DataAccess;//Nos dará permiso a tener acceso a nuestro contexto
using universityApiBakend.Services;

//10. Using de Serilog
using Serilog;


var builder = WebApplication.CreateBuilder(args);


//11.Configurar Serilog
builder.Host.UseSerilog((hostBuilderCtx, loggerConf) =>
{//Esto será las funciones o los parametros que va a recibir UseSerilog
    loggerConf
    .WriteTo.Console()//Se escribirá en la consola.Se usará UseSerilog para console
    .WriteTo.Debug()//Se escribirá en el debug.
    .ReadFrom.Configuration(hostBuilderCtx.Configuration);

}); 



//2. Connection with SQL Server Express
const string CONNECTIONNAME = "UniversityDB"; 
/*ObteneMOS la conexion:*/var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);


//3. Add Context to Servers of builder// Con esto tenemos nuestro contexto y podemos añadir distitntos modelos
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));



//7. Añadir servicio de JWT de autorización
builder.Services.AddJwTokenService(builder.Configuration);//No se autocompleta porque hay algún problema To do.


// Add services to the container.

builder.Services.AddControllers();


//4.  Añadir los servicios (carpeta de los servicios)

builder.Services.AddScoped<IStudentsService, StudentsService>();

//8. Añadir una política de autorización 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//9.Hacer la configuración para coger la autorizacion de JWT
builder.Services.AddSwaggerGen(options =>
{
    //Nosotros definimos la seguridad de autorización
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name =  "Authorization",
        Type =  SecuritySchemeType.Http,
        Scheme =    "Bearer",
        BearerFormat ="JWT",
        In = ParameterLocation.Header,
        Description= "JWT Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                }
            },
            
            new string[]
            {

            }



        }
    });
}
    
    );

//5. Configurar el CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//12. Añadir para que la aplicación utilice Serilog
app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//6. Tell app to use CORS
app.UseCors("CorsPolicy");
app.Run();

