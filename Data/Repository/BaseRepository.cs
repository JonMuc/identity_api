using Domain.Interfaces.Repository;

namespace Data.Repository
{
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _connectionString = "Server=tcp:noticia-bd.database.windows.net,1433;Initial Catalog=noticia-base;Persist Security Info=False;User ID=user157923admin;Password=nOv5vMJ&n!@cH2l;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //_connectionString = "Data Source=JOAOLUIZ;Initial Catalog=identityDB;Integrated Security=True";
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
