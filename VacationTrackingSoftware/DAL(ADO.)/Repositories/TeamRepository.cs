using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL_ADO._.Data;
using DAL_ADO._.Generic;
using NLog;

namespace DAL_ADO._.Repositories
{
    public class TeamRepository : GenericMethods, ITeamRepository
    {
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
                    while (reader.Read())
                    {
                        var currentManager = new AppUser();
                        try
                        {
                            currentManager = formOfUser(3, reader);
                        }
                        catch(Exception ex) {
                            currentManager = null;
                        }
                        teams.Add(new Team() { Id = reader.GetInt32(0), Name = reader.GetString(1), Manager = currentManager });
                    }
                }
            }
            return teams;
        }

        public void Create(Team entity)
        {
            string sqlExpression = $"INSERT INTO dbo.Teams (Name,ManagerId) VALUES (@name,@managerId)";
            var sqlParameterManagerId = new SqlParameter();
            if (entity.Manager == null) sqlParameterManagerId = new SqlParameter("@managerId", DBNull.Value);
            else sqlParameterManagerId = new SqlParameter("@managerId", entity.Manager.Id);
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", entity.Name),sqlParameterManagerId  };
            OperationUDI(sqlExpression, sqlParameters);
        }

        public void Delete(Team entity)
        {
            string sqlExpression = $"DELETE FROM dbo.Teams WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id) };
            OperationUDI(sqlExpression, sqlParameters);
        }

        public List<Team> FindByListIdTeam(List<int> teamIds)
        {
            List<Team> teams = new List<Team>();

            using (var connection = Database.GetConnection())
            {
                var parameters = new string[teamIds.Count];
                var cmd = new SqlCommand();
                for (int i = 0; i < teamIds.Count; i++)
                {
                    parameters[i] = string.Format("@teamsId{0}", i);
                    cmd.Parameters.AddWithValue(parameters[i], teamIds[i]);
                }
                if (teamIds.Any()) {
                    cmd.CommandText = string.Format("SELECT[x].[Id], [x].[ManagerId], [x].[Name] FROM[Teams] AS[x] WHERE[x].[Id] IN ({0})", string.Join(", ", parameters));
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            teams.Add(new Team() { Id = reader.GetInt32(0), Name = reader.GetString(2), Manager = null });
                        }
                    }
                }
            }
            return teams;
        }
        public List<Team> FindTeamsByManager(string managerId)
        {
            List<Team> teams = new List<Team>();
            string sqlExpression = "SELECT * "
                                 + "FROM[TeamUsers] AS[x.TeamUsers] "
                                 + "LEFT JOIN[AspNetUsers] AS[t.User] ON[x.TeamUsers].[UserId] = [t.User].[Id] "
                                 + "LEFT JOIN [Teams] AS [t.Team] ON [x.TeamUsers].[TeamId] = [t.Team].[Id] "
                                 + "INNER JOIN( "
                                 + "SELECT DISTINCT [x0].[Id] "
                                 + "FROM [Teams] AS [x0] " 
                                 +"LEFT JOIN [AspNetUsers] AS[x.Manager0] ON [x0].[ManagerId] = [x.Manager0].[Id] "
                                 + "WHERE[x0].[ManagerId] = @managerId "
                                 + ") AS[t] ON[x.TeamUsers].[TeamId] = [t].[Id] "
                                 +"ORDER BY[t].[Id] ";
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
                        var teamUserId = reader.GetInt32(0);
                        var teamId = reader.GetInt32(1);
                        var teamName = reader.GetString(23);
                        var team = teams.Where(x => x.Id == teamId).FirstOrDefault();
                        if (team == null)
                        {
                            team = new Team()
                            {
                                Id = teamId,
                                Name = teamName,
                                TeamUsers = new List<TeamUser>()
                            };
                            team.TeamUsers.Add(new TeamUser()
                            {
                                Id=teamUserId,
                                Team = null,
                                User = formOfUser(3, reader)
                            });
                            teams.Add(team);
                        }
                        else {
                            team.TeamUsers.Add(new TeamUser()
                            {
                                Id = teamUserId,
                                Team = null,
                                User = formOfUser(3, reader)
                            });
                        }
                        
                    }
                }
            }
            return teams;
        }

        public List<Team> FindTeamsByManagerForUpdate(string managerId)
        {
            List<Team> teams = new List<Team>();
            string sqlExpression = "SELECT * "
                                 + "FROM[Teams] AS[x] " 
                                 + "LEFT JOIN[AspNetUsers] AS[x.Manager] ON[x].[ManagerId] = [x.Manager].[Id] "
                                 + "WHERE[x].[ManagerId] = @managerId "
                                 +"ORDER BY[x].[Id]";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@managerId",managerId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var currentManager = new AppUser();
                        try
                        {
                            currentManager = formOfUser(3, reader);
                        }
                        catch
                        {
                            currentManager = null;
                        }
                        teams.Add(new Team() { Id = reader.GetInt32(0), Name = reader.GetString(1), Manager = currentManager});
                    }
                }
            }
            return teams;
            throw new NotImplementedException();
        }

        public List<Team> GetAll()
        {
            List<Team> teams = new List<Team>();
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
                        teams.Add(new Team() { Id = reader.GetInt32(0), Name = reader.GetString(1), Manager=null});
                    }
                }
            }
            return teams;
        }

        public Team GetById(int id)
        {
            Team team = new Team();
            string sqlExpression = $"Select * from dbo.Teams where Id=@id";
            using (var connection = Database.GetConnection())
            {
                if (id != 0)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            team.Id = reader.GetInt32(0);
                            team.Manager = null;
                            team.Name = reader.GetString(1);
                        }
                    }
                }
                else {
                    team = null;
                }
            }
            return team;
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
            string sqlExpression = $"UPDATE dbo.Teams SET Name=@name, ManagerId =@managerId WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (entity.Manager == null)
            {
                sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", entity.Name),
                                                                          new SqlParameter("@managerId", DBNull.Value),
                                                                          new SqlParameter("@id",entity.Id)};
            }
            else {
                sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", entity.Name),
                                                                          new SqlParameter("@managerId", entity.Manager.Id),
                                                                          new SqlParameter("@id",entity.Id)};
            }

            OperationUDI(sqlExpression, sqlParameters);
        }
    }
}
