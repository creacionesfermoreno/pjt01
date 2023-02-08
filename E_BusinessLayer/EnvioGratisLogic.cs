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
    public class EnvioGratisLogic : IDisposable
    {
        EnvioGratisData oEnvioGratisData = null;
        public EnvioGratisLogic()
        {
            oEnvioGratisData = new EnvioGratisData();
        }

        public RespItemEnvioGratisDTO EnvioGratisGetItem(ReqFilterEnvioGratisDTO oReqFilter)
        {
            RespItemEnvioGratisDTO oRespItem = new RespItemEnvioGratisDTO();

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
                EnvioGratisDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseEnvioGratis.ecommerce_uspBuscarEnvioGratis:
                            {
                                oItem = new EnvioGratisDTO();
                                oItem = oEnvioGratisData.ecommerce_uspBuscarEnvioGratis(oReqFilter.Item);
                            }
                            break;
                        case filterCaseEnvioGratis.ecommerce_uspBuscarEnvioGratisXtienda:
                            {
                                oItem = new EnvioGratisDTO();
                                oItem = oEnvioGratisData.ecommerce_uspBuscarEnvioGratisXtienda(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new EnvioGratisDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new EnvioGratisDTO();
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

        public RespEnvioGratisDTO ExecuteTransac(ReqEnvioGratisDTO oReq)
        {
            RespEnvioGratisDTO oResp = new RespEnvioGratisDTO();

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
                        foreach (EnvioGratisDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oEnvioGratisData.ecommerce_uspRegistrarEnvioGratis(item);
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
