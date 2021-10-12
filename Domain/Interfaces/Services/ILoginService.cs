using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILoginService
    {
        Task<Usuario> LoginAsync(Usuario usuario);
    }
}
