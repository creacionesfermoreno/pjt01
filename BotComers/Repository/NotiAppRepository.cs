using BotComers.Helpers;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using iTextSharp.xmp.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Operation = E_DataModel.Common.Operation;

namespace BotComers.Repository
{
    public class NotiAppRepository
    {

        //register
        public string Register(NotificacionDTO item)
        {
            string code = null;
            List<NotificacionDTO> list = new List<NotificacionDTO>();
            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoNotificacionesApp = "0",
                TipeNoty = item.TipeNoty,
                Send = item.Send,
                Title = item.Title,
                Body = item.Body,
                DescriptionHtml = item.DescriptionHtml,
                Recurrent = item.Recurrent,
                Group = item.Group,
                UrlImage= item.UrlImage,
                Operation = Operation.Create,
            }); ;
            ReqNotificacionDTO oReq = new ReqNotificacionDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespNotificacionDTO oResp = null;
            using (NotificacionappLogic logic = new NotificacionappLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                code = oResp.MessageList[0].Code;
            }

            return code;
        }



        //register detail
        public bool RegisterDetail(NotificacionDTO item)
        {
            bool resp = false;
            List<NotificacionDTO> list = new List<NotificacionDTO>();
            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoNotificacionesApp = item.CodigoNotificacionesApp,
                CodigoNotificacionesAppDestinatarios = 0,
                IdUser = item.IdUser,
                Read = item.Read,
                Send = item.Send,
                Operation = Operation.NotiAppRegisterDetail,
            });
            ReqNotificacionDTO oReq = new ReqNotificacionDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespNotificacionDTO oResp = null;
            using (NotificacionappLogic logic = new NotificacionappLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                resp = true;
            }
            return resp;
        }  
        
        
        //update send notiapp 
        public bool UpdateSend(string code)
        {
            bool resp = false;
            List<NotificacionDTO> list = new List<NotificacionDTO>();
            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoNotificacionesApp = code,
                Operation = Operation.Update,
            });
            ReqNotificacionDTO oReq = new ReqNotificacionDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespNotificacionDTO oResp = null;
            using (NotificacionappLogic logic = new NotificacionappLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                resp = true;
            }
            return resp;
        }



        //get Item notiapp
        public NotificacionDTO getItemNotiApp(string code)
        {

            NotificacionDTO oDto = new NotificacionDTO();
            oDto.CodigoSede = Commun.CodigoSede;
            oDto.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oDto.CodigoNotificacionesApp = code;
            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()
            {
                FilterCase = filterCaseNotificacionApp.BuscarPorCodigo,
                Item = oDto,
                User = Commun.Usuario,
            };
            RespItemNotificacionDTO oResp = null;
            using (NotificacionappLogic oLogic = new NotificacionappLogic())
            {
                oResp = oLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oDto = oResp.Item;

            }
            return oDto;
        }



        //list actives
        public List<NotificacionDTO> ListActives(int dias)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            NotificacionDTO oModel = new NotificacionDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;
            oModel.Days = dias;

            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()

            {
                FilterCase = filterCaseNotificacionApp.ListActive,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 9999,
                    PageRecords = 0
                }
            };

            RespListNotificacionDTO oResp = null;
            using (NotificacionappLogic oLogic = new NotificacionappLogic())
            {
                oResp = oLogic.GetList(oReq);
            }

            return oResp.List;
        }


        //list x vencer
        public List<NotificacionDTO> ListByVencer(int dias)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            NotificacionDTO oModel = new NotificacionDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;
            oModel.Days = dias;

            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()

            {
                FilterCase = filterCaseNotificacionApp.ListByVencer,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 9999,
                    PageRecords = 0
                }
            };

            RespListNotificacionDTO oResp = null;
            using (NotificacionappLogic oLogic = new NotificacionappLogic())
            {
                oResp = oLogic.GetList(oReq);
            }

            return oResp.List;
        }


        //list x vencer
        public List<NotificacionDTO> ListVencidos(int dias)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            NotificacionDTO oModel = new NotificacionDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;
            oModel.Days = dias;

            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()

            {
                FilterCase = filterCaseNotificacionApp.ListVencids,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 9999,
                    PageRecords = 0
                }
            };

            RespListNotificacionDTO oResp = null;
            using (NotificacionappLogic oLogic = new NotificacionappLogic())
            {
                oResp = oLogic.GetList(oReq);
            }

            return oResp.List;
        }





    }
}