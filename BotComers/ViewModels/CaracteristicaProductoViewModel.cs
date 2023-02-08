using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class CaracteristicaProductoViewModel
    {
        public int ProductoID { get; set; }
        public string NombreCarateristica { get; set; }
        public string Title { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioActual { get; set; }
        public string UrlImage { get; set; }
        public List<CaracteristicaProductoViewModel> ListaCaracteristica { get; set; }
        public List<Images> ListaImagen { get; set; }
    }

    public class Images
    {
        public string Descripcion { get; set; }

        public string UrlImage { get; set; }

    }
}