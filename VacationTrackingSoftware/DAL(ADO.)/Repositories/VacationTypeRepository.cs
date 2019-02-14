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
    public class VacationTypeRepository : IVacationTypeRepository
    {

        public void Create(VacationType entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(VacationType entity)
        {
            throw new NotImplementedException();
        }

        public VacationType FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<VacationType> GetAll()
        {
            List<VacationType> vacationTypes = new List<VacationType>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from dbo.VacationTypes", connection);
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

        public void Update(VacationType entity)
        {
            throw new NotImplementedException();
        }
    }
}
