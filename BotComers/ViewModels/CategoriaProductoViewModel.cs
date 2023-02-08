using System.Collections.Generic;

namespace BotComers.ViewModels
{
    public class CategoriaProductoViewModel
    {
        public int CategoriaID { get; set; }
        public string NombreCategoria { get; set; }

        public List<CatalogoProductoViewModel> ListaProducto { get; set; }
        public List<ProductoNuevoViewModel> ListaProductoNuevo { get; set; }
        public List<CaracteristicaProductoViewModel> ListaProductCate { get; set; }
        public List<UsuariosViewModel> ListaUsarios { get; set; }
        public List<DetalleCarritoViewModel> ListaCarrito { get; set; }
    }

}