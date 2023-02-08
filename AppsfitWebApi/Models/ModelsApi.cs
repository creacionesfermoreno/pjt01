using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class ModelsApi
    {
    }

    //*************************************** TICKET ******************************
    //SE USA PARA ARMAR EL TICKET HEADER
    public class HeaderItem
    {
        public string ruc { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }

        public string nro { get; set; }
        public string date { get; set; }
        public string hour { get; set; }
        public string customer { get; set; }
        public string address { get; set; }
        public string dni { get; set; }

        public string created { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Debe { get; set; }
        public string Frase { get; set; }
        public string FormaPago { get; set; }
    }

    //SE USA PARA ARMAR EL TICKET BODY
    public class DetailV
    {
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }

    //*************************************** TICKET ******************************

}