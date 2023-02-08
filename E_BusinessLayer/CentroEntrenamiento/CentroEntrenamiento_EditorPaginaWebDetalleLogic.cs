using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_BusinessLayer.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebDetalleLogic : IDisposable
    {
        CentroEntrenamiento_EditorPaginaWebDetalleData oCentroEntrenamiento_EditorPaginaWebDetalleData = null;
        public CentroEntrenamiento_EditorPaginaWebDetalleLogic()
        {
            oCentroEntrenamiento_EditorPaginaWebDetalleData = new CentroEntrenamiento_EditorPaginaWebDetalleData();
        }

        public RespListCentroEntrenamiento_EditorPaginaWebDetalleDTO ResponseGetList(ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO oReqFilter)
        {
            RespListCentroEntrenamiento_EditorPaginaWebDetalleDTO oRespList = new RespListCentroEntrenamiento_EditorPaginaWebDetalleDTO();

            oRespList.List = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();
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

                    List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> CategoriaDTOList = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_EditorPaginaWebDetalle.ecommerce_uspListarEdicionPaginaWebDetalle:                          
                            CategoriaDTOList = oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspListarEdicionPaginaWebDetalle(oReqFilter.Item);
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

        public RespItemCentroEntrenamiento_EditorPaginaWebDetalleDTO ResponseGetItem(ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_EditorPaginaWebDetalleDTO oRespItem = new RespItemCentroEntrenamiento_EditorPaginaWebDetalleDTO();

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
                CentroEntrenamiento_EditorPaginaWebDetalleDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_EditorPaginaWebDetalle.ecommerce_uspBuscarEdicionPaginaWebDetalle:
                            {
                                oItem = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                                oItem = oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspBuscarEdicionPaginaWebDetalle(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
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

        public RespCentroEntrenamiento_EditorPaginaWebDetalleDTO ExecuteTransac(ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO oReq)
        {
            RespCentroEntrenamiento_EditorPaginaWebDetalleDTO oResp = new RespCentroEntrenamiento_EditorPaginaWebDetalleDTO();

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
                string Codigo = string.Empty;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        string CodigoOutput = string.Empty;
                        foreach (CentroEntrenamiento_EditorPaginaWebDetalleDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    Codigo = oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspRegistrarEdicionPaginaWebDetalle(item);

                                    break;
                                case Operation.Update:
                                    oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspActualizarEdicionPaginaWebDetalle(item);
                                    break;
                                case Operation.Delete:
                                    oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspEliminarEdicionPaginaWebDetalle(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = Codigo,
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
