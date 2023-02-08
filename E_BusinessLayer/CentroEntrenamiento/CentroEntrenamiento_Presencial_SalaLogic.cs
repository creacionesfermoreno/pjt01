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
    public class CentroEntrenamiento_Presencial_SalaLogic : IDisposable
    {
        CentroEntrenamiento_Presencial_SalaData oCentroEntrenamiento_Presencial_SalaData = null;
        public CentroEntrenamiento_Presencial_SalaLogic()
        {
            oCentroEntrenamiento_Presencial_SalaData = new CentroEntrenamiento_Presencial_SalaData();
        }

        public RespListCentroEntrenamiento_Presencial_SalaDTO CentroEntrenamiento_Presencial_SalaGetList(ReqFilterCentroEntrenamiento_Presencial_SalaDTO oReqFilter)
        {

            RespListCentroEntrenamiento_Presencial_SalaDTO oRespList = new RespListCentroEntrenamiento_Presencial_SalaDTO();

            oRespList.List = new List<CentroEntrenamiento_Presencial_SalaDTO>();
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

                    List<CentroEntrenamiento_Presencial_SalaDTO> CategoriaDTOList = new List<CentroEntrenamiento_Presencial_SalaDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Presencial_Sala.CentroEntrenamiento_uspListarSala_Presencial:
                          
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspListarSala_Presencial(oReqFilter.Item);
                            break;
                        case filterCaseCentroEntrenamiento_Presencial_Sala.CentroEntrenamiento_uspListarSalaMaquinas_Presencial:
                          
                            CategoriaDTOList = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspListarSalaMaquinas_Presencial(oReqFilter.Item);
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

        public RespCentroEntrenamiento_Presencial_SalaDTO ExecuteTransac(ReqCentroEntrenamiento_Presencial_SalaDTO oReq)
        {
            RespCentroEntrenamiento_Presencial_SalaDTO oResp = new RespCentroEntrenamiento_Presencial_SalaDTO();

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
                        foreach (CentroEntrenamiento_Presencial_SalaDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspRegistrarSala_Presencial(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial(item);
                                    break;
                                case Operation.Update:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspEditarSala_Presencial(item);
                                    break;
                                case Operation.Delete:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspEliminarSala_Presencial(item);
                                    break;
                                case Operation.CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial:
                                    CodigoOutput = oCentroEntrenamiento_Presencial_SalaData.CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial(item);
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
