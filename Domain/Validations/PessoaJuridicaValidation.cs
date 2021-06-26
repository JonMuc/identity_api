using Domain.Models;
using Domain.Util;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Validations
{
    public class PessoaJuridicaValidation
    {
        public PessoaJuridicaValidation()
        {
        }

        public void ValidarSalvarPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            var errosResponse = new List<string>(0);

            if (pessoaJuridica.NomeFantasia == null || pessoaJuridica.NomeFantasia == "")
            {
                errosResponse.Add("Campo 'Nome Fantasia' é obrigatório.");
            }
            if (pessoaJuridica.RazaoSocial == null || pessoaJuridica.RazaoSocial == "")
            {
                errosResponse.Add("Campo 'Razão Social' é obrigatório.");
            }
            if (pessoaJuridica.CNPJ == null || pessoaJuridica.CNPJ == "")
            {
                errosResponse.Add("Campo 'CNPJ' é obrigatório.");
            }
            else
            {
                if (ValidarCnpj(pessoaJuridica.CNPJ))
                {
                    errosResponse.Add("O 'CNPJ' informado é inválido.");
                }
            }
            if (pessoaJuridica.Email == null || pessoaJuridica.Email == "")
            {
                errosResponse.Add("Campo 'Email' é obrigatório.");
            }
            else
            {
                if (ValidarEmail(pessoaJuridica.Email))
                {
                    errosResponse.Add("O 'Email' informado é inválido.");
                }
            }


            //Vefifica se existe erro
            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }

        //TRUE para Valido
        public bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return !cnpj.EndsWith(digito);
        }


        //TRUE para Valido
        public bool ValidarEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (rg.IsMatch(email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
