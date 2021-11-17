using Domain.Models;
using Domain.Models.Request;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILoginService
    {
        Task<Usuario> LoginAsync(LoginRequest request);
    }
}
