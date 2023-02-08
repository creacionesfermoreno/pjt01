using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ItemsVentaAjusteInventarioDTO : AuditoriaDTO
    {      
        public int CodigoItemsVentaAjusteInventario { get; set; }
        public int CodigoAlmacen { get; set; }
        public string DesAlmacen { get; set; }
        public DateTime FechaAjuste { get; set; }
        public string Observaciones { get; set; }
        public Decimal TotalAjuste { get; set; }
        public int Estado { get; set; }
        public List<ItemsVentaAjusteInventarioDetalleDTO> listaDetalle { get; set; }       
        public Common.Operation Operation { get; set; }

        public DateTime? b_FechaAjusteInicio { get; set; }
        public DateTime? b_FechaAjusteFin { get; set; }
    }
    
    public class ReqItemsVentaAjusteInventarioDTO : Request
    {
        public List<ItemsVentaAjusteInventarioDTO> List { get; set; }
    }

    public class ReqFilterItemsVentaAjusteInventarioDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public ItemsVentaAjusteInventarioDTO Item { get; set; }
        public Common.filterCaseItemsVentaAjusteInventario FilterCase { get; set; }
    }

    public class RespItemsVentaAjusteInventarioDTO : Response
    {

    }

    public class RespItemItemsVentaAjusteInventarioDTO : Response
    {
        public ItemsVentaAjusteInventarioDTO Item { get; set; }
    }

    public class RespListItemsVentaAjusteInventarioDTO : Response
    {
        public List<ItemsVentaAjusteInventarioDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
