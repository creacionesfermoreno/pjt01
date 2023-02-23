using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BotComers.Controllers;

namespace BotComers.Repository.Services
{
    public class MpagoService
    {
        private static string HOSTMPAGO = ConfigurationManager.AppSettings["HOSTMPAGO"];

        
        public async Task<ResponseModel> TypeIdentificatonsMPServ(string token)
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
                    string Uri = $"{HOSTMPAGO}/v1/identification_types";
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var response = await client.GetAsync(Uri);

                    var result = await response.Content.ReadAsStringAsync();
                   
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = JsonConvert.DeserializeObject<List<IdentificationType>>(result);
                        responseModel.Success = true;
                        responseModel.Date = resp;
                    }
                    else
                    {
                        var resp = JsonConvert.DeserializeObject<IdentificationTypeError>(result);
                        responseModel.Message1 = $"Token inválido: {resp.message}";
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

    }

    //******************************** Model Response*******************************************
    class IdentificationTypeError
    {
        public string message { get; set; }
        public string error { get; set; }
        public int status { get; set; }
        public List<object> cause { get; set; }

    }
      class IdentificationType
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int min_length { get; set; }
        public int max_length { get; set; }
    }
    //******************************** Model Response*******************************************
}