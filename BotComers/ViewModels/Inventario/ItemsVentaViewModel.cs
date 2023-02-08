using E_DataModel;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace BotComers.ViewModels.Inventario
{
    public class ItemsVentaViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int CodigoItemVenta { get; set; }
        public string Nombre { get; set; }

        public HttpPostedFile ImageFile { get; set; }
        public HttpPostedFileWrapper ImageFile2 { get; set; }

        public Decimal PrecioVenta { get; set; }
        public Decimal PrecioTotal { get; set; }
        public int CodigoTipoImpuesto { get; set; }
        public int CodigoUnidadMedida { get; set; }
        public int CodigoTipoItem { get; set; }
        public int CodigoAlmacen { get; set; }
        public int ItemInventariable { get; set; }
        public string Referencia { get; set; }
        public string Descripcion { get; set; }
        public int CodigoCategoriaItem { get; set; }
        public string CodigoProductoSUNAT { get; set; }
        public int CodigoCuentaContable { get; set; }
        public string UrlImagen { get; set; }
        public string CodigoImagen { get; set; }
        public int Estado { get; set; }
        public int VisualizarTiendaVirtual { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Accion { get; set; }
        public List<ItemsVentaInventarioDTO> lista_ItemsVentaInventarioDTO { get; set; }
        public List<SelectListItem> ListAlmacenes { get; set; }
        public List<SelectListItem> ListCategorias { get; set; }
        public List<CategoriasDTO> ListCategoriasDTO { get; set; }
        public decimal d_CantidadActual { get; set; }
        public decimal d_CostoUnidad { get; set; }

        public decimal d_CostoPromedio { get; set; }
        public decimal d_CostoTotal { get; set; }

    }


}