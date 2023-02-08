using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class errorController : Controller
    {
        // GET: error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorPageNotAuthentication()
        {
            return View();
        }

        public ActionResult ErrorPageNotDominio()
        {
            return View();
        }
        public ActionResult ErrorPageNotDominioTienda()
        {
            return View();
        }
        public ActionResult ErrorPageSessionFinish()
        {
            return View();
        }


    }
}