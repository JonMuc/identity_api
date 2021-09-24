﻿using Dapper;
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
            var sql = @" INSERT INTO tbl_usuario (Nome, Email, Senha, Telefone, CriadoEm, AtualizadoEm, IdAtualizadoPor, IdCriadoPor, Foto, IdGoogle, IdFacebook, PerfilLinkedin, PerfilInstagram, PerfilTwitter, Descricao)
                                    VALUES (@Nome, @Email, @Senha, @Telefone, @CriadoEm, @AtualizadoEm, @IdAtualizadoPor, @IdCriadoPor, @Foto, @IdGoogle, @IdFacebook, @PerfilLinkedin, @PerfilInstagram, @PerfilTwitter, @Descricao)
                         SELECT @@IDENTITY";

            return await _unitOfWork.Connection.ExecuteScalarAsync<long>(sql, request, _unitOfWork?.Transaction);
        }

        public async Task<Usuario> AtualizarUsuarioAsync(Usuario user)
        {
            var sql = @" UPDATE tbl_usuario
                            SET Nome = @Nome, Email = @Email, Senha = @Senha, Telefone = @Telefone, CriadoEm = @CriadoEm, AtualizadoEm = @AtualizadoEm, IdAtualizadoPor = @IdAtualizadoPor, IdCriadoPor = @IdCriadoPor,
                                Foto = @Foto, IdGoogle = @IdGoogle, IdFacebook = @IdFacebook, PerfilLinkedin = @PerfilLinkedin, PerfilInstagram = @PerfilInstagram, PerfilTwitter = @PerfilTwitter, Descricao = @Descricao
                            WHERE Id = @Id";

            await _unitOfWork.Connection.ExecuteAsync(sql, user, _unitOfWork?.Transaction);
            return user;
        }

        public async Task DeletarUsuarioAsync(long idUsuario)
        {
            var sql = @" DELETE FROM tbl_usuario WHERE Id = @Id";
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

        public IEnumerable<Usuario> BuscarUsuarioPorId(long idUsuario)
        {
            using SqlConnection conexao = new(_connectionString);
            string sql = @"SELECT * FROM TBL_USUARIO";
            return conexao.Query<Usuario>(sql);
        }
    }
}
