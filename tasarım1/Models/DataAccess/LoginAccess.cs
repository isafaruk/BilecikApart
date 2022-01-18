using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using tasarım1.Models.Utility;

namespace tasarım1.Models.DataAccess
{

    public class LoginAccess
    {
        static string connString = ConnectionString.CName;

        public static int LoginControl(string kadi,string kpass)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int result = 0;
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kadi", kadi);
                cmd.Parameters.AddWithValue("@password", kpass);
                SqlParameter dnLogin = new SqlParameter();
                dnLogin.ParameterName = "@value";
                dnLogin.SqlDbType = SqlDbType.Int;
                dnLogin.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(dnLogin);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(dnLogin.Value);
                return result;
                             
            }

        }
        public static void IsActive(string userName)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spIsActive", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", userName);
                conn.Open();
                cmd.ExecuteNonQuery();


            }
        }

    }
}