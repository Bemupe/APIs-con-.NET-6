namespace universityApiBakend.Models.DataModels
{
    public class UserTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }//Nos ayudará a crear el tokens del usuario y enviarlo
        public string UserName { get; set; }//Nombre del usuario
        public TimeSpan Validity { get; set; }//Será la validez del tokens
        public string RefreshToken { get; set; }
        public string EmailId { get; set; }//Id de cada email donde se envian los tokens
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
