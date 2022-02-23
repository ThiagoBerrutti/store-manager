using StoreAPI.Dtos;

namespace StoreAPI.Identity
{
    public class AuthResponse
    {
        public UserAuthViewModel User { get; set; }
        public string Token { get; set; }
        
        public AuthResponse()
        {            
        }

        public AuthResponse(UserAuthViewModel user, string token)
        {
            User = user;
            Token = token;
        }
    }
}