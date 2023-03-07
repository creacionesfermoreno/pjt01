using Antlr.Runtime;
using BotComers.Helpers;
using BotComers.Repository;
using BotComers.Repository.Services;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class notiappController : Controller
    {
        // GET: notiapp
        public ActionResult Index()
        {
            return View();
        }


        //list pagination
        public ActionResult ListPagination(int PageNumber = 1)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            NotificacionDTO oModel = new NotificacionDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;

            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()

            {
                FilterCase = filterCaseNotificacionApp.Listar,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListNotificacionDTO oResp = null;
            using (NotificacionappLogic oLogic = new NotificacionappLogic())
            {
                oResp = oLogic.GetList(oReq);
            }

            return Json(oResp, JsonRequestBehavior.AllowGet);
        }



        //list pagination address
        public ActionResult ListPaginationAddress(string code, int PageNumber = 1)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            NotificacionDTO oModel = new NotificacionDTO();
            oModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oModel.CodigoSede = Commun.CodigoSede;
            oModel.CodigoNotificacionesApp = code;

            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()

            {
                FilterCase = filterCaseNotificacionApp.listAddressPaginate,
                Item = oModel,
                User = Commun.Usuario,

                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListNotificacionDTO oResp = null;
            using (NotificacionappLogic oLogic = new NotificacionappLogic())
            {
                oResp = oLogic.GetList(oReq);
            }

            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        //register
        public async Task<ActionResult> RegisterNotiApp(string group = "", int type = 0, string title = "", string body = "", string image = "", bool recurrent = false, string description = "", int optionMode = 1)
        {

            ResponseModel _objResponseModel = new ResponseModel();

            List<string> listErrors = new List<string>();

            if (String.IsNullOrEmpty(group))
            {
                listErrors.Add("Campo grupo de personas requerido");
            }
            if (type == 0)
            {
                listErrors.Add("Campo tipo de notificacion requerido");
            }
            if (String.IsNullOrEmpty(title))
            {
                listErrors.Add("Campo título requerido");
            }
            if (String.IsNullOrEmpty(body))
            {
                listErrors.Add("Campo asunto requerido");
            }

            if (String.IsNullOrEmpty(description) || description == " ")
            {
                listErrors.Add("Campo descripción requerido");
            }

            if (listErrors.Count() > 0)
            {
                _objResponseModel.Success = false;
                _objResponseModel.Date = listErrors;
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }
            try
            {

                NotiAppRepository repo = new NotiAppRepository();
                //send now
                if (optionMode == 1)
                {
                    List<NotificacionDTO> listUser = new List<NotificacionDTO>();

                    switch (group)
                    {
                        case "ac-1":
                            listUser = repo.ListActives(1);
                            break;
                        case "ac-7":
                            listUser = repo.ListActives(7);
                            break;
                        case "ac-15":
                            listUser = repo.ListActives(15);
                            break;
                        case "ac-30":
                            listUser = repo.ListActives(30);
                            break;
                        case "xven":
                            listUser = repo.ListByVencer(0);
                            break;
                        case "xven-3":
                            listUser = repo.ListByVencer(3);
                            break;
                        case "xven-7":
                            listUser = repo.ListByVencer(7);
                            break;
                        case "xven-15":
                            listUser = repo.ListByVencer(15);
                            break;
                        case "ven-3":
                            listUser = repo.ListVencidos(3);
                            break;
                        case "ven-7":
                            listUser = repo.ListVencidos(7);
                            break;
                        case "ven-15":
                            listUser = repo.ListVencidos(15);
                            break;
                        case "ven-30":
                            listUser = repo.ListVencidos(30);
                            break;
                        default:
                            break;
                    };

                    if (listUser.Count() == 0)
                    {
                        _objResponseModel.Date = new List<string>() { "No se encontraron destinatarios válidos" };
                        _objResponseModel.Success = false;
                        return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
                    }

                    //register header

                    NotificacionDTO model = new NotificacionDTO();
                    model.Group = group;
                    model.TipeNoty = type;
                    model.Title = title;
                    model.Body = body;
                    model.UrlImage = image;
                    model.Recurrent = recurrent;
                    model.DescriptionHtml = description;
                    model.Send = optionMode == 1 ? true : false;

                    var code = repo.Register(model);

                    if (code != null)
                    {
                        List<string> tokens = new List<string>();

                        foreach (NotificacionDTO item in listUser)
                        {
                            tokens.Add(item.TokenDevice);
                        }


                        NotiappServices notiserv = new NotiappServices();

                        var send = await notiserv.sendMessagesFirebase(title, body, tokens, image);
                        if (send.Success)
                        {
                            NotificacionDTO modelDetail = new NotificacionDTO();
                            foreach (NotificacionDTO item in listUser)
                            {
                                modelDetail.Send = true;
                                modelDetail.IdUser = item.IdUser;
                                modelDetail.Read = false;
                                modelDetail.CodigoNotificacionesApp = code;
                                var registerD = repo.RegisterDetail(modelDetail);
                            }
                        }

                        _objResponseModel.Date = listUser;
                        _objResponseModel.Success = true;
                        _objResponseModel.Message1 = "Notificacion enviada correctamente";
                    }

                }
                else
                {
                    //alone saved
                    NotificacionDTO model = new NotificacionDTO();
                    model.Group = group;
                    model.TipeNoty = type;
                    model.Title = title;
                    model.Body = body;
                    model.UrlImage = image;
                    model.Recurrent = recurrent;
                    model.DescriptionHtml = description;
                    model.Send = optionMode == 1 ? true : false;
                    var code = repo.Register(model);
                    if (code != null)
                    {
                        _objResponseModel.Success = true;
                        _objResponseModel.Message1 = "Notificación registrada";
                    }
                    else
                    {
                        _objResponseModel.Success = false;
                        _objResponseModel.Date = new List<string>() { "No se puedo guardar la notificación" };
                    }
                }
            }
            catch (Exception ex)
            {

                _objResponseModel.Date = new List<string>() { ex.Message };
                _objResponseModel.Success = false;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }


        //send noti
        public async Task<ActionResult> SendNotiApp(string code)
        {
            ResponseModel response = new ResponseModel();

            NotiAppRepository repo = new NotiAppRepository();
            var data = repo.getItemNotiApp(code);
            if (!string.IsNullOrEmpty(data.Group) && !string.IsNullOrEmpty(data.Title))
            {

                List<NotificacionDTO> listUser = new List<NotificacionDTO>();
                switch (data.Group)
                {
                    case "ac-1":
                        listUser = repo.ListActives(1);
                        break;
                    case "ac-7":
                        listUser = repo.ListActives(7);
                        break;
                    case "ac-15":
                        listUser = repo.ListActives(15);
                        break;
                    case "ac-30":
                        listUser = repo.ListActives(30);
                        break;
                    case "xven":
                        listUser = repo.ListByVencer(0);
                        break;
                    case "xven-3":
                        listUser = repo.ListByVencer(3);
                        break;
                    case "xven-7":
                        listUser = repo.ListByVencer(7);
                        break;
                    case "xven-15":
                        listUser = repo.ListByVencer(15);
                        break;
                    case "ven-3":
                        listUser = repo.ListVencidos(3);
                        break;
                    case "ven-7":
                        listUser = repo.ListVencidos(7);
                        break;
                    case "ven-15":
                        listUser = repo.ListVencidos(15);
                        break;
                    case "ven-30":
                        listUser = repo.ListVencidos(30);
                        break;
                    default:
                        break;
                };

                if (listUser.Count() == 0)
                {
                    response.Message1 = "No se encontraron destinatarios válidos";
                    response.Success = false;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }

                List<string> tokens = new List<string>();

                foreach (NotificacionDTO item in listUser)
                {
                    tokens.Add(item.TokenDevice);
                }


                NotiappServices notiserv = new NotiappServices();

                var send = await notiserv.sendMessagesFirebase(data.Title, data.Body, tokens, data.UrlImage);
                if (send.Success)
                {

                    //update send notiapp
                    var updSend = repo.UpdateSend(data.CodigoNotificacionesApp);

                    NotificacionDTO modelDetail = new NotificacionDTO();
                    foreach (NotificacionDTO item in listUser)
                    {
                        modelDetail.Send = true;
                        modelDetail.IdUser = item.IdUser;
                        modelDetail.Read = false;
                        modelDetail.CodigoNotificacionesApp = code;
                        var registerD = repo.RegisterDetail(modelDetail);
                    }
                }
                response.Success = true;
                response.Message1 = "Notificacion enviada correctamente";
            }
            else
            {
                response.Success = false;
                response.Message1 = "No se encontro notificación";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }



        //destroy
        public ActionResult DestroyNotiApp(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<NotificacionDTO> list = new List<NotificacionDTO>();

            bool validadorParametros = true;

            if (String.IsNullOrEmpty(code))
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }
            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoNotificacionesApp = code,

                Operation = Operation.Delete,
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
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
                _objResponseModel.Success = true;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }



        //getItem
        public ActionResult getItemNotiApp(string code)
        {
            NotiAppRepository repo = new NotiAppRepository();
            ResponseModel response = new ResponseModel();

            try
            {
                var data = repo.getItemNotiApp(code);
                response.Date = data;
                response.Success = true;
            }
            catch (Exception)
            {

                response.Success = false;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }



    }
}