using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class CategoryApiModel
    {
        public int CodigoMenu { get; set; }
        public int CodigoMenuSuperior { get; set; }
        public string Descripcion { get; set; }
        public string UrlUbicacion { get; set; }
        public string UrlImagen { get; set; }
        public string CodigoImagenPortada { get; set; }
    }
}