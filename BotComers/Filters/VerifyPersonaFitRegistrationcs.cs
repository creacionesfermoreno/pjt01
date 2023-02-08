using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace BotComers.Filters
{
    public class VerifyPersonaFitRegistrationcs : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var _CodigoUnidadNegocio_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaFit"];
            var _CodigoSede_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoSede_PersonaFit"];

            if (_CodigoUnidadNegocio_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/pg/registrate.cshtml"
                };
            }
            else if (_CodigoSede_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/pg/registrate.cshtml"
                };
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var _CodigoUnidadNegocio_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoUnidadNegocio_PersonaFit"];
            var _CodigoSede_PersonaFit = HttpContext.Current.Request.Cookies["_CodigoSede_PersonaFit"];

            if (_CodigoUnidadNegocio_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/pg/registrate.cshtml"
                };
            }
            else if (_CodigoSede_PersonaFit == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/pg/registrate.cshtml"
                };
            }

        }

    }
}