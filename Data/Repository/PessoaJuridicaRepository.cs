using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        public static List<PessoaJuridica> _listaPessoasJuridica = new List<PessoaJuridica>();

        public PessoaJuridica AdicionarPessoaJuridica(PessoaJuridica pessoa)
        {
            pessoa.Id = _listaPessoasJuridica.Count() == 0 ? 1 : _listaPessoasJuridica.OrderByDescending(x => x.Id).First().Id + 1;
            _listaPessoasJuridica.Add(pessoa);
            return pessoa;
        }

        public PessoaJuridica AtualizarPessoaJuridica(PessoaJuridica pessoa)
        {
            var index = _listaPessoasJuridica.FindIndex(x => x.Id == pessoa.Id);
            _listaPessoasJuridica[index] = pessoa;
            return pessoa;
        }

        public bool DeletarPessoaJuridica(PessoaJuridica pessoa)
        {
            var index = _listaPessoasJuridica.FindIndex(x => x.Id == pessoa.Id);
            if (index == -1)
            {
                return false;
            }
            _listaPessoasJuridica.RemoveAt(index);
            return true;
        }

        public PessoaJuridica BuscarPessoaJuridica(long idPessoa)
        {
            return _listaPessoasJuridica.First(x => x.Id == idPessoa);
        }

        public List<PessoaJuridica> ListarPessoaJuridica()
        {
            return _listaPessoasJuridica;
        }
    }
}
