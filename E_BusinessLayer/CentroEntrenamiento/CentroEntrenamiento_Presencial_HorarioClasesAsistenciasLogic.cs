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
    public class CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic : IDisposable
    {
        CentroEntrenamiento_Presencial_HorarioClasesAsistenciasData oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData = null;
        public CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic()
        {
            oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasData();
        }
        public RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO ExecuteTransac(ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq)
        {
            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = new RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();

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
                        foreach (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias(item);
                                    break;
                                case Operation.Update:
                                     oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(item);
                                    break;
                                case Operation.UpdateMarcarAsistenciaReserva:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias(item);
                                    break;
                                case Operation.CreateReservarYMarcarAsistencia:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(item);
                                    break;
                                case Operation.Delete:
                                    //oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.ecommerce_uspEliminarCentroEntrenamiento_Presencial_HorarioClasesAsistencias(item);
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

        public RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO CentroEntrenamiento_Presencial_HorarioClasesAsistenciasGetList(ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReqFilter)
        {

            RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oRespList = new RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();

            oRespList.List = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();
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

                    List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> CategoriaDTOList = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio:

                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion:

                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking:

                            CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking(oReqFilter.Item);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
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
