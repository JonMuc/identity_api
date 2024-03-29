﻿using Domain.Models.Enums;
using System;

namespace Domain.Models.Dto
{
    public class ViewComentario
    {
        public long Id { get; set; }
        public long QuantidadeSubComentario { get; set; }
        public long IdComentario { get; set; }
        public long IdUsuario { get; set; }
        public string Mensagem { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
        public DateTime DataComentario { get; set; }
        public long QuantidadeLike { get; set; }
        public long QuantidadeDeslike { get; set; }
        public TipoAvaliacao ComentarioAvaliado { get; set; }
    }
}
