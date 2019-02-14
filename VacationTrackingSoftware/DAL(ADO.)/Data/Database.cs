using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.IdentityModel.Protocols;

namespace DAL_ADO._.Data
{
    public class Database
    {
        public static SqlConnection GetConnection() {
            return new SqlConnection("Server=CH1346\\OPOPOV3;Database=VTS2;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
