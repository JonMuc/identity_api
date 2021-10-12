using Domain.Models;
using Domain.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INoticiaFavoritoRepository
    {
        Task<long> AdicionarNoticiaFavoritoAsync(NoticiaFavorito noticiaFavorito);
        Task<NoticiaFavorito> GetNoticiaFavoritoById(long idNoticiaFavorito);
        Task DeletarNoticiaFavoritoAsync(long idNoticiaFavorito);
        Task<NoticiaFavorito> AtualizarNoticiaFavoritoAsync(NoticiaFavorito noticiaFavorito);
        Task<List<Noticia>> ListarNoticiaFavoritoAsync(long idUsuario);
    }
}
