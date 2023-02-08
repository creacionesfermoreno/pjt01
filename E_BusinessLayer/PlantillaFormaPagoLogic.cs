using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Transactions;

namespace E_BusinessLayer
{
    public class PlantillaFormaPagoLogic : IDisposable
    {
        private PlantillaFormaPagoData oPlantillaFormaPagoData = null;

        public PlantillaFormaPagoLogic()
        {
            oPlantillaFormaPagoData = new PlantillaFormaPagoData();
        }

        public RespListPlantillaFormaPagoDTO PlantillaFormaPagoGetList(ReqFilterPlantillaFormaPagoDTO oReqFilter)
        {
            RespListPlantillaFormaPagoDTO oRespList = new RespListPlantillaFormaPagoDTO();

            oRespList.List = new List<PlantillaFormaPagoDTO>();
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

                    List<PlantillaFormaPagoDTO> CategoriaDTOList = new List<PlantillaFormaPagoDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasePlantillaFormaPago.ecommerce_uspListarAdminFormaPago:

                            CategoriaDTOList = oPlantillaFormaPagoData.ecommerce_uspListarAdminFormaPago(oReqFilter.Item);
                            break;
                        case filterCasePlantillaFormaPago.listTypesPasarela:

                            CategoriaDTOList = oPlantillaFormaPagoData.listPasarelaTypes(oReqFilter.Item);
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

        public RespItemPlantillaFormaPagoDTO PlantillaFormaPagoGetItem(ReqFilterPlantillaFormaPagoDTO oReqFilter)
        {
            RespItemPlantillaFormaPagoDTO oRespItem = new RespItemPlantillaFormaPagoDTO();

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
                PlantillaFormaPagoDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasePlantillaFormaPago.ecommerce_uspBuscarFormaPago_MercadoPago:
                            {
                                oItem = new PlantillaFormaPagoDTO();
                                oItem = oPlantillaFormaPagoData.ecommerce_uspBuscarFormaPago_MercadoPago(oReqFilter.Item);
                            }
                            break;

                        case filterCasePlantillaFormaPago.ecommerce_uspBuscarFormaPago_Yape:
                            {
                                oItem = new PlantillaFormaPagoDTO();
                                oItem = oPlantillaFormaPagoData.ecommerce_uspBuscarFormaPago_Yape(oReqFilter.Item);
                            }
                            break;

                        case filterCasePlantillaFormaPago.ecommerce_uspBuscarFormaPago_ContraEntrega:
                            {
                                oItem = new PlantillaFormaPagoDTO();
                                oItem = oPlantillaFormaPagoData.ecommerce_uspBuscarFormaPago_ContraEntrega(oReqFilter.Item);
                            }
                            break;

                        default:
                            {
                                oItem = new PlantillaFormaPagoDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new PlantillaFormaPagoDTO();
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

        public RespPlantillaFormaPagoDTO ExecuteTransac(ReqPlantillaFormaPagoDTO oReq)
        {
            RespPlantillaFormaPagoDTO oResp = new RespPlantillaFormaPagoDTO();

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
                        foreach (PlantillaFormaPagoDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.ecommerce_uspRegistrarFormaPago_MercadoPago:
                                    oPlantillaFormaPagoData.ecommerce_uspRegistrarFormaPago_MercadoPago(item);
                                    break;

                                case Operation.ecommerce_uspRegistrarFormaPago_Yape:
                                    oPlantillaFormaPagoData.ecommerce_uspRegistrarFormaPago_Yape(item);
                                    break;

                                case Operation.ecommerce_uspRegistrarFormaPago_ContraEntrega:
                                    oPlantillaFormaPagoData.ecommerce_uspRegistrarFormaPago_ContraEntrega(item);
                                    break;

                                case Operation.Update:

                                    break;

                                case Operation.Delete:

                                    break;
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