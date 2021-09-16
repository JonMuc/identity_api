﻿using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.AppServices
{
    public class UsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public ResponseViewModel AdicionarUsuario(Usuario usuario)
        {
            return _usuarioService.AdicionarUsuario(usuario);
        }

        //public ResponseViewModel ListarPessoaFisica()
        //{
        //    return _usuarioService.ListarPessoaFisica();
        //}

        //public ResponseViewModel AtualizarPessoaFisica(PessoaFisica pessoa)
        //{
        //    return _usuarioService.AtualizarPessoaFisica(pessoa);
        //}

        //public ResponseViewModel DeletarPessoaFisica(PessoaFisica pessoa)
        //{
        //    return _usuarioService.DeletarPessoaFisica(pessoa);
        //}

        //public ResponseViewModel BuscarPessoaFisica(long idPessoa)
        //{
        //    return _usuarioService.BuscarPessoaFisica(idPessoa);
        //}
    }
}