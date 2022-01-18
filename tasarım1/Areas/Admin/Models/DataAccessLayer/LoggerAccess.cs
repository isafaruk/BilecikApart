using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using tasarım1.Models.Utility;

namespace tasarım1.Areas.Admin.Models.DataAccessLayer
{
    public class LoggerAccess
    {
        static string connString = ConnectionString.CName; //ConnectionString' den çağırma

        public static IEnumerable<Logger> GetAllLogger()
        {
            List<Logger> users = new List<Logger>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spGetLogger", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader(); //Veri okuyucu açık
                while (read.Read())
                {
                    Logger user = new Logger();
                    user.LoggerId = Convert.ToInt32(read["LoggerId"]);
                    user.LoggerName = read["LoggerName"].ToString();
                    user.LoggerAction = read["LoggerAction"].ToString();
                    user.LoggerDate = (DateTime)read["LoggerDate"];
                    user.LoggerIP = read["LoggerIP"].ToString();
                    user.LoggerBrowser = read["LoggerBrowser"].ToString();
                    users.Add(user);
                }
                conn.Close();
            }
            return users;
        }
    }
}