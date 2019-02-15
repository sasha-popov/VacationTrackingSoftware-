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
        private AppUser formOfUser(int skip, SqlDataReader reader)
        {
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
            TeamUser teamUser = new TeamUser();
            string sqlExpression = "SELECT TOP(1) *"
                                    + "FROM[TeamUsers] AS[x]"
                                    + "LEFT JOIN[AspNetUsers] AS[x.User] ON[x].[UserId] = [x.User].[Id]"
                                    + "LEFT JOIN[Teams] AS[x.Team] ON[x].[TeamId] = [x.Team].[Id]"
                                    + "WHERE[x].[UserId] = @userId";

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teamUser.Id=(int)reader.GetValue(0);
                        teamUser.Team.Id = (int)reader.GetValue(1);
                        teamUser.Team.Name = (string)reader.GetValue(23);
                        teamUser.User = formOfUser(3, reader);
                    }
                }
            }
            return teamUser;
        }

        public List<AppUser> FindForManager(string managerId)
        {
            List<AppUser> users = new List<AppUser>();
            string sqlExpression = "SELECT *"
                                    + "FROM[TeamUsers] AS[x]"
                                    + "LEFT JOIN[AspNetUsers] AS[x.User] ON[x].[UserId] = [x.User].[Id]"
                                    + "LEFT JOIN[Teams] AS[x.Team] ON[x].[TeamId] = [x.Team].[Id]"
                                    + "LEFT JOIN[AspNetUsers] AS[x.Team.Manager] ON[x.Team].[ManagerId] = [x.Team.Manager].[Id]"
                                    + "WHERE[x.Team.Manager].[Id] = @managerId";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@managerId", managerId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        users.Add(formOfUser(3, reader));
                    }
                }
            }
            return users;
        }

        public List<TeamUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TeamUser> GetAllWithDetails()
        {
            List<TeamUser> teamUsers = new List<TeamUser>();
            string sqlExpression = "SELECT *" +
                                    "FROM[TeamUsers] AS[x]" +
                                    "LEFT JOIN[AspNetUsers] AS[x.User] ON[x].[UserId] = [x.User].[Id]" +
                                    "LEFT JOIN[Teams] AS[x.Team] ON[x].[TeamId] = [x.Team].[Id]";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Team team = new Team() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(23) };
                        teamUsers.Add(new TeamUser() {Id=(int)reader.GetValue(0),Team=team,User=formOfUser(3,reader) });
                    }
                }
            }
            return teamUsers;
        }

        public TeamUser GetById(int id)
        {
            TeamUser teamUser = null;
            string sqlExpression = $"Select * from dbo.TeamUsers where Id=@id";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teamUser.Id = (int)reader.GetValue(0);
                        teamUser.Team = null;
                        teamUser.User = null;
                    }
                }
            }
            return teamUser;
        }

        public void Save()
        {
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TeamUser entity)
        {
            string sqlExpression = $"UPDATE dbo.TeamUsers SET TeamId=@teamId, UserId=@userId WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@teamId", entity.Team.Id), new SqlParameter("@id", entity.Id), new SqlParameter("@userId", entity.User.Id) };
            OperationUDI(sqlExpression, sqlParameters);
        }
    }
}
