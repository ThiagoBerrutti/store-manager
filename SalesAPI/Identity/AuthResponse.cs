using SalesAPI.Dtos;

namespace SalesAPI.Identity
{
    public class AuthResponse
    {
        public UserViewModel User { get; set; }
        public string Token { get; set; }
        
        public AuthResponse()
        {
            User = null;
            Token = string.Empty;
        }

        public AuthResponse(UserViewModel user, string token)
        {
            User = user;
            Token = token;
        }
    }
}