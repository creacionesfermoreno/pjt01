using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class ProductoNuevoViewModel
    {
        public int ProductoID { get; set; }
        public string Title { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioActual { get; set; }
        public string UrlImage { get; set; }
        public string CantidadMaxima { get; set; }
        public List<Images> ListaImagen { get; set; }

        public List<CategoriaProductoViewModel> ListaCategoria { get; set; }
        public List<ProductoNuevoViewModel> ListaProductoNuevo { get; set; }
        public List<BannerProductoViewModel> ListaBanner { get; set; }
        public List<CaracteristicaProductoViewModel> ListaProducto { get; set; }

        public class Images
        {
            public string Descripcion { get; set; }

        }
    }
}
