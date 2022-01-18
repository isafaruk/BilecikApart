using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using tasarım1.Models;
using tasarım1.Models.Utility;
using static System.Net.WebRequestMethods;

namespace tasarım1.Areas.Admin.Models.DataAccessLayer
{

    public class ApartAccess
    {
        
        static string connString = ConnectionString.CName; //ConnectionString' den çağırma
        

        public static IEnumerable<Apartlar> GetAllApart()
        {
            List<Apartlar> aparts = new List<Apartlar>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllApart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader(); //Veri okuyucu açık
                while (read.Read())
                {
                    Apartlar apart = new Apartlar();
                    apart.id = Convert.ToInt32(read["id"]);
                    apart.ad = read["ad"].ToString();
                    apart.telefon = read["telefon"].ToString();
                    apart.adres = read["adres"].ToString();
                    apart.eposta = read["eposta"].ToString();
                    apart.website = read["website"].ToString();
                    apart.aciklama = read["aciklama"].ToString();
                    aparts.Add(apart);
                }
                conn.Close();
            }
            return aparts;
        }
        public static void AddApart(Apartlar apart, List<HttpPostedFileBase> file)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand("spAddApart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ad", apart.ad);
                cmd.Parameters.AddWithValue("@Telefon", apart.telefon);
                cmd.Parameters.AddWithValue("@Adres", apart.adres);
                cmd.Parameters.AddWithValue("@Eposta", apart.eposta);
                cmd.Parameters.AddWithValue("@Website", apart.website);
                cmd.Parameters.AddWithValue("@Aciklama", apart.aciklama);
                
