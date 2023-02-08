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
    public class CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic : IDisposable
    {

        CentroEntrenamiento_Presencial_HorarioClasesConfiguracionData oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData = null;
        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic()
        {
            oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionData();
        }

        public RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReqFilter)
        {

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oRespList = new RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            oRespList.List = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
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

                    List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CategoriaDTOList = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario:
                            
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb:
                            
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion:
                            
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking:

                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb"]);
                            }
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(oReqFilter.Item);
                            break; 
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb"]);
                            }
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas:
                          
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion:

                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS:
                           
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario"]);
                            }
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(200);
                            }
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES(oReqFilter.Item,oReqFilter.Paging);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE:
                        
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE(oReqFilter.Item);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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
        
        public RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oRespItem = new RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

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
                CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                oItem = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspObtenerFechasReservas_Configuracion:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                oItem = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                oItem = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                oItem = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                oItem = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros(oReqFilter.Item);
                            }
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                oItem = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
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

        public RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO ExecuteTransac(ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq)
        {
            RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = new RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

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
                        foreach (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspRegistrarPresencial_HorarioClasesConfiguracion(item);
                                    break;
                                case Operation.Update:
                                    oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion(item);                                    
                                    break;
                                case Operation.Delete:
                                    oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL:
                                    oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL(item);
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
