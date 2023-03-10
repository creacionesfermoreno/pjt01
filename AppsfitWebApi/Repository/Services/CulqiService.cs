using AppsfitWebApi.Controllers;
using AppsfitWebApi.Helpers;
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository;
using AppsfitWebApi.Repository.CulqiServices;

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Input;

namespace AppsfitWebApi.Repository.CulqiServices
{
    public class CulqiService 
    {
 
        private string CULQISECUREV2 = ConfigurationManager.AppSettings["SECURE_CULQI_V2"];
        private string CULQIV2 = ConfigurationManager.AppSettings["CULQI_V2"];


        //validate card return source Id
        public async Task<ResponseApi> validateCardTokenService(object model ,string token)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{CULQISECUREV2}/tokens";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri,content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseCulqi>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                }
                else
                {
                    responseModel.Message1 = resp.merchant_message;
                    responseModel.Message2 = resp.user_message;
                    responseModel.Success = false;
                } 
            }
            return responseModel;
        } 
        
        
        //create customer
        public async Task<ResponseApi> CreateCustomerServ(object model ,string token)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{CULQIV2}/customers";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri,content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseCulqi>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                    responseModel.Message2 = resp.email;
                }
                else
                {
                    responseModel.Message1 = resp.merchant_message;
                    responseModel.Message2 = resp.user_message;
                    responseModel.Success = false;
                } 
            }
            return responseModel;
        }



         //create card
        public async Task<ResponseApi> CreateCardServ(object model ,string token)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{CULQIV2}/cards";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri,content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseCulqi>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                    responseModel.Message2 = resp.email;
                }
                else
                {
                    responseModel.Message1 = resp.merchant_message;
                    responseModel.Message2 = resp.user_message;
                    responseModel.Success = false;
                } 
            }
            return responseModel;
        }


           //create suscription
        public async Task<ResponseApi> CreateSuscriptionServ(object model ,string token)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{CULQIV2}/subscriptions";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri,content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseCulqi>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                    responseModel.Message2 = resp.email;
                }
                else
                {
                    responseModel.Message1 = resp.merchant_message;
                    responseModel.Message2 = resp.user_message;
                    responseModel.Success = false;
                } 
            }
            return responseModel;
        }



        //charge post 
        public async Task<ResponseApi> chargeService(ChargeAPI model, string token)
        {
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{CULQIV2}/charges";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri, content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseCulqi>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                }
                else
                {
                    responseModel.Message1 = resp.merchant_message;
                    responseModel.Message2 = resp.user_message;
                    responseModel.Success = false;
                }
            }
            return responseModel;
        }



    }

    

   
}