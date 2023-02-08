
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace BotComers.Filters
{
    public class VerifyPersonaFitAuthenticationcs : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var _CodigoUnidadNegocio_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaFit"];
            var _CodigoSede_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoSede_PersonaFit"];
            var _Usuario_PersonaFit = HttpContext.Current.Request.Cookies["_Usuario_PersonaFit"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/pg/login.cshtml"
                };

            }
            else if (_CodigoUnidadNegocio_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_CodigoSede_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_Usuario_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var _CodigoUnidadNegocio_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaFit"];
            var _CodigoSede_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoSede_PersonaFit"];
            var _Usuario_PersonaFit = HttpContext.Current.Request.Cookies["_Usuario_PersonaFit"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/pg/login.cshtml"
                };
            }
            else if (_CodigoUnidadNegocio_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_CodigoSede_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_Usuario_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }

        }


        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        // The user is not authenticated
        //        base.HandleUnauthorizedRequest(filterContext);
        //    }
        //    else if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
        //    {
        //        // The user is not in any of the listed roles => 
        //        // show the unauthorized view
        //        filterContext.Result = new ViewResult
        //        {
        //            ViewName = "~/Views/Shared/UnauthorizedAccess.cshtml"
        //        };
        //    }
        //    else
        //    {
        //        base.HandleUnauthorizedRequest(filterContext);
        //    }
        //}

    }
}