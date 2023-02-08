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
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using BotComers.Helpers;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Gimnasio;
using System.Configuration;
using BotComers.Models.apiwhatsapp;
using BotComers.Repository.Gimnasio;
using BotComers.Repository.WhatsappServices;// .Utils.Whatsapp.Templates;
using iTextSharp.tool.xml.html;
//using BotComers.Services.Whatsapp.Templates;
using System.Collections;
using MercadoPago;
using System.Security.Principal;
using static iTextSharp.tool.xml.html.HTML;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Reflection;
using BotComers.ViewModels;
using NUnit.Framework.Internal;
using ExcelDataReader;
using SpreadsheetLight;
using PagedList;
using System.Globalization;
//using CsvHelper;
//using CsvHelper.Configuration;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace BotComers.Controllers
{
    public class whatsappController : Controller
    {


        // GET: whatsapp
        public ActionResult Index()
        {
            return View();
        }




        //send message 
        public async Task<ActionResult> sendMessage(int tipo, string phone, string codeconfig, string message = "")
        {

            ResponseHttp _response = new ResponseHttp();
            bool validator = true;

            if (phone == String.Empty)
            {
                _response.Message = "campo número telefonico requerido";
                validator = false;
            }
            if (codeconfig == String.Empty || codeconfig == null)
            {
                _response.Message = "campo codigo configuracon whatsapp requerido";
                validator = false;
            }
            if (!validator)
            {
                return Json(_response, JsonRequestBehavior.AllowGet);
            }



            try
            {
                object data = null;
                if (tipo == 1)
                {
                    //template
                    Languagex _language = new Languagex();
                    _language.code = "en_US";

                    TemplateWhatsapp _template = new TemplateWhatsapp();
                    _template.messaging_product = "whatsapp";
                    _template.to = phone;
                    _template.type = "template";
                    _template.template = new Template() { name = "hello_world", language = _language };
                    data = _template;
                }
                else
                {

                    if (string.IsNullOrEmpty(message))
                    {
                        _response.Message = "campo mensaje requerido";
                        validator = false;
                    }


                    Individual _individual = new Individual();
                    _individual.messaging_product = "whatsapp";
                    _individual.recipient_type = "individual";
                    _individual.to = phone;
                    _individual.type = "text";
                    _individual.text = new Text() { preview_url = false, body = message };
                    data = _individual;

                }

                WhatsappTemplateServices _send = new WhatsappTemplateServices();
                WhatsappRepository _repository = new WhatsappRepository();

                //get account
                var account = _repository.getItemByCodeRepository(codeconfig);
                if (account.Token == String.Empty && account.IdAccount == String.Empty)
                {
                    _response.Message = "Cuenta inválida";
                    _response.Status = false;
                    validator = false;
                }

                if (!validator)
                {
                    return Json(_response, JsonRequestBehavior.AllowGet);
                }
                bool send = await _send.SenMessageServices(account?.IdPhone, account?.Token, "messages", data);

                if (send)
                {

                    _response.Message = "enviado correctamente";
                    _response.Status = true;
                }
                else
                {
                    _response.Message = "no se pudo enviar";
                    _response.Status = false;
                }
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.ToString();

            }
            return Json(_response, JsonRequestBehavior.AllowGet);

        }

        //**************************** START CONFIG ************************************  

        //register
        public async Task<ActionResult> registerConfig(string idapp, string idphone, string idaccount, string numberphone, string token, string sdk)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();
            bool validadorParametros = true;


            if (string.IsNullOrEmpty(idapp))
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio Identificador de la aplicacion";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (idphone == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio Identificador de número de teléfono.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (idaccount == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio Identificador de la cuenta de WhatsApp Business";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (sdk == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar Verison SDK.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (token == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un Token.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (numberphone == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un número telefonico.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }


            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            //validate idphone 
            bool verifyPhone = await verifyAccount(idphone, token, new { });
            if (!verifyPhone)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Identificador de número de teléfono /Token inválido";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";

                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            //validate account 
            object data = new { };
            bool verify = await verifyAccount(idaccount, token, data);
            if (!verify)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Identificador de la cuenta de WhatsApp Business /Token inválido no es válido.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";

                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            list.Add(new WhatsappConfigDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoWhatsappConfiguracion = "0",
                IdentificadorApp = idapp,
                IdPhone = idphone,
                IdAccount = idaccount,
                Token = token,
                SDK = sdk,
                Estado = true,
                NumberPhone = numberphone,
                Operation = Operation.Create,
            }); ;
            ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
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

        //update 
        public async Task<ActionResult> updateConfig(string code, string idphone, string idaccount, string numberphone, string token, string sdk)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();
            bool validadorParametros = true;

            if (code == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio code.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (idphone == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio Identificador de número de teléfono.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (idaccount == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio Identificador de la cuenta de WhatsApp Business";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (sdk == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar Verison SDK.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (token == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un Token.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (numberphone == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un número telefonico.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }


            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            //validate idphone 
            bool verifyPhone = await verifyAccount(idphone, token, new { });
            if (!verifyPhone)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Identificador de número de teléfono";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";

                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            //validate account 
            object data = new { };
            bool verify = await verifyAccount(idaccount, token, data);
            if (!verify)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Identificador de la cuenta de WhatsApp Business no es válido.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";

                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            list.Add(new WhatsappConfigDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoWhatsappConfiguracion = code,
                IdPhone = idphone,
                IdAccount = idaccount,
                Token = token,
                SDK = sdk,
                Estado = true,
                NumberPhone = numberphone,
                Operation = Operation.Update,
            }); ;
            ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
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

        //list
        public ActionResult ConfigAll()
        {
            List<WhatsappConfigDTO> lista = new List<WhatsappConfigDTO>();

            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()

            {
                FilterCase = FilterCaseGlobal.List,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //search by code
        public ActionResult ConfigByCode(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (code == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoWhatsappConfiguracion = code;
            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()
            {
                FilterCase = FilterCaseGlobal.SearchByCode,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
            };
            RespItemWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                _objResponseModel.Date = oResp.Item;
                _objResponseModel.Status = 0;
            }


            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }

        //delete by code
        public ActionResult DestroyConfigByCode(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();

            bool validadorParametros = true;

            if (code == String.Empty)
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
            list.Add(new WhatsappConfigDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoWhatsappConfiguracion = code,

                Operation = Operation.Delete,
            }); ;
            ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
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

        //get templates
        public async Task<ActionResult> getTemplates(string code)
        {
            ResponseModel _responseModel = new ResponseModel();
            WhatsappRepository _repository = new WhatsappRepository();
            var account = _repository.getItemByCodeRepository(code);
            if (account.IdPhone == String.Empty)
            {

                _responseModel.Status = 1;
                _responseModel.Message1 = "No se encontro identificador";
                return Json(_responseModel, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var templates = await callExecuteGet(account.IdAccount, account.Token, "message_templates", new { });

                if (templates.Status == 0)
                {
                    _responseModel.Date = templates.Date;
                    _responseModel.Status = 0;

                }
                else
                {
                    _responseModel.Status = 1;
                    _responseModel.Message1 = "datos inválidos";
                }
            }

            return Json(_responseModel, JsonRequestBehavior.AllowGet);
        }

        //**************************** END CONFIG ************************************  



        //**************************** START TEMPLATES ************************************  

        public async Task<ActionResult> registerTemplate(string code, string name, string language, string category, Dictionary<string, string> header, string body, string footer, ButtonsOptional bottons)
        {
            ResponseModel _responseModel = new ResponseModel();
            bool valid = true;
            TemplateUtil _templateUtil = new TemplateUtil();
            WhatsappTemplateServices _whatsappServvices = new WhatsappTemplateServices();
            WhatsappRepository _whatsappRepository = new WhatsappRepository();

            if (code == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio codigo";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            if (string.IsNullOrEmpty(language))
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio lenguaje";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            if (name == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio nombre del template";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            else if (category == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio agregar categoria.";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            else if (body == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio agregar cuerpo del mensaje.";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            if (!valid)
            {
                return Json(_responseModel, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var account = _whatsappRepository.getItemByCodeRepository(code);
                if (account.Token == String.Empty && account.IdAccount == String.Empty)
                {
                    _responseModel.Message1 = "Cuenta inválida";
                    _responseModel.Status = 1;
                    valid = false;
                }

                if (!valid)
                {
                    return Json(_responseModel, JsonRequestBehavior.AllowGet);
                }

                TemplateModel _data = new TemplateModel();
                List<object> components = new List<object>();
                string nameSanite = Commun.removeCaracterSpecials(name);
                _data.name = nameSanite.ToLower();
                _data.category = category;
                _data.language = language;
                components.Add(_templateUtil.TemplateBody(body));

                if (footer != String.Empty)
                {
                    components.Add(_templateUtil.TemplateFooter(footer));
                }
                if (header["type"] == "0")
                {
                    //type header neither

                }
                else if (header["type"] == "1")
                {  //type header text
                    components.Add(_templateUtil.TemplateHeaderText(header["value"]));
                }
                else if (header["type"] == "2")
                {  //type header text
                    //get idupload
                    var generate = await generateIdUpload(account?.IdentificadorApp, account?.Token);

                    if (generate.Success == true)
                    {
                        if (header["medio"] == "image")
                        {
                            components.Add(_templateUtil.TemplateImage((string)generate?.Date));
                        }
                        else if (header["medio"] == "video")
                        {
                            components.Add(_templateUtil.TemplateVideo((string)generate?.Date));
                        }
                        else if (header["medio"] == "document")
                        {
                            components.Add(_templateUtil.TemplateDoc((string)generate?.Date));
                        }
                        else { }

                    }
                    else
                    {
                        _responseModel.Status = 1;
                        _responseModel.Message1 = "No se pudo generar identificador de subida de medios";
                        return Json(_responseModel, JsonRequestBehavior.AllowGet);
                    }

                }
                else { }

                if (bottons?.type == "1")
                {
                    components.Add(_templateUtil.TemplateAction(bottons));

                }
                else if (bottons?.type == "2")
                {
                    if (bottons?.valueQuick != null)
                    {
                        components.Add(_templateUtil.TemplateQuickReply(bottons));
                    }

                }
                else { }

                _data.components = components;
                _responseModel = await _whatsappServvices.RegisterTemplateServices(account?.IdAccount, account?.Token, "message_templates", _data);
            }
            catch (Exception ex)
            {
                _responseModel.Message1 = ex.Message;
                _responseModel.Status = 1;
            }
            return Json(_responseModel, JsonRequestBehavior.AllowGet);
        }

        //delete template
        public async Task<ActionResult> destroyTemplate(string code, string name)
        {
            ResponseModel _responseModel = new ResponseModel();
            bool valid = true;
            WhatsappTemplateServices _whatsappServvices = new WhatsappTemplateServices();
            WhatsappRepository _whatsappRepository = new WhatsappRepository();

            if (code == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio codigo";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            if (name == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio nombre del template";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }

            if (!valid)
            {
                return Json(_responseModel, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var account = _whatsappRepository.getItemByCodeRepository(code);
                if (account.Token == String.Empty && account.IdAccount == String.Empty)
                {
                    _responseModel.Message1 = "Cuenta inválida";
                    _responseModel.Status = 1;
                    valid = false;
                }

                if (!valid)
                {
                    return Json(_responseModel, JsonRequestBehavior.AllowGet);
                }
                object model = new { };
                string uri = $"message_templates?name={name}";
                _responseModel = await _whatsappServvices.DestroyTemplateServices(account.IdAccount, account.Token, uri, model);
            }
            catch (Exception ex)
            {
                _responseModel.Status = 1;
            }
            return Json(_responseModel, JsonRequestBehavior.AllowGet);
        }

        //generate H
        public async Task<ResponseModel> generateIdUpload(string idapp, string token)
        {

            WhatsappTemplateServices _services = new WhatsappTemplateServices();

            var image = Server.MapPath("~/Content/assets/images/pain.jpg");
            ResponseModel _response = new ResponseModel();

            try
            {
                var sessioapp = await _services.sessionUploadServices(idapp, token, "uploads");
                if (sessioapp.Success == true)
                {
                    var id = sessioapp.Date.ToString();
                    var h = _services.UploadServices(token, id, image);
                    if (h.Success == true)
                    {
                        _response.Date = h.Date;
                        _response.Success = true;

                    }
                    else { _response.Success = false; }

                }
                else { _response.Success = false; }
            }
            catch (Exception ex)
            {
                _response.Message1 = ex.Message;
                _response.Success = false;
            }
            return _response;
        }

        //get Item template
        public async Task<ActionResult> getItemTemplate(string code, string name)
        {
            ResponseModel _responseModel = new ResponseModel();
            bool valid = true;
            WhatsappTemplateServices _whatsappServvices = new WhatsappTemplateServices();
            WhatsappRepository _whatsappRepository = new WhatsappRepository();

            if (code == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio codigo";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }
            if (name == String.Empty)
            {
                _responseModel.Status = 2;
                _responseModel.Message1 = "Es obligatorio nombre del template";
                _responseModel.Message2 = "Vuelve a intentarlo más tarde.";
                valid = false;
            }

            if (!valid)
            {
                return Json(_responseModel, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var account = _whatsappRepository.getItemByCodeRepository(code);
                if (account.Token == String.Empty && account.IdAccount == String.Empty)
                {
                    _responseModel.Message1 = "Cuenta inválida";
                    _responseModel.Status = 1;
                    valid = false;
                }

                if (!valid)
                {
                    return Json(_responseModel, JsonRequestBehavior.AllowGet);
                }
                object model = new { };
                string uri = $"message_templates?name={name}";
                _responseModel = await _whatsappServvices.GetItemTemplateServices(account.IdAccount, account.Token, uri, model);
            }
            catch (Exception ex)
            {
                _responseModel.Status = 1;
            }
            return Json(_responseModel, JsonRequestBehavior.AllowGet);
        }

        //templates custom generate

        //**************************** END TEMPLATES ************************************  



        //**************************** START CAMPAÑAS ************************************  

        //register campaign
        public ActionResult CampaignsRegister(string codeconfig, int cola, int trespuesta, string template, string language, string campaign, string excel, Dictionary<string, string> header, string bparams = "", DateTime? date = null)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();
            bool validadorParametros = true;


            if (template == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio plantilla.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (cola == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio cola destino.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (trespuesta == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio tiempo respuesta.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (language == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio el campo idioma.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (codeconfig == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio codigo whatsapp configuracion.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (campaign == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio campaña";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            else if (excel == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un Token.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            WhatsappConfigDTO _model = new WhatsappConfigDTO();

            _model.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            _model.CodigoSede = Commun.CodigoSede;
            _model.UsuarioCreacion = Commun.Usuario;
            _model.CodigoWhatsappCampania = "0";
            _model.CodigoWhatsappConfiguracion = codeconfig;
            _model.NombreWhatsappCampania = campaign;
            _model.UrlDestinatarios = excel;
            _model.IdTemplate = "123456789";
            _model.NameTemplate = template;
            _model.Languaje = language;
            _model.ColaDestino= cola;
            _model.TiempoRespuesta = trespuesta;
            _model.TypeHeader = header["type"];
            if (header["type"] == "NINGUNA")
            {
                _model.ParametersHeader = "";
            }
            else if (header["type"] == "TEXT")
            {
                _model.ParametersHeader = header["text"];
            }
            else
            {
                _model.ParametersHeader = header["url"];
            }
            _model.ParametersBody = bparams;

            if (date == null)
            {
                _model.FechaHoraProgramado = null;
            }
            else
            {
                _model.FechaHoraProgramado = date;
            }


            _model.Operation = Operation.RegisterCamp;

            list.Add(_model);
            ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
                _objResponseModel.Message2 = oResp.MessageList[0].Code;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }
        //update campaign
        public ActionResult CampaignsUpdate(string code, string campaign, DateTime date, string excel, Dictionary<string, string> header, string bparams = "")
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();
            bool validadorParametros = true;

            if (code == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio codigo whatsapp configuracion.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (campaign == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio campaña";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (date == null)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio fecha";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (excel == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un Token.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            WhatsappConfigDTO _model = new WhatsappConfigDTO();

            _model.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            _model.CodigoSede = Commun.CodigoSede;
            _model.UsuarioCreacion = Commun.Usuario;
            _model.CodigoWhatsappCampania = code;
            _model.NombreWhatsappCampania = campaign;
            _model.UrlDestinatarios = excel;
            if (header["type"] == "NINGUNA")
            {
                _model.ParametersHeader = "";
            }
            else if (header["type"] == "TEXT")
            {
                _model.ParametersHeader = header["text"];
            }
            else
            {
                _model.ParametersHeader = header["url"];
            }
            _model.ParametersBody = bparams;
            _model.FechaHoraProgramado = date;
            _model.Operation = Operation.UpdateCamp;

            list.Add(_model);
            ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
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

        //list campaigs
        public ActionResult Campaigns(string codeconfig, DateTime? start = null, DateTime? end = null)
        {
            List<WhatsappConfigDTO> lista = new List<WhatsappConfigDTO>();

            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoWhatsappConfiguracion = codeconfig;
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;

            if (start != null && end != null)
            {
                oWhatsappConfigDTO.FechaInicio = (DateTime)start;
                oWhatsappConfigDTO.FechaFin = (DateTime)end;

            }
            else
            {

                DateTime now = DateTime.Now;
                DateTime firtsDay = new DateTime(now.Year, now.Month, 1);
                
                DateTime lastDay = firtsDay.AddMonths(1).AddDays(-1);
                oWhatsappConfigDTO.FechaInicio = firtsDay;
                oWhatsappConfigDTO.FechaFin = lastDay;

            }



            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()

            {
                FilterCase = FilterCaseGlobal.ListCamp,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
                
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //search by code
        public ActionResult CampaignsByCode(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (code == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoWhatsappCampania = code;
            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()
            {
                FilterCase = FilterCaseGlobal.SearchByCodeCamp,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
            };
            RespItemWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                _objResponseModel.Date = oResp.Item;
                _objResponseModel.Status = 0;
            }


            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }

        //delete by code
        public ActionResult DestroyCampaign(string code)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();

            bool validadorParametros = true;

            if (code == String.Empty)
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
            list.Add(new WhatsappConfigDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoWhatsappCampania = code,

                Operation = Operation.DestroyCamp,
            }); ;
            ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
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

        //send template
        public async Task<ActionResult> SendCampaign(string code)
        {
            ResponseModel _model = new ResponseModel();
            bool valid = true;

            WhatsappRepository _repository = new WhatsappRepository();

            //get item campaign
            var campaign = _repository.getItemCampaignRepository(code);
            if (campaign?.CodigoWhatsappCampania == String.Empty && campaign?.CodigoWhatsappConfiguracion == String.Empty)
            {
                _model.Message1 = "Cuenta inválida";
                _model.Status = 1;
                valid = false;
            }

            if (!valid)
            {
                return Json(_model, JsonRequestBehavior.AllowGet);
            }

            //save temp excel
            var client = new WebClient();
            String url = campaign?.UrlDestinatarios;
            var fullPath = Path.GetTempFileName();
            client.DownloadFile(url, fullPath);
            SLDocument sl = new SLDocument(fullPath);

            //clear tempFile
            var file = new FileInfo(fullPath);
            file.Delete();

            //data number
            List<string> numberPhone = new List<string>();

            ValuesData _valuesData = new ValuesData();
            List<List<string>> _values = new List<List<string>>();

            //reading
            int iRow = 2;
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                string codigocliente = sl.GetCellValueAsString(iRow, 1);
                string nombrecliente = sl.GetCellValueAsString(iRow, 2);
                string apellidoscliente = sl.GetCellValueAsString(iRow, 3);
                string celular = sl.GetCellValueAsString(iRow, 4);
                string correo = sl.GetCellValueAsString(iRow, 5);
                string doc = sl.GetCellValueAsString(iRow, 6);
                string membresia = sl.GetCellValueAsString(iRow, 7);
                string contrato = sl.GetCellValueAsString(iRow, 8);
                string dateStart = sl.GetCellValueAsString(iRow, 9);
                string dateEnd = sl.GetCellValueAsString(iRow, 10);

                List<string> dt = new List<string>() { codigocliente, nombrecliente, apellidoscliente, celular, correo, doc, membresia, contrato, dateStart, dateEnd };
                _values.Add(dt);
                numberPhone.Add(celular);
                iRow++;
            }

            _valuesData.values = _values;


            WhatsappTemplateServices _service = new WhatsappTemplateServices();

            //get account
            var account = _repository.getItemByCodeRepository(campaign?.CodigoWhatsappConfiguracion);
            if (account.Token == String.Empty && account.IdAccount == String.Empty)
            {
                _model.Message1 = "Cuenta inválida";
                _model.Status = 1;
                valid = false;
            }

            if (!valid)
            {
                return Json(_model, JsonRequestBehavior.AllowGet);
            }

            TemplateUtil _utils = new TemplateUtil();
            try
            {
                for (int n = 0; n < numberPhone.Count(); n++)
                {
                    List<string> values = new List<string>();
                    values = _valuesData.values[n];
                    //formate template validations
                    var _send = _utils.FormatSendTemplate(campaign, numberPhone[n], values);
                    //send message
                    bool send = await _service.SenMessageServices(account?.IdPhone, account?.Token, "messages", _send);
                    //register detail send
                    var registerDetail = _repository.registerCampaignDetailRepository(campaign?.CodigoWhatsappCampania, $"{values[1]} {values[2]}", numberPhone[n], send);
                    //update status send
                    var changeStatus = _repository.updateCampaignStateRepository(campaign?.CodigoWhatsappCampania, true);
                }
                _model.Success = true;
                _model.Message1 = "enviado correctamente";
            }
            catch (Exception ex)
            {

                _model.Success = false;
                _model.Message1 = ex.Message;
            }

            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        //list campaign detail

        public ActionResult CampaignsDetail(string code)
        {
            List<WhatsappConfigDTO> lista = new List<WhatsappConfigDTO>();

            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoWhatsappCampania = code;
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()

            {
                FilterCase = FilterCaseGlobal.ListCampDetail,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //**************************** END CAMPAÑAS ************************************  


        public async Task<bool> verifyAccount(string IdAccount, string token, object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string EndPoint = ConfigurationManager.AppSettings["HostFB"];
                string Uri = $"{EndPoint}/{IdAccount}";
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = await client.GetAsync(Uri);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<ResponseModel> callExecuteGet(string IdAccount, string token, string uriComplemente, object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            ResponseModel _response = new ResponseModel();

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
                    ResponseTemplate resp = JsonConvert.DeserializeObject<ResponseTemplate>(result);
                    _response.Date = resp;
                    _response.Status = 0;
                    return _response;
                }
                else
                {

                    _response.Status = 2;
                    return _response;
                }
            }
        }



        public ActionResult uploadFile(ClienteViewModel request)
        {
            ResponseModel _response = new ResponseModel();
            Random random = new Random();
            var file2 = request.ImageFile;
            string ruta = string.Empty;
            if (file2 != null)
            {
                var fileName = Path.GetFileName(file2.FileName);
                var extention = Path.GetExtension(file2.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });
                string name = random.Next() + "";
                ruta = UploadImgageAzure.UploadFilesAzure(obj, (Commun.SubDominio + "_" + name + extention), "galeriawhatsapp");

                _response.Message1 = ruta;
                _response.Status = 0;
            }
            else
            {
                _response.Status = 1;
            }
            return Json(_response, JsonRequestBehavior.AllowGet);
        }


    }

    public class xsdfsfd
    {
        public int codigocliente { get; set; }
        public string nombrecliente { get; set; }
    }

    public class ValuesData
    {
        public List<List<string>> values { get; set; }
    }


    public class Language
    {
        public string code { get; set; }
    }

    public class SendModelTemplate
    {
        public string messaging_product { get; set; }
        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public TemplateSend template { get; set; }
    }

    public class TemplateSend
    {
        public string name { get; set; }
        public Language language { get; set; }
        public List<object> components { get; set; }
    }






    //model individual

    public class Individual
    {
        public string messaging_product { get; set; }
        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public Text text { get; set; }
    }

    public class Text
    {
        public bool preview_url { get; set; }
        public string body { get; set; }
    }




    //model template
    public class Languagex
    {
        public string code { get; set; }
    }

    public class TemplateWhatsapp
    {
        public string messaging_product { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public Template template { get; set; }
    }

    public class Template
    {
        public string name { get; set; }
        public Languagex language { get; set; }
    }


    public class TemplateModel
    {
        public string name { get; set; }
        public string language { get; set; }
        public string category { get; set; }
        public List<object> components { get; set; }
    }


    public class ResponseHttp
    {

        public string Message { get; set; }
        public bool Status { get; set; }
        public Object Data { get; set; }

    }



    public class Data
    {
        public string phone_text { get; set; }
        public string phonevalue { get; set; }
        public string url_text { get; set; }
        public string url_link { get; set; }
    }

    public class ButtonsOptional
    {
        public string type { get; set; }
        public Data data { get; set; }
        public List<string> valueQuick { get; set; }
    }




}