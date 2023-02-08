using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;


namespace BotComers.Filters
{
    public class VerifyBusinessAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var _CodigoUnidadNegocio_Business = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_Business"];
            var _CodigoSede_Business = HttpContext.Current.Request.Cookies["_CodigoSede_Business"];
            var _Usuario_Business = HttpContext.Current.Request.Cookies["_Usuario_Business"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageNotAuthentication.cshtml"
                };

            }
            else if (_CodigoUnidadNegocio_Business == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_CodigoSede_Business == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_Usuario_Business == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var _CodigoUnidadNegocio_Business = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_Business"];
            var _CodigoSede_Business = HttpContext.Current.Request.Cookies["_CodigoSede_Business"];
            var _Usuario_Business = HttpContext.Current.Request.Cookies["_Usuario_Business"];

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageNotAuthentication.cshtml"
                };
            }
            else if (_CodigoUnidadNegocio_Business == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_CodigoSede_Business == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/error/ErrorPageSessionFinish.cshtml"
                };
            }
            else if (_Usuario_Business == null)
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