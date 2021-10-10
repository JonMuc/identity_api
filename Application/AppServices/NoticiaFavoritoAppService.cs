using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class NoticiaFavoritoAppService : BaseAppService
    {        
        private readonly INoticiaFavoritoService _noticiaFavoritoService;

        public NoticiaFavoritoAppService(IUnitOfWork unitOfWork, INoticiaFavoritoService noticiaFavoritoService) : base(unitOfWork)
        {
            _noticiaFavoritoService = noticiaFavoritoService;
        }

        //public ResponseViewModel ListarManchete()
        //{
        //    var listaNoticia = new List<Noticia>();

        //    listaNoticia = _crawlingGoogleService.ListarManchetes();
        //    var g1 = _crawlingG1Service.ListarManchetes();
        //    listaNoticia.AddRange(g1);
        //    var result = listaNoticia.OrderBy(elem => Guid.NewGuid());
        //    return new ResponseViewModel { Sucesso = true, Objeto = result };
        //}

        //public ResponseViewModel ListarMancheteG1()
        //{
        //    var result = _crawlingG1Service.ListarManchetes();
        //    return new ResponseViewModel { Sucesso = true, Objeto = result };
        //}

        public async Task<ResponseViewModel> AdicionarNoticiaFavorito(NoticiaFavorito noticiaFavorito)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaFavoritoService.AdicionarNoticiaFavorito(noticiaFavorito);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> AtualizarNoticiaFavorito(NoticiaFavorito edit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaFavoritoService.AtualizarNoticiaFavorito(edit);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeletarNoticiaFavoritoById(int idNoticiaFavorito)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _noticiaFavoritoService.DeletarNoticiaFavoritoById(idNoticiaFavorito);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }

        public async Task<ResponseViewModel> VisualizarNoticiaFavoritoById(int idNoticiaFavorito)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _noticiaFavoritoService.VisualizarNoticiaFavoritoById(idNoticiaFavorito);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }

        //public async Task<ResponseViewModel> ListarNoticiaPorTipo(TipoNoticia tipoNoticia)
        //{
        //    using (_unitOfWork)
        //    {
        //        _unitOfWork.BeginTransaction();
        //        var data = await _noticiaService.ListarNoticiaPorTipo(tipoNoticia);
        //        _unitOfWork.CommitTransaction();
        //        return new ResponseViewModel { Sucesso = true, Objeto = data };
        //    }
        //}
    }
}
