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
    public class CuponesLogic : IDisposable
    {
        CuponesData oCuponesData = null;
        public CuponesLogic()
        {
            oCuponesData = new CuponesData();
        }

        public RespListCuponesDTO CuponesGetList(ReqFilterCuponesDTO oReqFilter)
        {
            RespListCuponesDTO oRespList = new RespListCuponesDTO();

            oRespList.List = new List<CuponesDTO>();
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

                    List<CuponesDTO> CategoriaDTOList = new List<CuponesDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCupones.ecommerce_uspListar_Cupones:

                            CategoriaDTOList = oCuponesData.ecommerce_uspListar_Cupones(oReqFilter.Item);
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

        public RespItemCuponesDTO CuponesGetItem(ReqFilterCuponesDTO oReqFilter)
        {
            RespItemCuponesDTO oRespItem = new RespItemCuponesDTO();

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
                CuponesDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCupones.ecommerce_uspBuscar_Cupones:
                            {
                                oItem = new CuponesDTO();
                                oItem = oCuponesData.ecommerce_uspBuscar_Cupones(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCupones.ecommerce_uspBuscar_CuponesXCodigoPromocion:
                            {
                                oItem = new CuponesDTO();
                                oItem = oCuponesData.ecommerce_uspBuscar_CuponesXCodigoPromocion(oReqFilter.Item);
                            }
                            break;                       
                    }

                    oRespItem.Item = new CuponesDTO();
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

        public RespCuponesDTO ExecuteTransac(ReqCuponesDTO oReq)
        {
            RespCuponesDTO oResp = new RespCuponesDTO();

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
                        foreach (CuponesDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oCuponesData.ecommerce_uspRegistrar_Cupones(item);
                                    break;
                                case Operation.Update:
                                    oCuponesData.ecommerce_uspActualizar_Cupones(item);
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
