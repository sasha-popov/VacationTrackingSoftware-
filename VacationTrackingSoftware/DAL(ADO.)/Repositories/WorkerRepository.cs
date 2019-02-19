using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL_ADO._.Data;
using DAL_ADO._.Generic;

namespace DAL_ADO._.Repositories
{
    public class WorkerRepository : GenericMethods, IWorkerRepository
    {
        public void Create(Worker entity)
        {
            string sqlExpression = $"INSERT INTO dbo.Workers (DateRecruitment,UserId) VALUES (@dateRecruitment,@userId)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@dateRecruitment", entity.DateRecruitment),
                                                                          new SqlParameter("@userId", entity.User.Id)};
            OperationUDI(sqlExpression, sqlParameters);
        }
        public void Delete(Worker entity)
        {
            string sqlExpression = $"DELETE FROM dbo.Workers WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id) };
            OperationUDI(sqlExpression, sqlParameters);
        }

        public List<Worker> GetAll()
        {
            throw new NotImplementedException();
        }

        public Worker GetById(int id)
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

        public void Update(Worker entity)
        {
            throw new NotImplementedException();
        }
    }
}
