using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasarım1.Areas.Admin.Models
{
    public class Logger
    {
        public int LoggerId { get; set; }
        public string LoggerName { get; set; }
        public string LoggerAction { get; set; }
        public DateTime LoggerDate { get; set; }
        public string LoggerIP { get; set; }
        public string LoggerBrowser { get; set; }
    }
}