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
    public class CentroEntrenamiento_MenuPlantillaLogic : IDisposable
    {
        CentroEntrenamiento_MenuPlantillaData oCentroEntrenamiento_MenuPlantillaData = null;
        public CentroEntrenamiento_MenuPlantillaLogic()
        {
            oCentroEntrenamiento_MenuPlantillaData = new CentroEntrenamiento_MenuPlantillaData();
        }

        public RespListCentroEntrenamiento_MenuPlantillaDTO CentroEntrenamiento_MenuPlantillaGetList(ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReqFilter)
        {

            RespListCentroEntrenamiento_MenuPlantillaDTO oRespList = new RespListCentroEntrenamiento_MenuPlantillaDTO();

            oRespList.List = new List<CentroEntrenamiento_MenuPlantillaDTO>();
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

                    List<CentroEntrenamiento_MenuPlantillaDTO> CategoriaDTOList = new List<CentroEntrenamiento_MenuPlantillaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarMenuPlantilla:
                            
                            CategoriaDTOList = oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspListarMenuPlantilla(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarSEG_Planes:

                            CategoriaDTOList = oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspListarSEG_Planes();
                            break;
                        case filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarMenuPlantillaPlan:

                            CategoriaDTOList = oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspListarMenuPlantillaPlan(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_MenuPlantilla.SEGListarPerfilMenu:

                            CategoriaDTOList = oCentroEntrenamiento_MenuPlantillaData.SEGListarPerfilMenu(oReqFilter.Item);
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
        
        public RespItemCentroEntrenamiento_MenuPlantillaDTO CentroEntrenamiento_MenuPlantillaGetItem(ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_MenuPlantillaDTO oRespItem = new RespItemCentroEntrenamiento_MenuPlantillaDTO();

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
                CentroEntrenamiento_MenuPlantillaDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspBuscarMenuPlantilla:
                            {
                                oItem = new CentroEntrenamiento_MenuPlantillaDTO();
                                oItem = oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspBuscarMenuPlantilla(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_MenuPlantillaDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_MenuPlantillaDTO();
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

        public RespCentroEntrenamiento_MenuPlantillaDTO ExecuteTransac(ReqCentroEntrenamiento_MenuPlantillaDTO oReq)
        {
            RespCentroEntrenamiento_MenuPlantillaDTO oResp = new RespCentroEntrenamiento_MenuPlantillaDTO();

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


                        foreach (CentroEntrenamiento_MenuPlantillaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspRegistrarMenuPlantilla(item);
                                    break;
                                case Operation.Update:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspActualizarMenuPlantilla(item);
                                    break;                              
                                case Operation.Delete:
                                    //oCentroEntrenamiento_MenuPlantillaData.ecommerce_uspEliminarCentroEntrenamiento_MenuPlantilla(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspEliminarMenuPlantillaPlan:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspEliminarMenuPlantillaPlan(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspRegistrarMenuPlantillaPlan:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspRegistrarMenuPlantillaPlan(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspActualizarMenuPlantillaOrden:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspActualizarMenuPlantillaOrden(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspRegistrarPerfilMenu:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspRegistrarPerfilMenu(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspEliminarPerfilMenu:
                                    oCentroEntrenamiento_MenuPlantillaData.CentroEntrenamiento_uspEliminarPerfilMenu(item);
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
