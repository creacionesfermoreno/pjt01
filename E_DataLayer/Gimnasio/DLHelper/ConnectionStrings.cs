using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace E_DataLayer.DLHelper
{
    public class ConnectionStrings
    {
        public static string APFITNESS
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString.ToString();
            }
        }
    }
}
