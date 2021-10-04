using Domain.Models;
using Domain.Models.Enums;
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
        Task<List<Noticia>> ListarNoticiaPorTipo(TipoNoticia tipoNoticia);
    }
}
