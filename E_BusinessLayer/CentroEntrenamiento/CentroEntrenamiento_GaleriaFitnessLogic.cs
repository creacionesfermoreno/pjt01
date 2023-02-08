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
    public class CentroEntrenamiento_GaleriaFitnessLogic : IDisposable
    {
        CentroEntrenamiento_GaleriaFitnessData oCentroEntrenamiento_GaleriaFitnessData = null;
        public CentroEntrenamiento_GaleriaFitnessLogic()
        {
            oCentroEntrenamiento_GaleriaFitnessData = new CentroEntrenamiento_GaleriaFitnessData();
        }
        
        public RespListCentroEntrenamiento_GaleriaFitnessDTO ResponseGetList(ReqFilterCentroEntrenamiento_GaleriaFitnessDTO oReqFilter)
        {
            RespListCentroEntrenamiento_GaleriaFitnessDTO oRespList = new RespListCentroEntrenamiento_GaleriaFitnessDTO();

            oRespList.List = new List<CentroEntrenamiento_GaleriaFitnessDTO>();
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

                    List<CentroEntrenamiento_GaleriaFitnessDTO> CategoriaDTOList = new List<CentroEntrenamiento_GaleriaFitnessDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_GaleriaFitness.CentroEntrenamiento_uspListarGaleriaFitness:
                            CategoriaDTOList = oCentroEntrenamiento_GaleriaFitnessData.CentroEntrenamiento_uspListarGaleriaFitness(oReqFilter.Item);
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

        public RespCentroEntrenamiento_GaleriaFitnessDTO ExecuteTransac(ReqCentroEntrenamiento_GaleriaFitnessDTO oReq)
        {
            RespCentroEntrenamiento_GaleriaFitnessDTO oResp = new RespCentroEntrenamiento_GaleriaFitnessDTO();

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
                        foreach (CentroEntrenamiento_GaleriaFitnessDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    Codigo = oCentroEntrenamiento_GaleriaFitnessData.CentroEntrenamiento_uspRegistrarGaleriaFitness(item);

                                    break;
                                case Operation.Update:
                                    oCentroEntrenamiento_GaleriaFitnessData.CentroEntrenamiento_uspActualizarGaleriaFitness(item);
                                    break;
                                case Operation.Delete:
                                    oCentroEntrenamiento_GaleriaFitnessData.CentroEntrenamiento_uspEliminarGaleriaFitness(item);
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
