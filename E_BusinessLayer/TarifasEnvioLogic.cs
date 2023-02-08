using System;
using System.Collections.Generic;
using System.Transactions;
using System.Configuration;
using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;

namespace E_BusinessLayer
{
    public class TarifasEnvioLogic : IDisposable
    {
        TarifasEnvioData oTarifasEnvioData = null;
        public TarifasEnvioLogic()
        {
            oTarifasEnvioData = new TarifasEnvioData();
        }

        public RespListTarifasEnvioDTO TarifasEnvioGetList(ReqFilterTarifasEnvioDTO oReqFilter)
        {

            RespListTarifasEnvioDTO oRespList = new RespListTarifasEnvioDTO();

            oRespList.List = new List<TarifasEnvioDTO>();
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

                    List<TarifasEnvioDTO> CategoriaDTOList = new List<TarifasEnvioDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseTarifasEnvio.ecommerce_uspListarAdminTarifasEnvio:
                            CategoriaDTOList = oTarifasEnvioData.ecommerce_uspListarAdminTarifasEnvio(oReqFilter.Item);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oTarifasEnvioData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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

        public RespTarifasEnvioDTO ExecuteTransac(ReqTarifasEnvioDTO oReq)
        {
            RespTarifasEnvioDTO oResp = new RespTarifasEnvioDTO();

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
                        foreach (TarifasEnvioDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTarifasEnvioData.ecommerce_uspRegistrarAdminTarifasEnvio(item);
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
