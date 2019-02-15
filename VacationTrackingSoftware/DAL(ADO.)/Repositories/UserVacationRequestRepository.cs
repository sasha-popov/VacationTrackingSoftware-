﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL_ADO._.Data;

namespace DAL_ADO._.Repositories
{
    public class UserVacationRequestRepository : IUserVacationRequestRepository
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

        private UserVacationRequest formOfVacationRequest(int skip, SqlDataReader reader)
        {
            return new UserVacationRequest()
            {
                Id = reader.GetInt32(0 + skip),
                StartDate = reader.GetDateTime(1 + skip),
                EndDate = reader.GetDateTime(2 + skip),
                VacationType = new VacationType() { Id = reader.GetInt32(7 + skip), Name = reader.GetString(8 + skip) },
                Payment = reader.GetInt32(4 + skip),
                User = formOfUser(9, reader)
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
        public void Create(UserVacationRequest entity)
        {
            string sqlExpression = $"INSERT INTO dbo.UserVacationRequest (StartDate,EndDate,VacationTypeId,Payment,Status,UserId) VALUES (@startDate,@endDate,@vacationTypeId,@payment,@status,@userId)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@startDate", entity.StartDate),
                                                                          new SqlParameter("@endDate", entity.EndDate),
                                                                          new SqlParameter("@vacationTypeId", entity.VacationType),
                                                                          new SqlParameter("@payment", entity.Payment),
                                                                          new SqlParameter("@status", entity.Status),
                                                                          new SqlParameter("@userId", entity.User.Id)};
            OperationUDI(sqlExpression, sqlParameters);
        }

        public void Delete(UserVacationRequest entity)
        {
            string sqlExpression = $"DELETE FROM dbo.UserVacationRequests WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id) };
            OperationUDI(sqlExpression);
        }

        public List<UserVacationRequest> FindForUser(string userId)
        {
            List<UserVacationRequest> userVacationRequests = new List<UserVacationRequest>();
            string sqlExpression = "SELECT *"
                              + "FROM dbo.UserVacantionRequests"
                              + "Inner join dbo.VacationTypes on dbo.UserVacantionRequests.VacationTypeId = dbo.VacationTypes.Id"
                              + "Inner join dbo.AspNetUsers on dbo.UserVacantionRequests.UserId = dbo.AspNetUsers.Id"
                              + "where dbo.UserVacantionRequests.UserId=@userId";
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
                        userVacationRequests.Add(formOfVacationRequest(0, reader));
                    }
                }
            }
            return userVacationRequests;
        }

        public List<UserVacationRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> GetAllWithTypeHolidays()
        {
            List<UserVacationRequest> userVacationRequests = new List<UserVacationRequest>();
            string sqlExpression = "SELECT *"
                              + "FROM dbo.UserVacantionRequests"
                              + "Inner join dbo.VacationTypes on dbo.UserVacantionRequests.VacationTypeId = dbo.VacationTypes.Id"
                              + "Inner join dbo.AspNetUsers on dbo.UserVacantionRequests.UserId = dbo.AspNetUsers.Id";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userVacationRequests.Add(formOfVacationRequest(0, reader));
                    }
                }
            }
            return userVacationRequests;
        }

        public UserVacationRequest GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> GetForListOfUsers(List<AppUser> users)
        {
            List<UserVacationRequest> userVacationRequests = new List<UserVacationRequest>();
            List<string> userIds = new List<string>(); 
            users.ForEach(x => userIds.Add(x.Id));
            using (var connection = Database.GetConnection())
            {
                var parameters = new string[userIds.Count];
                var cmd = new SqlCommand();
                for (int i = 0; i < userIds.Count; i++)
                {
                    parameters[i] = string.Format("@userIds{0}", i);
                    cmd.Parameters.AddWithValue(parameters[i], userIds[i]);
                }
                cmd.CommandText = string.Format("SELECT *"
                          +"FROM dbo.UserVacantionRequests"
                          + "Inner join dbo.VacationTypes on dbo.UserVacantionRequests.VacationTypeId = dbo.VacationTypes.Id"
                          + "Inner join dbo.AspNetUsers on dbo.UserVacantionRequests.UserId = dbo.AspNetUsers.Id"
                          + "where dbo.UserVacantionRequests.UserId in ({0})", string.Join(", ", parameters));
                cmd.Connection = Database.GetConnection();
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userVacationRequests.Add(formOfVacationRequest(0, reader));
                    }
                }
            }
            return userVacationRequests;
        }

        //public UserVacationRequest GetWithWorker(DateTime startDate, DateTime endDate, string userId)
        //{
        //    throw new NotImplementedException();
        //}

        public void Save()
        {
           
        }

        public Task SaveAsync()
        {
            return null;
        }

        public void Update(UserVacationRequest entity)
        {
            string sqlExpression = $"UPDATE dbo.UserVacationRequests SET StartDate=@startDate, EndDate=@endDate, VacationTypeId=@vacationTypeId,Payment=@payment,Status=@status,UserId=@userId WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@startDate", entity.StartDate),
                                                                          new SqlParameter("@endDate", entity.EndDate),
                                                                          new SqlParameter("@vacationTypeId", entity.VacationType.Id),
                                                                          new SqlParameter("@payment", entity.Payment),
                                                                          new SqlParameter("@status", entity.Status),
                                                                          new SqlParameter("@userId", entity.User.Id)};

            OperationUDI(sqlExpression, sqlParameters);
        }
    }
}
