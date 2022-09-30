using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universityApiBakend.DataAccess;
using universityApiBakend.Helpers;
using universityApiBakend.Models.DataModels;

namespace universityApiBakend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwSettings _jwSetting;

        public AccountController (UniversityDBContext context, JwSettings jwSettings)
        {
            _context = context;
            _jwSetting = jwSettings;
        }

        //Funcion para obtener los logins y los tokens
        //Ejemplo de usuers
        //TO DO: Cambiar por usuarios reales en base de datos
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id=1,
                Email="martin@imaginagroup.com",
                Name="Admin",
                Password="Admin"
            },
            new User()
            {
                Id=2,
                Email="pepe@imaginagroup.com",
                Name="User1",
                Password="pepe"
            }
        };

        //Funcion para obtener un tokens
        [HttpPost]
        public IActionResult GetToken (UserLogin userLogin)
        {
            try
            {
                var Tokens = new UserTokens();

                //Buscar un usuario en el contexto con LINQ
                var searchUser = (from user in _context.Users
                                  where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                  select user).FirstOrDefault();//Devolvemos todo el ususario).

               // Console.WriteLine("User found", searchUser);

                //To Do: Cambiar a serachUser
               // var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));//Comprobamos si el usuario es válido. Esto quedaría obsoleto porque hicimos la busqueda anteriormente 
                
                //var Valid = searchUser != null;
                if (searchUser != null) //Si es valido
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));//Ya está implementado
                    //Si el usuario es válido creamos el token
                    Tokens = JwHelpers.GenTokenKey(new UserTokens()
                    {
                        //UserName = user.Name,
                        //EmailId= user.Email,
                        //Id=user.Id,
                        //GuidId=Guid.NewGuid(),
                        UserName = searchUser.Name,
                        EmailId= searchUser.Email,
                        Id= searchUser.Id,
                        GuidId=Guid.NewGuid(),

                    }, _jwSetting);
                }
                else//Si el usuario no existe o no es válido
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Tokens);
            }catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator")]//Espera el esquema de autorización. Solo el rolo administrador (desarrollado dentro Jwthelpers). Esta ruta sólol lo podrá controlar el usuario administrador
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

    }
}
