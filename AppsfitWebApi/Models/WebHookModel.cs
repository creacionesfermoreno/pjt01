using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class WebHookModel
    {
    }

    public class WHCulqiSuscription
    {
        public string @object { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string idSubscription { get; set; }
        public long creation_date { get; set; }
        public string data { get; set; }
    }
}