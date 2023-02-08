using BotComers.Filters;
using BotComers.ViewModels.Inventario;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class almacenesController : Controller
    {
        // GET: almacenes
        public ActionResult Index()
        {
            List<AlmacenesViewModel> lista = new List<AlmacenesViewModel>();

            //using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            //{
            //    ItemsVentaViewModel request = new ItemsVentaViewModel();
            //    request.CodigoUnidadNegocio = 1;//Commun.CodigoUnidadNegocio;
            //    request.CodigoSede = 1;//Commun.CodigoSede;
            //    request.UsuarioCreacion = "admin";//Commun.Usuario;
            //    lista = oItemsVentaRepository.ecommerce_uspListarItemsVenta_Paginacion(request, 1);
            //}

            return View(lista);
        }


    }
}