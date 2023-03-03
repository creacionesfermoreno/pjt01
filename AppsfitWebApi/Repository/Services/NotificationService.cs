
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
            var credentials = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:/Users/David/Desktop/SOFTWAREFITS/pjt01/AppsfitWebApi/Content/file/private_key.json"),
               // Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../Content/file/private_key.json")),


        });

            return credentials;
        }
    }
}