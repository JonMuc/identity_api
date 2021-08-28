using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ICrawlingGoogleService
    {
        List<Noticia> ListarManchetes();
    }
}
