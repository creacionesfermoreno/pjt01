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
        public async Task<ActionResult> registerPasarela(string code, string keypublic, string keyprivate, int status, string type, bool created)
        {
            ResponseModel responseModel = new ResponseModel();
            bool validator = true;

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
                    respValid = await prepo.ValidCredentialPaypalRep(keypublic, keyprivate);
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
                    var vpaypal = await prepo.ValidCredentialPaypalRep(account?.Valor1, account?.Valor2);
                    if (vpaypal.Success)
                    {
                        PasarelaHelper pasarelaHelper = new PasarelaHelper();

                        List<ItemPaypal> items = new List<ItemPaypal>();
                        UnitAmount unitAmount = new UnitAmount() { currency_code = "USD", value = decimal.Parse("5.00") };
                        items.Add(new ItemPaypal() { name = "T-Shirt", description = "Green XL", quantity = 1, unit_amount = unitAmount });

                        object header = pasarelaHelper.OrderHelper(items);
                        //generate order
                        var order = await pemserv.PaypalOrderServ(header, vpaypal.Message1);
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

                default:
                    break;
            }

            //****************************************** validate crendentiales **********************************
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //capture order payed paypal
        public async Task<ActionResult> CaptureOrder(string token, string order)
        {
            ResponseModel response = new ResponseModel();
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(order))
            {
                try
                {
                    PasarelaEmpresaService peserv = new PasarelaEmpresaService();
                    var capture = await peserv.PaypalOrderCaptureServ(model: new { }, token: token, orderId: order);
                    if (capture.Success)
                    {
                        response.Status = 0;
                        response.Success = true;
                        response.Message1 = "Pago realizo correctamente";
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

        

    }


}