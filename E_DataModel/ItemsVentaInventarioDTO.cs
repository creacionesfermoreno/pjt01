using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ItemsVentaInventarioDTO : AuditoriaDTO
    {        
        public int CodigoItemsVentaInventario { get; set; }
        public int CodigoItemVenta { get; set; }
        public string Nombre { get; set; }

        public int CodigoUnidadMedida { get; set; }
        public Decimal CostoUnidad { get; set; }
        public int CodigoAlmacen { get; set; }
        public string DesAlmacen { get; set; }
        public Decimal CantidadInicial { get; set; }
        public Decimal CantidadMinima { get; set; }
        public Decimal CantidadMaxima { get; set; }

        public Decimal CantidadActual { get; set; }
        public Decimal CantidadAjuste { get; set; }
        public Decimal CantidadFinal { get; set; }
        public int CodigoTipoAjuste { get; set; }
        public string DesTipoAjuste { get; set; }
        public string ColorTipoAjuste { get; set; }
        public int Estado { get; set; }

        public Common.Operation Operation { get; set; }
    }


    public class ReqItemsVentaInventarioDTO : Request
    {
        public List<ItemsVentaInventarioDTO> List { get; set; }
    }

    public class ReqFilterItemsVentaInventarioDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public ItemsVentaInventarioDTO Item { get; set; }
        public Common.filterCaseItemsVentaInventario FilterCase { get; set; }
    }

    public class RespItemsVentaInventarioDTO : Response
    {

    }

    public class RespItemItemsVentaInventarioDTO : Response
    {
        public ItemsVentaInventarioDTO Item { get; set; }
    }

    public class RespListItemsVentaInventarioDTO : Response
    {
        public List<ItemsVentaInventarioDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}


