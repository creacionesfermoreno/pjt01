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
    public class WhatsappConfigLogic : IDisposable
    {
        WhatsappConfigData oWhatsappConfigData = null;
        public WhatsappConfigLogic()
        {
            oWhatsappConfigData = new WhatsappConfigData();
        }

        public RespListWhatsappConfigDTO GetList(ReqFilterWhatsappConfigDTO oReqFilterWhatsappConfigDTO)
        {

            RespListWhatsappConfigDTO oRespListWhatsappConfigDTO = new RespListWhatsappConfigDTO();

            oRespListWhatsappConfigDTO.List = new List<WhatsappConfigDTO>();
            oRespListWhatsappConfigDTO.User = oReqFilterWhatsappConfigDTO.User;
            oRespListWhatsappConfigDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterWhatsappConfigDTO.User))
            {
                oRespListWhatsappConfigDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterWhatsappConfigDTO.Paging == null)
            {
                oRespListWhatsappConfigDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
            if (oRespListWhatsappConfigDTO.MessageList.Count == 0)
            {
                try
                {
                    if (!oReqFilterWhatsappConfigDTO.Paging.All && oReqFilterWhatsappConfigDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterWhatsappConfigDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<WhatsappConfigDTO> NotificacionDTOList = new List<WhatsappConfigDTO>();

                    switch (oReqFilterWhatsappConfigDTO.FilterCase)
                    {
                        case FilterCaseGlobal.List:
                            NotificacionDTOList = oWhatsappConfigData.List(oReqFilterWhatsappConfigDTO.Item);
                            break;
                        case FilterCaseGlobal.ListCamp:
                            NotificacionDTOList = oWhatsappConfigData.ListCampaign(oReqFilterWhatsappConfigDTO.Item);
                            break;
                        case FilterCaseGlobal.ListCampDetail:
                            NotificacionDTOList = oWhatsappConfigData.ListCampaignDetail(oReqFilterWhatsappConfigDTO.Item);
                            break;

                    }

                    oRespListWhatsappConfigDTO.List = NotificacionDTOList;
                    oRespListWhatsappConfigDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListWhatsappConfigDTO.Success = false;
                    oRespListWhatsappConfigDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }
            return oRespListWhatsappConfigDTO;

        }



        public RespItemWhatsappConfigDTO GetItem(ReqFilterWhatsappConfigDTO oReqFilterWhatsappConfigDTO)
        {
            RespItemWhatsappConfigDTO oRespItemNotificacionDTO = new RespItemWhatsappConfigDTO();

            oRespItemNotificacionDTO.Success = false;
            oRespItemNotificacionDTO.Item = null;
            oRespItemNotificacionDTO.User = oReqFilterWhatsappConfigDTO.User;
            oRespItemNotificacionDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterWhatsappConfigDTO.User))
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
                WhatsappConfigDTO oNotificacionDTO = null;
                try
                {
                    switch (oReqFilterWhatsappConfigDTO.FilterCase)
                    {
                        case FilterCaseGlobal.SearchByCode:
                            {
                                oNotificacionDTO = new WhatsappConfigDTO();
                                oNotificacionDTO = oWhatsappConfigData.SearchByCode(oReqFilterWhatsappConfigDTO.Item);
                            }
                            break;
                        case FilterCaseGlobal.SearchByCodeCamp:
                            {
                                oNotificacionDTO = new WhatsappConfigDTO();
                                oNotificacionDTO = oWhatsappConfigData.SearchByCodeCampaign(oReqFilterWhatsappConfigDTO.Item);
                            }
                            break;

                        default:
                            {
                                oNotificacionDTO = new WhatsappConfigDTO();
                            }
                            break;
                    }

                    oRespItemNotificacionDTO.Item = new WhatsappConfigDTO();
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


        public RespWhatsappConfigDTO ExecuteTransac(ReqWhatsappConfigDTO oReqWhatsappConfigDTO)
        {
            RespWhatsappConfigDTO oRespWhatsappConfigDTO = new RespWhatsappConfigDTO();

            oRespWhatsappConfigDTO.MessageList = new List<Mensaje>();
            oRespWhatsappConfigDTO.User = oReqWhatsappConfigDTO.User;

            if (String.IsNullOrEmpty(oReqWhatsappConfigDTO.User))
            {
                oRespWhatsappConfigDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oRespWhatsappConfigDTO.MessageList.Count == 0)
            {
                int Estado = 0;
                string code = "";
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (WhatsappConfigDTO item in oReqWhatsappConfigDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oWhatsappConfigData.Register(item);
                                    break;
                                case Operation.Update:
                                    oWhatsappConfigData.Update(item);
                                    break;

                                case Operation.Delete:
                                    oWhatsappConfigData.Destroy(item);
                                    break;
                                case Operation.RegisterCamp:
                                    code = oWhatsappConfigData.RegisterCampaign(item);
                                    break;
                                case Operation.UpdateCamp:
                                    oWhatsappConfigData.UpdateCampaign(item);
                                    break;
                                case Operation.UpdateCampStatu:
                                    oWhatsappConfigData.UpdateCampaignStatu(item);
                                    break;
                                case Operation.DestroyCamp:
                                    oWhatsappConfigData.DestroyCampaign(item);
                                    break;
                                case Operation.RegisterCampDetail:
                                    oWhatsappConfigData.RegisterCampaignDetail(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespWhatsappConfigDTO.Success = true;
                        oRespWhatsappConfigDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
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
                        oRespWhatsappConfigDTO.Success = false;
                        oRespWhatsappConfigDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespWhatsappConfigDTO;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
