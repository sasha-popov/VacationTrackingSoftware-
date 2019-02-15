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
    public class VacationPolicyRepository : IVacationPolicyRepository
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
        public void Create(VacationPolicy entity)
        {
            string sqlExpression = $"INSERT INTO dbo.VacationPolicies (WorkingYear,VacationTypeId,Count,Payments) VALUES (@workingYear,@vacationTypeId,@count,@payments)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@workingYear", entity.WorkingYear),
                new SqlParameter("@vacationTypeId", entity.VacationType.Id),
                new SqlParameter("@count", entity.Count),
                new SqlParameter("@payments", entity.Payments)};
            OperationUDI(sqlExpression, sqlParameters);
        }

        public void Delete(VacationPolicy entity)
        {
            string sqlExpression = $"DELETE FROM dbo.VacationPolicies WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@id", entity.Id) };
            OperationUDI(sqlExpression);
        }

        public List<VacationPolicy> FindCurrentVacationPolicy(UserVacationRequest userVacationRequest)
        {
            throw new NotImplementedException();
        }


        public List<VacationPolicy> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VacationPolicy> GetAllVacationPoliciesWithTypes()
        {
            List<VacationPolicy> vacationPolicies = new List<VacationPolicy>();
            string sqlExpression = " SELECT *"
                                 + "FROM dbo.VacationPolicies"
                                 + "inner join dbo.VacationTypes on dbo.VacationPolicies.VacationTypeId = dbo.VacationTypes.Id";

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);                
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vacationPolicies.Add(new VacationPolicy() {Id=reader.GetInt32(0),WorkingYear=reader.GetInt32(1),Count=reader.GetInt32(3),Payments= reader.GetInt32(4),
                                                                   VacationType =new VacationType() {Id= reader.GetInt32(5),Name=reader.GetString(6) } });
                    }
                }
            }
            return vacationPolicies;
        }

        public VacationPolicy GetById(int id)
        {
            VacationPolicy vacationPolicy = null;
            string sqlExpression = $"Select * from dbo.VacationPolicies where Id=@id";
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
                        vacationPolicy.Id = reader.GetInt32(0);
                        vacationPolicy.WorkingYear = reader.GetInt32(1);
                        vacationPolicy.VacationType = null;
                        vacationPolicy.Count = reader.GetInt32(3);
                        vacationPolicy.Payments = reader.GetInt32(4);
                        }
                }
            }
            return vacationPolicy;
        }

        public void Save()
        {

        }

        public Task SaveAsync()
        {
            return null;
        }

        public void Update(VacationPolicy entity)
        {
            string sqlExpression = $"UPDATE dbo.VacaiomPolicies SET WorkingYear=@workingYear, VacationTypeId=@vacationTypeId,Count=@count,Payments=@payments WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@workingYear", entity.WorkingYear),
                new SqlParameter("@vacationTypeId", entity.VacationType.Id),
                new SqlParameter("@count", entity.Count),
                new SqlParameter("@payments", entity.Payments),
                new SqlParameter("@id", entity.Id)};
            OperationUDI(sqlExpression, sqlParameters);
        }
    }
}
