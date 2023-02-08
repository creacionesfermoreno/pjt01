using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository.Inventario;
using E_DataModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace BotComers.Controllers
{
    [VerifyBusinessAuthentication]
    public class admintiendafitController : Controller
    {
        // GET: admintienda
        public ActionResult Index()
        {
            return View();
        }

        //CUPONES
        public ActionResult cupones()
        {
            List<CuponesDTO> lista = new List<CuponesDTO>();

            using (CuponesRepository CuponesRepository = new CuponesRepository())
            {
                CuponesDTO request = new CuponesDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = "appsfit";//Commun.Usuario;
                lista = CuponesRepository.ecommerce_uspListar_Cupones(request);
            }

            return View(lista);
        }

        public ActionResult addcupon()
        {
            return View();
        }

        public ActionResult editcupon(string id, string idCupon)
        {
            CuponesDTO oRow = new CuponesDTO();

            using (CuponesRepository CuponesRepository = new CuponesRepository())
            {
                CuponesDTO request = new CuponesDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoCupon = idCupon;
                oRow = CuponesRepository.ecommerce_uspBuscar_Cupones(request);
            }

            return View(oRow);
        }

        public ActionResult ecommerce_uspRegistrar_Cupones(CuponesDTO request)
        {
            using (CuponesRepository oRepository = new CuponesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrar_Cupones(request), JsonRequestBehavior.AllowGet);
            }
        }

        //TARIFAS DE ENVIO
        public ActionResult tarifasdeenvio(string id)
        {
            List<TarifasEnvioDTO> lista = new List<TarifasEnvioDTO>();

            using (TarifasEnvioRepository CuponesRepository = new TarifasEnvioRepository())
            {
                TarifasEnvioDTO request = new TarifasEnvioDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.TipoUbigeo = 2;
                request.Ubigeo = "0010000";
                lista = CuponesRepository.ecommerce_uspListar_TarifasEnvio(request);
            }

            return View(lista);
        }

        public ActionResult ecommerce_uspRegistrarAdminTarifasEnvio(TarifasEnvioDTO request)
        {
            using (TarifasEnvioRepository oRepository = new TarifasEnvioRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarAdminTarifasEnvio(request), JsonRequestBehavior.AllowGet);
            }
        }

        //FORMAS DE PAGOS PLANTILLAS

        public ActionResult formasdepago(string id)
        {
            List<PlantillaFormaPagoDTO> lista = new List<PlantillaFormaPagoDTO>();

            using (PlantillaFormaPagoRepository CuponesRepository = new PlantillaFormaPagoRepository())
            {
                PlantillaFormaPagoDTO request = new PlantillaFormaPagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                lista = CuponesRepository.ecommerce_uspListarAdminFormaPago(request);
            }

            return View(lista);
        }

        public ActionResult mercadopago(string id)
        {
            PlantillaFormaPagoDTO row = new PlantillaFormaPagoDTO();

            using (PlantillaFormaPagoRepository CuponesRepository = new PlantillaFormaPagoRepository())
            {
                PlantillaFormaPagoDTO request = new PlantillaFormaPagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                row = CuponesRepository.ecommerce_uspBuscarPlantillaFormaPago(request);
                if (row == null)
                {
                    row = new PlantillaFormaPagoDTO();
                    row.MercadoPago_Publickey = string.Empty;
                    row.MercadoPago_Accesstoken = string.Empty;
                }
            }

            return View(row);
        }

        public ActionResult ecommerce_uspRegistrarFormaPago_MercadoPago(PlantillaFormaPagoDTO request)
        {
            using (PlantillaFormaPagoRepository oRepository = new PlantillaFormaPagoRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarFormaPago_MercadoPago(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult yape(string id)
        {
            PlantillaFormaPagoDTO row = new PlantillaFormaPagoDTO();

            using (PlantillaFormaPagoRepository CuponesRepository = new PlantillaFormaPagoRepository())
            {
                PlantillaFormaPagoDTO request = new PlantillaFormaPagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                row = CuponesRepository.ecommerce_uspBuscarFormaPago_Yape(request);
                if (row == null)
                {
                    row = new PlantillaFormaPagoDTO();
                    row.Yape_NroCelular = string.Empty;
                    row.Yape_CodigoQR = string.Empty;
                }
            }

            return View(row);
        }

        public ActionResult ecommerce_uspRegistrarFormaPago_Yape(PlantillaFormaPagoDTO request)
        {
            using (PlantillaFormaPagoRepository oRepository = new PlantillaFormaPagoRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarFormaPago_Yape(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult contraentrega(string id)
        {
            PlantillaFormaPagoDTO row = new PlantillaFormaPagoDTO();

            using (PlantillaFormaPagoRepository CuponesRepository = new PlantillaFormaPagoRepository())
            {
                PlantillaFormaPagoDTO request = new PlantillaFormaPagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                row = CuponesRepository.ecommerce_uspBuscarFormaPago_ContraEntrega(request);
                if (row == null)
                {
                    row = new PlantillaFormaPagoDTO();
                    row.ContraEntrega_InstruccionesCorreo = string.Empty;
                    row.ContraEntrega_InstruccionesCheckout = string.Empty;
                }
            }

            return View(row);
        }

        public ActionResult ecommerce_uspRegistrarFormaPago_ContraEntrega(PlantillaFormaPagoDTO request)
        {
            using (PlantillaFormaPagoRepository oRepository = new PlantillaFormaPagoRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarFormaPago_ContraEntrega(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult enviogratis(string id)
        {
            EnvioGratisDTO row = new EnvioGratisDTO();

            using (EnvioGratisRepository oRepository = new EnvioGratisRepository())
            {
                EnvioGratisDTO request = new EnvioGratisDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                row = oRepository.ecommerce_uspBuscarEnvioGratis(request);
                if (row == null)
                {
                    row = new EnvioGratisDTO();
                    row.FechaInicio = DateTime.Now;
                    row.FechaInicio = DateTime.Now;
                    row.Valor = 0;
                }
            }

            return View(row);
        }

        public ActionResult ecommerce_uspRegistrarEnvioGratis(EnvioGratisDTO request)
        {
            using (EnvioGratisRepository oRepository = new EnvioGratisRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarEnvioGratis(request), JsonRequestBehavior.AllowGet);
            }
        }

    }
}