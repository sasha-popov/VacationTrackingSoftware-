using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL_ADO._.Data;

namespace DAL_ADO._.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private void OperationUDI(string sqlExpression, List<SqlParameter> parameters = null)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                int number = command.ExecuteNonQuery();
                connection.Close();
            }
        }
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
