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
    public class ItemsVentaLogic : IDisposable
    {
        ItemsVentaData oItemsVentaData = null;
        public ItemsVentaLogic()
        {
            oItemsVentaData = new ItemsVentaData();
        }

        public RespListItemsVentaDTO ItemsVentaGetList(ReqFilterItemsVentaDTO oReqFilter)
        {

            RespListItemsVentaDTO oRespList = new RespListItemsVentaDTO();

            oRespList.List = new List<ItemsVentaDTO>();
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

                    List<ItemsVentaDTO> CategoriaDTOList = new List<ItemsVentaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        //*********************** API *************************
                        case filterCaseItemsVenta.ecommerce_productbycate:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oItemsVentaData.ListProductByCategory(oReqFilter.Item);
                            break;
                        //*********************** API *************************

                        case filterCaseItemsVenta.ecommerce_uspListarItemsVenta_Paginacion:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oItemsVentaData.ecommerce_uspListarItemsVenta_Paginacion(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        case filterCaseItemsVenta.ecommerce_uspBuscadorItemsVentaInventariable:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oItemsVentaData.ecommerce_uspBuscadorItemsVentaInventariable(oReqFilter.Item);
                            break;
                        case filterCaseItemsVenta.ecommerce_uspListarValorInventario_Paginaciones:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oItemsVentaData.ecommerce_uspListarValorInventario_Paginaciones(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        case filterCaseItemsVenta.ecommerce_uspListarValorInventario_PuntoVenta:
                            
                            CategoriaDTOList = oItemsVentaData.ecommerce_uspListarValorInventario_PuntoVenta(oReqFilter.Item);
                            break;
                        case filterCaseItemsVenta.ecommerce_uspListarItemsVenta_PorCategoriaPaginacion:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oItemsVentaData.ecommerce_uspListarItemsVenta_PorCategoriaPaginacion(oReqFilter.Item, oReqFilter.Paging);

                            break;
                    
                        default:
                            {
                                // CategoriaDTOList = oItemsVentaData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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

        public RespItemItemsVentaDTO ItemsVentaGetItem(ReqFilterItemsVentaDTO oReqFilter)
        {
            RespItemItemsVentaDTO oRespItem = new RespItemItemsVentaDTO();

            oRespItem.Success = false;
            oRespItem.Item = null;
            oRespItem.User = oReqFilter.User;
            oRespItem.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Socios no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItem.MessageList.Count == 0)
            {
                ItemsVentaDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseItemsVenta.ecommerce_uspBuscarItemsVentasParaGuardarFoto:
                            {                            
                                oItem = new ItemsVentaDTO();
                                oItem = oItemsVentaData.ecommerce_uspBuscarItemsVentas(oReqFilter.Item);
                            }
                            break;
                        case filterCaseItemsVenta.ecommerce_uspBuscarItemsVentas:
                            {
                                ItemsVentaInventarioData oItemsVentaInventarioData = new ItemsVentaInventarioData();
                                ItemsVentaInventarioDTO oItemsVentaInventarioDTO = new ItemsVentaInventarioDTO();

                                oItem = new ItemsVentaDTO();
                                oItem = oItemsVentaData.ecommerce_uspBuscarItemsVentas(oReqFilter.Item);

                                oItemsVentaInventarioDTO.CodigoUnidadNegocio = oReqFilter.Item.CodigoUnidadNegocio;
                                oItemsVentaInventarioDTO.CodigoSede = oReqFilter.Item.CodigoSede;
                                oItemsVentaInventarioDTO.CodigoItemVenta = oReqFilter.Item.CodigoItemVenta;

                                oItem.lista_ItemsVentaInventarioDTO = oItemsVentaInventarioData.ecommerce_uspListarItemsVentaInventario(oItemsVentaInventarioDTO);

                            }
                            break;
                        case filterCaseItemsVenta.ecommerce_uspBuscarItemsVentasTienda:
                            {                              
                                oItem = new ItemsVentaDTO();
                                oItem = oItemsVentaData.ecommerce_uspBuscarItemsVentasTienda(oReqFilter.Item);                               
                            }
                            break;
                        default:
                            {
                                oItem = new ItemsVentaDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new ItemsVentaDTO();
                    oRespItem.Item = oItem;
                    oRespItem.Success = true;
                    oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItem.Success = false;
                    oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItem;
        }

        public RespItemsVentaDTO ExecuteTransac(ReqItemsVentaDTO oReq)
        {
            RespItemsVentaDTO oResp = new RespItemsVentaDTO();

            oResp.MessageList = new List<Mensaje>();
            oResp.User = oReq.User;

            if (String.IsNullOrEmpty(oReq.User))
            {
                oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Usuario no es válida.",
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
                        foreach (ItemsVentaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oItemsVentaData.ecommerce_uspRegistrarItemsVenta(item);

                                    if (item.CodigoTipoItem == 1 && item.ItemInventariable == 1)
                                    {
                                        ItemsVentaInventarioData oItemsVentaInventarioData = new ItemsVentaInventarioData();
                                        foreach (ItemsVentaInventarioDTO itemInventario in item.lista_ItemsVentaInventarioDTO)
                                        {
                                            itemInventario.CodigoItemVenta = CodigoOutput;
                                            oItemsVentaInventarioData.ecommerce_uspRegistrarItemsVentaInventario(itemInventario);
                                        }
                                    }
                                    break;
                                case Operation.Update:
                                    oItemsVentaData.ecommerce_uspActualizarItemsVenta(item);
                                    if (item.CodigoTipoItem == 1 && item.ItemInventariable == 1)
                                    {
                                        ItemsVentaInventarioData oItemsVentaInventarioData = new ItemsVentaInventarioData();
                                        foreach (ItemsVentaInventarioDTO itemInventario in item.lista_ItemsVentaInventarioDTO)
                                        {
                                            if (itemInventario.CodigoItemsVentaInventario == 0)
                                            {
                                                oItemsVentaInventarioData.ecommerce_uspRegistrarItemsVentaInventario(itemInventario);
                                            }
                                            else
                                            {
                                                oItemsVentaInventarioData.ecommerce_uspActualizarItemsVentaInventario(itemInventario);
                                            }
                                           
                                        }
                                    }
                                    break;
                                case Operation.Delete:
                                    oItemsVentaData.ecommerce_uspEliminarItemsVenta(item);
                                    break;
                                case Operation.UpdateFoto:
                                    oItemsVentaData.ecommerce_uspActualizarItemsVentaFoto(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoOutput,
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
