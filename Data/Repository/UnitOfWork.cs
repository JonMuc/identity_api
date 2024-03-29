﻿using Domain.Interfaces.Repository;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IDbConnection connection)
        {            
            Connection = new SqlConnection("Server=tcp:noticia-bd.database.windows.net,1433;Initial Catalog=noticia-base;Persist Security Info=False;User ID=user157923admin;Password=nOv5vMJ&n!@cH2l;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public void BeginTransaction()
        {
            if (Transaction == null)
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Dispose();
            }
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;

                Connection.Close();
                Connection.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
