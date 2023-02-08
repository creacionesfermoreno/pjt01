using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class InventariofitRepository : IDisposable
    {

        public List<ItemsVentaInventarioDTO> ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones(int CodigoUnidadNegocio, int CodigoSede, int CodigoItemVenta, int PageNumber)
        {
            List<ItemsVentaInventarioDTO> lista = new List<ItemsVentaInventarioDTO>();

            ReqFilterItemsVentaInventarioDTO oReq = new ReqFilterItemsVentaInventarioDTO()
            {
                Item = new ItemsVentaInventarioDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoItemVenta = CodigoItemVenta,
                    CodigoAlmacen = 1
                },
                FilterCase = filterCaseItemsVentaInventario.ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListItemsVentaInventarioDTO oResp = null;

            using (ItemsVentaInventarioLogic oItemsVentaInventarioLogic = new ItemsVentaInventarioLogic())
            {
                oResp = oItemsVentaInventarioLogic.ItemsVentaInventarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}