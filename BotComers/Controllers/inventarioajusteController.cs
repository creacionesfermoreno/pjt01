using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository.Inventario;
using BotComers.ViewModels.Inventario;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class inventarioajusteController : Controller
    {
        // GET: inventarioajuste
        public ActionResult Index()
        {
            ItemsVentaAjusteInventarioViewModel request = new ItemsVentaAjusteInventarioViewModel();
            List<ItemsVentaAjusteInventarioViewModel> lista = new List<ItemsVentaAjusteInventarioViewModel>();
            using (ItemsVentaAjusteInventarioRepository oRepository = new ItemsVentaAjusteInventarioRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;

                request.CodigoItemsVentaAjusteInventario = 0;
                request.DesAlmacen = string.Empty;
                request.b_FechaAjusteInicio = null;
                request.b_FechaAjusteFin = null;
                request.Observaciones = string.Empty;

                request.UsuarioCreacion = Commun.Usuario;
                request.PageNumber = 1;
                lista = oRepository.ecommerce_uspListarItemsVentaAjusteInventario_Paginacion(request);
            }

            return View(lista);
        }

        public ActionResult ListarItemsVentaAjusteInventario_Paginacion(ItemsVentaAjusteInventarioViewModel request)
        {

            List<ItemsVentaAjusteInventarioViewModel> lista = new List<ItemsVentaAjusteInventarioViewModel>();
            using (ItemsVentaAjusteInventarioRepository oRepository = new ItemsVentaAjusteInventarioRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.CodigoItemsVentaAjusteInventario = 0;
                request.DesAlmacen = string.Empty;
                request.Observaciones = string.Empty;
                request.UsuarioCreacion = Commun.Usuario;

                //request.b_FechaAjusteInicio = DateTime.Now;
                //request.b_FechaAjusteFin = DateTime.Now;
                //request.PageNumber = 1;
                return Json(oRepository.ecommerce_uspListarItemsVentaAjusteInventario_Paginacion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult add()
        {
            return View();
        }

        public ActionResult BuscadorItemsVentaInventariable(ItemsVentaViewModel request)
        {
            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.Nombre = request.Nombre == null ? string.Empty : request.Nombre;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oItemsVentaRepository.ecommerce_uspBuscadorItemsVentaInventariable(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RegistrarAjusteInventario(ItemsVentaAjusteInventarioViewModel request)
        {
            using (ItemsVentaAjusteInventarioRepository oItemsVentaAjusteInventarioRepository = new ItemsVentaAjusteInventarioRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oItemsVentaAjusteInventarioRepository.ecommerce_uspRegistrarItemsVenta(request), JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult ListarAlmacenes(AlmacenesViewModel request)
        {
            using (AlmacenesRepository oAlmacenesRepository = new AlmacenesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oAlmacenesRepository.ecommerce_uspListarAlmacenes(request.CodigoUnidadNegocio, request.CodigoSede, 1), JsonRequestBehavior.AllowGet);
            }
        }

    }
}