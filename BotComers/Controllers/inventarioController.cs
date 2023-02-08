using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository.Inventario;
using BotComers.ViewModels.Inventario;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class inventarioController : Controller
    {
        // GET: inventario
        public ActionResult Index()
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                ItemsVentaViewModel request = new ItemsVentaViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoAlmacen = 1;
                lista = oItemsVentaRepository.ecommerce_uspListarValorInventario_Paginaciones(request, 1);
            }

            return View(lista);
        }

        public ActionResult ListarValorInventario(int CodigoAlmacen, string Nombre)
        {
            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                ItemsVentaViewModel request = new ItemsVentaViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoAlmacen = CodigoAlmacen;
                request.Nombre = Nombre;
                return Json(oItemsVentaRepository.ecommerce_uspListarValorInventario_Paginaciones(request, 1), JsonRequestBehavior.AllowGet);
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