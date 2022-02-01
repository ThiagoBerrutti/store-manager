using System.Threading.Tasks;

namespace SalesAPI.Identity.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateJWTAsync(User user);
    }
}