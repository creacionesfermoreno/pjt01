using AppsfitWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/webhook")]
    public class WebHookController : Controller
    {
        // GET: WebHook
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("culqi")]
        public HttpResponseMessage WebHook()
        {
            ResponseApi response = new ResponseApi();
            return HttpResponseJson(response);

        }


        public HttpResponseMessage HttpResponseJson(ResponseApi responseModel)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(responseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}