using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository;
using BotComers.Repository.Inventario;
using BotComers.ViewModels;
using BotComers.ViewModels.Inventario;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class itemsventaController : Controller
    {
        public ActionResult Index()
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                ItemsVentaViewModel request = new ItemsVentaViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                lista = oItemsVentaRepository.ecommerce_uspListarItemsVenta_Paginacion(request, 1);
            }

            return View(lista);
        }

        public ActionResult ListarItemsVenta(string Buscador)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                ItemsVentaViewModel request = new ItemsVentaViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Nombre = Buscador;
                return Json(oItemsVentaRepository.ecommerce_uspListarItemsVenta_Paginacion(request, 1), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult add()
        {

            return View();
        }

        public ActionResult edit(int id)
        {
            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                int CodigoItemVenta = id;
                return View(oItemsVentaRepository.ecommerce_uspBuscarItemsVentas(CodigoUnidadNegocio, CodigoSede, CodigoItemVenta));
            }
        }

        public ActionResult RegistrarItemsVenta(ItemsVentaViewModel request)
        {
            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oItemsVentaRepository.ecommerce_uspRegistrarItemsVenta(request), JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult EliminarItemsVenta(ItemsVentaViewModel request)
        {
            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oItemsVentaRepository.ecommerce_uspEliminarItemsVenta(request), JsonRequestBehavior.AllowGet);
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

        public ActionResult uspListarCategorias()
        {
            CategoriasProductosViewModel request = new CategoriasProductosViewModel();
            using (CategoriasRepository oCategoriasRepository = new CategoriasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.CodigoMenuSuperior = 0;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oCategoriasRepository.ecommerce_uspListarCategorias(request.CodigoUnidadNegocio, request.CodigoSede, request.CodigoMenuSuperior), JsonRequestBehavior.AllowGet);
            }
        }

        #region PERMISOS

        public ActionResult SEGListarPerfilMenu()
        {
            E_DataModel.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaDTO request = new E_DataModel.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaDTO();
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.CodigoPerfil = Commun.CodigoPerfil;

            List<E_DataModel.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaDTO> lista = null;

            E_DataModel.CentroEntrenamiento.ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new E_DataModel.CentroEntrenamiento.ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                FilterCase = E_DataModel.Common.filterCaseCentroEntrenamiento_MenuPlantilla.SEGListarPerfilMenu,
                User = "appsfit",
                Item = request,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            E_DataModel.CentroEntrenamiento.RespListCentroEntrenamiento_MenuPlantillaDTO oResp = null;

            using (E_BusinessLayer.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new E_BusinessLayer.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.CentroEntrenamiento_MenuPlantillaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
