using Domain.Models;
using Domain.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface INoticiaFavoritoService
    {
        Task<long> AdicionarNoticiaFavorito(NoticiaFavorito noticiaFavorito);
        Task<NoticiaFavorito> VisualizarNoticiaFavoritoById(long idNoticiaFavorito);
        Task<NoticiaFavorito> AtualizarNoticiaFavorito(NoticiaFavorito noticiaFavorito);
        Task DeletarNoticiaFavoritoById(long idNoticiaFavorito);
        Task<List<Noticia>> ListarNoticiaFavorito(long idUsuario);
    }
}
