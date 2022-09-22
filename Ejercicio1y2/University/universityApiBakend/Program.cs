//1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using universityApiBakend.DataAccess;//Nos dará permiso a tener acceso a nuestro contexto
var builder = WebApplication.CreateBuilder(args);

//2. Connection with SQL Server Express
const string CONNECTIONNAME = "UniversityDB"; 
/*ObteneMOS la conexion:*/var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);


//3. Add Context to Servers of builder// Con esto tenemos nuestro contexto y podemos añadir distitntos modelos
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
