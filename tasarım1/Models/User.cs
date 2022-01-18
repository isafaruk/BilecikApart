using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasarım1.Models
{
    public class User
    {
        public int id { get; set; }
        public string kullaniciadi { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public int yetki { get; set; }
        public int telefon { get; set; }
        public string eposta { get; set; }
        public string sifre { get; set; }
        public string OnayKodu { get; set; }
        public enum KullaniciDurum
        {
            True = 1,
            False = 0
        }
        public enum KullaniciYetki { 
            Admin=0,
            Moderator=1,
            Kullanici=2
        }
    }
}