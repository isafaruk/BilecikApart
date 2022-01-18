using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using tasarım1.Models.Utility;

namespace tasarım1.Models.DataAccess
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
        public static IEnumerable<Apartlar> GetAllApartWithPhoto()
        {
            List<Apartlar> aparts = new List<Apartlar>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllApartWithPhoto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    Fotograflar foto = new Fotograflar();
                    Apartlar apart = new Apartlar();
                    apart.id = Convert.ToInt32(read["id"]);
                    apart.ad = read["ad"].ToString();
                    apart.aciklama = read["aciklama"].ToString();
                    foto.anafoto = read["anafoto"].ToString();
                    apart.fotograf = foto;
                    aparts.Add(apart);
                }
                conn.Close();
            }
            return aparts;
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
                apart.fotograf = ApartFotograf(id);
                apart.binaOzellik = ApartBinaOzellik(id);
                apart.odaOzellik = ApartOdaOzellik(id);
                apart.hizmetler = ApartHizmetler(id);
            }
            return apart;
        }

        public static Fotograflar ApartFotograf(int id)
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
                }
            }
            return fotograf;
        }
        public static BinaOzellik ApartBinaOzellik(int id)
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
        public static OdaOzellik ApartOdaOzellik(int id)
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
        public static Hizmetler ApartHizmetler(int id)
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

        public static IEnumerable<Apartlar> GetSearch(string yazi)
        {
            List<Apartlar> aparts = new List<Apartlar>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("spGetSearch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yazi", yazi);
                conn.Open();
                cmd.ExecuteNonQuery();
                
                SqlDataReader read = cmd.ExecuteReader();
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
    }
}