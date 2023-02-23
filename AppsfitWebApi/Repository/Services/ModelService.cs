using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Repository.Services
{
    public class ModelService
    {





    }

    //******************************************** Mercado Pago *************************************************
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    
    public class ResponseMP
    {
        public string external_reference { get; set; }
        public string auto_return { get; set; }
        public string init_point { get; set; }
        public string sandbox_init_point { get; set; }
        public string site_id { get; set; }
        public string id { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string status_detail { get; set; }
        public string error { get; set; }
    }


}
        
        
     

    //******************************************** Mercado Pago *************************************************


