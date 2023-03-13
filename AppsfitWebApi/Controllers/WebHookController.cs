using AppsfitWebApi.Helpers;
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository;
using AppsfitWebApi.Repository.Services;
using E_DataModel.Gimnasio;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/webhook")]
    public class WebHookController : ApiController
    {
        // GET: WebHook


        [HttpPost]
        [Route("culqi")]
        public async Task<HttpResponseMessage> WebHookHandle([FromBody] WHCulqiSuscription req)
        {
            ResponseApi response = new ResponseApi();

            //var payload = await new StreamReader(req.B).ReadToEndAsync();
            string codeValiCreation = null;
            switch (req.type)
            {
                case "subscription.creation.succeeded":
                    codeValiCreation = "creation";

                    break;
                case "subscription.charge.succeeded":
                    if (codeValiCreation == null)
                    {
                        //cobros recurrrentes
                        HomeRepository hrepo = new HomeRepository();
                        var data =  hrepo.getMembresiaSuscriptionByIdSuscription(req.idSubscription);
                        if(data.DataJsonPasarela == null || data.DataJsonPasarela == "")
                        {
                            response.Message1 = "error";
                            response.Success = false;
                            return HttpResponseJson(response);
                        }
                        ReqSuscriptionCulqi DataJsonPasarela = JsonConvert.DeserializeObject<ReqSuscriptionCulqi>(data.DataJsonPasarela);

                        var reqData = DataJsonPasarela;


                        //config dates membresia
                        var fstart = (DateTime)reqData.membresia.FechaInicio;
                        var fend = (DateTime)reqData.membresia.FechaFin;

                        DateTime date_1 = new DateTime(fstart.Year, fstart.Month, fstart.Day);
                        DateTime date_2 = new DateTime(fend.Year, fend.Month, fend.Day);

                        TimeSpan Diff_dates = date_2.Subtract(date_1);
                        int daysDiff = Diff_dates.Days;

                        DateTime now = Convert.ToDateTime(DateTime.Now);
                        var FInicio = now.AddDays(1);
                        var FFin = now.AddDays(daysDiff);

                        reqData.membresia.FechaInicio = FInicio;
                        reqData.membresia.FechaFin = FFin;
                        reqData.membresia.FechaFinMembresia = now;

                        //********************* last process charge culqi********************

                        MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
                        AspNetHelper oHelper = new AspNetHelper();

                        //save membresia
                        int value = oHelper.ValidateInputMembresia(reqData.membresia.FechaFinMembresia);
                        reqData.membresia.TipoIngreso = value;
                        reqData.membresia.CodigoMebresiaOrigen = value;
                        reqData.membresia.DefaultKeyEmpresa = reqData.DefaultKeyEmpresa;
                        reqData.membresia.CodigoSocio = reqData.CodigoSocio;
                        int IdMembresia = repositoryMem.GuardarMembresiaContratoRepository(reqData.membresia);

                        //pay
                        MembresiaAPI membresiaAPI = new MembresiaAPI();
                        membresiaAPI.CodigoMembresia = IdMembresia;
                        membresiaAPI.Costo = reqData.membresia.Costo;
                        membresiaAPI.Descripcion = reqData.membresia.Descripcion;

                        VentasDTO oVentasDTO = new VentasDTO();
                        oVentasDTO.DefaultKeyEmpresa = reqData.DefaultKeyEmpresa;
                        oVentasDTO.CodigoSocio = reqData.CodigoSocio;
                        oVentasDTO.RazonSocial_Sr = reqData.sale.RazonSocial_Sr;
                        oVentasDTO.RUC_DNI = reqData.sale.RUC_DNI;
                        oVentasDTO.Direccion = reqData.sale.Direccion;
                        oVentasDTO.TotalNeto = reqData.membresia.Costo;
                        oVentasDTO.Comentario = $"suscripción culqi, IdSuscripción:{req.idSubscription}";
                        oVentasDTO.NroTarjeta = "";
                        oVentasDTO.NroBoucher = reqData.sale.NroBoucher;

                        var pay = repositoryMem.PayMembresiaRepository(oVentasDTO, reqData.CodigoSede, reqData.CodigoUnidadNegocio, membresiaAPI);
                        if (pay.Success == true)
                        {

                            //************************* BackgroundJob *********************
                            var Request = HttpContext.Current.Request;
                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                            _ = JobApiHelper.SendReceiptJob("", reqData.CodigoSede, reqData.CodigoUnidadNegocio, int.Parse(pay.Message1), reqData.Email, baseUrl, 2);
                            //************************* BackgroundJob *********************
                            response.Message1 = "Su compra ha sido exitosa.";
                            response.Message2 = req.idSubscription;
                            response.Success = true;
                            response.Status = 0;
                        }
                        else
                        {
                            response.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                            response.Success = false;
                            response.Status = 1;
                        }
                    }

                    break;
                default:
                    break;
            }

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