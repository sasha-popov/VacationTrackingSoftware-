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
    public class TeamRepository : ITeamRepository
    {
        //UDI-UPDATE, DELETE,INSERT 
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
        public List<Team> AllTeamsWithManager()
        {
            throw new NotImplementedException();
        }

        public void Create(Team entity)
        {
            
            string sqlExpression = $"INSERT INTO dbo.Teams (Name,ManagerId) VALUES (@name,@managerId)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", entity.Name), new SqlParameter("@managerId", "fd38c574-4f9f-4e57-9e28-af1ff7c476b8") };
            OperationUDI(sqlExpression, sqlParameters);
        }

        public void Delete(Team entity)
        {
            string sqlExpression = $"DELETE FROM dbo.Teams WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id) };
            OperationUDI(sqlExpression);
        }

        public List<Team> FindByListIdTeam(List<int> teamsId)
        {
            throw new NotImplementedException();
        }

        public List<Team> FindByManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<Team> FindTeamsByManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<Team> FindTeamsByManagerForUpdate(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<Team> GetAll()
        {
            List<Team> companyHolidays = new List<Team>();
            string sqlExpression = $"Select * from dbo.Teams";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.HasRows)
                    {
                        companyHolidays.Add(new Team() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1)/*, Manager = (string)reader.GetValue(2)*/ });
                    }
                }
            }
            return companyHolidays;
        }

        public Team GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
        }

        public Task SaveAsync()
        {
            return null;
        }

        public void Update(Team entity)
        {
            throw new NotImplementedException();
        }
    }
}
