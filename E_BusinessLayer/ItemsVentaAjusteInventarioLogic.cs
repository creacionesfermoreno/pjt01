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
    public class ItemsVentaAjusteInventarioLogic : IDisposable
    {
        ItemsVentaAjusteInventarioData oItemsVentaAjusteInventarioData = null;
        public ItemsVentaAjusteInventarioLogic()
        {
            oItemsVentaAjusteInventarioData = new ItemsVentaAjusteInventarioData();
        }

        public RespListItemsVentaAjusteInventarioDTO ItemsVentaGetList(ReqFilterItemsVentaAjusteInventarioDTO oReqFilter)
        {

            RespListItemsVentaAjusteInventarioDTO oRespList = new RespListItemsVentaAjusteInventarioDTO();

            oRespList.List = new List<ItemsVentaAjusteInventarioDTO>();
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

                    List<ItemsVentaAjusteInventarioDTO> DTOList = new List<ItemsVentaAjusteInventarioDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseItemsVentaAjusteInventario.ecommerce_uspListarItemsVentaAjusteInventario_Paginacion:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            DTOList = oItemsVentaAjusteInventarioData.ecommerce_uspListarItemsVentaAjusteInventario_Paginacion(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oItemsVentaData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
                            }
                            break;
                    }

                    oRespList.List = DTOList;
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


        public RespItemsVentaAjusteInventarioDTO ExecuteTransac(ReqItemsVentaAjusteInventarioDTO oReq)
        {
            RespItemsVentaAjusteInventarioDTO oResp = new RespItemsVentaAjusteInventarioDTO();

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
                        int CodigoOutput = 0;
                        foreach (ItemsVentaAjusteInventarioDTO item in oReq.List)
                        {
                            ItemsVentaAjusteInventarioDetalleData oItemsVentaAjusteInventarioDetalleData = new ItemsVentaAjusteInventarioDetalleData();

                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    
                                    CodigoOutput = oItemsVentaAjusteInventarioData.ecommerce_uspRegistrarItemsVentaAjusteInventario(item);
                                    foreach (ItemsVentaAjusteInventarioDetalleDTO oItemsVentaAjusteInventarioDetalleDTO in item.listaDetalle)
                                    {
                                        oItemsVentaAjusteInventarioDetalleDTO.CodigoItemsVentaAjusteInventario = CodigoOutput;
                                        oItemsVentaAjusteInventarioDetalleDTO.CodigoAlmacen = item.CodigoAlmacen;
                                        oItemsVentaAjusteInventarioDetalleData.ecommerce_uspRegistrarItemsVentaAjusteInventarioDetalle(oItemsVentaAjusteInventarioDetalleDTO);
                                    }
                                    break;
                                    //case Operation.Update:
                                    //    oItemsVentaAjusteInventarioData.ecommerce_uspActualizarItemsVentaAjusteInventario(item);
                                    //    break;
                                    //case Operation.Delete:
                                    //    oItemsVentaAjusteInventarioData.ecommerce_uspEliminarItemsVentaAjusteInventario(item);
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
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

   
}
