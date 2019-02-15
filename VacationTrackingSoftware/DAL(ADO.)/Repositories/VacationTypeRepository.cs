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
    public class VacationTypeRepository : IVacationTypeRepository
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
        public void Create(VacationType entity)
        {
            //VacationType does not create
        }

        public void Delete(VacationType entity)
        {
            //VacationType does not delete
        }

        public VacationType FindByName(string name)
        {
            VacationType vacationType = new VacationType();
            string sqlExpression = "SELECT TOP 1 *"
                                + "from dbo.VacationTypes"
                                + "where Name = @name";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vacationType.Id = reader.GetInt32(0);
                        vacationType.Name = reader.GetString(1);
                    }
                }
                reader.Close();
            }
            return vacationType;

        }

        public List<VacationType> GetAll()
        {
            string sqlExpression= "select * from dbo.VacationTypes";
            List<VacationType> vacationTypes = new List<VacationType>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vacationTypes.Add(new VacationType() { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1) });
                    }
                }
                reader.Close();
            }
            return vacationTypes;
        }


        public VacationType GetById(int id)
        {
            return null;
            //this method does not use
        }

        public void Save()
        {
            
        }

        public Task SaveAsync()
        {
            return null;
        }

        public void Update(VacationType entity)
        {
            //VacationType does not update
        }
    }
}
