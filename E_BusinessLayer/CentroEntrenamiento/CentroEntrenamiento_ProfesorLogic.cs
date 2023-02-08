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
    public class CentroEntrenamiento_ProfesorLogic : IDisposable
    {
        CentroEntrenamiento_ProfesorData oCentroEntrenamiento_ProfesorData = null;
        public CentroEntrenamiento_ProfesorLogic()
        {
            oCentroEntrenamiento_ProfesorData = new CentroEntrenamiento_ProfesorData();
        }

        public RespListCentroEntrenamiento_ProfesorDTO CentroEntrenamiento_ProfesorGetList(ReqFilterCentroEntrenamiento_ProfesorDTO oReqFilter)
        {

            RespListCentroEntrenamiento_ProfesorDTO oRespList = new RespListCentroEntrenamiento_ProfesorDTO();

            oRespList.List = new List<CentroEntrenamiento_ProfesorDTO>();
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

                    List<CentroEntrenamiento_ProfesorDTO> CategoriaDTOList = new List<CentroEntrenamiento_ProfesorDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Profesor.CentroEntrenamiento_uspListarPresencial_uspListarPersonalClasesGrupales:
                           
                            CategoriaDTOList = oCentroEntrenamiento_ProfesorData.CentroEntrenamiento_uspListarPresencial_uspListarPersonalClasesGrupales(oReqFilter.Item);
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

        public RespItemCentroEntrenamiento_ProfesorDTO CentroEntrenamiento_ProfesorGetItem(ReqFilterCentroEntrenamiento_ProfesorDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_ProfesorDTO oRespItem = new RespItemCentroEntrenamiento_ProfesorDTO();

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
                CentroEntrenamiento_ProfesorDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_Profesor.CentroEntrenamiento_uspBuscarProfesorPorNombres:
                            {
                                oItem = new CentroEntrenamiento_ProfesorDTO();
                                oItem = oCentroEntrenamiento_ProfesorData.CentroEntrenamiento_uspBuscarProfesorPorNombres(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_ProfesorDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_ProfesorDTO();
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
        
        public RespCentroEntrenamiento_ProfesorDTO ExecuteTransac(ReqCentroEntrenamiento_ProfesorDTO oReq)
        {
            RespCentroEntrenamiento_ProfesorDTO oResp = new RespCentroEntrenamiento_ProfesorDTO();

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
                        foreach (CentroEntrenamiento_ProfesorDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoOutput = oCentroEntrenamiento_ProfesorData.CentroEntrenamiento_uspRegistrarProfesor(item);
                                    break;
                                case Operation.Update:
                                     oCentroEntrenamiento_ProfesorData.CentroEntrenamiento_uspActualizarProfesor(item);

                                    break;
                                case Operation.Delete:
                                    //oCentroEntrenamiento_ProfesorData.ecommerce_uspEliminarCentroEntrenamiento_Profesor(item);
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
