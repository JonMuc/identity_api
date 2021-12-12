using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Util;
using System;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class AvaliacaoValidation
    {

        public AvaliacaoValidation()
        {            
        }

        public void VerificarExistenciaAvaliacao(List<Avaliacao> avaliacao)
        {
            var errosResponse = new List<string>(0);

            if (avaliacao == null)
            {
                errosResponse.Add("Não existe nenhuma avaliação com os dados informados. Tente novamente.");
                throw new ParametroException(errosResponse);
            }
        }

        public void ValidarTipoAvaliacao(TipoAvaliacao tipoAvaliacao)
        {
            var errosResponse = new List<string>(0);

            if (!Enum.IsDefined(typeof(TipoAvaliacao), tipoAvaliacao))
            {
                errosResponse.Add("O valor de TipoAvaliação é inválido.");
                throw new ParametroException(errosResponse);
            }
        }

        public void ValidarInclusaoAvaliacao(List<Avaliacao> listAvaliacao)
        {
            var errosResponse = new List<string>(0);

            //TODO JV - VOCE ESTA PEGANDO UMA LISTA E VERIFICANDO ITEM A ITEM PRA VER SE TEM ALGUEM COM STATUS ATIVO '0', NAO DEVE FICAR ASSIM
            //CRIAR UM METODO NO REPOSITORY QUE RECEBE O ID DO COMENTARIO E USUARIO E JA VERIFICA SE EXISTE ALGUM ATIVO
            // DA FORMA COMO VOCE FEZ TERA UM CUSTO EXTREMAMENTE GRANTE EM UMA APLICACAO COM MUITOS USUARIOS
            if (listAvaliacao != null)
            {
                foreach (Avaliacao avaliacao in listAvaliacao)
                {
                    //TODO JV - VOCE FAZ UMA COMPARACAO COM ENUM, PQ ESTA COLOCANDO == 0 ?
                    // COMPARACAO TEM QUE SER COM MESMO TIPO  == StatusRegistro.Ativo, QUALQUER ALTERACAO LA JA REFLETE EM TUDO, E NAO COLOCAR 0 QUE É UMA INFORMACAO DO BANCO
                    if (avaliacao.StatusRegistro == 0)
                    {
                        errosResponse.Add("Já existe uma avaliação para este caso.");
                        throw new ParametroException(errosResponse);
                    }
                }
            }            
        }

        public void ValidarExclusaoAvaliacao(List<Avaliacao> listAvaliacao)
        {
            VerificarExistenciaAvaliacao(listAvaliacao);
            var errosResponse = new List<string>(0);
            var check = 0;

            foreach(Avaliacao avaliacao in listAvaliacao)
            {
                if (avaliacao.StatusRegistro == 0)
                {
                    check++;
                }
            }

            if (check != 1)
            {
                errosResponse.Add("Não é possível excluir esta avaliação.");
                throw new ParametroException(errosResponse);
            }
        }
    }
}
