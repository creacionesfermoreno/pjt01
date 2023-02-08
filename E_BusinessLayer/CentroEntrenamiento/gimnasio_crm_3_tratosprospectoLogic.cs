using System;
using System.Collections.Generic;

using System.Transactions;
using System.Configuration;
using E_DataLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_BusinessLayer.CentroEntrenamiento
{
    public class gimnasio_crm_3_tratosprospectoLogic : IDisposable
    {
        gimnasio_crm_3_tratosprospectoData ogimnasio_crm_3_tratosprospectoData = null;
        public gimnasio_crm_3_tratosprospectoLogic()
        {
            ogimnasio_crm_3_tratosprospectoData = new gimnasio_crm_3_tratosprospectoData();
        }

        public RespListgimnasio_crm_3_tratosprospectoDTO gimnasio_crm_3_tratosprospectoGetList(ReqFiltergimnasio_crm_3_tratosprospectoDTO oReqFilter)
        {

            RespListgimnasio_crm_3_tratosprospectoDTO oRespList = new RespListgimnasio_crm_3_tratosprospectoDTO();

            oRespList.List = new List<gimnasio_crm_3_tratosprospectoDTO>();
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

                    List<gimnasio_crm_3_tratosprospectoDTO> CategoriaDTOList = new List<gimnasio_crm_3_tratosprospectoDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto:

                            CategoriaDTOList = ogimnasio_crm_3_tratosprospectoData.CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto(oReqFilter.Item);
                            break;
                        case filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial:

                            CategoriaDTOList = ogimnasio_crm_3_tratosprospectoData.CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial(oReqFilter.Item);
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

        public RespItemgimnasio_crm_3_tratosprospectoDTO gimnasio_crm_3_tratosprospectoGetItem(ReqFiltergimnasio_crm_3_tratosprospectoDTO oReqFilter)
        {
            RespItemgimnasio_crm_3_tratosprospectoDTO oRespItem = new RespItemgimnasio_crm_3_tratosprospectoDTO();

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
                gimnasio_crm_3_tratosprospectoDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto:
                            {
                                oItem = new gimnasio_crm_3_tratosprospectoDTO();
                                oItem = ogimnasio_crm_3_tratosprospectoData.CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto(oReqFilter.Item);
                            }
                            break;
                        case filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto:
                            {
                                oItem = new gimnasio_crm_3_tratosprospectoDTO();
                                oItem = ogimnasio_crm_3_tratosprospectoData.CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new gimnasio_crm_3_tratosprospectoDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new gimnasio_crm_3_tratosprospectoDTO();
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

        public Respgimnasio_crm_3_tratosprospectoDTO ExecuteTransac(Reqgimnasio_crm_3_tratosprospectoDTO oReq)
        {
            Respgimnasio_crm_3_tratosprospectoDTO oResp = new Respgimnasio_crm_3_tratosprospectoDTO();

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
                        int CodigoOperacion = 100;
                        string DetalleOperacion = string.Empty;
                        foreach (gimnasio_crm_3_tratosprospectoDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:

                                    //VALIDACION SE PODRA CREAR UN TRATO SIEMPRE Y CUANDO EL PROSPECTO NO TENGA UN TRATO ABIERTO EN EL EMBUDO DE VENTA
                                    if (ogimnasio_crm_3_tratosprospectoData.uspValidarExisteTratoAbiertoEmbudo_gimnasio_crm_3_tratosprospecto(item) == 0)
                                    {
                                        CodigoOperacion = 100;
                                        DetalleOperacion = "Se guardo correctamente el trato.";
                                        ogimnasio_crm_3_tratosprospectoData.CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto(item);
                                    }
                                    else
                                    {
                                        CodigoOperacion = 0;
                                        DetalleOperacion = "El prospecto ya tiene un trato abierto en el embudo de venta seleccionado";
                                    }
                                    break;
                                case Operation.Update:
                                    ogimnasio_crm_3_tratosprospectoData.uspActualizar_gimnasio_crm_3_tratosprospecto(item);
                                    DetalleOperacion = "Se actualizo correctamente el trato.";
                                    break;
                                case Operation.UpdateEstado:
                                    ogimnasio_crm_3_tratosprospectoData.uspActualizar_gimnasio_crm_3_tratosprospectoEstado(item);
                                    DetalleOperacion = "Se actualizo correctamente el trato.";
                                    break;
                                case Operation.UpdateEtapa:
                                    ogimnasio_crm_3_tratosprospectoData.uspActualizar_gimnasio_crm_3_tratosprospectoEtapa(item);
                                    DetalleOperacion = "Se actualizo correctamente el trato.";
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoOperacion,
                            Detalle = DetalleOperacion,
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
