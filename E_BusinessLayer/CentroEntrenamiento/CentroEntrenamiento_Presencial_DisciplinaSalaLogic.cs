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
    public class CentroEntrenamiento_Presencial_DisciplinaSalaLogic : IDisposable
    {
        CentroEntrenamiento_Presencial_DisciplinaSalaData oCentroEntrenamiento_Presencial_DisciplinaSalaData = null;
        public CentroEntrenamiento_Presencial_DisciplinaSalaLogic()
        {
            oCentroEntrenamiento_Presencial_DisciplinaSalaData = new CentroEntrenamiento_Presencial_DisciplinaSalaData();
        }

        public RespListCentroEntrenamiento_Presencial_DisciplinaSalaDTO CentroEntrenamiento_Presencial_DisciplinaSalaGetList(ReqFilterCentroEntrenamiento_Presencial_DisciplinaSalaDTO oReqFilter)
        {

            RespListCentroEntrenamiento_Presencial_DisciplinaSalaDTO oRespList = new RespListCentroEntrenamiento_Presencial_DisciplinaSalaDTO();

            oRespList.List = new List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO>();
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

                    List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> CategoriaDTOList = new List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_DisciplinaSala.CentroEntrenamiento_uspListarDisciplinaSala_Presencial:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_DisciplinaSalaData.CentroEntrenamiento_uspListarDisciplinaSala_Presencial(oReqFilter.Item);
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

        public RespCentroEntrenamiento_Presencial_DisciplinaSalaDTO ExecuteTransac(ReqCentroEntrenamiento_Presencial_DisciplinaSalaDTO oReq)
        {
            RespCentroEntrenamiento_Presencial_DisciplinaSalaDTO oResp = new RespCentroEntrenamiento_Presencial_DisciplinaSalaDTO();

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
                        int CodigoOutput = 0;
                        foreach (CentroEntrenamiento_Presencial_DisciplinaSalaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_DisciplinaSalaData.CentroEntrenamiento_uspRegistrarDisciplinaSala_Presencial(item);
                                    break;
                                case Operation.Update:
                                    // oCentroEntrenamiento_Presencial_DisciplinaSalaData.ecommerce_uspActualizarCentroEntrenamiento_Presencial_DisciplinaSala(item);

                                    break;
                                case Operation.Delete:
                                    //oCentroEntrenamiento_Presencial_DisciplinaSalaData.ecommerce_uspEliminarCentroEntrenamiento_Presencial_DisciplinaSala(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoOutput,
                            Detalle = "Proceso Grabado Correctamente.",
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
