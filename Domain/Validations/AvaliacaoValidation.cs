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
            
            if (listAvaliacao != null) //Só existirá elementos na lista se tiver avaliação ativa no BD (status registro = 0)
            {  
                errosResponse.Add("Já existe uma avaliação para este caso.");
                throw new ParametroException(errosResponse);               
            }            
        }

        public void ValidarExclusaoAvaliacao(List<Avaliacao> listAvaliacao)
        {
            VerificarExistenciaAvaliacao(listAvaliacao);
            var errosResponse = new List<string>(0);                
            errosResponse.Add("Não é possível excluir esta avaliação.");
            throw new ParametroException(errosResponse);            
        }
    }
}
