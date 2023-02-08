using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace E_DataLayer
{
    public static class Helper
    {
        public static string Conexion()
        {
            //var sqlconnectstring = new SqlConnectionStringBuilder();
            ////  sqlconnectstring.IntegratedSecurity = true;
            ////  sqlconnectstring.IntegratedSecurity = true;
            ////  sqlconnectstring.IntegratedSecurity = true;
            //sqlconnectstring.InitialCatalog = "appsfit";
            //sqlconnectstring.DataSource = "tcp:appsfitserver.database.windows.net,1433";
            //sqlconnectstring.UserID = "appsfit";
            //sqlconnectstring.Password = "!Hx&w@-_TrEtV2-";
            //return sqlconnectstring.ToString();

            return ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString;
        }
    }
}
