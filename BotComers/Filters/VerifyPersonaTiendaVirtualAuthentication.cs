using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace BotComers.Filters
{
    public class VerifyPersonaTiendaVirtualAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var _CodigoUnidadNegocio_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaTiendaVirtual"];
            var _CodigoSede_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoSede_PersonaTiendaVirtual"];
            var _Usuario_PersonaFit = HttpContext.Current.Request.Cookies["_Usuario_PersonaTiendaVirtual"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/tienda/login.cshtml"
                };

            }
            else if (_CodigoUnidadNegocio_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageNotDominioTienda.cshtml"
                };
            }
            else if (_CodigoSede_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageNotDominioTienda.cshtml"
                };
            }
            else if (_Usuario_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/tienda/login.cshtml"
                };
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var _CodigoUnidadNegocio_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaTiendaVirtual"];
            var _CodigoSede_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoSede_PersonaTiendaVirtual"];
            var _Usuario_PersonaFit = HttpContext.Current.Request.Cookies["_Usuario_PersonaTiendaVirtual"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/tienda/login.cshtml"
                };
            }
            else if (_CodigoUnidadNegocio_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageNotDominioTienda.cshtml"
                };
            }
            else if (_CodigoSede_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageNotDominioTienda.cshtml"
                };
            }
            else if (_Usuario_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/tienda/login.cshtml"
                };
            }

        }

    }
}