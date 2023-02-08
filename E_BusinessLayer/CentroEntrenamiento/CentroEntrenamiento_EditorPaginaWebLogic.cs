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
    public class CentroEntrenamiento_EditorPaginaWebLogic : IDisposable
    {
        CentroEntrenamiento_EditorPaginaWebData oCentroEntrenamiento_EditorPaginaWebData = null;
        public CentroEntrenamiento_EditorPaginaWebLogic()
        {
            oCentroEntrenamiento_EditorPaginaWebData = new CentroEntrenamiento_EditorPaginaWebData();
        }

        public RespItemCentroEntrenamiento_EditorPaginaWebDTO CentroEntrenamiento_EditorPaginaWebGetItem(ReqFilterCentroEntrenamiento_EditorPaginaWebDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_EditorPaginaWebDTO oRespItem = new RespItemCentroEntrenamiento_EditorPaginaWebDTO();

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
                CentroEntrenamiento_EditorPaginaWebDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_EditorPaginaWeb.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva:
                            {
                                oItem = new CentroEntrenamiento_EditorPaginaWebDTO();
                                oItem = oCentroEntrenamiento_EditorPaginaWebData.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(oReqFilter.Item);
                                //TRAINERR
                                CentroEntrenamiento_EditorPaginaWebDetalleData oCentroEntrenamiento_EditorPaginaWebDetalleData = new CentroEntrenamiento_EditorPaginaWebDetalleData();
                                CentroEntrenamiento_EditorPaginaWebDetalleDTO oCentroEntrenamiento_EditorPaginaWebDetalleDTO = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                                oCentroEntrenamiento_EditorPaginaWebDetalleDTO.CodigoUnidadNegocio = oReqFilter.Item.CodigoUnidadNegocio;
                                oCentroEntrenamiento_EditorPaginaWebDetalleDTO.CodigoSede = oReqFilter.Item.CodigoSede;
                                oCentroEntrenamiento_EditorPaginaWebDetalleDTO.Tipo = "trainner";
                                oItem.List_Trainners = oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspListarEdicionPaginaWebDetalle(oCentroEntrenamiento_EditorPaginaWebDetalleDTO);
                                //SERVICIOS
                                oCentroEntrenamiento_EditorPaginaWebDetalleDTO.Tipo = "servicios";
                                oItem.List_Servicios = oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspListarEdicionPaginaWebDetalle(oCentroEntrenamiento_EditorPaginaWebDetalleDTO);
                                //PLANES o PRECIOS
                                oCentroEntrenamiento_EditorPaginaWebDetalleDTO.Tipo = "planes";
                                oItem.List_Planes = oCentroEntrenamiento_EditorPaginaWebDetalleData.ecommerce_uspListarEdicionPaginaWebDetalle(oCentroEntrenamiento_EditorPaginaWebDetalleDTO);

                            }
                            break;
                        case filterCaseCentroEntrenamiento_EditorPaginaWeb.uspBuscarLogoCorporativo:
                            {
                                oItem = new CentroEntrenamiento_EditorPaginaWebDTO();
                                oItem = oCentroEntrenamiento_EditorPaginaWebData.CentroEntrenamiento_uspBuscarLogoCorporativo(oReqFilter.Item);
                              
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_EditorPaginaWebDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_EditorPaginaWebDTO();
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

        public RespCentroEntrenamiento_EditorPaginaWebDTO ExecuteTransac(ReqCentroEntrenamiento_EditorPaginaWebDTO oReq)
        {
            RespCentroEntrenamiento_EditorPaginaWebDTO oResp = new RespCentroEntrenamiento_EditorPaginaWebDTO();

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
                        foreach (CentroEntrenamiento_EditorPaginaWebDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    //CodigoOutput = oCentroEntrenamiento_EditorPaginaWebData.CentroEntrenamiento_uspActualizarEdicionPaginaWeb(item);
                                    break;
                                case Operation.Update:
                                    oCentroEntrenamiento_EditorPaginaWebData.CentroEntrenamiento_uspActualizarEdicionPaginaWeb(item);

                                    break;
                                case Operation.CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina:
                                    oCentroEntrenamiento_EditorPaginaWebData.CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina(item);

                                    break;
                                case Operation.UpdateFoto:
                                    oCentroEntrenamiento_EditorPaginaWebData.CentroEntrenamiento_uspActualizarEdicionPaginaWeb_Foto(item);

                                    break;
                                case Operation.Delete:
                                    //oCentroEntrenamiento_EditorPaginaWebData.ecommerce_uspEliminarCentroEntrenamiento_EditorPaginaWeb(item);
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
