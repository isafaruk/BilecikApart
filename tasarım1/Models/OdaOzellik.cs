using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace tasarım1.Models
{
    public class OdaOzellik
    {
        public int OdaOzellikID { get; set; }
        [DisplayName("Wi-Fi")]
        public bool wifi { get; set; }
        [DisplayName("Balkonlu Oda")]
        public bool balkonluOda { get; set; }
        [DisplayName("Çalışma Masası")]
        public bool calismaMasasi { get; set; }
        [DisplayName("TV")]
        public bool tv { get; set; }
        [DisplayName("Mini Buzdolabı")]
        public bool miniBuzdolabi { get; set; }
        [DisplayName("Kişisel Dolap")]
        public bool kisiselDolap { get; set; }
        [DisplayName("Duş-WC")]
        public bool dusWc { get; set; }
    }
}