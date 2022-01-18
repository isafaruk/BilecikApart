using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace tasarım1.Models
{
    public class Apartlar
    {
        public int id { get; set; }
        [DisplayName("Ad")]
        public string ad { get; set; }
        [DisplayName("Telefon")]
        public string telefon { get; set; }
        [DisplayName("Adres")]
        public string adres { get; set; }
        [DisplayName("E-Posta")]
        public string eposta { get; set; }
        [DisplayName("Website")]
        public string website { get; set; }
        [DisplayName("Açıklama")]
        public string aciklama { get; set; }
        public Fotograflar fotograf { get; set; }
        public BinaOzellik binaOzellik { get; set; }
        public OdaOzellik odaOzellik { get; set; }
        public Hizmetler hizmetler { get; set; }   
    }
}