using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ItemsVentaDTO: AuditoriaDTO
    {      
        public int CodigoItemVenta { get; set; }
        public string Nombre { get; set; }
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
        public List<ItemsVentaInventarioDTO> lista_ItemsVentaInventarioDTO { get; set; }

        public Common.Operation Operation { get; set; }

        public decimal d_CantidadActual { get; set; }
        public decimal d_CostoUnidad { get; set; }

        public decimal d_CostoPromedio { get; set; }
        public decimal d_CostoTotal { get; set; }

    }

    public class ReqItemsVentaDTO : Request //Peticion de un CRUD
    {
        public List<ItemsVentaDTO> List { get; set; }
    }

    public class ReqFilterItemsVentaDTO : Request //Peticion de un List o Items
    {
        public Common.Paging Paging { get; set; }
        public ItemsVentaDTO Item { get; set; }
        public Common.filterCaseItemsVenta FilterCase { get; set; }
    }

    public class RespItemsVentaDTO : Response //respuesta de un CRUD
    {

    }

    public class RespItemItemsVentaDTO : Response
    {
        public ItemsVentaDTO Item { get; set; }
    } //respuesta de un ITEM

    public class RespListItemsVentaDTO : Response
    {
        public List<ItemsVentaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } //respuesta de un LIST
}
