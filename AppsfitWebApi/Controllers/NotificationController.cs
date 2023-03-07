
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository.Services;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Base;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/notification")]
    public class NotificationController : ApiController
    {
        /******NOTIFICATION********/

        //[HttpPost]
        //[Route("send")]
        //public async Task<HttpResponseMessage> sendNotificationPush([FromBody] NotificationModel notif)
        //{
        //    ResponseApi responseApi = new ResponseApi();
        //    NotificationService notificationService = new NotificationService();
        //    var messaging = FirebaseMessaging.DefaultInstance;


        //    if (messaging == null)
        //    {
        //        notificationService.getFirebaseApp();
        //        messaging = FirebaseMessaging.DefaultInstance;
        //    }

        //    //
        //    var message = new Message()
        //    {
        //        Notification = new Notification()
        //        {
        //            Title = notif.title,
        //            Body = notif.body,
        //            //ImageUrl = "https://contenidosappsfit.blob.core.windows.net/socios/PerfilHombre.png",


        //        },
        //        Token = notif.token,
        //        Data = new Dictionary<string, string>()
        //       {
        //         { "type", notif.data.type },
        //         { "name", notif.data.name },
        //         { "details", notif.data.details },
        //         { "email", notif.data.email }
        //       }
        //    };

        //    //Verificar token de registro

        //    //
        //    var response = await messaging.SendAsync(message);



        //    // Obtener messageId de la respuesta.
        //    var messageId = response;
        //    if (messageId.StartsWith("projects/"))
        //    {
        //        messageId = messageId.Substring("projects/".Length);
        //    }
        //    if (messageId.Contains("/messages/"))
        //    {
        //        messageId = messageId.Substring(messageId.IndexOf("/messages/") + "/messages/".Length);
        //    }

        //    if(messageId == "")
        //    {
        //        responseApi.Message1 = "No se envio la notificación.";
        //        responseApi.Message2 = messageId;
        //        responseApi.Status = 1;
        //        responseApi.Success = false;
        //    }
        //    else
        //    {
        //        responseApi.Message1 = "Notificación enviada";
        //        responseApi.Message2 = messageId;
        //        responseApi.Status = 0;
        //        responseApi.Success = true;
        //    }


        //    //return Ok(jsonResponse);
        //    return HttpResponseJson(responseApi);
        //}


        //notifications by user
        [HttpPost]
        [Route("user")]
        public async Task<HttpResponseMessage> NotificationByUser([FromBody] NotiApp item)
        {
            ResponseApi response = new ResponseApi();

            if (!ModelState.IsValid || item == null)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Success = false;
                response.Status = 2;
                response.Errors = allErrors;
            }
            else
            {
                List<NotificacionDTO> lista = new List<NotificacionDTO>();

                NotificacionDTO oModel = new NotificacionDTO();
                oModel.DefaultKeyEmpresa = item.DefaultKeyEmpresa;
                oModel.DefaultKeyUser = item.DefaultKeyUser;

                ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()

                {
                    FilterCase = filterCaseNotificacionApp.listNotisByUser,
                    Item = oModel,
                    User = "appsFit",

                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 999,
                        PageRecords = 0
                    }
                };

                RespListNotificacionDTO oResp = null;
                using (NotificacionappLogic oLogic = new NotificacionappLogic())
                {
                    oResp = oLogic.GetList(oReq);

                    List<NotiApi> list = new List<NotiApi>();
                    foreach (NotificacionDTO modelx in oResp.List)
                    {
                        list.Add(new NotiApi()
                        {
                            Title = modelx.Title,
                            Body = modelx.Body,
                            DescriptionHtml = modelx.DescriptionHtml,
                            UrlImage = modelx.UrlImage,
                            Read = modelx.Read,
                            DescFechaCreacion = modelx.DescFechaCreacion,
                            IdUser = modelx.IdUser,
                            CodigoNotificacionesAppDestinatarios = modelx.CodigoNotificacionesAppDestinatarios,
                            CodigoNotificacionesApp = modelx.CodigoNotificacionesApp,
                        });
                    }
                    response.Date = list;
                    response.Success = true;
                }
            }
            return HttpResponseJson(response);
        }


        //update read noti user
        [HttpPost]
        [Route("read")]
        public async Task<HttpResponseMessage> UpdNotiRead([FromBody] NotiRead item)
        {
            ResponseApi response = new ResponseApi();


            if (!ModelState.IsValid || item == null)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Success = false;
                response.Status = 2;
                response.Errors = allErrors;
            }
            else
            {
                List<NotificacionDTO> list = new List<NotificacionDTO>();
                list.Add(new NotificacionDTO()
                {
                    CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                    CodigoSede = item.CodigoSede,
                    CodigoNotificacionesApp = item.CodigoNotificacionesApp,
                    CodigoNotificacionesAppDestinatarios = item.CodigoNotificacionesAppDestinatarios,
                    IdUser = item.IdUser,
                    Operation = Operation.NotiAppReadUpdate,
                });
                ReqNotificacionDTO oReq = new ReqNotificacionDTO()
                {
                    List = list,
                    User = "appsFit"
                };

                RespNotificacionDTO oResp = null;
                using (NotificacionappLogic logic = new NotificacionappLogic())
                {
                    oResp = logic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    response.Success = true;
                    response.Message1 = "actualizado correctamente";
                }
            }

            return HttpResponseJson(response);
        }




        [HttpGet]
        [Route("dev")]
        public async Task<HttpResponseMessage> dev()
        {
            ResponseApi response = new ResponseApi();
            return HttpResponseJson(response);
        }



        public HttpResponseMessage HttpResponseJson(ResponseApi responseModel)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(responseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

    }

}