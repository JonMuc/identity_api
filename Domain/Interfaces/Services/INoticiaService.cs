using Domain.Models;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface INoticiaService
    {
        Task<long> AdicionarNoticia(Noticia noticia);
        Task<Noticia> VisualizarNoticiaById(long idNoticia);
        Task<Noticia> AtualizarNoticia(Noticia noticia);
        Task DeletarNoticiaById(long idNoticia);
        Task<IEnumerable<Noticia>> ListarNoticiaPorTipo(NoticiaRequest tipoNoticia);
        bool VerificarNoticiaExistente(string noticia);
    }
}
