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
    public class TeamUserRepository : ITeamUserRepository
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
        public void Create(TeamUser entity)
        {
            string sqlExpression = $"INSERT INTO dbo.TeamUsers (TeamId,UserId) VALUES (@teamId,@userId)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@teamId", entity.Team.Id), new SqlParameter("@userId", entity.User.Id) };
            OperationUDI(sqlExpression, sqlParameters);
        }

        public void Delete(TeamUser entity)
        {
            string sqlExpression = $"DELETE FROM dbo.TeamUsers WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id) };
            OperationUDI(sqlExpression);
        }

        public TeamUser FindByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> FindForManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<TeamUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TeamUser> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public TeamUser GetById(int id)
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

        public void Update(TeamUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
