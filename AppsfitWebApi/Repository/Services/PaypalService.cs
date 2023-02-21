using AppsfitWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json.Serialization;
using static iTextSharp.text.pdf.AcroFields;
using AppsfitWebApi.Helpers;
using System.Configuration;

namespace AppsfitWebApi.Repository.Services
{
    public class PaypalService
    {
        
        public string PAYPAL_DEMO = ConfigurationManager.AppSettings["HOST_PAYPAL_DEMO"];
        public string PAYPAL_PROD = ConfigurationManager.AppSettings["HOST_PAYPAL"];


        //generate token
        public async Task<ResponseApi> PaypalTokenService(string clientId, string secretId,bool entorno = false)
        {
            object model = new { };
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {

                string Uri = entorno? $"{PAYPAL_PROD}/v1/oauth2/token": $"{PAYPAL_DEMO}/v1/oauth2/token"; 

                var authenticationString = $"{clientId}:{secretId}";
                var credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                client.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");
                var data = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                };
                var response = await client.PostAsync(Uri, new FormUrlEncodedContent(data));

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponsePaypay>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.access_token;
                }
                else
                {
                    responseModel.Message1 = resp.error_description;
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            return responseModel;
        }


        //generate order
        public async Task<ResponseApi> PaypalOrderService(object model, string token,bool entorno = false)
        {
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {

                string Uri = entorno ? $"{PAYPAL_PROD}/v2/checkout/orders" : $"{PAYPAL_DEMO}/v2/checkout/orders";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri, content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponsePaypay>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                    responseModel.Message2 = resp.status;
                    responseModel.Date = resp.links;
                }
                else
                {
                    responseModel.Message1 = resp.message;
                    responseModel.Message2 = resp.error_description;
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            return responseModel;
        }

        //capture order
        public async Task<ResponseApi> PaypalOrderCaptureService(object model, string token, string orderId,bool entorno = false)
        {
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {

                string Uri = entorno ? $"{PAYPAL_PROD}/v2/checkout/orders/{orderId}/capture" : $"{PAYPAL_DEMO}/v2/checkout/orders/{orderId}/capture";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri, content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponsePaypay>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                    responseModel.Message2 = resp.status;

                }
                else
                {
                    responseModel.Message1 = resp.name;
                    responseModel.Message2 = resp.message;
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            return responseModel;
        }


        //show order items
        public async Task<ResponseApi> PaypalOrderDetailService(string token, string orderId,bool entorno = false)
        {
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();

            string Uri = entorno ? $"{PAYPAL_PROD}/v2/checkout/orders/{orderId}" : $"{PAYPAL_DEMO}/v2/checkout/orders/{orderId}";


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await client.GetAsync(Uri);
            var result = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<OrderDetailPaypal>(result);
            if (response.IsSuccessStatusCode)
            {
                responseModel.Success = true;
                responseModel.Date = resp.purchase_units;
            }
            else
            {
                responseModel.Message1 = resp.name;
                responseModel.Message2 = resp.message;
                responseModel.Success = false;
                responseModel.Status = 1;
            }

            return responseModel;
        }

    }



}