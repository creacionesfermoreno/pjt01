using BotComers.Controllers;
using BotComers.Repository.WhatsappServices;
using MercadoPago.DataStructures.PaymentMethod;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BotComers.Repository.PasarelaEmpresaServices
{
    public class PasarelaEmpresaService
    {
        private string CULQISECUREV2 = ConfigurationManager.AppSettings["SECURE_CULQI_V2"];
        private string CULQIV2 = ConfigurationManager.AppSettings["CULQI_V2"];


        public string PAYPAL_DEMO = ConfigurationManager.AppSettings["HOST_PAYPAL_DEMO"];
        public string PAYPAL_PROD = ConfigurationManager.AppSettings["HOST_PAYPAL"];

        //*************************************** SERVICES CULQI *******************************
        public ResponseModel validatekeyService(string key)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient($"{CULQISECUREV2}/tokens");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {key}");
                request.AddHeader("Content-Type", "application/json");

                var body = new
                {
                    card_number = "4111111111111111",
                    cvv = "123",
                    expiration_month = 9,
                    expiration_year = 25,
                    email = "ichard@piedpiper.com",
                    metadata = new
                    {
                        dni = "5831543"
                    }
                };

                string requestJson = JsonConvert.SerializeObject(body);
                request.AddParameter("application/json", requestJson, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode > 0)
                {
                    var resp = JsonConvert.DeserializeObject<RespondeCheckCulqui>(response.Content);
                    if (resp.id != null)
                    {
                        responseModel.Success = true;
                        responseModel.Date = resp.id;
                        responseModel.Message1 = resp.id;
                    }
                    else { responseModel.Success = false; }

                }
                else { responseModel.Success = false; }
            }
            catch (Exception)
            {
                responseModel.Success = false;
            }
            return responseModel;
        }

        public ResponseModel validatekeyPrivateService(string key)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient($"{CULQIV2}/charges");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Bearer {key}");
                request.AddHeader("Content-Type", "application/json");

                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode > 0)
                {
                    var resp = JsonConvert.DeserializeObject<RespondeCheckCulqui>(response.Content);
                    if (resp.data != null)
                    {
                        responseModel.Success = true;
                    }
                    else { responseModel.Success = false; }

                }
                else { responseModel.Success = false; }
            }
            catch (Exception)
            {
                responseModel.Success = false;
            }
            return responseModel;
        }

        //charge 
        public async Task<ResponseModel> chargeCulqiServ(object model, string token)
        {
            ResponseModel responseModel = new ResponseModel();
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
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            return responseModel;
        }


        //CULQI PLANS
        public async Task<ResponseModel> PlanesCulqiServ(string token)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var client = new HttpClient();
                string Uri = $"{CULQIV2}/plans";
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.GetAsync(Uri);
                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<PlanCulqi>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Date = resp.data;
                }
                else
                {
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            catch (Exception ex)
            {
                responseModel.Status = 1;
                responseModel.Message1 = ex.Message;
            }

            return responseModel;

        }


        //*************************************** END SERVICES CULQI *******************************


        //***************************************  SERVICES PAYPAL *******************************

        //generate token
        public async Task<ResponseModel> PaypalTokenService(string clientId, string secretId, bool entorno = false)
        {
            object model = new { };
            ResponseModel responseModel = new ResponseModel();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                using (var content = new ByteArrayContent(byteData))
                {
                    string Uri;
                    if (entorno == false) { Uri = $"{PAYPAL_DEMO}/v1/oauth2/token"; }
                    else { Uri = $"{PAYPAL_PROD}/v1/oauth2/token"; }


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
            }
            catch (Exception ex)
            {
                responseModel.Message1 = ex.Message;
                responseModel.Success = false;
                responseModel.Status = 1;
            }

            return responseModel;
        }


        //generate order
        public async Task<ResponseModel> PaypalOrderServ(object model, string token, bool entorno = false)
        {
            ResponseModel responseModel = new ResponseModel();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {

                string Uri;
                if (entorno == false) { Uri = $"{PAYPAL_DEMO}/v2/checkout/orders"; }
                else { Uri = $"{PAYPAL_PROD}/v2/checkout/orders"; }

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
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            return responseModel;
        }


        //capture order
        public async Task<ResponseModel> PaypalOrderCaptureServ(object model, string token, string orderId, bool entorno = false)
        {
            ResponseModel responseModel = new ResponseModel();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {

                string Uri;
                if (entorno == false) { Uri = $"{PAYPAL_DEMO}/v2/checkout/orders/{orderId}/capture"; }
                else { Uri = $"{PAYPAL_PROD}/v2/checkout/orders/{orderId}/capture"; }

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
                    responseModel.Date = resp.purchase_units;
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


        //***************************************  END SERVICES PAYPAL *******************************

    }

    public class RespondeCheckCulqui
    {
        public string id { get; set; }
        public List<object> data { get; set; }

    }



    public class ResponsePaypay
    {
        public string access_token { get; set; }
        public string error_description { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public List<Link> links { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }



    //**************************************************************
    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class Capture
    {
        public string id { get; set; }
        public string status { get; set; }
        public Amount amount { get; set; }
    }

    public class Payments
    {
        public List<Capture> captures { get; set; }
    }

    public class PurchaseUnit
    {
        public Payments payments { get; set; }
    }





    //**************************************************************************
    public class ResponseCulqi
    {
        public string id { get; set; }
        public string merchant_message { get; set; }

    }


    public class Datum
    {
        public string id { get; set; }
        public long creation_date { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string currency_code { get; set; }
        public int interval_count { get; set; }
        public string interval { get; set; }
        public int limit { get; set; }
        public int trial_days { get; set; }
        public int total_subscriptions { get; set; }

    }



    public class PlanCulqi
    {
        public List<Datum> data { get; set; }
    }

}