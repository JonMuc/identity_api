using System;
using System.Data;

namespace Domain.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void CommitTransaction();

        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }
    }
}
