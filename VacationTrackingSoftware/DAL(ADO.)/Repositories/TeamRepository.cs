using System;
using System.Collections.Generic;
using System.Data;
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
        private AppUser formOfUser(int skip, SqlDataReader reader) {
            return new AppUser()
            {
                Id = (string)reader.GetValue(0 + skip),
                UserName = (string)reader.GetValue(1 + skip),
                NormalizedUserName = (string)reader.GetValue(2 + skip),
                Email = (string)reader.GetValue(3 + skip),
                NormalizedEmail = (string)reader.GetValue(4 + skip),
                PasswordHash = (string)reader.GetValue(6 + skip),
                SecurityStamp = (string)reader.GetValue(7 + skip),
                ConcurrencyStamp = (string)reader.GetValue(8 + skip),
                LockoutEnabled = (bool)reader.GetValue(13 + skip),
                FirstName = (string)reader.GetValue(15 + skip),
                LastName = (string)reader.GetValue(16 + skip),
            };
        }
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
            List<Team> teams = new List<Team>();
            string sqlExpression = "SELECT * FROM dbo.Teams LEFT JOIN dbo.AspNetUsers on AspNetUsers.Id = Teams.ManagerId";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.HasRows)
                    {
                        teams.Add(new Team() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Manager = formOfUser(3, reader) });
                    }
                }
            }
            return teams;
        }

        public void Create(Team entity)
        {

            string sqlExpression = $"INSERT INTO dbo.Teams (Name,ManagerId) VALUES (@name,@managerId)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", entity.Name), new SqlParameter("@managerId", entity.Manager.Id) };
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
            List<Team> teams = new List<Team>();

            using (var connection = Database.GetConnection())
            {
                var parameters = new string[teamsId.Count];
                var cmd = new SqlCommand();
                for (int i = 0; i < teamsId.Count; i++)
                {
                    parameters[i] = string.Format("@teamsId{0}", i);
                    cmd.Parameters.AddWithValue(parameters[i], teamsId[i]);
                }
                cmd.CommandText = string.Format("SELECT[x].[Id], [x].[ManagerId], [x].[Name] FROM[Teams] AS[x] WHERE[x].[Id] IN ({0})", string.Join(", ", parameters));
                cmd.Connection = Database.GetConnection();
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teams.Add(new Team() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Manager = null });
                    }
                }
            }
            return teams;
        }
        //public List<Team> FindByManager(string managerId)
        //{
        //    throw new NotImplementedException();
        //}

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
                    while (reader.Read())
                    {
                        companyHolidays.Add(new Team() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Manager=null});
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
