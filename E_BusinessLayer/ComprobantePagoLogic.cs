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
    public class ComprobantePagoLogic : IDisposable
    {

        ComprobantePagoData oComprobantePagoData = null;
        public ComprobantePagoLogic()
        {
            oComprobantePagoData = new ComprobantePagoData();
        }

        public RespComprobantePagoDTO ExecuteTransac(ReqComprobantePagoDTO oReq)
        {
            RespComprobantePagoDTO oResp = new RespComprobantePagoDTO();

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
                        foreach (ComprobantePagoDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oComprobantePagoData.ecommerce_uspRegistrarComprobantePago(item);                                   
                                    break;
                                case Operation.Update:
                                    //oComprobantePagoData.ecommerce_uspActualizarComprobantePago(item);
                                    //if (item.CodigoTipoItem == 1 && item.ItemInventariable == 1)
                                    //{
                                    //    ComprobantePagoInventarioData oComprobantePagoInventarioData = new ComprobantePagoInventarioData();
                                    //    foreach (ComprobantePagoInventarioDTO itemInventario in item.lista_ComprobantePagoInventarioDTO)
                                    //    {
                                    //        if (itemInventario.CodigoItemVenta == 0)
                                    //        {
                                    //            oComprobantePagoInventarioData.ecommerce_uspRegistrarComprobantePagoInventario(itemInventario);
                                    //        }
                                    //        else
                                    //        {
                                    //            oComprobantePagoInventarioData.ecommerce_uspActualizarComprobantePagoInventario(itemInventario);
                                    //        }

                                    //    }
                                    //}
                                    break;
                                case Operation.Delete:
                                   // oComprobantePagoData.ecommerce_uspEliminarComprobantePago(item);
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

        public RespListComprobantePagoDTO ComprobantePagoGetList(ReqFilterComprobantePagoDTO oReqFilter)
        {

            RespListComprobantePagoDTO oRespList = new RespListComprobantePagoDTO();

            oRespList.List = new List<ComprobantePagoDTO>();
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

                    List<ComprobantePagoDTO> CategoriaDTOList = new List<ComprobantePagoDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseComprobantePago.CentroEntrenamiento_uspTotalPagosProductosRangoFechas:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oComprobantePagoData.CentroEntrenamiento_uspTotalPagosProductosRangoFechas(oReqFilter.Item);
                            break;                                              
                        default:
                            {
                                // CategoriaDTOList = oComprobantePagoData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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
