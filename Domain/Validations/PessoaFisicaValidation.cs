using Domain.Models;
using Domain.Util;
using System.Collections.Generic;

namespace Domain.Validations
{
    public class PessoaFisicaValidation
    {
        public PessoaFisicaValidation()
        {
        }

        public void ValidarSalvarPessoaFisica(PessoaFisica pessoaFisica)
        {
            var errosResponse = new List<string>(0);

            if (!pessoaFisica.DataNascimento.HasValue)
            {
                errosResponse.Add("Campo 'Data Nascimento' é obrigatório.");
            }
            if (pessoaFisica.Nome == null || pessoaFisica.Nome == "")
            {
                errosResponse.Add("Campo 'Nome' é obrigatório.");
            }
            if (pessoaFisica.RG == null || pessoaFisica.RG == "")
            {
                errosResponse.Add("Campo 'RG' é obrigatório.");
            }
            else
            {
                if (pessoaFisica.RG.Length < 7)
                {
                    errosResponse.Add("O 'RG' informado é inválido.");
                }
            }
            if (pessoaFisica.CPF == null || pessoaFisica.CPF == "")
            {
                errosResponse.Add("Campo 'CPF' é obrigatório.");
            }
            else
            {
                if (ValidarCpf(pessoaFisica.CPF))
                {
                    errosResponse.Add("O 'CPF' informado é inválido.");
                }
            }

            if (errosResponse.Count > 0)
            {
                throw new ParametroException(errosResponse);
            }
        }

        //TRUE para Valido
        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return !cpf.EndsWith(digito);
        }
    }
}
