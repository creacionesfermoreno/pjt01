using BotComers.Filters;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class managementController : Controller
    {
        public ActionResult Index()
        {
            //EmpresaViewModel viewEmpresa = new EmpresaViewModel();
            //viewEmpresa.loadEmpresa = new EmpresaViewModelLoad();

            //using (EmpresaRepository oEmpresaRepository = new EmpresaRepository())
            //{
            //    viewEmpresa.loadEmpresa.listGridEmpresa = oEmpresaRepository.ecommerce_uspListarEmpresas_Paginacion(1);
            //    string filterEstado = "ecommerce_EstadoEmpresa";
            //    viewEmpresa.loadEmpresa.listEstadoEmpresa = oEmpresaRepository.ecommerce_uspListarMaestro(filterEstado);
            //    string filterPais = "ecommerce_Pais";
            //    viewEmpresa.loadEmpresa.listPaisEmpresa = oEmpresaRepository.ecommerce_uspListarMaestro(filterPais);
            //    string filterDocumentoEmpresa = "ecommerce_TipoDocumentoEmpresa";
            //    viewEmpresa.loadEmpresa.listTipoDocumentoEmpresa = oEmpresaRepository.ecommerce_uspListarMaestro(filterDocumentoEmpresa);
            //}

            return View();
        }
    }
}