                int result = AddBuildProp(apart.binaOzellik);
                cmd.Parameters.AddWithValue("@BinaOzellikID", result);
                int result2 = AddRoomProp(apart.odaOzellik);
                cmd.Parameters.AddWithValue("@OdaOzellikID", result2);
                int result3 = AddServices(apart.hizmetler);
                cmd.Parameters.AddWithValue("@HizmetlerID", result3);
                int result4 = AddPhoto(apart.fotograf, file);
                cmd.Parameters.AddWithValue("@FotografID", result4);
                

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static int AddBuildProp(BinaOzellik binaOzellik)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int tableID = 0;
                SqlCommand cmd = new SqlCommand("spAddBuildProp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@asansor", binaOzellik.asansor);
                cmd.Parameters.AddWithValue("@utuOdasi", binaOzellik.utuOdasi);
                cmd.Parameters.AddWithValue("@sporSalonu", binaOzellik.sporSalonu);
                cmd.Parameters.AddWithValue("@etutSalonu", binaOzellik.etutSalonu);
                cmd.Parameters.AddWithValue("@kafeterya", binaOzellik.kafeterya);
                cmd.Parameters.AddWithValue("@oyunOdasi", binaOzellik.oyunOdasi);
                SqlParameter ID = new SqlParameter();
                ID.ParameterName = "@Value";
                ID.SqlDbType = SqlDbType.Int;
                ID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                tableID = (int)ID.Value;
                return tableID;
            }
        }
        public static int AddRoomProp(OdaOzellik OdaOzellik)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int tableID = 0;
                SqlCommand cmd = new SqlCommand("spAddRoomProp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wifi", OdaOzellik.wifi);
                cmd.Parameters.AddWithValue("@balkonluOda", OdaOzellik.balkonluOda);
                cmd.Parameters.AddWithValue("@calismaMasasi", OdaOzellik.calismaMasasi);
                cmd.Parameters.AddWithValue("@tv", OdaOzellik.tv);
                cmd.Parameters.AddWithValue("@miniBuzdolabi", OdaOzellik.miniBuzdolabi);
                cmd.Parameters.AddWithValue("@kisiselDolap", OdaOzellik.kisiselDolap);
                cmd.Parameters.AddWithValue("@dusWc", OdaOzellik.dusWc);
                SqlParameter ID = new SqlParameter();
                ID.ParameterName = "@Value";
                ID.SqlDbType = SqlDbType.Int;
                ID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                tableID = (int)ID.Value;
                return tableID;
            }
        }
        public static int AddServices(Hizmetler hizmetler)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int tableID = 0;
                SqlCommand cmd = new SqlCommand("spAddServices", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@guvenlik", hizmetler.guvenlik);
                cmd.Parameters.AddWithValue("@temizlik", hizmetler.temizlik);
                cmd.Parameters.AddWithValue("@mutfak", hizmetler.mutfak);
                cmd.Parameters.AddWithValue("@kameraSistemi", hizmetler.kameraSistemi);
                cmd.Parameters.AddWithValue("@yemek", hizmetler.yemek);
                SqlParameter ID = new SqlParameter();
                ID.ParameterName = "@Value";
                ID.SqlDbType = SqlDbType.Int;
                ID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                tableID = (int)ID.Value;
                return tableID;
            }
        }
        public static int AddPhoto(Fotograflar fotograf, List<HttpPostedFileBase> file)
        {
           
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int tableID = 0;
                SqlCommand cmd = new SqlCommand("spAddPhoto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                int i = 0;
                foreach (HttpPostedFileBase singleFile in file) 
                {
                    string targetFolder = HttpContext.Current.Server.MapPath("~/Images");
                    string fileName = Path.GetFileNameWithoutExtension(singleFile.FileName);
                    string exe = Path.GetExtension(singleFile.FileName);
                    string fileName2 = fileName + DateTime.Now.ToString("yymmssfff") + exe;
                    string fileName3 = Path.Combine(targetFolder, fileName2);
                    singleFile.SaveAs(fileName3);
                    if (i == 0) 
                    {
                        cmd.Parameters.AddWithValue("@anafoto", fileName2);
                    }
                    else
                    {
                        string foto = "@foto" + i.ToString();
                        cmd.Parameters.AddWithValue(foto, fileName2);
                    }
                    i++;
                }
                
                SqlParameter ID = new SqlParameter();
                ID.ParameterName = "@Value";
                ID.SqlDbType = SqlDbType.Int;
                ID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


                tableID = (int)ID.Value;
                
                return tableID;
            }

        }

        public static Apartlar GetApartlar(int id)
        {
            Apartlar apart = new Apartlar();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sqlQuery = "SELECT * FROM apartlar WHERE id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    apart.id = Convert.ToInt32(read["id"]);
                    apart.ad = read["ad"].ToString();
                    apart.telefon = read["telefon"].ToString();
                    apart.adres = read["adres"].ToString();
                    apart.eposta = read["eposta"].ToString();
                    apart.website = read["website"].ToString();
                    apart.aciklama = read["aciklama"].ToString();
                }
                apart.fotograf = ApartPhoto(id);
                apart.binaOzellik = ApartBuildProp(id);
                apart.odaOzellik = ApartRoomProp(id);
                apart.hizmetler = ApartServices(id);
            }
            return apart;
        }

        public static Fotograflar ApartPhoto(int id)
        {
            Fotograflar fotograf = new Fotograflar();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sqlQuery = "select * from fotograf where id= (SELECT fotograf_id FROM apartlar WHERE id=" + id + ")";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    fotograf.anafoto = read["anafoto"].ToString();
                    fotograf.foto1 = Convert.ToString(read["foto1"]);
                    fotograf.foto2 = Convert.ToString(read["foto2"]);
                    fotograf.foto3 = Convert.ToString(read["foto3"]);
                    fotograf.foto4 = Convert.ToString(read["foto4"]);
                    fotograf.foto5 = Convert.ToString(read["foto5"]);
                    fotograf.foto6 = Convert.ToString(read["foto6"]);
                    fotograf.foto7 = Convert.ToString(read["foto7"]);
                    fotograf.foto8 = Convert.ToString(read["foto8"]);
                    fotograf.foto9 = Convert.ToString(read["foto9"]);
                }
            }
            return fotograf;
        }
        public static BinaOzellik ApartBuildProp(int id)
        {
            BinaOzellik binaOzellik = new BinaOzellik();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sqlQuery = "select * from bina_ozellik where id= (SELECT bina_ozellik_id FROM apartlar WHERE id=" + id + ")";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    binaOzellik.utuOdasi = Convert.ToBoolean(read["utu_odasi"]);
                    binaOzellik.asansor = Convert.ToBoolean(read["asansor"]);
                    binaOzellik.sporSalonu = Convert.ToBoolean(read["spor_salonu"]);
                    binaOzellik.etutSalonu = Convert.ToBoolean(read["etut_salonu"]);
                    binaOzellik.kafeterya = Convert.ToBoolean(read["kafeterya"]);
                    binaOzellik.oyunOdasi = Convert.ToBoolean(read["oyun_odasi"]);

                }
            }
            return binaOzellik;
        }
        public static OdaOzellik ApartRoomProp(int id)
        {
            OdaOzellik odaOzellik = new OdaOzellik();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sqlQuery = "select * from oda_ozellik where id= (SELECT oda_ozellik_id FROM apartlar WHERE id=" + id + ")";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    odaOzellik.wifi = Convert.ToBoolean(read["wifi"]);
                    odaOzellik.balkonluOda = Convert.ToBoolean(read["balkonlu_oda"]);
                    odaOzellik.calismaMasasi = Convert.ToBoolean(read["calisma_masasi"]);
                    odaOzellik.tv = Convert.ToBoolean(read["tv"]);
                    odaOzellik.miniBuzdolabi = Convert.ToBoolean(read["mini_buzdolabi"]);
                    odaOzellik.kisiselDolap = Convert.ToBoolean(read["kisisel_dolap"]);
                    odaOzellik.dusWc = Convert.ToBoolean(read["dus_wc"]);
                }
            }
            return odaOzellik;
        }
        public static Hizmetler ApartServices(int id)
        {
            Hizmetler hizmetler = new Hizmetler();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sqlQuery = "select * from hizmetler where id= (SELECT hizmetler_id FROM apartlar WHERE id=" + id + ")";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    hizmetler.guvenlik = Convert.ToBoolean(read["guvenlik"]);
                    hizmetler.temizlik = Convert.ToBoolean(read["temizlik"]);
                    hizmetler.mutfak = Convert.ToBoolean(read["mutfak"]);
                    hizmetler.kameraSistemi = Convert.ToBoolean(read["kamera_sistemi"]);
                    hizmetler.yemek = Convert.ToBoolean(read["yemek"]);
                }
            }
            return hizmetler;
        }

        public static void DeleteApart(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteApart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void EditApart(Apartlar apart)
        {
            //Apart düzenleme
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spEditApart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", apart.id);
                cmd.Parameters.AddWithValue("@Ad", apart.ad);
                cmd.Parameters.AddWithValue("@Telefon", apart.telefon);
                cmd.Parameters.AddWithValue("@Adres", apart.adres);
                cmd.Parameters.AddWithValue("@Eposta", apart.eposta);
                cmd.Parameters.AddWithValue("@Website", apart.website);
                cmd.Parameters.AddWithValue("@Aciklama", apart.aciklama);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    
}