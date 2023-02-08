using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class DetalleCarritoViewModel
    {
        public int DetalleID { get; set; }
        public string NombreProducto { get; set; }
        public string Cantidad { get; set; }
        public string Eliminar { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string UrlImagen { get; set; }
        public List<DetalleCarritoViewModel> ListaDetalleCarrito { get; set; }
        public List<Images> Imagen { get; set; }
        public List<DetalleCanasta> ListaDetalleCanasta { get; set; }
        public List<ResumenCarrito> ListaResumenCarrito { get; set; }
        public class DetalleCanasta
        {
            public string Descricpcion { get; set; }
            public decimal Precio { get; set; }
            public decimal Total { get; set; }
            public List<Images> ListaImagen { get; set; }
        }
        public class ResumenCarrito
        {
            public decimal SubTotal { get; set; }
            public decimal Descuento { get; set; }
            public decimal Igv { get; set; }
            public decimal Total { get; set; }
            public List<Images> ListaImagen { get; set; }
        }
        public class Images
        {
            public string Descripcion { get; set; }
            public string UrlImage { get; set; }
        }

    }
}
