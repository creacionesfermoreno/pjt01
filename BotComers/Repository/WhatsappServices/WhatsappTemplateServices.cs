using BotComers.Controllers;
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
using BotComers.Models.apiwhatsapp;
using DocumentFormat.OpenXml.Wordprocessing;
using Org.BouncyCastle.Ocsp;
using RestSharp;
using System.Web.Http.Results;
using System.Net;

namespace BotComers.Repository.WhatsappServices
{
    public class WhatsappTemplateServices
    {

        //register template
        public async Task<ResponseModel> RegisterTemplateServices(string IdAccount, string token, string uriComplemente, object model)
        {
            ResponseModel _responseModel = new ResponseModel();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string EndPoint = ConfigurationManager.AppSettings["HostFB"];
                string Uri = $"{EndPoint}/{IdAccount}/{uriComplemente}";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseRegisterTemplate>(result);
                    _responseModel.Status = 0;
                    _responseModel.Message1 = resp.id;

                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseRegisterTemplate>(result);
                    _responseModel.Status = 1;
                    _responseModel.Message1 = "" + resp.error?.error_user_title;
                    _responseModel.Message2 = "" + resp.error?.error_user_msg;
                    _responseModel.Message3 = "" + resp.error?.type;
                }
                return _responseModel;
            }
        }


        //delete template
        public async Task<ResponseModel> DestroyTemplateServices(string IdAccount, string token, string uriComplemente, object model)
        {
            ResponseModel _responseModel = new ResponseModel();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string EndPoint = ConfigurationManager.AppSettings["HostFB"];
                string Uri = $"{EndPoint}/{IdAccount}/{uriComplemente}";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.DeleteAsync(Uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseRegisterTemplate>(result);
                    _responseModel.Status = 0;
                    _responseModel.Success = resp.success;

                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseRegisterTemplate>(result);
                    _responseModel.Status = 1;
                    _responseModel.Message1 = "" + resp.error?.error_user_title;
                    _responseModel.Message2 = "" + resp.error?.error_user_msg;
                    _responseModel.Message3 = "" + resp.error?.type;
                }
                return _responseModel;
            }
        }


        //send template
        public async Task<bool> SenMessageServices(string IdAccount, string token, string uriComplemente, object model)
        {
            bool resp;
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            using (var content = new ByteArrayContent(byteData))
            {
                string EndPoint = ConfigurationManager.AppSettings["HostFB"];
                string Uri = $"{EndPoint}/{IdAccount}/{uriComplemente}";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.PostAsync(Uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    resp = true;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    resp = false;

                }
                return resp;
            }
        }

        //get Item template by name template
        public async Task<ResponseModel> GetItemTemplateServices(string IdAccount, string token, string uriComplemente, object model)
        {
            ResponseModel _responseModel = new ResponseModel();
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string EndPoint = ConfigurationManager.AppSettings["HostFB"];
                string Uri = $"{EndPoint}/{IdAccount}/{uriComplemente}";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.GetAsync(Uri);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseItemTemplate>(result);
                    _responseModel.Status = 0;
                    _responseModel.Date = resp.data[0];

                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<ResponseItemTemplate>(result);
                    _responseModel.Status = 1;

                }
                return _responseModel;
            }
        }

        //service creation session graph-api
        public async Task<ResponseModel> sessionUploadServices(string IdAccount, string token, string uriComplemente)
        {
            ResponseModel _responseModel = new ResponseModel();
            var client = new HttpClient();

            string EndPoint = ConfigurationManager.AppSettings["HostFB"];
            string Uri = $"{EndPoint}/{IdAccount}/{uriComplemente}";

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");
            var values = new Dictionary<string, string>
                {
                    { "file_length", "64240" },
                    { "file_type", "image/jpg" },
                    { "file_name", "general" },
                    { "resource", "https://graph.microsoft.com" },
                    { "session_type", "attachment" },
               };

            var response = await client.PostAsync(Uri, new FormUrlEncodedContent(values));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseRegisterTemplate>(result);
                _responseModel.Success = true;
                _responseModel.Date = resp.id;
            }
            else {    _responseModel.Success = false;  }
            return _responseModel;
        }


        //upload data binary
        public ResponseModel UploadServices(string token, string uriComplemente, string image)
        {
            ResponseModel _responseModel = new ResponseModel();
            try
            {
                string EndPoint = ConfigurationManager.AppSettings["HostFB"];
                string Uri = $"{EndPoint}/{uriComplemente}";

                var client = new RestClient(Uri);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"OAuth {token}");
                request.AddHeader("file_offset", "0");
                request.AddFile("data-binary", image);
                IRestResponse response = client.Execute(request);

                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode > 0)
                {
                    var resp = JsonConvert.DeserializeObject<ResponseRegisterTemplate>(response.Content);
                    _responseModel.Success = true;
                    _responseModel.Date = resp.h;
                }
                else { _responseModel.Success = false; }
            }
            catch (Exception ex)
            {
                _responseModel.Success = false;
                _responseModel.Message1 = ex.Message;
            }
            return _responseModel;

        }


    }

    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public int code { get; set; }
        public int error_subcode { get; set; }
        public bool is_transient { get; set; }
        public string error_user_title { get; set; }
        public string error_user_msg { get; set; }
        public string fbtrace_id { get; set; }
    }

    public class ResponseRegisterTemplate
    {
        public string id { get; set; }
        public string h { get; set; }
        public Error error { get; set; }
        public bool success { get; set; }
    }




}