using BotComers.Controllers;
using BotComers.Helpers;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Gimnasio;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using BotComers.Models;
using MailKit.Net.Smtp;
using BotComers.ViewModels;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using System.IO;


namespace BotComers.Repository
{
    public class HomeRepository
    {

        public ResponseModel SendEmailMassiveRepo(List<FilePeople> person, string subject, string content, List<FileSendModel> attachments, string path)
        {
            ResponseModel resp = new ResponseModel();

            try
            {                
                var config = getConfiguracionRepo();
                if (!String.IsNullOrEmpty(config.EmailHost) && !String.IsNullOrEmpty(config.EmailPort) && !String.IsNullOrEmpty(config.EmailUser) && !String.IsNullOrEmpty(config.EmailKey))
                {
                    string servidor = config.EmailHost;
                    int puerto = Int32.Parse(config.EmailPort);
                    string GmailUser = config.EmailUser;
                    string GmailPass = config.EmailKey;
                    MimeMessage mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress(config.NombreComercial ?? "Appsfit", GmailUser));

                    InternetAddressList list = new InternetAddressList();

                    foreach (FilePeople c in person)
                    {
                        bool valid = Commun.IsValidEmail(c.Email);
                        if (valid)
                        {
                            list.Add(new MailboxAddress(c.Name, c.Email));
                        }
                    }

                    // mensaje.To.Add(new MailboxAddress("dev", GmailUser));
                    mensaje.Bcc.AddRange(list);
                    // mensaje.Bcc.AddRange(list);

                  
                    mensaje.Subject = subject;

                    BodyBuilder bodyContent = new BodyBuilder();
                    
                    foreach (FileSendModel a in attachments)
                    {
                        bodyContent.Attachments.Add(path + a.Name);
                    }

                    bodyContent.HtmlBody = content;

                    mensaje.Body = bodyContent.ToMessageBody();
                    SmtpClient ClienteSmtp = new SmtpClient();
                    ClienteSmtp.CheckCertificateRevocation = false;
                    ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                    ClienteSmtp.Authenticate(GmailUser, GmailPass);
                    ClienteSmtp.Send(mensaje);
                    ClienteSmtp.Disconnect(true);

                    resp.Success= true;
                }
                else
                {
                    resp.Success = false;
                    resp.Message1 ="No tiene cuenta de correo";
                }

            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message1 = ex.Message;

            }
            return resp;
        }


        //upload azure
        public ResponseModel uploadFileRepo(ClienteViewModel request, string azureFolder)
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
                ruta = UploadImgageAzure.UploadFilesAzure(obj, (Commun.SubDominio + "_" + name + extention), azureFolder);
                _response.Message1 = ruta;
                _response.Success= true;
            }
            else
            {
                _response.Success = false;
            }
            return _response;
        }

        //get configurations
        public ConfiguracionDTO getConfiguracionRepo()
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Codigo = Commun.CodigoSede;
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = Commun.Usuario,
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.BuscarPorCodigo
            };
            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }
            return oConfiguracionDTO;
        }

    }
}