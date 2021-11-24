using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INoticiaRepository
    {
        Task<long> AdicionarNoticiaAsync(Noticia noticia);
        Task<Noticia> GetNoticiaById(long idNoticia);
        Task DeletarNoticiaAsync(long idNoticia);
        Task<Noticia> AtualizarNoticiaAsync(Noticia noticia);
        Task<IEnumerable<Noticia>> ListarNoticiaPorTipoAsync(NoticiaRequest request);
        Task<bool> VerificarExistenciaTituloAsync(string titulo);
        long AdicionarNoticia(Noticia request);
        bool VerificarExistenciaTitulo(string titulo);
        Task<IEnumerable<Noticia>> ListarNoticiaAsync(NoticiaRequest request);
        Task<IEnumerable<ViewNoticia>> ListarNoticias(NoticiaRequest request);
    }
}
