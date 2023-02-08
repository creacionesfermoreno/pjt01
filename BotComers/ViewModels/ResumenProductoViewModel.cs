using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class ResumenProductoViewModel
    {
        public int ResumenID { get; set; }
        public string Descricpcion { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Inpuestos { get; set; }

        public List<Images> ListaImagen { get; set; }
        public class Images
        {
            public string Descripcion { get; set; }
            public string UrlImage { get; set; }
        }

    }
}
