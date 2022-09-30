using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using universityApiBakend.Models.DataModels;

namespace universityApiBakend.Helpers
{
    //Los helper lo utilizaremos para generar tokens cuando tengamos que logear a un usuario 
    public static class JwHelpers
    {
        //Este método lo que hará será una lista de clims para poder trabajar con ello y añadir tokens. Esto sería un helper.
        public static IEnumerable<Claim>GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name,userAccounts.UserName),
                new Claim(ClaimTypes.Email,userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))


            };

            if (userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administ"));

            }else if (userAccounts.UserName=="User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }
            return claims;
        }

        //Llamará al id y teniendo el id se los pasaremos a la lista de claim anterior (claims)
        public static IEnumerable<Claim>GetClaims (this UserTokens userAccounts, out Guid Id)
        {
            Id =Guid.NewGuid();
            return GetClaims(userAccounts,Id);
        }

        //Ahora obtenemos el token para obtener el Jason Web tokens
        public static UserTokens GenTokenKey (UserTokens model, JwSettings jwSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if (model == null)
                {
                    throw new ArgumentException(nameof(model));
                }

                //PARA CREAR EL TOKENS NECESITAMOS


                //1.Obtener la llave secreta "Secret key"
                var key = System.Text.Encoding.ASCII.GetBytes(jwSettings.IssuerSigningKey);

                Guid Id;
                //2. Expira en un día determinado
                DateTime expireTime = DateTime.UtcNow.AddDays(1);//Id auto generado

                //Validar el tokens
                userToken.Validity = expireTime.TimeOfDay;//La validez del tokens será de un sólo dia

                //Generar nuestro JWT
                var jwToken = new JwtSecurityToken(
                    issuer: jwSettings.ValidIssuer,
                    audience: jwSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,//Tiempo de expiracion anterior a un momento
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(//Espera la estructura de firma
                        new SymmetricSecurityKey(key),//Clave con una key Simetrica
                        SecurityAlgorithms.HmacSha256));//Algoritmo para cifrar la información

                        
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;

                return userToken;//De esta forma lo devolvemos y ya lo podemos utilizar dentro de nuestros controladores 
                    
                    
            }
            catch (Exception exception) { throw new Exception("Error Generating the JWT", exception);  }
        }
    }
}
