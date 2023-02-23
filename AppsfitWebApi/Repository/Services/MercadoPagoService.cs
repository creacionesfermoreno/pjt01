using AppsfitWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppsfitWebApi.Repository.Services
{
    public class MercadoPagoService
    {
        private string MERCADO_PAGO = ConfigurationManager.AppSettings["MERCADO_PAGO"];


        //generate preferences
        public async Task<ResponseApi> PreferencesMpagoServ(object model, string token)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{MERCADO_PAGO}/checkout/preferences";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri, content);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseMP>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.id;
                    responseModel.Message2 = resp.init_point;
                    responseModel.Message3 = resp.sandbox_init_point;
                    responseModel.Date = resp.external_reference;
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
        
        
        //consult status paymet preference
        public async Task<ResponseApi> StatePaymentMpagoServ(string token,string payment)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            object model = new { };
            ResponseApi responseModel = new ResponseApi();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string Uri = $"{MERCADO_PAGO}/v1/payments/{payment}";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.GetAsync(Uri);

                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseMP>(result);
                if (response.IsSuccessStatusCode)
                {
                    responseModel.Success = true;
                    responseModel.Message1 = resp.status;
                    responseModel.Message2 = resp.status_detail;
                    responseModel.Message3 = resp.external_reference;
                }
                else
                {
                    responseModel.Message1 = resp.error;
                    responseModel.Success = false;
                    responseModel.Status = 1;
                }
            }
            return responseModel;
        }

    }
}