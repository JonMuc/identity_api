using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ICrawlingG1Service
    {
        List<Noticia> ListarManchetes();
    }
}
