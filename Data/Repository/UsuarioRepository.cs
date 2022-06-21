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

        public async Task<long> SeguirUsuario(Crz_SeguirUsuario usuario)
        {
            var sql = @" INSERT INTO crz_seguir_usuario (IdUsuarioSeguidor, IdUsuarioSeguido)
                                    VALUES (@Id, @IdUsuarioSeguido)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, usuario, _unitOfWork?.Transaction);
        }

        public async Task<long> DeseguirUsuario(long idUsuarioDeseguido, long idUsuarioDeseguindo)
        {
            var sql = @" delete CRZ_SEGUIR_USUARIO where IdUsuarioSeguidor = @idUsuarioDeseguindo AND IdUsuarioSeguido = @idUsuarioDeseguido";

            await _unitOfWork.Connection.ExecuteAsync(sql, new { idUsuarioDeseguido = idUsuarioDeseguido, idUsuarioDeseguindo = idUsuarioDeseguindo }, _unitOfWork?.Transaction);
            return idUsuarioDeseguindo;
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

        public async Task<IEnumerable<Usuario>> VisualizarSeguidores(long idUsuario)
        {
            //SELECT CRZ_SEGUIR_USUARIO.IdUsuarioSeguidor FROM crz_seguir_usuario WHERE IdUsuarioSeguido = 10
            var sql = @" SELECT usuario.Descricao, usuario.NomeUsuario, usuario.Nome, usuario.Id FROM crz_seguir_usuario crz
                        INNER JOIN TBL_USUARIO usuario on crz.IdUsuarioSeguidor = usuario.Id
                        WHERE crz.IdUsuarioSeguido = @Id";

            var obj = new Usuario
            {
                Id = idUsuario
            };
            return await _unitOfWork.Connection.QueryAsync<Usuario>(sql, obj, _unitOfWork?.Transaction);
        }

        public async Task<IEnumerable<Usuario>> VisualizarSeguindo(long idUsuario)
        {
            //SELECT CRZ_SEGUIR_USUARIO.IdUsuarioSeguidor FROM crz_seguir_usuario WHERE IdUsuarioSeguido = 10
            var sql = @" SELECT usuario.Descricao, usuario.NomeUsuario, usuario.Nome, usuario.Id FROM crz_seguir_usuario crz
                        INNER JOIN TBL_USUARIO usuario on crz.IdUsuarioSeguido = usuario.Id
                        WHERE crz.IdUsuarioSeguidor = @Id";

            var obj = new Usuario
            {
                Id = idUsuario
            };
            return await _unitOfWork.Connection.QueryAsync<Usuario>(sql, obj, _unitOfWork?.Transaction);
        }

        public async Task<Usuario> VisualizarPerfilUsuario(long idUsuario)
        {
            var sql = @" select us.NomeUsuario, us.Descricao, us.Nome, us.Foto, us.PerfilFacebook, us.PerfilInstagram, us.PerfilLinkedin, us.PerfilTwitter,
                        (SELECT count(*) from CRZ_SEGUIR_USUARIO where IdUsuarioSeguido = us.Id and StatusRegistro = 0) as quantidadeSeguidores,
                        (SELECT count(*) from CRZ_SEGUIR_USUARIO where IdUsuarioSeguidor = us.Id and StatusRegistro = 0) as quantidadeSeguindo
                        from TBL_USUARIO us where us.Id = @Id";

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

        public async Task<bool> VerificarExistenciaNomeUser(string nomeUser)
        {
            string sql = @"SELECT count(*) FROM TBL_USUARIO where NomeUsuario = @nomeUser";
            return await _unitOfWork.Connection.ExecuteScalarAsync<bool>(sql, new { nomeUser }, _unitOfWork?.Transaction);
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
