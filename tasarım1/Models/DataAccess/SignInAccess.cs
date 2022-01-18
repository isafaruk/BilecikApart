using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using tasarım1.Helpers;
using tasarım1.Models.Utility;

namespace tasarım1.Models.DataAccess
{
    public class SignInAccess
    {
        static string connString = ConnectionString.CName;

        public static int UserAdd(string ad, string soyad, string kullaniciadi, string telefon, string eposta, string passw)
        {

            int result = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spUserAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ad", ad);
                cmd.Parameters.AddWithValue("@Soyad", soyad);
                cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciadi);
                cmd.Parameters.AddWithValue("@Telefon", telefon);
                cmd.Parameters.AddWithValue("@Eposta", eposta);
                cmd.Parameters.AddWithValue("@Pass", passw);
                SqlParameter controlSignIn = new SqlParameter();
                controlSignIn.ParameterName = "@Value";
                controlSignIn.SqlDbType = SqlDbType.Int;
                controlSignIn.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(controlSignIn);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(controlSignIn.Value);
                return result;
                //conn.Close();
            }
        }

        public static int ControlPass(string name, string pass)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spControlPass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Pass", pass);
                SqlParameter controlPass = new SqlParameter();
                controlPass.ParameterName = "@Value";
                controlPass.SqlDbType = SqlDbType.Int;
                controlPass.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(controlPass);
                conn.Open();
                cmd.ExecuteNonQuery();
                int result = Convert.ToInt32(controlPass.Value);
                return result;
            }
        }

        public static void ChangePass(string name, string pass)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spChangePass", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Pass", pass);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}