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


        public RespListNotificacionDTO GetList(ReqFilterNotificacionDTO oReqFilter)
        {

            RespListNotificacionDTO oRespList = new RespListNotificacionDTO();

            oRespList.List = new List<NotificacionDTO>();
            oRespList.User = oReqFilter.User;
            oRespList.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
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
                        oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_custom"]);
                    }

                    List<NotificacionDTO> NotificacionDTOList = new List<NotificacionDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseNotificacionApp.Listar:
                            uint NRegisters = 0;
                            NotificacionDTOList = oNotificacionappData.ListPagination(oReqFilter.Item, oReqFilter.Paging, out NRegisters);
                            oRespList.Paging = new Paging();
                            oRespList.Paging.TotalRecord = NRegisters;
                            oRespList.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_custom"]);
                            oRespList.Paging.PageNumber = oReqFilter.Paging.PageNumber;
                            break;
                        
                        case filterCaseNotificacionApp.listAddressPaginate:
                            uint NroRegisters = 0;
                            NotificacionDTOList = oNotificacionappData.ListPaginationAddress(oReqFilter.Item, oReqFilter.Paging, out NroRegisters);
                            oRespList.Paging = new Paging();
                            oRespList.Paging.TotalRecord = NroRegisters;
                            oRespList.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_custom"]);
                            oRespList.Paging.PageNumber = oReqFilter.Paging.PageNumber;
                            break;
                            
                        case filterCaseNotificacionApp.listNotisByUser:
                            
                            NotificacionDTOList = oNotificacionappData.ListPaginationAppByUser(oReqFilter.Item);
                            
                            break;

                        case filterCaseNotificacionApp.ListActive:
                            NotificacionDTOList = oNotificacionappData.ListCustomerActive(oReqFilter.Item);
                            break;

                        case filterCaseNotificacionApp.ListByVencer:
                            NotificacionDTOList = oNotificacionappData.ListCustomerByVencer(oReqFilter.Item);
                            break;

                        case filterCaseNotificacionApp.ListVencids:
                            NotificacionDTOList = oNotificacionappData.ListCustomerVencids(oReqFilter.Item);
                            break;


                    }

                    oRespList.List = NotificacionDTOList;
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


        public RespItemNotificacionDTO GetItem(ReqFilterNotificacionDTO oReqFilter)
        {
            RespItemNotificacionDTO oRespItem = new RespItemNotificacionDTO();

            oRespItem.Success = false;
            oRespItem.Item = null;
            oRespItem.User = oReqFilter.User;
            oRespItem.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItem.MessageList.Count == 0)
            {
                NotificacionDTO oNotificacionDTO = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseNotificacionApp.BuscarPorCodigo:
                            {
                                oNotificacionDTO = new NotificacionDTO();
                                oNotificacionDTO = oNotificacionappData.SearchNotiApp(oReqFilter.Item);
                            }
                            break;

                        default:
                            {
                                oNotificacionDTO = new NotificacionDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new NotificacionDTO();
                    oRespItem.Item = oNotificacionDTO;
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
                string code = "";
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (NotificacionDTO item in reqNotificacionDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    code = oNotificacionappData.Registrar(item);
                                    break; 
                                
                                case Operation.Update:
                                    oNotificacionappData.UpdateSend(item);
                                    break;
                                case Operation.NotiAppReadUpdate:
                                    oNotificacionappData.UpdateReadUser(item);
                                    break;
                                case Operation.Delete:
                                    oNotificacionappData.Destroy(item);
                                    break;
                                case Operation.NotiAppRegisterDetail:
                                    oNotificacionappData.RegisterDetail(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        respNotificacionDTO.Success = true;
                        respNotificacionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Estado,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion,
                            Code = code,
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
                            Tipo = TipoMensaje.Error,

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
