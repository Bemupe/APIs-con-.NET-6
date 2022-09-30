using APIVersionControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIVersionControl.Controllers.V2
{
    [ApiVersion("2.0")]//Opción para señalar la versión del controlador
    [Route("api/v{version:apiVersion}/[controller]")]//Establecemos una ruta específica para consumir esta versión del controlador. Esto "version:apiVersion" lo que hace es señalar la versión
    [ApiController]
    public class UsersController : ControllerBase
    {
        //Vamos a definir el comportamiento de nuestra API restful. Consumiremos peticiones de Dummi API  
        //Vamos hacer una url de ejemplo de peticiones de flujos asincronos con apis
        private const string ApiTestUrl = "https://dummyapi.io/data/v1/user?page=1&limit=10";//Devuelve 30 usuarios aleatorios.
        private const string AppID = "63346690635b0da5df5c4adc"; //Clave que vamos a utilizar generando la AppId con Dummy para lo cual tendremos que registrarnos
        private readonly HttpClient _httpClient;//Hacer una instancia de nuestro HttpClient para hacer peticiones HTTP asincronas. Esto lo tendremos que injectar en el constructor para hacer peticiones 

        //Inicialización
        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Hacer la primera de las rutas
        [MapToApiVersion("2.0")]//De esta forma, esta ruta,sólo estará disponible para la versión 1.0 del controlador
        [HttpGet(Name = "GetUserData")]
        public async Task<IActionResult> GetUserDataAsyn()
        {
            _httpClient.DefaultRequestHeaders.Clear();//Limpiamos las cabeceras
            _httpClient.DefaultRequestHeaders.Add("app-id", AppID);//Cogemos "app-id" de la web Dummy y nos aseguramos que sea el AppID de la variable private

            var response = await _httpClient.GetStreamAsync(ApiTestUrl);//Vamos hacer una solicitud a la ruta "ApiTestURL", a Dummy APIS y la metemos en response (respuesta de Dummy). Await lo que hace, es esperar para obtener la respuesta

            var usersData = await JsonSerializer.DeserializeAsync<UserResponseData>(response); ;//Ahora vamos a gestionar lo que vamos a devolver.
                                                                                               //Ahora tenemos los datos tal y como queremos devolverlo a los clientes
                                                                                               //En esta segunda versión vamos a cambiar lo que va a devolver. Devolverá sólo la lista de usuarios
            var users = usersData?.data;//En la versión 1 devolvemos todo y en la versión 2 devolvemos la lista de usarios 

            return Ok(users);//Devolvemos los datos de los usuarios solicitados al API restful
        }
    }
}
