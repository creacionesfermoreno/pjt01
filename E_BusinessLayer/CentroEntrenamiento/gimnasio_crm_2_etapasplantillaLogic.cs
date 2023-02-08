using System;
using System.Collections.Generic;

using System.Transactions;
using System.Configuration;
using E_DataLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_BusinessLayer.CentroEntrenamiento
{
    public class gimnasio_crm_2_etapasplantillaLogic : IDisposable
    {
        gimnasio_crm_2_etapasplantillaData ogimnasio_crm_2_etapasplantillaData = null;
        public gimnasio_crm_2_etapasplantillaLogic()
        {
            ogimnasio_crm_2_etapasplantillaData = new gimnasio_crm_2_etapasplantillaData();
        }

        public RespListgimnasio_crm_2_etapasplantillaDTO gimnasio_crm_2_etapasplantillaGetList(ReqFiltergimnasio_crm_2_etapasplantillaDTO oReqFilter)
        {

            RespListgimnasio_crm_2_etapasplantillaDTO oRespList = new RespListgimnasio_crm_2_etapasplantillaDTO();

            oRespList.List = new List<gimnasio_crm_2_etapasplantillaDTO>();
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

                    List<gimnasio_crm_2_etapasplantillaDTO> CategoriaDTOList = new List<gimnasio_crm_2_etapasplantillaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasegimnasio_crm_2_etapasplantilla.CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla:

                            CategoriaDTOList = ogimnasio_crm_2_etapasplantillaData.CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla(oReqFilter.Item);
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

        public RespItemgimnasio_crm_2_etapasplantillaDTO gimnasio_crm_2_etapasplantillaGetItem(ReqFiltergimnasio_crm_2_etapasplantillaDTO oReqFilter)
        {
            RespItemgimnasio_crm_2_etapasplantillaDTO oRespItem = new RespItemgimnasio_crm_2_etapasplantillaDTO();

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
                gimnasio_crm_2_etapasplantillaDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasegimnasio_crm_2_etapasplantilla.CentroEntrenamiento_uspBuscar_gimnasio_crm_2_etapasplantilla:
                            {
                                oItem = new gimnasio_crm_2_etapasplantillaDTO();
                                oItem = ogimnasio_crm_2_etapasplantillaData.CentroEntrenamiento_uspBuscar_gimnasio_crm_2_etapasplantilla(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new gimnasio_crm_2_etapasplantillaDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new gimnasio_crm_2_etapasplantillaDTO();
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

        public Respgimnasio_crm_2_etapasplantillaDTO ExecuteTransac(Reqgimnasio_crm_2_etapasplantillaDTO oReq)
        {
            Respgimnasio_crm_2_etapasplantillaDTO oResp = new Respgimnasio_crm_2_etapasplantillaDTO();

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
                        foreach (gimnasio_crm_2_etapasplantillaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    ogimnasio_crm_2_etapasplantillaData.CentroEntrenamiento_uspRegistrar_gimnasio_crm_2_etapasplantilla(item);
                                    break;
                                case Operation.Update:
                                    ogimnasio_crm_2_etapasplantillaData.CentroEntrenamiento_uspActualizar_gimnasio_crm_2_etapasplantilla(item);
                                    break;
                                case Operation.Delete:
                                    ogimnasio_crm_2_etapasplantillaData.CentroEntrenamiento_uspEliminar_gimnasio_crm_2_etapasplantilla(item);
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
