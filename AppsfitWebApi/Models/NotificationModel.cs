
using AppsfitWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace AppsfitWebApi.Models
{
    public class NotificationModel
    {
        [Required]
        public string token { get; set; }
        public string priority { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string body { get; set; }
        public Data data { get; set; }
        public List<string> tokens { get; set; }


    }
    public class Data
    {
        public string type { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public string email { get; set; }

    }

    public class ResponseAdminFirebase
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