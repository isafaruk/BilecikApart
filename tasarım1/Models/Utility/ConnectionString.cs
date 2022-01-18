using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasarım1.Models.Utility
{
    public class ConnectionString
    {
        private static string cName = "Data Source=.\\SQLEXPRESS; Initial Catalog=apart;integrated security=true";
        public static string CName
        {
            get => cName;
        }
    }
}