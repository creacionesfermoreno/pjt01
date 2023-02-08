using E_DataModel;
using System.Collections.Generic;
using System.Web;
namespace BotComers.ViewModels
{
    public class CategoriasProductosViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int CodigoMenu { get; set; }
        public int CodigoMenuSuperior { get; set; }
        public string Descripcion { get; set; }
        public string UrlUbicacion { get; set; }
        public string UrlImagen { get; set; }
        public string CodigoImagenPortada { get; set; }
        public string Tipo { get; set; }
        public int Orden { get; set; }
        public int Estado { get; set; }
        public string Accion { get; set; }
        public string UsuarioCreacion { get; set; }

        public List<ItemsVentaDTO> listaItemsVenta { get; set; }
        public HttpPostedFileWrapper ImageFile2 { get; set; }
    }
}