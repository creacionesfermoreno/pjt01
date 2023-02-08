using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ItemsVentaAjusteInventarioDetalleDTO:AuditoriaDTO
    {       
        public int CodigoItemsVentaAjusteInventario { get; set; }
        public int CodigoItemsVentaAjusteInventarioDetalle { get; set; }
        public int CodigoItemVenta { get; set; }
        public int CodigoAlmacen { get; set; }

        public string NombreItemVenta { get; set; }
        public string UrlImagen { get; set; }

        public Decimal CantidadActual { get; set; }
        public int CodigoTipoAjuste { get; set; } 
        public string DesTipoAjuste { get; set; }
        public Decimal CantidadAjuste { get; set; }
        public Decimal CantidadFinal { get; set; }
        public Decimal CostoUnidad { get; set; }
        public Decimal TotalAjuste { get; set; }
        public int Estado { get; set; }    
        public Common.Operation Operation { get; set; }
    }


    public class ReqItemsVentaAjusteInventarioDetalleDTO : Request
    {
        public List<ItemsVentaAjusteInventarioDetalleDTO> List { get; set; }
    }

    public class ReqFilterItemsVentaAjusteInventarioDetalleDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public ItemsVentaAjusteInventarioDetalleDTO Item { get; set; }
        public Common.filterCaseItemsVentaAjusteInventarioDetalle FilterCase { get; set; }
    }

    public class RespItemsVentaAjusteInventarioDetalleDTO : Response
    {

    }

    public class RespItemItemsVentaAjusteInventarioDetalleDTO : Response
    {
        public ItemsVentaAjusteInventarioDetalleDTO Item { get; set; }
    }

    public class RespListItemsVentaAjusteInventarioDetalleDTO : Response
    {
        public List<ItemsVentaAjusteInventarioDetalleDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
