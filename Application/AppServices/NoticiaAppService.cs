﻿using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Request;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class NoticiaAppService : BaseAppService
    {
        private readonly INoticiaService _noticiaService;
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaAppService(IUnitOfWork unitOfWork, INoticiaService noticiaService, INoticiaRepository noticiaRepository) : base(unitOfWork)
        {
            _noticiaService = noticiaService;
            _noticiaRepository = noticiaRepository;
        }

        public async Task<ResponseViewModel> ListarManchete(NoticiaRequest request)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaService.ListarManchetes(request);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> ListarNoticias(NoticiaRequest request)
        {
            request.PageIndex = request.PageIndex * request.PageSize;
            request.IdBase = request.IdBase == 0 ? await _noticiaRepository.ObterIdNoticiaRecente() : request.IdBase;
            var result = await _noticiaRepository.ListarNoticias(request);
            foreach (ViewNoticia noticia in result)
            {
                noticia.UrlImage = noticia.UrlImage.Replace("w100-h100", "w1000-h1000");
            }
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public async Task<ResponseViewModel> ListarMancheteG1(DataRequest request)
        {
            request.PageIndex = request.PageIndex * request.PageSize;
            request.IdBase = request.IdBase == 0 ? await _noticiaRepository.ObterIdNoticiaRecente() : request.IdBase;
            var result = await _noticiaRepository.ListarManchetesTemp(request);
            foreach (ViewNoticia noticia in result)
            {
                noticia.UrlImage = noticia.UrlImage.Replace("w100-h100", "w500-h500");
            }
            return new ResponseViewModel { Sucesso = true, Objeto = result };
        }

        public async Task<ResponseViewModel> AdicionarNoticia(Noticia noticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaService.AdicionarNoticia(noticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> AtualizarNoticia(Noticia edit)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var result = await _noticiaService.AtualizarNoticia(edit);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = result };
            }
        }

        public async Task<ResponseViewModel> DeletarNoticiaById(int idNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                await _noticiaService.DeletarNoticiaById(idNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = "O registro foi excluído com sucesso!" };
            }
        }

        public async Task<ResponseViewModel> VisualizarNoticiaById(long idNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _noticiaService.VisualizarNoticiaById(idNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }

        public async Task<ResponseViewModel> ListarNoticiaPorTipo(NoticiaRequest tipoNoticia)
        {
            using (_unitOfWork)
            {
                _unitOfWork.BeginTransaction();
                var data = await _noticiaService.ListarNoticiaPorTipo(tipoNoticia);
                _unitOfWork.CommitTransaction();
                return new ResponseViewModel { Sucesso = true, Objeto = data };
            }
        }
    }
}
