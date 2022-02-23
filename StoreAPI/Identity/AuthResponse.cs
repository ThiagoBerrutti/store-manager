using StoreAPI.Dtos;

namespace StoreAPI.Identity
{
    public class AuthResponse
    {
        public UserAuthDto User { get; set; }
        public string Token { get; set; }
        
        public AuthResponse()
        {            
        }

        public AuthResponse(UserAuthDto user, string token)
        {
            User = user;
            Token = token;
        }
    }
}