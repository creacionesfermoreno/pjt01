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
    public class AspNetUsersDireccionesEntregaLogic : IDisposable
    {
        AspNetUsersDireccionesEntregaData oAspNetUsersDireccionesEntregaData = null;
        public AspNetUsersDireccionesEntregaLogic()
        {
            oAspNetUsersDireccionesEntregaData = new AspNetUsersDireccionesEntregaData();
        }

        public RespListAspNetUsersDireccionesEntregaDTO AspNetUsersDireccionesEntregaGetList(ReqFilterAspNetUsersDireccionesEntregaDTO oReqFilter)
        {
            RespListAspNetUsersDireccionesEntregaDTO oRespList = new RespListAspNetUsersDireccionesEntregaDTO();

            oRespList.List = new List<AspNetUsersDireccionesEntregaDTO>();
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

                    List<AspNetUsersDireccionesEntregaDTO> CategoriaDTOList = new List<AspNetUsersDireccionesEntregaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseAspNetUsersDireccionesEntrega.ecommerce_uspListarAspNetUsers_DireccionesEntrega:                          
                            CategoriaDTOList = oAspNetUsersDireccionesEntregaData.ecommerce_uspListarAspNetUsers_DireccionesEntrega(oReqFilter.Item);
                            break;
                        default:
                            {                                
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

        public RespAspNetUsersDireccionesEntregaDTO ExecuteTransac(ReqAspNetUsersDireccionesEntregaDTO oReq)
        {
            RespAspNetUsersDireccionesEntregaDTO oResp = new RespAspNetUsersDireccionesEntregaDTO();

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
                        string CodigoOutput = string.Empty;
                        foreach (AspNetUsersDireccionesEntregaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oAspNetUsersDireccionesEntregaData.ecommerce_uspRegistrarAspNetUsers_DireccionesEntrega(item);
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
                            Detalle = CodigoOutput,
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
