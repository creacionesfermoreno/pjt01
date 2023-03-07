
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
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string UrlImage { get; set; }
       // public Data data { get; set; }

        [Required]
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

    public class NotiApp
    {
        [Required]
        public string DefaultKeyEmpresa { get; set; }
        [Required]
        public string DefaultKeyUser { get; set; }
        
    }

    public class NotiApi
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string UrlImage { get; set; }
        public string DescriptionHtml { get; set; }
        public bool Read { get; set; }
        public string DescFechaCreacion { get; set; }
        public int CodigoNotificacionesAppDestinatarios { get; set; }
        public string IdUser { get; set; }
        public string CodigoNotificacionesApp { get; set; }

    }

    public class NotiRead
    {
        [Required]
        public int CodigoUnidadNegocio { get; set; }
        [Required]
        public int CodigoSede { get; set; }
        [Required]
        public string CodigoNotificacionesApp { get; set; }
        [Required]
        public int CodigoNotificacionesAppDestinatarios { get; set; }
        [Required]
        public string IdUser { get; set; }
    }

}