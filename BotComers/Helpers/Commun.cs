//using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static BotComers.Controllers.operacionesfitController;

namespace BotComers.Helpers
{
    public class Commun
    {

        public static int CodigoUnidadNegocio
        {
            get
            {
                int codigo = 0;
                try
                {
                    codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_Business"].Value);
                    //var _UnidadNegocio = System.Web.HttpContext.Current.Session["_CodigoUnidadNegocio"] //((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoUnidadNegocio");
                    //if (_UnidadNegocio != null)
                    //    Int32.TryParse(_UnidadNegocio.Value.ToString(), out codigo);
                }
                catch (Exception ex)
                {
                }
                return codigo;
            }
        }

        public static int CodigoSede
        {

            get
            {
                int codigo = 0;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoSede_Business"].Value);
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static int CodigoPerfil
        {

            get
            {
                int codigo = 0;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoPerfil_Business"].Value);
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static int CodigoUnidadNegocio_PersonaTiendaVirtual
        {
            get
            {
                int codigo = 0;
                try
                {
                    codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaTiendaVirtual"].Value);
                }
                catch (Exception ex)
                {
                }
                return codigo;
            }
        }

        public static int CodigoSede_PersonaTiendaVirtual
        {

            get
            {
                int codigo = 0;
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoSede_PersonaTiendaVirtual"].Value);
                return codigo;
            }
        }

        public static int CodigoUnidadNegocio_PersonaFit
        {
            get
            {
                int codigo = 0;
                try
                {
                    codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaFit"].Value);
                    //var _UnidadNegocio = System.Web.HttpContext.Current.Session["_CodigoUnidadNegocio"] //((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoUnidadNegocio");
                    //if (_UnidadNegocio != null)
                    //    Int32.TryParse(_UnidadNegocio.Value.ToString(), out codigo);
                }
                catch (Exception ex)
                {
                }
                return codigo;
            }
        }

        public static int CodigoSede_PersonaFit
        {

            get
            {
                int codigo = 0;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoSede_PersonaFit"].Value);
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static int CodigoMembresia_PersonaFit
        {

            get
            {
                int codigo = 0;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoMembresia_PersonaFit"].Value);
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static int CodigoPaquete_PersonaFit
        {
            get
            {
                int codigo = 0;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoPaquete_PersonaFit"].Value);
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static int CodigoSocio_PersonaFit
        {
            get
            {
                int codigo = 0;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = Convert.ToInt32(HttpContext.Current.Request.Cookies["_CodigoSocio_PersonaFit"].Value);
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static string Perfil
        {

            get
            {
                string codigo = string.Empty;
                //var _CodigoSede = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoSede");
                codigo = System.Web.HttpContext.Current.Session["_CodigoPerfil"].ToString();
                //int codigo = 0;
                //if (_CodigoSede != null)
                //    Int32.TryParse(_CodigoSede.Value.ToString(), out codigo);

                return codigo;
            }
        }

        public static double TaxImpuesto
        {
            get
            {
                //var _UnidadNegocio = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("CodigoUnidadNegocio");

                //int codigo = 0;
                //if (_UnidadNegocio != null)
                //    Int32.TryParse(_UnidadNegocio.Value.ToString(), out codigo);

                return 18.00; //codigo;
            }
        }

        public static string NombreUnidadNegocio
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("NombreUnidadNegocio");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }
        public static string NombreComercial
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("NombreComercial");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }
        public static string Usuario
        {
            get
            {
                var _valor = HttpContext.Current.Request.Cookies["_Usuario_Business"].Value.ToString();//((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("Usuario");

                if (_valor != null)
                    return _valor;
                else
                    return string.Empty;

            }
        }
        public static string UsuarioTiendaVirtual
        {
            get
            {
                var _valor = HttpContext.Current.Request.Cookies["_Usuario_PersonaTiendaVirtual"].Value.ToString();//((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("Usuario");

                if (_valor != null)
                    return _valor;
                else
                    return string.Empty;

            }
        }
        public static string Usuario_PersonaFit
        {
            get
            {
                var _valor = HttpContext.Current.Request.Cookies["_Usuario_PersonaFit"].Value.ToString();//((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("Usuario");

                if (_valor != null)
                    return _valor;
                else
                    return string.Empty;

            }
        }
        public static string UsuarioFullName
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("FullName");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }
        public static string SubDominio
        {
            get
            {
                var _valor = HttpContext.Current.Request.Cookies["_SubDominio_Business"].Value.ToString();//((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("SubDominio");

                if (_valor != null)
                    return _valor;
                else
                    return string.Empty;

            }
        }

        public static string SubDominioTiendaVirtual
        {
            get
            {
                var _valor = HttpContext.Current.Request.Cookies["_SubDominio_PersonaTiendaVirtual"].Value.ToString();//((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("SubDominio");

                if (_valor != null)
                    return _valor;
                else
                    return string.Empty;

            }
        }

        public static string SubDominio_PersonaFit
        {
            get
            {
                var _valor = HttpContext.Current.Request.Cookies["_SubDominio_PersonaFit"].Value.ToString();//((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("SubDominio");

                if (_valor != null)
                    return _valor;
                else
                    return string.Empty;
            }
        }

        public static string RUC
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("RUC");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }
        public static string RazonSocial
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("RazonSocial");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }

        public static string BasicAutentication
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("BasicAutentication");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }

        public static string RolesRunning
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("Roles");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;
            }
        }

        public static string UrlLogoUnidadNegocio
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("UrlLogo");

                if (_valor != null)
                    return _valor.Value;
                else
                    return string.Empty;

            }
        }

        public static bool FacturacionElectronicaIntegrada
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("TieneFacturacionElectronica");
                bool resultado = false;
                if (_valor != null)
                {
                    if (!string.IsNullOrEmpty(_valor.Value))
                    {
                        int TieneFacturacion = 0;
                        int.TryParse(_valor.Value, out TieneFacturacion);
                        if (TieneFacturacion > 0)
                        {
                            resultado = true;
                        }
                    }
                }
                return resultado;
            }
        }

