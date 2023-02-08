using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository.Ingresos;
using BotComers.Repository.Inventario;
using BotComers.ViewModels;
using BotComers.ViewModels.Ingresos;
using BotComers.ViewModels.Inventario;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{

    [VerifyBusinessAuthentication]
    public class invoiceController : Controller
    {
        // GET: invoice
        public ActionResult Index()
        {
            List<ComprobanteViewModel> lista = new List<ComprobanteViewModel>();

            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                ComprobanteViewModel request = new ComprobanteViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.PageNumber = 1;
                request.b_FechaEmisionInicio = null;
                request.b_FechaEmisionFin = null;

                lista = oRepository.ecommerce_uspListarComprobante_Paginacion(request);
            }

            return View(lista);
        }

        public ActionResult ListarComprobante_Paginacion(ComprobanteViewModel request)
        {
            List<ComprobanteViewModel> lista = new List<ComprobanteViewModel>();

            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.PageNumber = request.PageNumber;
                request.CodigoEstadoEntrega = request.CodigoEstadoEntrega == 0 ? null : request.CodigoEstadoEntrega;
                request.b_FechaEmisionInicio = request.b_FechaEmisionInicio;
                request.b_FechaEmisionFin = request.b_FechaEmisionFin;

                return Json(oRepository.ecommerce_uspListarComprobante_Paginacion(request), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult add()
        {

            return View();
        }
        public ActionResult transaction(int idc, int idco)
        {
            ViewBag.idcomprobante = idco;
            List<ComprobanteViewModel> lista = new List<ComprobanteViewModel>();

            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                ComprobanteViewModel request = new ComprobanteViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.CodigoCliente = idc;
                request.CodigoComprobante = idco;
                request.UsuarioCreacion = Commun.Usuario;
                request.PageNumber = 1;
                request.b_FechaEmisionInicio = null;
                request.b_FechaEmisionFin = null;

                lista = oRepository.ecommerce_uspListarComprobante_Paginacion(request);
            }

            return View(lista);
        }

        public ActionResult BuscadorClientes(ClientesViewModel request)
        {
            using (ClientesRepository oRepository = new ClientesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspBuscadorClientes(request), JsonRequestBehavior.AllowGet);
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
        public ActionResult ListarTipoComprobante(TipoComprobanteViewModel request)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                return Json(oRepository.ecommerce_uspListarTipoComprobante(request), JsonRequestBehavior.AllowGet);
            }
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
        public ActionResult RegistrarComprobante(ComprobanteViewModel request)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarComprobante(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RegistrarComprobantePago(ComprobanteViewModel request)
        {
            using (ComprobantePagoRepository oRepository = new ComprobantePagoRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarComprobantePago(request), JsonRequestBehavior.AllowGet);
            }

        }


    }
}