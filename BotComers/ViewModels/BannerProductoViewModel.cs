using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class BannerProductoViewModel
    {
        public string BannerID { get; set; }
        public string NombreBanner { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string UrlImage { get; set; }
        public List<BannerProductoViewModel> ListaBanner { get; set; }
        public List<Images> Imagen { get; set; }
        public class Images
        {
            public string Descripcion { get; set; }
            public string UrlImage { get; set; }
        }
    }
}