        public static bool TieneControlSabana
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("TieneControlSabana");

                if (_valor != null)
                    if (!string.IsNullOrEmpty(_valor.Value))
                    {
                        return _valor.Value.ToString().ToUpper() == "TRUE" ? true : false;
                    }
                    else
                    {
                        return false;
                    }
                else
                    return false; //string.Empty;

            }
        }

        public static bool TieneFirmaIngresoHuesped
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("TieneFirmaIngresoHuesped");

                if (_valor != null)
                    if (!string.IsNullOrEmpty(_valor.Value))
                    {
                        return _valor.Value.ToString().ToUpper() == "TRUE" ? true : false;
                    }
                    else
                    {
                        return false;
                    }
                else
                    return false; //string.Empty;

            }
        }

        public static string RutaSunafact
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("RutaSunafact");

                if (_valor != null)
                    return _valor.Value;
                else
                    return "https://www.pse.pe/api/v1/f3e2fc0e95bc4835a9d3f1c581755b67e0aaed3df7304954b2bb19213e6a1199"; //string.Empty;

            }
        }

        public static string TokenSunafact
        {
            get
            {
                var _valor = ((System.Security.Claims.ClaimsIdentity)System.Web.HttpContext.Current.User.Identity).FindFirst("TokenSunafact");

                if (_valor != null)
                    return _valor.Value;
                else
                    return "eyJhbGciOiJIUzI1NiJ9.IjhhZjU0OTUwNWNkNTQxOGI5NzJiODg2ZjkyNWE5ZDgxYjJjMmRiOGZlZjhlNDQ4Yzk0M2ExMTk3ODI5ZmRmY2Ii.6buWQ1eRai8kxCbuBGC8ZTmctTaBgexIeJ0nB3Bs_4o"; //string.Empty;

            }
        }


        //validate email
        public static bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
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

        public static bool ContainsAnyCase(string haystack, string needle)
        {
            return haystack.IndexOf(needle, StringComparison.CurrentCultureIgnoreCase) != -1;
        }


        public static string TextReplace(string text, List<ParmTC> listparams)
        {

            string resplaces = text;
            foreach (ParmTC parm in listparams)
            {
                bool resp = Commun.ContainsAnyCase(resplaces, parm.Code);
                if (resp)
                {
                    string firstNames = resplaces.Replace(parm.Code, parm.Value);
                    resplaces = firstNames;
                }
            }
            return resplaces;
        }

        //remove caracter specials and concat spaces
        public static string removeCaracterSpecials(string str)
        {
            string strReplace = str.Replace(' ', '_');
            string strSanited = Regex.Replace(strReplace, @"[^\w\.@-]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            return strSanited;
        }
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

    }

    public class ParmTC
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public ParmTC(string name, string code, string value)
        {
            Name = name;
            Code = code;
            Value = value;
        }
    }
}