using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;

using BotComers.Helpers;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Gimnasio;
using System.Configuration;

using System.Collections;
using MercadoPago;
using System.Security.Principal;

using BotComers.ViewModels;
using System.Web.Helpers;
using E_DataModel;
using E_BusinessLayer;
using BotComers.Repository.PasarelaEmpresaServices;
using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Bcpg;
using BotComers.Repository;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Bibliography;
using E_DataModel.Base;

using MercadoPago.Config;


using BotComers.Repository.Services;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using System.Security.Policy;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BotComers.Controllers
{
    public class pasarelaController : Controller
    {
        private PasarelaEmpresaService pasareleSevices;
        public pasarelaController()
        {
            pasareleSevices = new PasarelaEmpresaService();
        }


        public ActionResult getTypePasarelas()
        {
            List<PlantillaFormaPagoDTO> lista = new List<PlantillaFormaPagoDTO>();

            PlantillaFormaPagoDTO plantillaFormaPagoDTO = new PlantillaFormaPagoDTO();
            plantillaFormaPagoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            plantillaFormaPagoDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterPlantillaFormaPagoDTO oReq = new ReqFilterPlantillaFormaPagoDTO()

            {
                FilterCase = filterCasePlantillaFormaPago.listTypesPasarela,
                Item = plantillaFormaPagoDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic plantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = plantillaFormaPagoLogic.PlantillaFormaPagoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        //list
        public ActionResult getAllPasarelaEM()
        {
            List<PasarelaEmpresaDTO> lista = new List<PasarelaEmpresaDTO>();

            PasarelaEmpresaDTO oPasarelaEmpresaDTO = new PasarelaEmpresaDTO();
            oPasarelaEmpresaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oPasarelaEmpresaDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()

            {
                FilterCase = FilterCasePasarelaEmpresa.List,
                Item = oPasarelaEmpresaDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListPasarelaEmpresaDTO oResp = null;
            using (PasarelaEmpresaLogic oPasarelaEmpresaLogic = new PasarelaEmpresaLogic())
            {
                oResp = oPasarelaEmpresaLogic.GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        //getItem pem
        public ActionResult getItemPasarelaEm(string code)
        {

            ResponseModel response = new ResponseModel();
            bool validadorParametros = true;
            if (string.IsNullOrEmpty(code))
            {
                response.Status = 2;
                response.Message1 = "Es obligatorio codigo.";
                response.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            PasarelaRepository repository = new PasarelaRepository();

            var data = repository.getItemAccount(code);
            if (!string.IsNullOrEmpty(data.Valor1))
            {
                response.Date = data;
                response.Status = 0;
                response.Success = true;
            }
            else
            {
                response.Status = 1;
                response.Success = false;
                response.Message1 = "No se encontro data";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //getItem pem active
        public ActionResult getItemPasarelaEmActive()
        {

            ResponseModel _objResponseModel = new ResponseModel();

            PasarelaEmpresaDTO oPasarelaEmpresaDTO = new PasarelaEmpresaDTO();
            oPasarelaEmpresaDTO.CodigoSede = Commun.CodigoSede;
            oPasarelaEmpresaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
            {
                FilterCase = FilterCasePasarelaEmpresa.ListActive,
                Item = oPasarelaEmpresaDTO,
                User = Commun.Usuario,
            };
            RespItemPasarelaEmpresaDTO oResp = null;
            using (PasarelaEmpresaLogic oPasarelaEmpresaLogic = new PasarelaEmpresaLogic())
            {
                oResp = oPasarelaEmpresaLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                _objResponseModel.Date = oResp.Item;
                _objResponseModel.Status = 0;
            }


            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }


        //delete pem
        public ActionResult deletePasarelaEM(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<PasarelaEmpresaDTO> list = new List<PasarelaEmpresaDTO>();

            bool validadorParametros = true;

            if (string.IsNullOrEmpty(code))
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }
            list.Add(new PasarelaEmpresaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoPlantillaFormaPago = code,
                Operation = Operation.DestroyPEmpresa,
            }); ;
            ReqPasarelaEmpresaDTO oReq = new ReqPasarelaEmpresaDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespPasarelaEmpresaDTO oResp = null;
            using (PasarelaEmpresaLogic logic = new PasarelaEmpresaLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }


        //register and update pasarela empresa 
        public async Task<ActionResult> registerPasarela(string code, string keypublic, string keyprivate, int status, string type, bool created, int entorProd = 0, string current = "")
        {
            ResponseModel responseModel = new ResponseModel();
            bool validator = true;
            bool entorno = Convert.ToBoolean(entorProd);

            if (string.IsNullOrEmpty(code))
            {
                responseModel.Message1 = "Campo pasarela de pago requerido";
                validator = false;
                responseModel.Status = 1;
            }
            if (string.IsNullOrEmpty(keypublic))
            {
                responseModel.Message1 = "Campo clave publica  requerido";
                validator = false;
                responseModel.Status = 1;
            }
            if (string.IsNullOrEmpty(keyprivate))
            {
                responseModel.Message1 = "Campo clave privada  requerido";
                validator = false;
                responseModel.Status = 1;
            }

            if (string.IsNullOrEmpty(type))
            {
                responseModel.Message1 = "Campo tipo methodo  requerido";
                validator = false;
                responseModel.Status = 1;
            }

            if (string.IsNullOrEmpty(type))
            {
                responseModel.Message1 = "Campo tipo methodo  requerido";
                validator = false;
                responseModel.Status = 1;
            }

            if (type == "MERCADO PAGO")
            {
                if (string.IsNullOrEmpty(current))
                {
                    responseModel.Message1 = "Campo tipo moneda  requerido";
                    validator = false;
                    responseModel.Status = 1;
                }
            }

            if (!validator)
            {
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            //****************************************** validate crendentiales **********************************
            string typeP = type.ToUpper();
            ResponseModel respValid = new ResponseModel();
            PasarelaRepository prepo = new PasarelaRepository();

            switch (typeP)
            {
                case "CULQI":
                    respValid = prepo.ValidCredentialCulqRep(keypublic, keyprivate);
                    break;

                case "PAYPAL":
                    respValid = await prepo.ValidCredentialPaypalRep(clientId: keypublic, secretId: keyprivate, entorno: entorno);
                    break;
                case "MERCADO PAGO":
                    MpagoService mpagoService = new MpagoService();
                    respValid = await mpagoService.TypeIdentificatonsMPServ(token: keyprivate);
                    break;
                default:
                    break;
            }

            //****************************************** validate crendentiales **********************************

            if (!respValid.Success)
            {
                return Json(respValid, JsonRequestBehavior.AllowGet);
            }

            //save or update
            Dictionary<string, dynamic> tdata = new Dictionary<string, dynamic>();
            tdata.Add("code", code);
            tdata.Add("kpublic", keypublic);
            tdata.Add("kpri", keyprivate);
            tdata.Add("status", status);
            tdata.Add("created", created);
            tdata.Add("entorno", entorno);
            tdata.Add("current", current);
            responseModel = prepo.registerUpAccountPay(tdata);
            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }


        //demo pay
        public async Task<ActionResult> DemoPayCard(string code, string type)
        {
            ResponseModel response = new ResponseModel();
            PasarelaRepository repository = new PasarelaRepository();

            bool validator = true;

            var account = repository.getItemAccount(code);
            if (string.IsNullOrEmpty(account.Valor1))
            {
                response.Message1 = "Campo tipo metodo pago  requerido";
                validator = false;
                response.Status = 1;
            }

            if (!validator)
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }


            //****************************************** validate crendentiales **********************************
            string typeP = type.ToUpper();
            ResponseModel respValid = new ResponseModel();
            PasarelaRepository prepo = new PasarelaRepository();
            PasarelaEmpresaService pemserv = new PasarelaEmpresaService();

            switch (typeP)
            {
                case "CULQI":
                    respValid = prepo.ValidCredentialCulqRep(account?.Valor1, account?.Valor2);
                    if (!respValid.Success)
                    {
                        return Json(respValid, JsonRequestBehavior.AllowGet);
                    }

                    string idT = respValid.Message1;

                    object dt = new
                    {
                        amount = "1500",
                        currency_code = "PEN",
                        email = "richard@piedpiper.com",
                        source_id = idT,
                        metadata = new
                        {
                            documentNumber = "77723083"
                        }
                    };

                    var charge = await pemserv.chargeCulqiServ(dt, account?.Valor2);
                    if (charge.Success)
                    {
                        response.Status = 0;
                        response.Success = true;
                        response.Message1 = "Su compra ha sido exitosa.";

                    }
                    else { response = charge; }

                    break;

                case "PAYPAL":
                    var vpaypal = await prepo.ValidCredentialPaypalRep(account?.Valor1, account?.Valor2, account.EstadoProduccion);
                    if (vpaypal.Success)
                    {
                        PasarelaHelper pasarelaHelper = new PasarelaHelper();

                        List<ItemPaypal> items = new List<ItemPaypal>();
                        UnitAmount unitAmount = new UnitAmount() { currency_code = "USD", value = 5 };
                        items.Add(new ItemPaypal() { name = "T-Shirt", description = "Green XL", quantity = 1, unit_amount = unitAmount });

                        object header = pasarelaHelper.OrderHelper(items);
                        //generate order
                        var order = await pemserv.PaypalOrderServ(header, vpaypal.Message1, account.EstadoProduccion);
                        if (order.Success)
                        {
                            response.Status = 0;
                            response.Message1 = order.Message1;
                            response.Success = true;
                        }
                        else { response = order; };
                    }
                    else { response = vpaypal; }

                    break;

                case "MERCADO PAGO":
                    MercadoPagoConfig.AccessToken = account.Valor2;
               
                    var Request = System.Web.HttpContext.Current.Request;
                    string hostAddress = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                    var request = new PreferenceRequest
                    {
                        Items = new List<PreferenceItemRequest>
                        { new PreferenceItemRequest {
                            Title = "Mi producto",
                            Quantity = 1,
                            CurrencyId = account.Valor3,
                            UnitPrice = 10,
                        },
                        },
                        AutoReturn= "approved",
                        BackUrls= new PreferenceBackUrlsRequest
                        {
                            Success = $"{hostAddress}/operacionesfit/configuracion/success",
                            Failure = $"{hostAddress}/operacionesfit/configuracion/failure",
                            Pending = $"{hostAddress}/operacionesfit/configuracion/pending",
                        },
                        
                    };

                    // Crea la preferencia usando el client
                    var client = new PreferenceClient();
                    Preference preference = await client.CreateAsync(request);
                    response.Message1 = preference.Id;
                    response.Message2 = preference.InitPoint;
                    response.Message3 = hostAddress;
                    response.Success = true;

                    break;

                default:
                    break;
            }

            //****************************************** validate crendentiales **********************************
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //capture order payed paypal
        public async Task<ActionResult> CaptureOrder(string token, string order, bool business = false, bool entornoP = false)
        {
            ResponseModel response = new ResponseModel();
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(order))
            {
                try
                {
                    PasarelaEmpresaService peserv = new PasarelaEmpresaService();
                    var capture = await peserv.PaypalOrderCaptureServ(model: new { }, token: token, orderId: order, entornoP);
                    if (capture.Success)
                    {
                        //************************* solo para pagos de negocios en dolares********************
                        if (business == true)
                        {
                            //register pago business
                            List<PurchaseUnit> dataCap = (List<PurchaseUnit>)capture.Date;
                            var transaction = dataCap[0].payments.captures[0].id;
                            decimal amount = decimal.Parse(dataCap[0].payments.captures[0].amount.value);
                            var save = MonthlyPayment(monto: amount, operacion: transaction);

                            if (save.Success)
                            {
                                response.Status = 0;
                                response.Success = true;
                                response.Message1 = "Pago realizo correctamente";
                            }
                            else
                            {
                                response.Status = 1;
                                response.Success = false;
                                response.Message1 = save.Message1;
                            }
                            //************************* solo para pagos de negocios en dolares********************
                        }
                        else
                        {
                            response.Status = 0;
                            response.Success = true;
                            response.Message1 = "Pago realizo correctamente";
                        }
                    }
                    else
                    {
                        response = capture;
                    }
                }
                catch (Exception ex)
                {

                    response.Success = false;
                    response.Status = 1;
                    response.Message1 = ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Status = 1;
                response.Message1 = "token, order requeridos";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //save buy businnes db - paypal
        public ResponseModel MonthlyPayment(decimal monto = 0, string operacion = "")
        {
            string mensaje = string.Empty;
            ResponseModel response = new ResponseModel();

            if (monto == 0 || String.IsNullOrEmpty(operacion))
            {
                response.Message1 = "Campos monto, operacion requeridas";
                response.Status = 1;
                response.Success = false;
                return response;
            }

            try
            {
                ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
                oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
                oConfiguracionDTO.FechaPago = DateTime.Now;
                oConfiguracionDTO.MontoMes = monto;
                oConfiguracionDTO.MontoAcuenta = monto;
                oConfiguracionDTO.NroOperacion = operacion;
                oConfiguracionDTO.CodigoNroCuenta = "96D64C87-8F4A-4B41-A177-7ED05C88057D";
                oConfiguracionDTO.UsuarioCreacion = "Paypal";
                oConfiguracionDTO.Operation = Operation.uspRegistrarConfiguracionPagosMensualidades;

                List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
                lista.Add(oConfiguracionDTO);

                ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
                {
                    List = lista,
                    User = "Admin"
                };
                RespConfiguracionDTO oResp = null;

                ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
                oResp = oConfiguracionLogic.ExecuteTransac(oReq);

                if (oResp.Success)
                {
                    response.Message1 = "Datos Guardados Correctamente";
                    response.Status = 0;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message1 = ex.Message;
                response.Status = 1;
                response.Success = false;

            }
            return response;

        }


        //*******************************************   PAY BUSINESS ************************************

        //buy - business - order
        public async Task<ActionResult> PaypalOrderBusiness(decimal monto = 0, string description = "", string clientP = "", string secretP = "", bool entornoP = false)
        {
            ResponseModel response = new ResponseModel();
            PasarelaRepository prepo = new PasarelaRepository();

            if (monto == 0 || String.IsNullOrEmpty(description) || String.IsNullOrEmpty(clientP) || String.IsNullOrEmpty(secretP))
            {
                response.Status = 1;
                response.Message1 = "Campos monto ,  descripcion requeridos";
                response.Success = false;
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //generate token
                var vpaypal = await prepo.ValidCredentialPaypalRep(clientId: clientP, secretId: secretP, entorno: entornoP);
                if (vpaypal.Success)
                {
                    PasarelaHelper pasarelaHelper = new PasarelaHelper();

                    List<ItemPaypal> items = new List<ItemPaypal>();
                    UnitAmount unitAmount = new UnitAmount() { currency_code = "USD", value = monto };
                    items.Add(new ItemPaypal() { name = "Software Appsfit", description = description, quantity = 1, unit_amount = unitAmount });

                    object header = pasarelaHelper.OrderHelper(items);

                    //generate order
                    PasarelaEmpresaService pemserv = new PasarelaEmpresaService();
                    var order = await pemserv.PaypalOrderServ(header, vpaypal.Message1, entornoP);
                    if (order.Success)
                    {
                        response.Status = 0;
                        response.Message1 = order.Message1;
                        response.Success = true;
                    }
                    else { response = order; };
                }
                else { response = vpaypal; }
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Message1 = ex.Message;
                response.Success = false;

            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //*******************************************   END PAY BUSINESS ************************************



        public async Task<ActionResult> DemoMPago()
        {
            ResponseModel response = new ResponseModel();


            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }


}