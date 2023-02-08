using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository;
using BotComers.Repository.Inventario;
using BotComers.ViewModels;
using BotComers.ViewModels.Inventario;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class itemsventafitController : Controller
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

        public ActionResult ActualizarFotoItemsVenta(ItemsVentaViewModel request)
        {
            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;

                var file2 = request.ImageFile2;
                string ruta;
                if (file2 != null)
                {
                    var fileName = Path.GetFileName(file2.FileName);
                    var extention = Path.GetExtension(file2.FileName);
                    var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                    var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                    var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                    ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.CodigoImagen + extention), "productos");
                    request.UrlImagen = ruta;
                    oItemsVentaRepository.ecommerce_uspActualizarItemsVentaFoto(request);
                }

                return Json(true, JsonRequestBehavior.AllowGet);

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

    }
}