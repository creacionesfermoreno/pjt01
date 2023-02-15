using BotComers.Controllers;
using BotComers.Repository.WhatsappServices;
using MercadoPago.DataStructures.PaymentMethod;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BotComers.Repository.PasarelaEmpresaServices
{
    public class PasarelaEmpresaService
    {
        private string hostCulqui = "https://secure.culqi.com/v2/";
        private string apiCulqui = "https://api.culqi.com/v2/";

        private string hostPaypal = "https://api-m.sandbox.paypal.com";

        //*************************************** SERVICES CULQI *******************************
        public ResponseModel validatekeyService(string key)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient($"{hostCulqui}tokens");
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
                    if(resp.id != null)
                    {
                        responseModel.Success = true;
                        responseModel.Date = resp.id;
                    }
                    else {  responseModel.Success = false;}

                }else { responseModel.Success = false; }
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
                var client = new RestClient($"{apiCulqui}charges");
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
                    if(resp.data != null)
                    {
                        responseModel.Success = true;
                    }
                    else {  responseModel.Success = false;}

                }else { responseModel.Success = false; }
            }
            catch (Exception)
            {
                responseModel.Success = false;
            }
            return responseModel;
        }
        //*************************************** END SERVICES CULQI *******************************


        //***************************************  SERVICES PAYPAL *******************************

        //generate token
        public async Task<ResponseModel> PaypalTokenService(string clientId, string secretId)
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
                    string Uri = $"{hostPaypal}/v1/oauth2/token";

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
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }
}