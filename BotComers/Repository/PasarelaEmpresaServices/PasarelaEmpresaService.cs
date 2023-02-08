using BotComers.Controllers;
using BotComers.Repository.WhatsappServices;
using MercadoPago.DataStructures.PaymentMethod;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace BotComers.Repository.PasarelaEmpresaServices
{
    public class PasarelaEmpresaService
    {
        private string hostCulqui = "https://secure.culqi.com/v2/";
        private string apiCulqui = "https://api.culqi.com/v2/";
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


    }

    public class RespondeCheckCulqui
    {
        public string id { get; set; }
        public List<object> data { get; set; }

    }
}