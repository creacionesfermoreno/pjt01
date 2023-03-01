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
    public class EmailCampaingLogic 
    {
        EmailCampaingData data = null;
        public EmailCampaingLogic()
        {
            data = new EmailCampaingData();
        }


        //EmailCampaingDTO
        public RespListEmailCampaingDTO GetList(ReqFilterEmailCampaingDTO oReqFilter)
        {

            RespListEmailCampaingDTO oRespList = new RespListEmailCampaingDTO();

            oRespList.List = new List<EmailCampaingDTO>();
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

                    List<EmailCampaingDTO> DTOList = new List<EmailCampaingDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case FilterEmailCampaing.ListPagination:
                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = 50;
                            }
                            uint NumeroRegistros = 0;

                            DTOList = data.ListPagination(oReqFilter.Item, oReqFilter.Paging, out NumeroRegistros);
                            oRespList.Paging = new Paging();
                            oRespList.Paging.TotalRecord = NumeroRegistros;
                            oRespList.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_custom"]);
                            oRespList.Paging.PageNumber = oReqFilter.Paging.PageNumber;
                            break;
                        
                        case FilterEmailCampaing.ListPaginationDetail:
                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = 50;
                            }
                            uint TotalRecord = 0;
                            DTOList = data.ListPaginationDetail(oReqFilter.Item, oReqFilter.Paging, out TotalRecord);
                            oRespList.Paging = new Paging();
                            oRespList.Paging.TotalRecord = TotalRecord;
                            oRespList.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_custom"]);
                            oRespList.Paging.PageNumber = oReqFilter.Paging.PageNumber;
                            break;

                        case FilterEmailCampaing.ListFiles:
                            DTOList = data.ListFile(oReqFilter.Item);
                            break;
                    }

                    oRespList.List = DTOList;
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


        public RespItemEmailCampaingDTO GetItem(ReqFilterEmailCampaingDTO oReqFilter)
        {
            RespItemEmailCampaingDTO oRespItem = new RespItemEmailCampaingDTO();

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
                EmailCampaingDTO oDTO = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case FilterEmailCampaing.Search:
                            {
                                oDTO = new EmailCampaingDTO();
                                oDTO = data.Search(oReqFilter.Item);
                            }
                            break;


                        default:
                            {
                                oDTO = new EmailCampaingDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new EmailCampaingDTO();
                    oRespItem.Item = oDTO;
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


        public RespEmailCampaingDTO ExecuteTransac(ReqEmailCampaingDTO oReq)
        {
            RespEmailCampaingDTO oResp = new RespEmailCampaingDTO();

            oResp.MessageList = new List<Mensaje>();
            oResp.User = oReq.User;

            if (String.IsNullOrEmpty(oReq.User))
            {
                oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oResp.MessageList.Count == 0)
            {
                int Estado = 0;
                string code = "";
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (EmailCampaingDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    code = data.Register(item);
                                    break;

                                case Operation.Update:
                                    data.Update(item);
                                    break;

                                case Operation.Delete:
                                    data.Destroy(item);
                                    break;

                                case Operation.UpdateSendCampaing:
                                    data.UpdateSend(item);
                                    break;

                                case Operation.CampaingRegisterFile:
                                    Estado = data.RegisterFile(item);
                                    break;

                                case Operation.CampaingDestroyFile:
                                    data.DestroyFile(item);
                                    break;  
                                
                                case Operation.CampaingRegisterDetail:
                                   code = data.RegisterDetail(item);
                                    break;

                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
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




       
    }
}
