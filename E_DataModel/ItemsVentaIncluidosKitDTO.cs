using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ItemsVentaIncluidosKitDTO : AuditoriaDTO
    {        
        public int CodigoItemIncluidosKitVenta { get; set; }
        public int CodigoItemVentaSuperior { get; set; }
        public int CodigoItemVenta { get; set; }
        public string Referencia { get; set; }
        public string Descripcion { get; set; }
        public Decimal Cantidad { get; set; }
        public int Estado { get; set; }

    }

    public class ReqItemsVentaIncluidosKitDTO : Request
    {
        public List<ItemsVentaIncluidosKitDTO> List { get; set; }
    }

    public class ReqFilterItemsVentaIncluidosKitDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public ItemsVentaIncluidosKitDTO Item { get; set; }
        public Common.filterCaseItemsVentaIncluidosKit FilterCase { get; set; }
    }

    public class RespItemsVentaIncluidosKitDTO : Response
    {

    }

    public class RespItemItemsVentaIncluidosKitDTO : Response
    {
        public ItemsVentaIncluidosKitDTO Item { get; set; }
    }

    public class RespListItemsVentaIncluidosKitDTO : Response
    {
        public List<ItemsVentaIncluidosKitDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
