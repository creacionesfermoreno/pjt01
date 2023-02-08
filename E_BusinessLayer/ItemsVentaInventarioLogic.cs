using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;

namespace E_BusinessLayer
{
    public class ItemsVentaInventarioLogic : IDisposable
    {
        ItemsVentaInventarioData oItemsVentaInventarioData = null;
        public ItemsVentaInventarioLogic()
        {
            oItemsVentaInventarioData = new ItemsVentaInventarioData();
        }

        public RespItemsVentaInventarioDTO ExecuteTransac(ReqItemsVentaInventarioDTO oReq)
        {
            RespItemsVentaInventarioDTO oResp = new RespItemsVentaInventarioDTO();

            oResp.MessageList = new List<Mensaje>();
            oResp.User = oReq.User;

            if (String.IsNullOrEmpty(oReq.User))
            {
                oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Emnpresa no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oResp.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {                        
                        foreach (ItemsVentaInventarioDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                //case Operation.Create:
                                //    CodigoOutput = oItemsVentaInventarioData.ecommerce_uspRegistrarItemsVentaInventario(item);
                                //    break;
                                //case Operation.Update:
                                //    oItemsVentaInventarioData.ecommerce_uspActualizarItemsVentaInventario(item);
                                //    break;
                                //case Operation.Delete:
                                //    oItemsVentaInventarioData.ecommerce_uspEliminarItemsVentaInventario(item);
                                //    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oResp.Success = false;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oResp;
        }


        public RespListItemsVentaInventarioDTO ItemsVentaInventarioGetList(ReqFilterItemsVentaInventarioDTO oReqFilter)
        {

            RespListItemsVentaInventarioDTO oRespList = new RespListItemsVentaInventarioDTO();

            oRespList.List = new List<ItemsVentaInventarioDTO>();
            oRespList.User = oReqFilter.User;
            oRespList.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilter.Paging == null)
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oRespList.MessageList.Count == 0)
            {
                try
                {

                    if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                    {
                        oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ItemsVentaInventarioDTO> CategoriaDTOList = new List<ItemsVentaInventarioDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseItemsVentaInventario.ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = 200;//Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oItemsVentaInventarioData.ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oItemsVentaInventarioData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
                            }
                            break;
                    }

                    oRespList.List = CategoriaDTOList;
                    oRespList.Success = true;

                }
                catch (Exception ex)
                {
                    oRespList.Success = false;
                    oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespList;

        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
