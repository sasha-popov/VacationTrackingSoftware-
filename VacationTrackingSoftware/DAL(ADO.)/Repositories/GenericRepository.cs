using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;

namespace DAL_ADO._.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //protected abstract string TableName { get; }
        //protected abstract TEntity DataRowToModel(DataRow dr);
        private readonly string connectionString = "Server=CH1346\\OPOPOV3;Database=VTS2;Trusted_Connection=True;";
        private SqlConnection sqlConnection;

        public void Create(TEntity entity)
        {
            using (var sqlConnection = new SqlConnection(connectionString)) {

            }
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
