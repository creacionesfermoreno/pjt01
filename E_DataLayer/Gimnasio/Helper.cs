using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace E_DataLayer.Gimnasio
{
    public static class Helper
    {
        public static string Conexion()
        {
            return ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;
        }
    }
}
