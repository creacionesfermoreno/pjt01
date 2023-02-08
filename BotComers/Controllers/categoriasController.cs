using BotComers.Helpers;
using BotComers.Repository;
using BotComers.ViewModels;
using E_DataModel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class categoriasController : Controller
    {
        // GET: categorias
        public ActionResult Index(string id)
        {
            using (CategoriasRepository oRepository = new CategoriasRepository())
            {
                CategoriasDTO request = new CategoriasDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //return Json(, JsonRequestBehavior.AllowGet);

                List<CategoriasProductosViewModel> listaView = new List<CategoriasProductosViewModel>();
                List<CategoriasDTO> lista = new List<CategoriasDTO>();
                listaView = oRepository.ecommerce_uspListarCategorias(request.CodigoUnidadNegocio, request.CodigoSede, 0);
                for (int i = 0; i < listaView.Count; i++)
                {
                    lista.Add(new CategoriasDTO()
                    {
                        CodigoMenu = listaView[i].CodigoMenu,
                        CodigoImagenPortada = listaView[i].CodigoImagenPortada,
                        Descripcion = listaView[i].Descripcion,
                        UrlImagen = listaView[i].UrlImagen
                    });
                }
                return View(lista);
            }

        }

        public JsonResult ImageUpload(CategoriasProductosViewModel request)
        {
            var file2 = request.ImageFile2;
            string ruta;
            if (file2 != null)
            {
                var fileName = Path.GetFileName(file2.FileName);
                var extention = Path.GetExtension(file2.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.CodigoImagenPortada + extention), "portadacategorias");

                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UrlImagen = ruta;

                using (CategoriasRepository oRepository = new CategoriasRepository())
                {
                    oRepository.ecommerce_uspActualizarCatalogoPortadaFoto(request);
                }

            }
            return Json(file2.FileName, JsonRequestBehavior.AllowGet);
        }



    }
}