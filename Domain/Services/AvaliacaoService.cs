using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly ComentarioValidation _comentarioValidation;
        private readonly NoticiaValidation _noticiaValidation;
        private readonly UsuarioValidation _usuarioValidation;
        private readonly AvaliacaoValidation _avaliacaoValidation;
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INoticiaRepository _noticiaRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;


        public AvaliacaoService(ComentarioValidation comentarioValidation, IComentarioRepository comentarioRepository,
                                IUsuarioRepository usuarioRepository, INoticiaRepository noticiaRepository,
                                NoticiaValidation noticiaValidation, UsuarioValidation usuarioValidation,
                                IAvaliacaoRepository avaliacaoRepository, AvaliacaoValidation avaliacaoValidation)
        {
            _comentarioValidation = comentarioValidation;
            _comentarioRepository = comentarioRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _usuarioRepository = usuarioRepository;
            _noticiaRepository = noticiaRepository;
            _noticiaValidation = noticiaValidation;
            _usuarioValidation = usuarioValidation;
            _avaliacaoValidation = avaliacaoValidation;
        }

        public async Task<long> AvaliarNoticia(long idUsuario, long idNoticia, TipoAvaliacao tipoAvaliacao)
        {
            //validação de entrada de dados
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuario));
            _noticiaValidation.VerificarExistenciaNoticia(await _noticiaRepository.GetNoticiaById(idNoticia));
            _avaliacaoValidation.ValidarTipoAvaliacao(tipoAvaliacao);

            //validação de inclusão no BD
            var listAvaliacao = await _avaliacaoRepository.GetAvaliacaoByUsuarioNoticia(idUsuario, idNoticia);

            if (listAvaliacao.Count > 0)
            {
                await ExcluirAvaliacaoNoticia(idUsuario, idNoticia);
            }
            
            //se for do mesmo tipo, o usuario quer remover a avaliacao
            if (listAvaliacao.Count > 0 && tipoAvaliacao == listAvaliacao.First().TipoAvaliacao)
            {
                return await Task.FromResult(Convert.ToInt64(0));
            }

            var request = new Avaliacao()
            {
                IdUsuario = idUsuario,
                IdNoticia = idNoticia,
                TipoAvaliacao = tipoAvaliacao,
                CriadoEm = DateTime.Now
            };
            return await _avaliacaoRepository.AdicionarAvaliacaoAsync(request);
        }

        public async Task<long> AvaliarComentario(long idUsuario, long idComentario, TipoAvaliacao tipoAvaliacao)
        {
            //validação de entrada de dados
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuario));
            _comentarioValidation.VerificarExistenciaComentario(await _comentarioRepository.GetComentarioById(idComentario));
            _avaliacaoValidation.ValidarTipoAvaliacao(tipoAvaliacao);

            //validação de inclusão no BD
            var listAvaliacao = await _avaliacaoRepository.GetAvaliacaoByUsuarioComentario(idUsuario, idComentario);

            if (listAvaliacao.Count > 0)
            {
                await ExcluirAvaliacaoComentario(idUsuario, idComentario);
            }

            //se for do mesmo tipo, o usuario quer remover a avaliacao
            if (listAvaliacao.Count > 0 && tipoAvaliacao == listAvaliacao.First().TipoAvaliacao)
            {
                return await Task.FromResult(Convert.ToInt64(0));
            }

            var request = new Avaliacao()
            {
                IdUsuario = idUsuario,
                IdComentario = idComentario,
                TipoAvaliacao = tipoAvaliacao,
                CriadoEm = DateTime.Now
            };
            return await _avaliacaoRepository.AdicionarAvaliacaoAsync(request);
        }

        public async Task ExcluirAvaliacaoComentario(long idUsuario, long idComentario)
        {
            //validação de entrada de dados
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuario));
            _comentarioValidation.VerificarExistenciaComentario(await _comentarioRepository.GetComentarioById(idComentario));

            //validação de exclusão no BD
            var listAvaliacao = await _avaliacaoRepository.GetAvaliacaoByUsuarioComentario(idUsuario, idComentario);
            _avaliacaoValidation.ValidarExclusaoAvaliacao(listAvaliacao);

            await _avaliacaoRepository.ExcluirAvaliacaoComentarioAsync(idUsuario, idComentario);
        }

        public async Task ExcluirAvaliacaoNoticia(long idUsuario, long idNoticia)
        {
            //validação de entrada de dados
            _usuarioValidation.VerificarExistenciaUsuario(await _usuarioRepository.GetUsuarioById(idUsuario));
            _noticiaValidation.VerificarExistenciaNoticia(await _noticiaRepository.GetNoticiaById(idNoticia));

            //validação de exclusão no BD
            var listAvaliacao = await _avaliacaoRepository.GetAvaliacaoByUsuarioNoticia(idUsuario, idNoticia);
            _avaliacaoValidation.VerificarExistenciaAvaliacao(listAvaliacao);
            

            await _avaliacaoRepository.ExcluirAvaliacaoNoticiaAsync(idUsuario, idNoticia);
        }

    }
}
