using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using tasarım1.Models.Utility;

namespace tasarım1.Areas.Admin.Models.DataAccessLayer
{
    public class AdminLogin
    {
        static string connString = ConnectionString.CName;

        public static int Login(string kadi,string kpass)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spAdminLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kAdminName", kadi);
                cmd.Parameters.AddWithValue("@kAdminPass", kpass);
                SqlParameter dnAdminLogin = new SqlParameter();
                dnAdminLogin.ParameterName = "@IsValued";
                dnAdminLogin.SqlDbType = SqlDbType.Int;
                dnAdminLogin.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(dnAdminLogin);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(dnAdminLogin.Value);
                return result;
            }
        }
    }
}