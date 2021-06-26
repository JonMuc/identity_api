using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public static List<PessoaFisica> _listaPessoasFisica = new List<PessoaFisica>();
        public UsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

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

        public IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario)
        {
            using SqlConnection conexao = new(_connectionString);
            string sql = @"SELECT * FROM TBL_USUARIOS";
            return conexao.Query<Usuario>(sql);
        }

        public List<PessoaFisica> ListarPessoaFisica()
        {
            return _listaPessoasFisica;
        }
    }
}
