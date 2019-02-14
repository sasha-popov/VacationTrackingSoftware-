﻿using System;
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
    public class CompanyHolidayRepository : ICompanyHolidayRepository
    {

        //UDI-UPDATE, DELETE,INSERT 
        private void OperationUDI(string sqlExpression, List<SqlParameter> parameters = null) {
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
        public void Create(CompanyHoliday entity)
        {
            string sqlExpression = $"INSERT INTO dbo.CompanyHolidays (Date,Description) VALUES (@date,@description)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@date",entity.Date), new SqlParameter("@description", entity.Description) };
            OperationUDI(sqlExpression,sqlParameters);
            
        }

        public void Delete(CompanyHoliday entity)
        {
            //change
            string sqlExpression = $"DELETE FROM dbo.CompanyHolidays WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id)};
            OperationUDI(sqlExpression);            
        }

        public List<CompanyHoliday> FindByDate(DateTime date)
        {
            List<CompanyHoliday> companyHolidays = new List<CompanyHoliday>();
                string sqlExpression = $"Select * from dbo.CompanyHolidays where Date=@date";
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@date", date);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            companyHolidays.Add(new CompanyHoliday() { Id = (int)reader.GetValue(0), Date = (DateTime)reader.GetValue(1), Description = (string)reader.GetValue(2) });
                        }
                    }
                }
            return companyHolidays;
        }

        public List<CompanyHoliday> GetAll()
        {
            List<CompanyHoliday> companyHolidays = new List<CompanyHoliday>();
            string sqlExpression = $"Select * from dbo.CompanyHolidays";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        companyHolidays.Add(new CompanyHoliday() { Id = (int)reader.GetValue(0), Date = (DateTime)reader.GetValue(1), Description = (string)reader.GetValue(2) });
                    }
                }
            }
            return companyHolidays;
        }

        public List<CompanyHoliday> GetAllHolidaysForCurrentYear()
        {
            List<CompanyHoliday> companyHolidays = new List<CompanyHoliday>();
            string sqlExpression = $"Select * from dbo.CompanyHolidays where year(Date)=@currentYear";
            using (var connection = Database.GetConnection())
            {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.Add("@currentYear", SqlDbType.Int);
                    command.Parameters["@currentYear"].Value = DateTime.Today.Year;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            companyHolidays.Add(new CompanyHoliday() { Id = (int)reader.GetValue(0), Date = (DateTime)reader.GetValue(1), Description = (string)reader.GetValue(2) });
                        }
                    }
            }
            return companyHolidays;
        }

        public CompanyHoliday GetById(int id)
        {
            CompanyHoliday companyHoliday = null;
            string sqlExpression = $"Select * from dbo.CompanyHolidays where Id=@id";
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
                        companyHoliday.Id=(int)reader.GetValue(0);
                        companyHoliday.Date = (DateTime)reader.GetValue(1);
                        companyHoliday.Description = (string)reader.GetValue(2);
                    }
                }
            }
            return companyHoliday;
        }

        public void Update(CompanyHoliday entity)
        {
            string sqlExpression = $"UPDATE dbo.CompanyHolidays SET Date=@date, Description =@description WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@date", entity.Date), new SqlParameter("@id", entity.Id), new SqlParameter("@description", entity.Description)};

            OperationUDI(sqlExpression,sqlParameters);
        }

        public void Save()
        {
        }

        public Task SaveAsync()
        {
            return null;
        }


    }
}