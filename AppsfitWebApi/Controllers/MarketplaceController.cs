﻿using AppsfitWebApi.Helpers;
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository;
using AppsfitWebApi.Repository.CulqiServices;
using E_BusinessLayer.CentroEntrenamiento;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/marketplace")]
    public class MarketplaceController : ApiController
    {

 

        //*********************************************** PLANES **********************************************

        [HttpPost]
        [Route("listarplanes")] //CodigoUnidadNegocio,CodigoSede
        public HttpResponseMessage getListPlanesApp(PlanesDTO request)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            bool validadorParametros = true;

            if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Codigo de sede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return HttpResponseJson(_objResponseModel);
            }

            try
            {
                List<PlanesApiModel> list = new List<PlanesApiModel>();
                PlanesDTO oPlanesDTO = new PlanesDTO();
                oPlanesDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
                oPlanesDTO.CodigoSede = request.CodigoSede;
                ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
                {
                    FilterCase = filterCasePlanes.listApp,
                    Item = oPlanesDTO,
                    User = "appsfit",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 0,
                        PageRecords = 9999
                    }
                };
                RespListPlanesDTO oResp = null;
                using (PlanesLogic oPlanesLogic = new PlanesLogic())
                {
                    oResp = oPlanesLogic.PlanesGetList(oReq);
                }
                if (oResp.Success)
                {

                    foreach (PlanesDTO item in oResp.List)
                    {
                        PlanesApiModel modelApi = new PlanesApiModel();
                        modelApi.CodigoPaquete = item.CodigoPaquete;
                        modelApi.CodigoTipoPaquete = item.CodigoTipoPaquete;
                        modelApi.DesTiempoPlan = item.DesTiempoMembresia;
                        modelApi.Descripcion = item.Descripcion;
                        modelApi.EstadoMembresiaInterdiaria = item.EstadoMembresiaInterdiaria;
                        modelApi.CongelamientoVigente = item.CongelamientoVigente;
                        modelApi.ValorTiempoPlan = item.ValorTiempoPlan;
                        modelApi.ValorSesiones = item.ValorSesiones;
                        modelApi.Costo = item.Costo;
                        modelApi.NroCupo = item.NroCupo;
                        modelApi.FechaVencimiento = item.FechaVencimiento;
                        modelApi.DesNomProfesor = item.desNomProfesor;
                        modelApi.DesTipoPaquete = item.DesTipoPaquete;
                        list.Add(modelApi);
                    }

                }
                _objResponseModel.Date = list;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
            }
            return HttpResponseJson(_objResponseModel);
        }


        [HttpPost]
        [Route("membresia/compra")]
        public async Task<HttpResponseMessage> ChargePost([FromBody] RequestCharge request)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                _objResponseModel.Success = false;
                _objResponseModel.Status = 2;
                _objResponseModel.Errors = allErrors;
            }
            else
            {
                bool validator = true;
                CulqiService services = new CulqiService();
                PasarelaEmpresaApiRepository repository = new PasarelaEmpresaApiRepository();

                try
                {
                    //search acount pasarela
                    var accountPasarela = repository.PasarelaActiveRepository(request.DefaultKeyEmpresa);
                    if (string.IsNullOrEmpty(accountPasarela?.Valor1))
                    {
                        _objResponseModel.Message1 = "No tiene una cuenta de pasarela registrada";
                        _objResponseModel.Status = 1;
                        _objResponseModel.Success = false;
                        validator = false;
                    }

                    if (!validator)
                    {
                        return HttpResponseJson(_objResponseModel);
                    }


                    //validate card return token id
                    request.card.email = request.Email;
                    var validate = await services.validateCardTokenService(request.card, accountPasarela?.Valor1);

                    if (validate.Success == true)
                    {
                        ChargeAPI ocharge = new ChargeAPI()
                        {
                            amount = request.charge.amount,
                            currency_code = "PEN",
                            email = request.Email,
                            source_id = validate?.Message1,
                            capture = false,
                            metadata = new MetadataCharge
                            {
                                documentNumber = request.sale.RUC_DNI,
                            }
                        };

                        //charge culqi
                        var charge = await services.chargeService(ocharge, accountPasarela?.Valor2);


                        if (charge.Success == true)
                        {
                            //********************* last process charge culqi********************

                            MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
                            AspNetHelper oHelper = new AspNetHelper();

                            //save membresia
                            int value = oHelper.ValidateInputMembresia(request.membresia.FechaFinMembresia);
                            request.membresia.TipoIngreso = value;
                            request.membresia.CodigoMebresiaOrigen = value;
                            request.membresia.DefaultKeyEmpresa = request.DefaultKeyEmpresa;
                            request.membresia.CodigoSocio = request.CodigoSocio;
                            int IdMembresia = repositoryMem.GuardarMembresiaContratoRepository(request.membresia);

                            //pay
                            MembresiaAPI membresiaAPI = new MembresiaAPI();
                            membresiaAPI.CodigoMembresia = IdMembresia;
                            membresiaAPI.Costo = request.membresia.Costo;
                            membresiaAPI.Descripcion = request.membresia.Descripcion;

                            VentasDTO oVentasDTO = new VentasDTO();
                            oVentasDTO.DefaultKeyEmpresa = request.DefaultKeyEmpresa;
                            oVentasDTO.CodigoSocio = request.CodigoSocio;
                            oVentasDTO.RazonSocial_Sr = request.sale.RazonSocial_Sr;
                            oVentasDTO.RUC_DNI = request.sale.RUC_DNI;
                            oVentasDTO.Direccion = request.sale.Direccion;
                            oVentasDTO.TotalNeto = request.membresia.Costo;
                            oVentasDTO.Comentario = request.sale.Comentario;
                            oVentasDTO.NroTarjeta = "";
                            oVentasDTO.NroBoucher = request.sale.NroBoucher;

                            var pay = repositoryMem.PayMembresiaRepository(oVentasDTO, request.CodigoSede, request.CodigoUnidadNegocio, membresiaAPI);
                            if (pay.Success == true)
                            {

                                //************************* BackgroundJob *********************
                                var Request = HttpContext.Current.Request;
                                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                                _ =  JobApiHelper.SendReceiptJob(request.CodigoSede, request.CodigoUnidadNegocio,int.Parse(pay.Message1),request.Email, baseUrl);
                                //************************* BackgroundJob *********************
                                _objResponseModel.Message1 = "Su compra ha sido exitosa.";
                                _objResponseModel.Success = true;
                                _objResponseModel.Status = 0;
                            }
                            else
                            {
                                _objResponseModel.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                                _objResponseModel.Success = false;
                                _objResponseModel.Status = 1;
                            }

                        }
                        else
                        {
                            _objResponseModel.Message1 = charge.Message1;
                            _objResponseModel.Message2 = charge.Message2;
                            _objResponseModel.Success = false;
                            _objResponseModel.Status = 1;
                        }
                    }
                    else
                    {
                        //token card invalid
                        _objResponseModel.Message1 = validate.Message1;
                        _objResponseModel.Message2 = validate.Message2;
                        _objResponseModel.Success = false;
                        _objResponseModel.Status = 1;
                    }

                }
                catch (Exception ex)
                {
                    _objResponseModel.Message1 = ex.Message;
                    _objResponseModel.Success = false;
                    _objResponseModel.Status = 1;
                }
            }

            return HttpResponseJson(_objResponseModel);

        }

        [HttpGet]
        [Route("dev")]
        public HttpResponseMessage dev()
        {
            ResponseApi _objResponseModel = new ResponseApi();
            _objResponseModel.Message1 = "dev api success";
            _objResponseModel.Status = 0;
            return HttpResponseJson(_objResponseModel);
        }



        //*********************************************** PLANES **********************************************

        public HttpResponseMessage HttpResponseJson(ResponseApi responseModel)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(responseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}