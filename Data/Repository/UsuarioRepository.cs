using Dapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public static List<Usuario> _listaUsuario = new List<Usuario>();
        public UsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<long> AdicionarUsuarioAsync(Usuario request)
        {
            var sql = @" INSERT INTO tbl_usuario (Nome, Email, Senha, Telefone, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, StatusRegistro, Foto, IdGoogle, IdFacebook, PerfilLinkedin, PerfilInstagram, PerfilTwitter, Descricao, NomeUsuario,  PerfilFacebook)
                                    VALUES (@Nome, @Email, @Senha, @Telefone, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @StatusRegistro, @Foto, @IdGoogle, @IdFacebook, @PerfilLinkedin, @PerfilInstagram, @PerfilTwitter, @Descricao, @NomeUsuario, @PerfilFacebook)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<Usuario> AtualizarUsuarioAsync(Usuario user)
        {
            var sql = @" UPDATE tbl_usuario
                            SET Nome = @Nome, NomeUsuario = @NomeUsuario, Email = @Email, Senha = @Senha, Telefone = @Telefone, CriadoEm = @CriadoEm, AtualizadoEm = @AtualizadoEm, IdAtualizadoPor = @IdAtualizadoPor, IdCriadoPor = @IdCriadoPor,
                                StatusRegistro = @StatusRegistro, Foto = @Foto, IdGoogle = @IdGoogle, IdFacebook = @IdFacebook, PerfilFacebook = @PerfilFacebook, PerfilLinkedin = @PerfilLinkedin, PerfilInstagram = @PerfilInstagram, PerfilTwitter = @PerfilTwitter, Descricao = @Descricao
                            WHERE Id = @Id";

            await _unitOfWork.Connection.ExecuteAsync(sql, user, _unitOfWork?.Transaction);
            return user;
        }

        public async Task DeletarUsuarioAsync(long idUsuario)
        {
            var sql = @" UPDATE tbl_usuario
                            SET StatusRegistro = 1
                            WHERE Id = @Id";
            var obj = new
            {
                Id = idUsuario
            };

            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);

        }

        public async Task<Usuario> GetUsuarioById(long idUsuario)
        {
            var sql = @" SELECT * FROM tbl_usuario WHERE Id = @Id";
            var obj = new Usuario
            {
                Id = idUsuario
            };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Usuario>(sql, obj, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuario(string nomeUsuario) {
            var sql = @" SELECT * FROM tbl_usuario WHERE NomeUsuario LIKE '%' + @NomeUsuario + '%'";
            var obj = new Usuario {
                NomeUsuario = nomeUsuario
            };
            return await _unitOfWork.Connection.QueryAsync<Usuario>(sql, obj, _unitOfWork?.Transaction);
        }

        public IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario)
        {
            using SqlConnection conexao = new(_connectionString);
            string sql = @"SELECT * FROM TBL_USUARIO";
            return conexao.Query<Usuario>(sql);
        }

        public async Task<bool> VerificarExistenciaEmail(string email)
        {
            string sql = @"SELECT count(*) FROM TBL_USUARIO where Email = @email";
            return await _unitOfWork.Connection.ExecuteScalarAsync<bool>(sql, new { email }, _unitOfWork?.Transaction);
        }

        public async Task<bool> LoginAsync(Usuario request)
        {
            string sql = @"SELECT count(*) FROM TBL_USUARIO where Email = @Email and Senha = @Senha";
            return await _unitOfWork.Connection.ExecuteScalarAsync<bool>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            string sql = @"SELECT * FROM TBL_USUARIO where Email = @email";
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { email }, _unitOfWork?.Transaction);
        }

        public async Task AtualizarTokenPush(long idUsuario, string tokenPush)
        {
            string sql = @"UPDATE tbl_usuario
                            SET TokenPush = @TokenPush
                            WHERE Id = @Id";
            var obj = new
            {
                Id = idUsuario,
                TokenPush = tokenPush
            };
            await _unitOfWork.Connection.ExecuteAsync(sql, obj, _unitOfWork?.Transaction);
        }
    }
}
