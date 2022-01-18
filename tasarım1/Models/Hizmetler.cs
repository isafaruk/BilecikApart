using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace tasarım1.Models
{
    public class Hizmetler
    {
        public int HizmetlerID { get; set; }
        [DisplayName("Güvenlik")]
        public bool guvenlik { get; set; }
        [DisplayName("Temizlik")]
        public bool temizlik { get; set; }
        [DisplayName("Mutfak")]
        public bool mutfak { get; set; }
        [DisplayName("Kamera Sistemi")]
        public bool kameraSistemi { get; set; }
        [DisplayName("Yemek")]
        public bool yemek { get; set; }

    }
}