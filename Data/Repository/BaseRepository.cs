using Domain.Interfaces.Repository;
using System;

namespace Data.Repository
{
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _connectionString = "Data Source=JOAOLUIZ;Initial Catalog=identityDB;Integrated Security=True";
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Connection.Close();
                _unitOfWork.Connection.Dispose();
            }
        }
    }
}
