using System;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

using AppsfitWebApi.ViewModels;

using E_BusinessLayer.Corporativo;
using E_BusinessLayer.Gimnasio;
using E_BusinessLayer.Configuracion;
using E_DataModel.Configuracion;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

using E_DataModel.Corporativo;

using AppsfitWebApi.Models;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;

using AppsfitWebApi.Helpers;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/access")]
    public class AccessController : ApiController
    {

        private ResponseModel EnviarCorreoConfirmacion(string correoDestino, string nombreDestino, string idUser)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            try
            {
                String servidor = "smtp.gmail.com";
                int puerto = 587;

                String GmailUser = "softwareparagimnasios@gmail.com";
                String GmailPass = "lpdyobnigzksdhvl";

                MimeMessage mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Appsfit", GmailUser));
                mensaje.To.Add(new MailboxAddress(nombreDestino, correoDestino)); //barberosnet@gmail.com
                mensaje.Subject = "Appsfit, confirmación de correo.";

                BodyBuilder CuerpoMensaje = new BodyBuilder();
                //CuerpoMensaje.TextBody = "Hola";
                CuerpoMensaje.HtmlBody = "<table border='0' cellpadding='1' cellspacing='1' style='width:100%'>" +
                        "<thead>" +
                            "<tr>" +
                                "<th scope='col' style='background-color:#606975'><img alt='' src='https://ckeditor.com/apps/ckfinder/userfiles/files/Appsfit_Logo_Blanco-sin-fondo.png' style='height:280px; width:380px' /></th>" +
                            "</tr>" +
                        "</thead>" +
                        "<tbody>" +
                        "    <tr>" +
                        "        <td>&nbsp;</td>" +
                        "    </tr>" +
                        "    <tr>" +
                        "        <td><span style='font-size:20px'>Hola " + nombreDestino + ", bienvenido a Appsfit</span></td>" +
                        "    </tr>" +
                        "    <tr>" +
                        "        <td><span style='font-size:20px'>Para culminar tu registro, valida tu correo electr&oacute;nico.</span></td>" +
                        "    </tr>" +
                        "    <tr>" +
                        "        <td>" +
                        "            <a target='_blank' href='https://webapiappsfit-cliente.azurewebsites.net/api/access/confirmarcorreopersonafit?Id=" + idUser + "'>" +
                        "                <button type='button' style='width:350px;padding: 10px 15px;background:#E0342B;text-decoration: none;color: #fff;'>" +
                        "                    VALIDAR CORREO" +
                        "                </button>" +
                        "            </a>          " +
                        "        </td>" +
                        "    </tr>" +
                        "</tbody>" +
                        "</table>";


                mensaje.Body = CuerpoMensaje.ToMessageBody();

                SmtpClient ClienteSmtp = new SmtpClient();
                ClienteSmtp.CheckCertificateRevocation = false;
                ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                ClienteSmtp.Authenticate(GmailUser, GmailPass);
                ClienteSmtp.Send(mensaje);
                ClienteSmtp.Disconnect(true);

                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "correo enviado correctamente.";
                _objResponseModel.Message2 = "Falta un paso más, ingresa a tu correo y confirma que eres tú por seguridad.";//solo tienes que ingresar tu nro de indentificación y clave.";

            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";//solo tienes que ingresar tu nro de indentificación y clave.";

            }

            return _objResponseModel;

        }

        /// <summary>
        /// REGISTRA UNA NUEVA PERSONA FITNESS EN LA APP
        /// </summary>
        /// <param name="AspNetUsersModel"></param>
        /// <returns>
        /// {
        ///     Status
        ///     Message1
        ///     Message2
        ///     Date
        /// }
        /// </returns>
        [HttpPost]
        [Route("registrarpersonafit")]
        public HttpResponseMessage ecommerce_uspRegistrar_AspNetUsers(AspNetUsersModel oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oitem.FullName == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro FullName.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.UserName == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro UserName.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.PasswordHash == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro PasswordHash.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.Email == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Email.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.PhoneNumber == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro PhoneNumber.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();

            list.Add(new AspNetUsersDTO()
            {
                CodigoUnidadNegocio = 0,
                CodigoSede = 0,
                UserType = 1,
                FullName = oitem.FullName,
                Photo = string.Empty,//oitem.Photo == null ? string.Empty : oitem.Photo,
                UserName = oitem.UserName,
                PasswordHash = oitem.PasswordHash,
                Identificacion = oitem.UserName,
                DefaultKey = string.Empty,
                Email = oitem.Email,
                EmailConfirmed = false,
                PhoneNumber = oitem.PhoneNumber,
                PhoneNumberConfirmed = false,
                SecurityStamp = string.Empty,
                Estate = 1,
                CodigoCargo = 0,
                UsuarioCreacion = "appsfit",
                Operation = Operation.ecommerce_uspRegistrar_AspNetUsers_AppFitness,
                TokenDevice = oitem.TokenDevice

            });

            ReqAspNetUsersDTO oReq = new ReqAspNetUsersDTO()
            {
                List = list,
                User = "admin"
            };
            RespAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {

                if (oResp.MessageList[0].Detalle == "1")
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "Aviso: El nro de documento y correo que estas intentando registrar ya esta en uso por otro usuario!";
                    _objResponseModel.Message2 = "Te recomendamos comunicarte con el administrador.";
                }
                else
                {

                    _objResponseModel = EnviarCorreoConfirmacion(oitem.Email, oitem.FullName, oResp.MessageList[0].Detalle);

                    if (_objResponseModel.Status == 2)
                    {
                        _objResponseModel.Status = 2;
                        _objResponseModel.Message1 = "Cuenta creada, felicitaciones, " + _objResponseModel.Message1;
                        _objResponseModel.Message2 = "Falta un paso más, ingresa a tu correo y confirma que eres tú por seguridad.";

                    }

                    _objResponseModel.Status = 2;
                }

            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        /// <summary>
        /// BUSCAR USUARIO
        /// </summary>
        /// <param name="AspNetUsersModel"></param>
        /// <returns>
        /// {
        ///     Status
        ///     Message1
        ///     Message2
        ///     Date
        /// }
        /// </returns>
        [HttpPost]
        [Route("reenviarcorreopersonafit")]
        public HttpResponseMessage ecommerce_AspNetUsers_Buscar(AspNetUsersModel oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oitem.Email == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Email.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            AspNetUsersDTO oAspNetUsersDTO = new AspNetUsersDTO();
            oAspNetUsersDTO.Id = oitem.Id;
            oAspNetUsersDTO.Email = oitem.Email;

            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                FilterCase = filterCaseAspNetUsers.ecommerce_AspNetUsers_Buscar,
                Item = oAspNetUsersDTO,
                User = "appsfit"
            };
            RespItemAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.AspNetUsersGetItem(oReq);
            }

            if (oResp.Success)
            {

                if (oResp.Item != null)
                {

                    _objResponseModel = EnviarCorreoConfirmacion(oAspNetUsersDTO.Email, oResp.Item.FullName, oResp.Item.Id);
                    _objResponseModel.Status = 2;

                    //_objResponseModel.Status = 0;
                    //_objResponseModel.Message1 = "Correo reenviado correctamente.";//¡Este usuario aún no se ha registrado!
                    //_objResponseModel.Message2 = string.Empty;

                }
                else
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "No encontramos ningún usuario.";
                    _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                }

            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }


        /// <summary>
        /// CONFIRMAR CORREO DEL USUARIO REGISTRADO
        /// </summary>
        /// <param name="AspNetUsersModel"></param>
        /// <returns>
        /// {
        ///     Status
        ///     Message1
        ///     Message2
        ///     Date
        /// }
        /// </returns>
        [HttpGet]
        [Route("confirmarcorreopersonafit")]
        public HttpResponseMessage ecommerce_uspValidarCorreo_AspNetUsers_AppFitness(string Id)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();

            list.Add(new AspNetUsersDTO()
            {
                //FullName = oitem.FullName,
                Id = Id,//oitem.Id,
                Operation = Operation.ecommerce_uspValidarCorreo_AspNetUsers_AppFitness,

            });

            ReqAspNetUsersDTO oReq = new ReqAspNetUsersDTO()
            {
                List = list,
                User = "admin"
            };
            RespAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Felicitaciones, correo confirmado.";
                _objResponseModel.Message2 = "";
            }
            else
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }


        /// <summary>
        /// VERIFICA Y VALIDA SI EL USUARIO ES CORRECTO
        /// </summary>
        /// <param name="AspNetUsersModel"></param>
        /// <returns>
        /// {
        ///     Status
        ///     Message1
        ///     Message2
        ///     Date
        /// }
        /// </returns>
        [HttpPost]
        [Route("loginpersonafit")]
        public HttpResponseMessage ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness(AspNetUsersModel oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oitem.UserName == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro UserName.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.PasswordHash == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro PasswordHash.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.Email == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Email.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            AspNetUsersDTO oAspNetUsersDTO = new AspNetUsersDTO();
            oAspNetUsersDTO.Email = oitem.Email;
            oAspNetUsersDTO.UserName = oitem.UserName;
            oAspNetUsersDTO.PasswordHash = oitem.PasswordHash;
            oAspNetUsersDTO.TokenDevice = oitem.TokenDevice;


            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                FilterCase = filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness,
                Item = oAspNetUsersDTO,
                User = "appsfit"
            };
            RespItemAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.AspNetUsersGetItem(oReq);
            }

            if (oResp.Success)
            {

                if (oResp.Item != null)
                {
                    if (oResp.Item.IdValidation == 0)
                    {
                        oAspNetUsersDTO.Id = oResp.Item.Id;
                        oAspNetUsersDTO.EmailConfirmed = oResp.Item.EmailConfirmed;
                        oAspNetUsersDTO.FullName = oResp.Item.FullName;
                        oAspNetUsersDTO.PasswordHash = string.Empty;
                        oAspNetUsersDTO.DefaultKey = Guid.NewGuid().ToString();

                        int validadorGuardarToken = 0;
                        using (Repository.AspNetUsersRepository repository = new Repository.AspNetUsersRepository())
                        {
                            validadorGuardarToken = repository.ecommerce_uspRegistrar_AspNetUsersToken_AppFitness(oAspNetUsersDTO);
                        }

                        if (validadorGuardarToken == 0)
                        {
                            _objResponseModel.Status = 0;
                            _objResponseModel.Message1 = oResp.Item.MensajeValidation; //¡El usuario y contraseña son correctos!
                            _objResponseModel.Message2 = string.Empty;
                            _objResponseModel.Date = oAspNetUsersDTO;

                            //EnviarCorreo("barberosnet@gmail.com", "CARLOS", "E38FD06E-E514-4DD1-B215-DDD5C4FA5A2D");
                        }
                        else if (validadorGuardarToken == 1)
                        {
                            _objResponseModel.Status = 3;
                            _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                            _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                        }


                    } else if (oResp.Item.IdValidation == 1)
                    {
                        _objResponseModel.Status = 1;
                        _objResponseModel.Message1 = oResp.Item.MensajeValidation;//Datos incorrectos
                        _objResponseModel.Message2 = string.Empty;

                    }
                    else if (oResp.Item.IdValidation == 2)
                    {
                        _objResponseModel.Status = 2;
                        _objResponseModel.Message1 = oResp.Item.MensajeValidation;//¡Este usuario aún no se ha registrado!
                        _objResponseModel.Message2 = string.Empty;

                    }

                }
                else
                {
                    _objResponseModel.Status = 3;
                    _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                    _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                }

            }
            else
            {
                _objResponseModel.Status = 3;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }




        //update password
        [HttpPost]
        [Route("updatepass")]
        public HttpResponseMessage ecommerce_AspNetUsers_updatePassword(AspNetUsersDTO oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            
            bool validadorParametros = true;
            if (oitem.DefaultKey == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DefaultKey.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.UserName == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro UserName.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.PasswordHash == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro PasswordHash.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.PasswordHashNueva == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro PasswordHash.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }


            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();

            list.Add(new AspNetUsersDTO()
            {
                DefaultKey = oitem.DefaultKey,
                UserName = oitem.UserName,
                PasswordHash = oitem.PasswordHash,
                PasswordHashNueva = oitem.PasswordHashNueva,
                Operation = Operation.UpdatePass,

            });

            ReqAspNetUsersDTO oReq = new ReqAspNetUsersDTO()
            {
                List = list,
                User = "admin"
            };
            RespAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                if(oResp.MessageList.Count > 0)
                {
                    if (oResp.MessageList[0].Codigo == 0)
                    {
                        _objResponseModel.Status = 0;
                        _objResponseModel.Message1 = "CLAVE CAMBIADA CORRECTAMENTE";
                    }
                    else if (oResp.MessageList[0].Codigo == 1)
                    {
                        _objResponseModel.Status = 2;
                        _objResponseModel.Message1 = "KEY INVALIDO O VENCIDO";
                    }
                    else if (oResp.MessageList[0].Codigo == 2)
                    {
                        _objResponseModel.Status = 2;
                        _objResponseModel.Message1 = "USUARIO O CLAVE INCORRECTO";
                    }
                }
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }



            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        //update password
        [HttpPost]
        [Route("resetpass")]
        public HttpResponseMessage ecommerce_AspNetUsers_resetPassword(AspNetUsersDTO user)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (user.UserName == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro UserName.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (user.Email == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Email.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
           
            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }


            AspNetUsersDTO oAspNetUsersDTO = new AspNetUsersDTO();
            
            oAspNetUsersDTO.Email = user.Email;

            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                FilterCase = filterCaseAspNetUsers.ecommerce_uspRecuperarClave_AspNetUsers_AppFitness,
                Item = oAspNetUsersDTO,
                User = "appsfit"
            };
            RespItemAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.AspNetUsersGetItem(oReq);
            }
            if(oResp.Success == true)
            {
                if(!String.IsNullOrEmpty(oResp.Item.PasswordHash))
                {


                    
                    List<AspNetUsersModel> list = new List<AspNetUsersModel>();
                    AspNetUsersModel d = new AspNetUsersModel();
                    d.Email = user.Email;
                    d.Nombres = oResp.Item.FullName;


                    list.Add(d);
                    var content =  @"

<body style=""height: 100%;
width: 100%;
margin: 0;
padding: 0;
font-family: 'Open Sans', sans-serif;
font-size: 16px;
background-color: #F6F9FC;"">
    <table align=""center"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse;
    width: 100%;
    max-width: 600px;
    font-size: inherit;
    margin-top: 40px;"">
        <thead>
            <tr>
                <th class=""brand__name"" style=""text-align: left; height: 60px;
                padding: 10px 20px;"">Appsfit</th>
            </tr>
        </thead>
        <tbody style=""border-top: 3px solid #E0342B;
        background-color: #FFFFFF;"">
            <tr>
                <td class=""center"" style="" font-size: 14px;
                line-height: 1.8;
                padding: 10px 20px; text-align: center;"">
                    <h2>Restablecer la contraseña</h2>
                </td>
            </tr>
            <tr>
                <td style="" font-size: 14px;
                line-height: 1.8;
                padding: 10px 20px;"">HOLA, "+oResp.Item.FullName+@",</td>
            </tr>
            <tr>
                <td class=""center"" style="" font-size: 14px;
                line-height: 1.8;
                padding: 10px 20px; text-align: center;"">Se activó un restablecimiento de contraseña en su cuenta.</td>
            </tr>
            <tr>
                <td class=""center space-y"" style="" font-size: 14px;
                line-height: 1.8;
                padding: 10px 20px; text-align: center;padding-top: 40px;
        padding-bottom: 40px;"">
                    <div class=""button--reset"" style=""
            padding: 12px 20px;
            border-radius: 5px;
            background-color: #E0342B;
            color: white;
            font-size: 14px;
            letter-spacing: 0.25px;
            text-decoration: none;
            text-transform: uppercase;
            border: 1px solid rgb(95, 95, 95);
          "">
                        Usuario: " + oResp.Item.UserName+ @" <br />
                        Contraseña: "+oResp.Item.PasswordHash+@"
                    </div>
                </td>
            </tr>

        </tbody>

    </table>
</body>


";

                    using (AspNetHelper helpers = new AspNetHelper())
                    {
                        var send = helpers.SendEmailMassive(list, "recuperar contraseña", content);
                        if(send.Status == 2)
                        {
                            _objResponseModel.Status = 0;
                            _objResponseModel.Message1 = "le enviamos sus credenciales a su correo";

                        }
                        else
                        {
                            _objResponseModel.Status = 1;
                            _objResponseModel.Message2 = send.Message1;
                            _objResponseModel.Message3 = oResp.Item.PasswordHash;
                        }
                        
                    }                
                }
                else
                {
                    _objResponseModel.Status = 1;
                    _objResponseModel.Message1 = "" + oResp.Item.MensajeValidation;
                   ;
                }
                
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "" + oResp.MessageList[0].Detalle;
            }
            

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }










    }









    public class ResponseModel
    {
        public string Message1 { set; get; }
        public string Message2 { set; get; }
        public string Message3 { set; get; }
        public int Status { set; get; }
        public object Date { set; get; }
    }

    //status 0:success:status 1:fail:status 2:error

}