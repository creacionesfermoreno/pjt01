using System;
using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace BotComers.Controllers
{
    public class softwareparagimnasiosController : Controller
    {
        // GET: softwareparagimnasios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult leadsappsfitmuchasgracias()
        {
            return View();
        }

        public ActionResult EnviarCorreoLeadsappsfit(contactoLead request)
        {
            string validador = String.Empty;
            
            string correo = request.correo;
            string nombreempresa = request.nombreempresa;
            string situacionempresa = request.situacionempresa;
            string tipoempresa = request.tipoempresa;
            string nroubicaciones = request.nroubicaciones;
            string nombregerente = request.nombregerente; 
            string celular = request.celular;
            string asunto = request.asunto;

            //string mensajeWhatsapp = "";

            //mensajeWhatsapp = mensajeWhatsapp + "Mi%20empresa%20se%20llama:%20" + nombreempresa + "%0A";
            //mensajeWhatsapp = mensajeWhatsapp + "Mi%20correo%20es:%20" + correo + "%0A";
            //mensajeWhatsapp = mensajeWhatsapp + "Tengo:%20" + nroubicaciones + "%0A";
            //mensajeWhatsapp = mensajeWhatsapp + "La%20situacion%20de%20mi%20empresa%20es:%20" + situacionempresa + "%0A";
            //mensajeWhatsapp = mensajeWhatsapp + "Mi%20tipo%20de%20empresa%20es:%20" + tipoempresa + "%0A";
            //mensajeWhatsapp = mensajeWhatsapp + "Asunto:%20" + asunto;

            try
            {
                if (correo == String.Empty)
                {
                    validador = "Falta ingresar su correo.";
                }else if (nombreempresa == String.Empty)
                {
                    validador = "Falta ingresar el nombre de su empresa.";
                }
                else if (situacionempresa == String.Empty)
                {
                    validador = "Falta ingresar la situación de su empresa.";
                }
                else if (tipoempresa == String.Empty)
                {
                    validador = "Falta ingresar el tipo de su empresa.";
                }
                else if (nombregerente == String.Empty)
                {
                    validador = "Falta ingresar su nombre de gerente.";
                }
                else if (celular == String.Empty)
                {
                    validador = "Falta ingresar su nro de celular.";
                }else if (asunto == String.Empty)
                {
                    validador = "Falta ingresar el asunto, ¿en que le podemos ayudar?.";
                }

                if (validador == String.Empty)
                {
                    String servidor = "smtp.gmail.com";
                    int puerto = 587;

                    String GmailUser = "softwareparagimnasios@gmail.com";
                    String GmailPass = "lpdyobnigzksdhvl";

                    MimeMessage mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress("Appsfit Leads", "softwareparagimnasios@gmail.com"));
                    mensaje.To.Add(new MailboxAddress("Appsfit Leads", "softwareparagimnasios@gmail.com")); //barberosnet@gmail.com
                    mensaje.Subject = "✅ " + nombreempresa + ", nuevo leads appsfit";

                    BodyBuilder CuerpoMensaje = new BodyBuilder();
                    //CuerpoMensaje.TextBody = "Hola";

                    CuerpoMensaje.HtmlBody = "<table border='0' cellpadding='1' cellspacing='1' style='width:100%'>" +
                    "<thead>" +
                        "<tr>" +
                            "<th scope='col' style='background-color:#E0342B;color:#fff;'>Hola Appsfit, haz recibido un nuevo Leads</th>" +
                        "</tr>" +
                    "</thead>" +
                    "<tbody>" +
                    "    <tr>" +
                    "        <td>&nbsp;</td>" +
                    "    </tr>" +
                    "    <tr>" +
                    "        <td>Correo: <span style='font-size:20px'>" + correo + "</td>" +
                    "    </tr>" +
                    "    <tr>" +
                    "        <td>Empresa: <span style='font-size:20px'>" + nombreempresa + "</td>" +
                    "    </tr>" +
                        "    <tr>" +
                    "        <td>Situación: <span style='font-size:20px'>" + situacionempresa + "</td>" +
                    "    </tr>" +
                    "    <tr>" +
                    "        <td>Tipo: <span style='font-size:20px'>" + tipoempresa + "</td>" +
                    "    </tr>" +
                    "    <tr>" +
                    "        <td>Nro sedes: <span style='font-size:20px'>" + nroubicaciones + "</td>" +
                    "    </tr>" +
                    "    <tr>" +
                    "        <td>Nombre gerente: <span style='font-size:20px'>" + nombregerente + "</td>" +
                    "    </tr>" +
                    "    <tr>" +
                    "        <td>asunto: <span style='font-size:20px'>" + asunto + "</td>" +
                    "    </tr>" +
                    "   <tr>" +
                    "        <td><a href='https://api.whatsapp.com/send?phone=" + celular + "' target='_blank'><button type='button' style='--tw - gradient - from: #128c7e; font-size: 28px; position: relative; color: #fff; --tw-gradient-from: #128c7e; --tw-gradient-stops: #25d366, #128c7e; background-image: linear-gradient(to bottom right,var(--tw-gradient-stops)); display: inline-block; width: 80%; height: 70px; line-height: 50px; font-weight: 500; transition: all .5s ease; text-align: center; border-radius: 50px; background-size: 300% 100%; box-shadow: 0px; border: 0px !important; '>Conversemos ahora <span style='font-size: 35px;' class='fa fa-whatsapp'></span></button></a></td>" +
                    "   </tr>" +
                    "</tbody>" +
                    "</table>";

                    mensaje.Body = CuerpoMensaje.ToMessageBody();

                    SmtpClient ClienteSmtp = new SmtpClient();
                    ClienteSmtp.CheckCertificateRevocation = false;
                    ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                    ClienteSmtp.Authenticate(GmailUser, GmailPass);
                    ClienteSmtp.Send(mensaje);
                    ClienteSmtp.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                validador = ex.Message;
            }

            return Json(validador, JsonRequestBehavior.AllowGet);
        }

        public ActionResult politicadeprivacidad()
        {
            return View();
        }

        public ActionResult comofunciona()
        {
            return View();
        }

        public ActionResult blog()
        {
            return View();
        }

        public ActionResult peru()
        {
            return View();
        }

        public ActionResult ecuador()
        {
            return View();
        }

        public ActionResult ebook_150421_planificacionfinacieraparagimnasios()
        {
            return View();
        }

    }


    public class contactoLead {
        public string correo { set; get; }
        public string nombreempresa { set; get; }
        public string situacionempresa { set; get; }
        public string tipoempresa { set; get; }
        public string nroubicaciones { set; get; }
        public string nombregerente { set; get; }
        public string celular { set; get; }
        public string asunto { set; get; }
    }

}