namespace APIVersionControl.DTO
{
    
    public class User
    {
        public string? id { get; set; }
        public string? title { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? picture { get; set; }
    }
    
    
    public class UserResponseData//Clase que se encargará de gestionar lar respuestas. Información que devolveríamos al cliente. Suele estar en otra clase 
    {
        public User[]? data { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    //Ahora es necesario hacer un controlador para cada uno de estos.
}
