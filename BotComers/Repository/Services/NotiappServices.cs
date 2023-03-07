using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BotComers.Controllers;
using FirebaseAdmin.Messaging;
using System.Web.Http;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Web.Configuration;

namespace BotComers.Repository.Services
{
    public class NotiappServices
    {
        //send messages all
        public async Task<ResponseModel> sendMessagesFirebase(string title, String body, List<string> tokens, string urlImage = "")
        {
            ResponseModel response = new ResponseModel();

            var messaging = FirebaseMessaging.DefaultInstance;

            if (messaging == null)
            {
                getFirebaseApp();
                messaging = FirebaseMessaging.DefaultInstance;
            }

            Notification notification = new Notification();
            if (urlImage != "")
            {
                notification.Title = title;
                notification.Body = body;
                notification.ImageUrl = urlImage;
            }
            else
            {
                notification.Title = title;
                notification.Body = body;
            }
            


            var message = new MulticastMessage()
            {
                Notification = notification,
                Tokens = tokens,

                // Data = new Dictionary<string, string>()
                // {
                //  { "type", notif.data.type },
                //  { "name", notif.data.name },
                //  { "details", notif.data.details },
                //  { "email", notif.data.email }
                //}
            };

            var resp = await messaging.SendMulticastAsync(message);
            if (resp.FailureCount == 0)
            {
                response.Date = resp.Responses;
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Date = resp.Responses;
            }



            //if (response.FailureCount > 0)
            //{
            //    // Manejar los dispositivos que fallaron
            //    for (int i = 0; i < response.Responses.Count; i++)
            //    {
            //        if (!response.Responses[i].IsSuccess)
            //        {
            //            // Manejo del error
            //            switch (response.Responses[i].Exception.GetType())
            //            {
            //                case Type t when t == typeof(FirebaseAdmin.Messaging.FirebaseMessagingException):
            //                    var messagingException = (FirebaseAdmin.Messaging.FirebaseMessagingException)response.Responses[i].Exception;
            //                    Console.WriteLine($"Error al enviar la notificación al dispositivo {notif.tokens[i]}: {messagingException.ErrorCode}");
            //                    break;
            //                case Type t when t == typeof(FirebaseException):
            //                    var firebaseException = (FirebaseException)response.Responses[i].Exception;
            //                    Console.WriteLine($"Error de Firebase al enviar la notificación al dispositivo {notif.tokens[i]}: {firebaseException.ErrorCode}");
            //                    break;
            //                default:
            //                    Console.WriteLine($"Error desconocido al enviar la notificación al dispositivo {notif.tokens[i]}: {response.Responses[i].Exception.Message}");
            //                    break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    String messageId = "";

            //    // Obtener el messageId de la primera respuesta
            //    messageId = response.Responses[0].MessageId;

            //    if (messageId.StartsWith("projects/"))
            //    {
            //        messageId = messageId.Substring("projects/".Length);
            //    }
            //    if (messageId.Contains("/messages/"))
            //    {
            //        messageId = messageId.Substring(messageId.IndexOf("/messages/") + "/messages/".Length);
            //    }

            //    //verficar messageId
            //    if (messageId == "")
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

            //}

            return response;
        }




        public FirebaseApp getFirebaseApp()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/file/private_key.json");
            var credentials = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(path),
            });
            return credentials;
        }


    }
}