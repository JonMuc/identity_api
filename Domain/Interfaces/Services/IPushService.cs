using Domain.Models;
using Domain.Models.Enums;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPushService
    {
        Task EnviarPush(Usuario usuario);        
    }
}
