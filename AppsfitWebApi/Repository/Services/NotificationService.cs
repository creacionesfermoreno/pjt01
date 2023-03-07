
using AppsfitWebApi.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace AppsfitWebApi.Repository.Services
{
    internal class NotificationService
    {
        // Autenticación con la API de Firebase
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