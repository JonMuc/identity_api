using Domain.Models;
using Domain.Models.Enums;
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
        Task<List<Noticia>> ListarNoticiaPorTipoAsync(TipoNoticia tipoNoticia);
        Task<bool> VerificarExistenciaTituloAsync(string titulo);
        long AdicionarNoticia(Noticia request);
        bool VerificarExistenciaTitulo(string titulo);
    }
}
