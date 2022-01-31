using SalesAPI.Models;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateJWTAsync(User user);
    }
}