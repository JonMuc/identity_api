using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class PessoaFisicaRepository : IPessoaFisicaRepository
    {
        public static List<PessoaFisica> _listaPessoasFisica = new List<PessoaFisica>();

        public PessoaFisica AdicionarPessoaFisica(PessoaFisica pessoa)
        {
            pessoa.Id = _listaPessoasFisica.Count() == 0 ? 1 : _listaPessoasFisica.OrderByDescending(x => x.Id).First().Id + 1;
            _listaPessoasFisica.Add(pessoa);
            return pessoa;
        }

        public PessoaFisica AtualizarPessoaFisica(PessoaFisica pessoa)
        {
            var index = _listaPessoasFisica.FindIndex(x => x.Id == pessoa.Id);
            _listaPessoasFisica[index] = pessoa;
            return pessoa;
        }

        public bool DeletarPessoaFisica(PessoaFisica pessoa)
        {
            var index = _listaPessoasFisica.FindIndex(x => x.Id == pessoa.Id);
            if (index == -1)
            {
                return false;
            }
            _listaPessoasFisica.RemoveAt(index);
            return true;
        }

        public PessoaFisica BuscarPessoaFisica(long idPessoa)
        {
            var result = _listaPessoasFisica.First(x => x.Id == idPessoa);
            return result;
        }

        public List<PessoaFisica> ListarPessoaFisica()
        {
            return _listaPessoasFisica;
        }
    }
}
