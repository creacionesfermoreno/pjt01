using AppsfitWebApi.Models;
using E_DataModel.Gimnasio;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace AppsfitWebApi.Helpers
{
    public class AspNetHelper : IDisposable
    {

        public static string HTMLToText(string HTMLCode)
        {
            // Remove new lines since they are not visible in HTML
            HTMLCode = HTMLCode.Replace("\n", " ");

            // Remove tab spaces
            HTMLCode = HTMLCode.Replace("\t", " ");

            // Remove multiple white spaces from HTML
            HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

            // Remove HEAD tag
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Replace special characters like &, <, >, " etc.
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
            // Note: There are many more special characters, these are just
            // most common. You can add new characters in this arrays if needed
            string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
   "&gt;", "&reg;", "&copy;", "&bull;", "&trade;","&#39;"};
            string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                sbHTML.Replace(OldWords[i], NewWords[i]);
            }

            // Check if there are line breaks (<br>) or paragraph (<p>)
            sbHTML.Replace("<br>", "\n<br>");
            sbHTML.Replace("<br ", "\n<br ");
            sbHTML.Replace("<p ", "\n<p ");

            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");
        }


        private bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }


        public ResponseModel SendEmailMassive(List<AspNetUsersModel> person, string email_asunto, string email_mensajecorreo)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            // ConfigurationManager.AppSettings["RecordNumForPage_ListarConfiguracion_apfitness_NumeroRegistros"]
            try
            {
                String servidor = "mail.saavedraromoabogados.pe";
                int puerto = 587;

                String GmailUser = "noreply@saavedraromoabogados.pe";
                String GmailPass = "GmoV&yOTI38";

                MimeMessage mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Appsfit", GmailUser));
                //mensaje.To.Add(new MailboxAddress(person.Nombre, person.Correo));

                InternetAddressList list = new InternetAddressList();

                foreach (AspNetUsersModel c in person)
                {
                    bool valid = IsValidEmail(c.Email);
                    if (valid)
                    {
                        list.Add(new MailboxAddress(c.Nombres, c.Email));
                    }
                }
                mensaje.To.AddRange(list);

                mensaje.Subject = email_asunto;

                BodyBuilder CuerpoMensaje = new BodyBuilder();
                //string path = Server.MapPath("~/Content/assets/images/");
                //foreach (ImageSend a in attachments)
                //{
                //    CuerpoMensaje.Attachments.Add(path + a.Name);
                //}

                CuerpoMensaje.HtmlBody = email_mensajecorreo;

                mensaje.Body = CuerpoMensaje.ToMessageBody();
                SmtpClient ClienteSmtp = new SmtpClient();
                ClienteSmtp.CheckCertificateRevocation = false;
                ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                ClienteSmtp.Authenticate(GmailUser, GmailPass);
                ClienteSmtp.Send(mensaje);
                ClienteSmtp.Disconnect(true);

                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "correo enviado correctamente.";
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";
            }
            return _objResponseModel;
        }


        public static ResponseApi SendEmailOne(ConfiguracionDTO config, string to,string subject, string HtmlBody, string fileName)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            try
            {
                string servidor = config.EmailHost;
                int puerto = int.Parse(config.EmailPort);
                string GmailUser = config.EmailUser;
                string GmailPass = config.EmailKey;
                

                MimeMessage mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress(config.NombreComercial ?? "Appsfit", GmailUser));
                mensaje.To.Add(new MailboxAddress(to, to)); 


                mensaje.Bcc.Add(new MailboxAddress(GmailUser, GmailUser));


                mensaje.Subject = subject;

                BodyBuilder CuerpoMensaje = new BodyBuilder();
                var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/assets/pdf/");
             
                CuerpoMensaje.Attachments.Add(path + fileName);
                
                CuerpoMensaje.HtmlBody = HtmlBody;

                mensaje.Body = CuerpoMensaje.ToMessageBody();
                SmtpClient ClienteSmtp = new SmtpClient();
                ClienteSmtp.CheckCertificateRevocation = false;
                ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                ClienteSmtp.Authenticate(GmailUser, GmailPass);
                ClienteSmtp.Send(mensaje);
                ClienteSmtp.Disconnect(true);
                _objResponseModel.Success = true;
                _objResponseModel.Message1 = "correo enviado correctamente.";
            }
            catch (Exception ex)
            {
                _objResponseModel.Success = false;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return _objResponseModel;
        }



        public static bool removeFile(string path)
        {

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int ValidateInputMembresia (DateTime date) {
            DateTime now = DateTime.Now;
            DateTime firtsDay = new DateTime(now.Year, now.Month, 1);
            int Value = 3;
            if (date >= firtsDay)
            {
                Value = 2;
            }
            return Value;
        }
    }




    public class ImageSend
    {
        public string Origin { get; set; }
        public string Name { get; set; }

        public ImageSend(string name, string origin)
        {
            Name = name;
            Origin = origin;
        }
    }

    public class ResponseModel
    {
        public string Message1 { set; get; }
        public string Message2 { set; get; }
        public string Message3 { set; get; }
        public int Status { set; get; }
        public bool Success { set; get; }
        public object Date { set; get; }
    }
}