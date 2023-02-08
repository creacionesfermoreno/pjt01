using BotComers.Helpers;
using BotComers.Repository.Configuracion;
using BotComers.ViewModels.Configuracion;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class configuracionController : Controller
    {
        // GET: configuracion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult usuarios()
        {
            List<AspNetUsersViewModel> lista = new List<AspNetUsersViewModel>();

            using (AspNetUsersRepository oAspNetUsersRepository = new AspNetUsersRepository())
            {
                AspNetUsersViewModel request = new AspNetUsersViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.PageNumber = 1;
                lista = oAspNetUsersRepository.ecommerce_uspListarAspNetUsers_Paginacion(request);
            }

            return View(lista);
        }

        public ActionResult usuariosadd()
        {
            return View();
        }

        public ActionResult RegistrarUsuario(AspNetUsersViewModel request)
        {
            using (AspNetUsersRepository oRepository = new AspNetUsersRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.UserType = 2;
                return Json(oRepository.ecommerce_uspRegistrar_AspNetUsers(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditClave(string id)
        {
            ViewBag.idUsuarioCambiarClave = id;
            return View();
        }

        public ActionResult EjecutarCambiarClave(AspNetUsersViewModel request)
        {
            using (AspNetUsersRepository oRepository = new AspNetUsersRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspActualizarCambiarClave_AspNetUsers(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult usuariosedit()
        {
            return View();
        }

    }
}