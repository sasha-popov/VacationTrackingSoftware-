using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using DAL_ADO._.Data;
using DAL_ADO._.Generic;

namespace DAL_ADO._.Repositories
{
    public class VacationPolicyRepository : GenericMethods, IVacationPolicyRepository
    {
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
            OperationUDI(sqlExpression,sqlParameters);
        }

        public List<VacationPolicy> FindCurrentVacationPolicy(UserVacationRequest userVacationRequest)
        {
            List<VacationPolicy> vacationPolicies = new List<VacationPolicy>();
            string sqlExpressionForYear = "Select DateRecruitment "
                                        + "from dbo.Workers "
                                        + "where UserId = @userId ";
            int workingYears = 0;
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpressionForYear, connection);
                command.Parameters.AddWithValue("@userId", userVacationRequest.User.Id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        workingYears = DateTime.Now.Year - reader.GetDateTime(0).Year;
                    }
                }
            }
            string sqlExpression = "SELECT TOP 2[x].[Id], [x].[Count], [x].[Payments], [x].[VacationTypeId], [x].[WorkingYear], [x.VacationType].[Id], [x.VacationType].[Name] "
                                 + "FROM[VacationPolicies] AS[x] "
                                 + "LEFT JOIN[VacationTypes] AS[x.VacationType] ON[x].[VacationTypeId] = [x.VacationType].[Id] "
                                 + "WHERE([x].[VacationTypeId] = @vacationTypeId) AND([x].[WorkingYear] >= @workingYears) "
                                 + "order by WorkingYear ";
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@vacationTypeId", userVacationRequest.VacationType.Id);
                command.Parameters.AddWithValue("@workingYears", workingYears);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vacationPolicies.Add(new VacationPolicy()
                        {
                            Id = reader.GetInt32(0),
                            Count = reader.GetInt32(1),
                            Payments = reader.GetInt32(2),
                            WorkingYear = reader.GetInt32(4),
                            VacationType = new VacationType() { Id = reader.GetInt32(5), Name = reader.GetString(6) }
                        });
                    }
                }
            }
            return vacationPolicies;
        }


        public List<VacationPolicy> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VacationPolicy> GetAllVacationPoliciesWithTypes()
        {
            List<VacationPolicy> vacationPolicies = new List<VacationPolicy>();
            string sqlExpression = " SELECT * "
                                 + "FROM dbo.VacationPolicies "
                                 + "inner join dbo.VacationTypes on dbo.VacationPolicies.VacationTypeId = dbo.VacationTypes.Id ";

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vacationPolicies.Add(new VacationPolicy()
                        {
                            Id = reader.GetInt32(0),
                            WorkingYear = reader.GetInt32(1),
                            Count = reader.GetInt32(3),
                            Payments = reader.GetInt32(4),
                            VacationType = new VacationType() { Id = reader.GetInt32(5), Name = reader.GetString(6) }
                        });
                    }
                }
            }
            return vacationPolicies;
        }

        public VacationPolicy GetById(int id)
        {
            VacationPolicy vacationPolicy = new VacationPolicy();
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
            string sqlExpression = $"UPDATE dbo.VacationPolicies SET WorkingYear=@workingYear, VacationTypeId=@vacationTypeId,Count=@count,Payments=@payments WHERE Id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@workingYear", entity.WorkingYear),
                new SqlParameter("@vacationTypeId", entity.VacationType.Id),
                new SqlParameter("@count", entity.Count),
                new SqlParameter("@payments", entity.Payments),
                new SqlParameter("@id", entity.Id)};
            OperationUDI(sqlExpression, sqlParameters);
        }
    }
}
