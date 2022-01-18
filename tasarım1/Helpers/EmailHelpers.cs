using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using tasarım1.Models;
using tasarım1.Models.Utility;

namespace tasarım1.Helpers
{
    public class EmailHelpers
    {
        static string connString = ConnectionString.CName;

        public static string OnayKodu = "";
        public static void OnayKoduGonder(string mail)
        {
            string bizimMail = "bilecik.apart.test@gmail.com";
            string sifre = "!!Sanane.123";

            Random rastgele = new Random();
            string harfler = "ABCDEFGHIJKLMNOPRSTUVYZWX";
            OnayKodu = "";
            for (int i = 0; i < 6; i++)
            {
                OnayKodu += harfler[rastgele.Next(harfler.Length)];
            }

            MailMessage mesaj = new MailMessage(bizimMail, mail, "Onay Kodu", "Üyeliğinizin onaylanması için geçerli onay kodunuz: " + OnayKodu);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(bizimMail, sifre);
            smtp.Send(mesaj);
            OnayKoduKaydet(mail,OnayKodu);
        }
        public static void OnayKoduKaydet(string mail,string code)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spUserAuthCodeAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KullaniciMail", mail);
                cmd.Parameters.AddWithValue("@Code", code);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static string OnayKoduGoster(string userName)
        {
            User user = new User();
            string onayKodu = String.Empty;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spUserAuthCodeShow", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", userName);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();               
                while (read.Read())
                {
                    user.OnayKodu = read["OnayKodu"].ToString();
                }
            }
            return user.OnayKodu;
        }

        public static void ContactEmail(string name, string email, string subject, string message)
        {
            string bizimMail = "bilecik.apart.test@gmail.com";
            string sifre = "!!Sanane.123";
            MailMessage mesaj = new MailMessage(bizimMail, bizimMail, subject, "Gönderen : "+name + email +"\n"+"Gelen Mesaj" + message);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(bizimMail, sifre);
            smtp.Send(mesaj);
        }



        public static string ForgotPass(string email)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int result = 0;
                string pass = "";
                SqlCommand cmd = new SqlCommand("spControlEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                SqlParameter dnValue = new SqlParameter();
                dnValue.ParameterName = "@value";
                dnValue.SqlDbType = SqlDbType.Int;
                dnValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(dnValue);
                
                SqlParameter dnPass = new SqlParameter();
                dnPass.ParameterName = "@pass";
                dnPass.SqlDbType = SqlDbType.VarChar;
                dnPass.Size = 50;
                dnPass.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(dnPass);
                conn.Open();
                cmd.ExecuteNonQuery();
                pass = Convert.ToString(dnPass.Value);
                result = Convert.ToInt32(dnValue.Value);

                if (result == 1)
                {
                    string bizimMail = "bilecik.apart.test@gmail.com";
                    string sifre = "!!Sanane.123";

                    MailMessage mesaj = new MailMessage(bizimMail, email, "Şifre", "Şifreniz: " + pass);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(bizimMail, sifre);
                    smtp.Send(mesaj);
                    return "E-Posta Başarıyla Gönderildi.";
                }
                else 
                {
                    return "Kayıtlı E-Posta Adresi Bulunamadı.";
                }
                
            }
            
        }

    }
}
