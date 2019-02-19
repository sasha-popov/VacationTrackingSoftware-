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

namespace DAL_ADO._.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private AppUser formOfUser(int skip, SqlDataReader reader) {
            return new AppUser()
            {
                Id = reader.GetString(0 + skip),
                UserName = reader.GetString(1 + skip),
                NormalizedUserName =reader.GetString(2 + skip),
                Email = reader.GetString(3 + skip),
                NormalizedEmail = reader.GetString(4 + skip),
                PasswordHash = reader.GetString(6 + skip),
                SecurityStamp = reader.GetString(7 + skip),
                ConcurrencyStamp = reader.GetString(8 + skip),
                LockoutEnabled = reader.GetBoolean(13 + skip),
                FirstName = reader.GetString(15 + skip),
                LastName = reader.GetString(16 + skip),
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
                    while (reader.Read())
                    {
                        var currentManager = new AppUser();
                        try
                        {
                            currentManager = formOfUser(3, reader);
                        }
                        catch {
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
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", entity.Name), new SqlParameter("@managerId", entity.Manager.Id) };
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
                    cmd.Connection = Database.GetConnection();
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
        //public List<Team> FindByManager(string managerId)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Team> FindTeamsByManager(string managerId)
        {
            List<Team> teams = new List<Team>();
            string sqlExpression = "SELECT * "
                                    + "FROM[Teams] AS[x] "
                                    + "LEFT JOIN[AspNetUsers] AS[x.Manager] ON[x].[ManagerId] = [x.Manager].[Id] "
                                    + "WHERE[x].[ManagerId] = @managerId ";

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
                        teams.Add(new Team() { Id = reader.GetInt32(0), Name = reader.GetString(1), Manager =formOfUser(3,reader)});
                    }
                }
            }
            return teams;
        }

        public List<Team> FindTeamsByManagerForUpdate(string managerId)
        {
            //var getTeamssOfManager = RepositoryContext.Teams.AsNoTracking().Include(x => x.Manager).AsNoTracking().Include(x => x.TeamUsers).AsNoTracking().Include("TeamUsers.User").AsNoTracking().Where(x => x.Manager.Id == managerId).AsNoTracking().ToList();
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
                        teams.Add(new Team() { Id = reader.GetInt32(0), Name = reader.GetString(1), Manager = currentManager });
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
