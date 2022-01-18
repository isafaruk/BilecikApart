using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace tasarım1.Models
{
    public class BinaOzellik
    {
        public int BinaOzellikID { get; set; }
        [DisplayName("Asansör")]
        public bool asansor { get; set; }
        [DisplayName("Ütü Odası")]
        public bool utuOdasi { get; set; }

        [DisplayName("Spor Salonu")]
        public bool sporSalonu { get; set; }
        [DisplayName("Etüt Salonu")]
        public bool etutSalonu { get; set; }
        [DisplayName("Kafeterya")]
        public bool kafeterya { get; set; }
        [DisplayName("Oyun Odası")]
        public bool oyunOdasi { get; set; }
        
    }
}