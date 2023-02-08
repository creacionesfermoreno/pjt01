using E_DataLayer;
using E_DataLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace E_BusinessLayer.Gimnasio
{
    public class NotificacionappLogic : IDisposable
    {

        
        NotificacionappData oNotificacionappData = null;
        public NotificacionappLogic()
        {
            oNotificacionappData = new NotificacionappData();
        }


        public RespListNotificacionDTO NotificacionappGetList(ReqFilterNotificacionDTO oReqFilterNotificacionDTO)
        {

            RespListNotificacionDTO oRespListNotificacionDTO = new RespListNotificacionDTO();

            oRespListNotificacionDTO.List = new List<NotificacionDTO>();
            oRespListNotificacionDTO.User = oReqFilterNotificacionDTO.User;
            oRespListNotificacionDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterNotificacionDTO.User))
            {
                oRespListNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterNotificacionDTO.Paging == null)
            {
                oRespListNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
            if (oRespListNotificacionDTO.MessageList.Count == 0)
            {
                try
                {
                    if (!oReqFilterNotificacionDTO.Paging.All && oReqFilterNotificacionDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterNotificacionDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<NotificacionDTO> NotificacionDTOList = new List<NotificacionDTO>();

                    switch (oReqFilterNotificacionDTO.FilterCase)
                    {
                        case filterCaseNotificacionApp.Listar:
                            NotificacionDTOList = oNotificacionappData.Listado(oReqFilterNotificacionDTO.Item);
                            break;
                       
                    }

                    oRespListNotificacionDTO.List = NotificacionDTOList;
                    oRespListNotificacionDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListNotificacionDTO.Success = false;
                    oRespListNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }
            return oRespListNotificacionDTO;

        }



        public RespItemNotificacionDTO NotificacionAppGetItem(ReqFilterNotificacionDTO oReqFilterNotificacionDTO)
        {
            RespItemNotificacionDTO oRespItemNotificacionDTO = new RespItemNotificacionDTO();

            oRespItemNotificacionDTO.Success = false;
            oRespItemNotificacionDTO.Item = null;
            oRespItemNotificacionDTO.User = oReqFilterNotificacionDTO.User;
            oRespItemNotificacionDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterNotificacionDTO.User))
            {
                oRespItemNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemNotificacionDTO.MessageList.Count == 0)
            {
                NotificacionDTO oNotificacionDTO = null;
                try
                {
                    switch (oReqFilterNotificacionDTO.FilterCase)
                    {
                        case filterCaseNotificacionApp.BuscarPorCodigo:
                            {
                                oNotificacionDTO = new NotificacionDTO();
                                oNotificacionDTO = oNotificacionappData.BuscarPorCodigoNotificacionApp(oReqFilterNotificacionDTO.Item);
                            }
                            break;
                      
                        default:
                            {
                                oNotificacionDTO = new NotificacionDTO();
                            }
                            break;
                    }

                    oRespItemNotificacionDTO.Item = new NotificacionDTO();
                    oRespItemNotificacionDTO.Item = oNotificacionDTO;
                    oRespItemNotificacionDTO.Success = true;
                    oRespItemNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemNotificacionDTO.Success = false;
                    oRespItemNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemNotificacionDTO;
        }


        public RespNotificacionDTO ExecuteTransac(ReqNotificacionDTO reqNotificacionDTO)
        {
            RespNotificacionDTO respNotificacionDTO = new RespNotificacionDTO();

            respNotificacionDTO.MessageList = new List<Mensaje>();
            respNotificacionDTO.User = reqNotificacionDTO.User;

            if (String.IsNullOrEmpty(reqNotificacionDTO.User))
            {
                respNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (respNotificacionDTO.MessageList.Count == 0)
            {
                int Estado = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (NotificacionDTO item in reqNotificacionDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oNotificacionappData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oNotificacionappData.Actualizar(item);
                                    break;
                                case Operation.Eliminarfiltro:
                                    oNotificacionappData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        respNotificacionDTO.Success = true;
                        respNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Estado,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        respNotificacionDTO.Success = false;
                        respNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return respNotificacionDTO;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
