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
    public class CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic : IDisposable
    {
        CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData = null;
        public CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic()
        {
            oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData();
        }

        public RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessGetList(ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReqFilter)
        {

            RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oRespList = new RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();

            oRespList.List = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
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

                    List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CategoriaDTOList = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness:
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal:                          
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion:                          
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion(oReqFilter.Item);
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

        public RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO ExecuteTransac(ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq)
        {
            RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = new RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();

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
                        int CodigoValidacionEliminarHorariosSalaMaquinas = 0;
                        foreach (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspRegistrarPresencial_ConfiguracionSalaFitness(item);
                                    break;
                                case Operation.Update:
                                    oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspActualizarPresencial_ConfiguracionSalaFitness(item);
                                    break;
                                case Operation.Delete:
                                    CodigoValidacionEliminarHorariosSalaMaquinas = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo:
                                    oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar:
                                    oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar:
                                    oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoValidacionEliminarHorariosSalaMaquinas,
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

        public RespItemCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessGetItem(ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oRespItem = new RespItemCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();

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
                CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion:
                            {
                                oItem = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();
                                oItem = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessData.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();
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


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
