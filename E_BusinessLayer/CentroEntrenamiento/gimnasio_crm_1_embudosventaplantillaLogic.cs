using System;
using System.Collections.Generic;

using System.Transactions;
using System.Configuration;
using E_DataLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_BusinessLayer.CentroEntrenamiento
{
    public class gimnasio_crm_1_embudosventaplantillaLogic : IDisposable
    {
        gimnasio_crm_1_embudosventaplantillaData ogimnasio_crm_1_embudosventaplantillaData = null;
        public gimnasio_crm_1_embudosventaplantillaLogic()
        {
            ogimnasio_crm_1_embudosventaplantillaData = new gimnasio_crm_1_embudosventaplantillaData();
        }

        public RespListgimnasio_crm_1_embudosventaplantillaDTO gimnasio_crm_1_embudosventaplantillaGetList(ReqFiltergimnasio_crm_1_embudosventaplantillaDTO oReqFilter)
        {

            RespListgimnasio_crm_1_embudosventaplantillaDTO oRespList = new RespListgimnasio_crm_1_embudosventaplantillaDTO();

            oRespList.List = new List<gimnasio_crm_1_embudosventaplantillaDTO>();
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

                    List<gimnasio_crm_1_embudosventaplantillaDTO> CategoriaDTOList = new List<gimnasio_crm_1_embudosventaplantillaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasegimnasio_crm_1_embudosventaplantilla.uspListar_gimnasio_crm_1_embudosventaplantilla:

                            CategoriaDTOList = ogimnasio_crm_1_embudosventaplantillaData.CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla(oReqFilter.Item);
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

        public RespItemgimnasio_crm_1_embudosventaplantillaDTO gimnasio_crm_1_embudosventaplantillaGetItem(ReqFiltergimnasio_crm_1_embudosventaplantillaDTO oReqFilter)
        {
            RespItemgimnasio_crm_1_embudosventaplantillaDTO oRespItem = new RespItemgimnasio_crm_1_embudosventaplantillaDTO();

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
                gimnasio_crm_1_embudosventaplantillaDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasegimnasio_crm_1_embudosventaplantilla.uspBuscar_gimnasio_crm_1_embudosventaplantilla:
                            {
                                oItem = new gimnasio_crm_1_embudosventaplantillaDTO();
                                oItem = ogimnasio_crm_1_embudosventaplantillaData.CentroEntrenamiento_uspBuscar_gimnasio_crm_1_embudosventaplantilla(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new gimnasio_crm_1_embudosventaplantillaDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new gimnasio_crm_1_embudosventaplantillaDTO();
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

        public Respgimnasio_crm_1_embudosventaplantillaDTO ExecuteTransac(Reqgimnasio_crm_1_embudosventaplantillaDTO oReq)
        {
            Respgimnasio_crm_1_embudosventaplantillaDTO oResp = new Respgimnasio_crm_1_embudosventaplantillaDTO();

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
                        foreach (gimnasio_crm_1_embudosventaplantillaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    ogimnasio_crm_1_embudosventaplantillaData.CentroEntrenamiento_uspRegistrar_gimnasio_crm_1_embudosventaplantilla(item);
                                    break;
                                case Operation.Update:
                                    ogimnasio_crm_1_embudosventaplantillaData.CentroEntrenamiento_uspActualizar_gimnasio_crm_1_embudosventaplantilla(item);
                                    break;
                                case Operation.Delete:
                                    ogimnasio_crm_1_embudosventaplantillaData.CentroEntrenamiento_uspEliminar_gimnasio_crm_1_embudosventaplantilla(item);
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
