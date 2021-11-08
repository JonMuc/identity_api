﻿using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IComentarioRepository
    {
        Task<long> AdicionarAsync(Comentario request);
        Task<IEnumerable<Comentario>> ListarComentariosNoticiaAsync(long idUsuarioa);
    }
}
