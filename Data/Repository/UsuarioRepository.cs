using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public static List<Usuario> _listaUsuario = new List<Usuario>();
        public UsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<int> AdicionarUsuarioAsync(Usuario request)
        {
            var sql = @" INSERT INTO tbl_usuario (Nome, Email, Senha, Telefone, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, Foto, IdGoogle, IdFacebook, PerfilLinkedin, PerfilInstagram, PerfilTwitter, Descricao)
                                    VALUES (@Nome, @Email, @Senha, @Telefone, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @Foto, @IdGoogle, @IdFacebook, @PerfilLinkedin, @PerfilInstagram, @PerfilTwitter, @Descricao)
                         SELECT @@IDENTITY";
            
            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, request, _unitOfWork?.Transaction);
        }

        //public PessoaFisica AtualizarPessoaFisica(PessoaFisica pessoa)
        //{
        //    var index = _listaPessoasFisica.FindIndex(x => x.Id == pessoa.Id);
        //    _listaPessoasFisica[index] = pessoa;
        //    return pessoa;
        //}

        //public bool DeletarPessoaFisica(PessoaFisica pessoa)
        //{
        //    var index = _listaPessoasFisica.FindIndex(x => x.Id == pessoa.Id);
        //    if (index == -1)
        //    {
        //        return false;
        //    }
        //    _listaPessoasFisica.RemoveAt(index);
        //    return true;
        //}

        public IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario)
        {
            using SqlConnection conexao = new(_connectionString);
            string sql = @"SELECT * FROM TBL_USUARIOS";
            return conexao.Query<Usuario>(sql);
        }

        //public List<PessoaFisica> ListarPessoaFisica()
        //{
        //    return _listaPessoasFisica;
        //}
    }
}
