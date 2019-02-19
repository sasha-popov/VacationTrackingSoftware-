using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL_ADO._.Data;

namespace DAL_ADO._.Generic
{
    public class GenericMethods
    {
        /**UDI-update,delete,insert*/
        public void OperationUDI(string sqlExpression, List<SqlParameter> parameters = null)
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
        public AppUser formOfUser(int skip, SqlDataReader reader)
        {
            return new AppUser()
            {
                Id = reader.GetString(0 + skip),
                UserName = reader.GetString(1 + skip),
                NormalizedUserName = reader.GetString(2 + skip),
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
    }
}
