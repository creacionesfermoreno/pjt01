using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotComers.Models.apiwhatsapp
{
   

    public class ResponseItemTemplate
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

}