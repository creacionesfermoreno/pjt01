using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotComers.Models.apiwhatsapp
{


    public class ResponseTemplate
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Button
    {
        public string type { get; set; }
        public string text { get; set; }
        public string url { get; set; }
    }

    public class Component
    {
        public string type { get; set; }
        public string text { get; set; }
        public List<Button> buttons { get; set; }
        public string format { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Datum
    {
        public string name { get; set; }
        public List<Component> components { get; set; }
        public string language { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string id { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
    }

    
}