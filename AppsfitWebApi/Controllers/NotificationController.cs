
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository.Services;
using E_DataModel.Base;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/notification")]
    public class NotificationController : ApiController
    {
        /******NOTIFICATION********/

        [HttpPost]
        [Route("send")]
        public async Task<HttpResponseMessage> sendNotificationPush([FromBody] NotificationModel notif)
        {
            ResponseApi responseApi = new ResponseApi();
            NotificationService notificationService = new NotificationService();
            var messaging = FirebaseMessaging.DefaultInstance;

            
            if (messaging == null)
            {
                notificationService.getFirebaseApp();
                messaging = FirebaseMessaging.DefaultInstance;
            }
            
            //
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = notif.title,
                    Body = notif.body,
                    //ImageUrl = "https://contenidosappsfit.blob.core.windows.net/socios/PerfilHombre.png",
                    
                    
                },
                Token = notif.token,
                Data = new Dictionary<string, string>()
               {
                 { "type", notif.data.type },
                 { "name", notif.data.name },
                 { "details", notif.data.details },
                 { "email", notif.data.email }
               }
            };

            //Verificar token de registro
            
            //
            var response = await messaging.SendAsync(message);

           

            // Obtener messageId de la respuesta.
            var messageId = response;
            if (messageId.StartsWith("projects/"))
            {
                messageId = messageId.Substring("projects/".Length);
            }
            if (messageId.Contains("/messages/"))
            {
                messageId = messageId.Substring(messageId.IndexOf("/messages/") + "/messages/".Length);
            }

            if(messageId == "")
            {
                responseApi.Message1 = "No se envio la notificación.";
                responseApi.Message2 = messageId;
                responseApi.Status = 1;
                responseApi.Success = false;
            }
            else
            {
                responseApi.Message1 = "Notificación enviada";
                responseApi.Message2 = messageId;
                responseApi.Status = 0;
                responseApi.Success = true;
            }


            //return Ok(jsonResponse);
            return HttpResponseJson(responseApi);
        }

        [HttpPost]
        [Route("sendUsers")]
        public async Task<HttpResponseMessage> sendNotificationPushMultiple([FromBody] NotificationModel notif)
        {
            ResponseApi responseApi = new ResponseApi();
            NotificationService notificationService = new NotificationService();
            NotificationModel model = new NotificationModel();
            var messaging = FirebaseMessaging.DefaultInstance;

            /*var registrationTokens = new List<string>()
            {
                ""
            };*/

            if (messaging == null)
            {
                notificationService.getFirebaseApp();
                messaging = FirebaseMessaging.DefaultInstance;
            }

            //
            var message = new MulticastMessage()
            {
                Notification = new Notification()
                {
                    Title = notif.title,
                    Body = notif.body,
                    
                },
                Tokens = notif.tokens,

                Data = new Dictionary<string, string>()
                {
                 { "type", notif.data.type },
                 { "name", notif.data.name },
                 { "details", notif.data.details },
                 { "email", notif.data.email }
               }
            };

            var response = await messaging.SendMulticastAsync(message);

            // Obtener messageId de la respuesta.


            if (response.FailureCount > 0)
            {
                // Manejar los dispositivos que fallaron
                for (int i = 0; i < response.Responses.Count; i++)
                {
                    if (!response.Responses[i].IsSuccess)
                    {
                        // Manejo del error
                        switch (response.Responses[i].Exception.GetType())
                        {
                            case Type t when t == typeof(FirebaseAdmin.Messaging.FirebaseMessagingException):
                                var messagingException = (FirebaseAdmin.Messaging.FirebaseMessagingException)response.Responses[i].Exception;
                                Console.WriteLine($"Error al enviar la notificación al dispositivo {notif.tokens[i]}: {messagingException.ErrorCode}");
                                break;
                            case Type t when t == typeof(FirebaseException):
                                var firebaseException = (FirebaseException)response.Responses[i].Exception;
                                Console.WriteLine($"Error de Firebase al enviar la notificación al dispositivo {notif.tokens[i]}: {firebaseException.ErrorCode}");
                                break;
                            default:
                                Console.WriteLine($"Error desconocido al enviar la notificación al dispositivo {notif.tokens[i]}: {response.Responses[i].Exception.Message}");
                                break;
                        }
                    }
                }
            }
            else
            {
                String messageId = "";

                // Obtener el messageId de la primera respuesta
                messageId = response.Responses[0].MessageId;

                if (messageId.StartsWith("projects/"))
                {
                    messageId = messageId.Substring("projects/".Length);
                }
                if (messageId.Contains("/messages/"))
                {
                    messageId = messageId.Substring(messageId.IndexOf("/messages/") + "/messages/".Length);
                }

                //verficar messageId
                if (messageId == "")
                {
                    responseApi.Message1 = "No se envio la notificación.";
                    responseApi.Message2 = messageId;
                    responseApi.Status = 1;
                    responseApi.Success = false;
                }
                else
                {
                    responseApi.Message1 = "Notificación enviada";
                    responseApi.Message2 = messageId;
                    responseApi.Status = 0;
                    responseApi.Success = true;
                }

            }



            return HttpResponseJson(responseApi);
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