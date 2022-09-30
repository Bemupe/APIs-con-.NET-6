//Con esta clase, nos permitirá los setting de JwtSetting a nuestro proyecto
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using universityApiBakend.Models.DataModels;

namespace universityApiBakend
{
    public static class AddJwtTokenServicesExtensions
    {
        //Con esto tendríamos un mecanismo de autenticación con JWT y un mecanismo de verificación de Jwt de tipo  bearer
        public static void AddJwTokenService (this IServiceCollection Services, IConfiguration Configuration)
        {
            //Añadimos JWT Settings
            var bindJwtSettings = new JwSettings();
            Configuration.Bind("JsonWebTokenKeys");//Buscamos en appSetting el nombre que le diamos a la clave "JsonWebTokenKeys" . De esta forma todas las propiedades que tenemos en JwtSettings, estarán dentro de JsonWebTokenKeys establecido en appsettings. Damos los valores sin tener que introducirlos manualmente.   

            Services.AddSingleton(bindJwtSettings);
            Services.AddAuthentication(options =>//Añadir autenticación
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//Definimos el esquema de autenticación, nuestro programa utilizará un sistema de autenticacion JWT 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //Especificaremos cada uno de los campos
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.ValidateLifetime,
                    ClockSkew = TimeSpan.FromDays(1)
                };
            });
        }
    }
}
