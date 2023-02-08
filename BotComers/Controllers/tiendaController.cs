using BotComers.Checkout_api.mercadopago;
using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository;
using BotComers.Repository.Configuracion;
using BotComers.Repository.Corporativo;
using BotComers.Repository.Ingresos;
using BotComers.Repository.Inventario;
using BotComers.ViewModels;
using BotComers.ViewModels.Configuracion;
using BotComers.ViewModels.Ingresos;
using BotComers.ViewModels.Inventario;
using E_BusinessLayer.Gimnasio;
using E_DataModel;
using E_DataModel.Gimnasio;
using MercadoPago.DataStructures.Payment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BotComers.Controllers
{
    public class tiendaController : Controller
    {
        // GET: BotComers
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                string SubDominio = string.Empty;
                List<EmpresaViewEditModel> oEmpresaViewEditModel = new List<EmpresaViewEditModel>();
                using (EmpresaRepository oRepository = new EmpresaRepository())
                {
                    oEmpresaViewEditModel = oRepository.ecommerce_uspObtenerEmpresaPorDominio(id);
                    if (oEmpresaViewEditModel.Count > 0)
                    {
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaTiendaVirtual", id);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaTiendaVirtual", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());
                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);

                        HttpCookie miCookieSeguridad_CodigoSede = new HttpCookie("_CodigoSede_PersonaTiendaVirtual", oEmpresaViewEditModel[0].CodigoSede.ToString());
                        miCookieSeguridad_CodigoSede.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_CodigoSede);

                        HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaTiendaVirtual", oEmpresaViewEditModel[0].ColorEmpresa.ToString());
                        miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                        HttpCookie miCookieSeguridad_LogoTipo = new HttpCookie("_LogoTipo_PersonaTiendaVirtual", oEmpresaViewEditModel[0].LogoTipo.ToString());
                        miCookieSeguridad_LogoTipo.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_LogoTipo);


                        return View();
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominioTienda", "error");
                    }

                }

            }

        }

        public ActionResult categoria(string id, string Idcat)
        {
            CategoriasProductosViewModel response = new CategoriasProductosViewModel();
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                using (CategoriasRepository oRepository = new CategoriasRepository())
                {
                    int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                    int CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;

                    response = oRepository.ecommerce_uspBuscarCategoriasTiendaVirtual(CodigoUnidadNegocio, CodigoSede, Idcat);
                }

            }
            return View(response);
        }

        // [HttpGet]
        public JsonResult uspBuscadorItemsVentaInventariable(string buscador)
        {
            ItemsVentaViewModel request = new ItemsVentaViewModel();
            using (ItemsVentaRepository oRepository = new ItemsVentaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                request.CodigoAlmacen = 1;
                request.Nombre = buscador;
                request.UsuarioCreacion = "appsfit";

                return Json(oRepository.ecommerce_uspBuscadorItemsVentaInventariable(request), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult confitienda(string id)
        {
            return View();
        }

        public JsonResult ImageUpload(ItemsVentaViewModel request)
        {
            //var file = request.ImageFile;
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

                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UrlImagen = ruta;

                using (ItemsVentaRepository oRepository = new ItemsVentaRepository())
                {
                    oRepository.ecommerce_uspActualizarItemsVentaFoto(request);
                }

            }
            return Json(file2.FileName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ecommerce_uspListarCategorias(CategoriasDTO request)
        {
            List<CategoriasProductosViewModel> listaView = new List<CategoriasProductosViewModel>();
            List<CategoriasDTO> lista = new List<CategoriasDTO>();

            using (CategoriasRepository ocatRepository = new CategoriasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                request.UsuarioCreacion = "appsfit";

                listaView = ocatRepository.ecommerce_uspListarCategorias(request.CodigoUnidadNegocio, request.CodigoSede, 0);
                for (int i = 0; i < listaView.Count; i++)
                {
                    lista.Add(new CategoriasDTO()
                    {
                        CodigoMenuSuperior = listaView[i].CodigoMenuSuperior,
                        CodigoMenu = listaView[i].CodigoMenu,
                        CodigoImagenPortada = listaView[i].CodigoImagenPortada,
                        Descripcion = listaView[i].Descripcion,
                        UrlImagen = listaView[i].UrlImagen
                    });
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ecommerce_uspListarItemsVenta_PorCategoriaPaginacion(ItemsVentaViewModel request)
        {
            using (ItemsVentaRepository oRepository = new ItemsVentaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                request.UsuarioCreacion = "appsfit";

                return Json(oRepository.ecommerce_uspListarItemsVenta_PorCategoriaPaginacion(request, 1), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult detalle(string id, string Idproducto)
        {
            ItemsVentaViewModel request = new ItemsVentaViewModel();

            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                using (ItemsVentaRepository oRepository = new ItemsVentaRepository())
                {

                    request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                    request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                    request.CodigoImagen = Idproducto;

                    request = oRepository.ecommerce_uspBuscarItemsVentasTienda(request);


                }
            }
            return View(request);
            //return View();
        }

        public ActionResult carrito(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }
        }

        [VerifyPersonaTiendaVirtualAuthentication]
        public ActionResult checkout(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult login(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult LogOff(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                FormsAuthentication.SignOut();
                Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));

                Request.Cookies.Remove("_Usuario_PersonaTiendaVirtual");
                //HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                //if (cookie != null)
                //{
                //    cookie.Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies.Add(cookie);
                //}

                //foreach (var cookie2 in Request.Cookies.AllKeys)
                //{
                //    Request.Cookies.Remove(cookie2);
                //}
                foreach (var cookie3 in Response.Cookies.AllKeys)
                {
                    Response.Cookies.Remove(cookie3);
                }

                string SubDominio = Commun.SubDominioTiendaVirtual;



                return RedirectToAction("login", new { id = SubDominio });
                //return Json(Url.Action("login", "tienda", new { id = SubDominio }));
            }

        }

        [HttpPost]
        public ActionResult ingresarPersonaFit(AspNetUsersViewModel request)
        {
            AspNetUsersViewModel oAspNetUsersViewModel = new AspNetUsersViewModel();
            oAspNetUsersViewModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
            oAspNetUsersViewModel.CodigoSede = 1;
            oAspNetUsersViewModel.UserName = request.UserName;
            oAspNetUsersViewModel.PasswordHash = request.PasswordHash;
            using (AspNetUsersRepository oItemsVentaRepository = new AspNetUsersRepository())
            {
                oAspNetUsersViewModel.Id = oItemsVentaRepository.ecommerce_AspNetUsers_ValidarUsuarioTiendaVirtual(oAspNetUsersViewModel);
            }
            if (oAspNetUsersViewModel.Id != "sinregistro" && oAspNetUsersViewModel.Id != string.Empty)
            {
                HttpCookie miCookieSeguridad_Usuario = new HttpCookie("_Usuario_PersonaTiendaVirtual", oAspNetUsersViewModel.Id);
                miCookieSeguridad_Usuario.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_Usuario);

                HttpCookie miCookieSeguridad_Sede = new HttpCookie("_CodigoSede_PersonaTiendaVirtual", oAspNetUsersViewModel.CodigoSede.ToString());
                miCookieSeguridad_Sede.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_Sede);

                string SubDominio = Commun.SubDominioTiendaVirtual;
                FormsAuthentication.SetAuthCookie(oAspNetUsersViewModel.UserName, false);

                return Json(Url.Action("checkout", "tienda", new { id = SubDominio }));
            }
            else
            {
                return Content("0");
            }
        }

        [HttpPost]
        public ActionResult GuardarPersonaTiendaVirtual(AspNetUsersViewModel request)
        {
            using (AspNetUsersRepository oRepository = new AspNetUsersRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = 1;//Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoCargo = 0;
                request.UserType = 1;
                request.UsuarioCreacion = "appsfit";
                request.PasswordHash = request.PasswordConfirmacion;
                //request.UsuarioCreacion = Commun.Usuario;
                int Validation = oRepository.ecommerce_uspRegistrar_AspNetUsersTiendaVirtual(request);
                if (Validation == 1)
                {
                    return Content("1");
                }
                else if (Validation == 0)
                {
                    HttpCookie _Usuario_PersonaFit = new HttpCookie("_Usuario_PersonaTiendaVirtual", request.Email.Trim());
                    HttpContext.Response.SetCookie(_Usuario_PersonaFit);
                    FormsAuthentication.SetAuthCookie(request.Email.Trim(), false);
                }

                return Content("1");

            }
        }

        public ActionResult ListarUbicaciones(UbicacionesDTO model)
        {
            List<UbicacionesDTO> lista = null;
            UbicacionesDTO oUbicacionesDTO = new UbicacionesDTO();
            oUbicacionesDTO.Tipo = model.Tipo;
            oUbicacionesDTO.Ubicaciones = model.Ubicaciones;
            oUbicacionesDTO.Buscador = model.Buscador == null ? string.Empty : model.Buscador;
            ReqFilterUbicacionesDTO oReq = new ReqFilterUbicacionesDTO()
            {
                Item = oUbicacionesDTO,
                User = "tienda",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUbicacionesDTO oResp = null;
            using (UbicacionesLogic oUbicacionesLogic = new UbicacionesLogic())
            {
                oResp = oUbicacionesLogic.UbicacionesGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [VerifyPersonaTiendaVirtualAuthentication]
        public ActionResult ecommerce_uspRegistrarAspNetUsersDireccionesEntrega(AspNetUsersDireccionesEntregaDTO request)
        {
            using (AspNetUsersDireccionesEntregaRepository oRepository = new AspNetUsersDireccionesEntregaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = 1;
                request.CorreoUsuario = Commun.UsuarioTiendaVirtual;
                string Validation = oRepository.ecommerce_uspRegistrarAspNetUsersDireccionesEntrega(request);
                return Json(Validation, JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaTiendaVirtualAuthentication]
        public JsonResult ecommerce_uspListarAspNetUsers_DireccionesEntrega(AspNetUsersDireccionesEntregaDTO request)
        {
            using (AspNetUsersDireccionesEntregaRepository oRepository = new AspNetUsersDireccionesEntregaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                //request.IdUser = request.IdUser;
                request.CorreoUsuario = Commun.UsuarioTiendaVirtual;

                return Json(oRepository.ecommerce_uspListarAspNetUsers_DireccionesEntrega(request), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaTiendaVirtualAuthentication]
        public ActionResult RegistrarComprobanteTiendaVirtual(ComprobanteViewModel request)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                request.UsuarioCreacion = Commun.UsuarioTiendaVirtual;
                return Json(oRepository.ecommerce_uspRegistrarComprobante_TiendaVirtual(request), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult process_payment()
        {

            var addInfoPayer = new AdditionalInfoPayer
            {
                FirstName = "Rubén",
                LastName = "González"
            };

            var item = new Item
            {
                Id = "producto123",
                Title = "celular blanco",
                Description = "4g, 32 gb",
                Quantity = 1,
                PictureUrl = "http://www.imagenes.com/celular.jpg",
                UnitPrice = 100.4m
            };

            List<Item> items = new List<Item>();
            items.Add(item);

            PaymentParameter oPaymentParameter = new PaymentParameter();
            oPaymentParameter.AccessToken = "TEST-1527733874560186-082420-8f0fd52891665f9f6195278db873b825-630976884";
            oPaymentParameter.TransactionAmount = float.Parse(Request["transactionAmount"]);
            oPaymentParameter.Token = Request["token"];
            oPaymentParameter.Description = Request["description"];
            oPaymentParameter.PaymentMethodId = Request["paymentMethodId"];
            oPaymentParameter.Email = Request["email"];
            oPaymentParameter.docType = Request["docType"];
            oPaymentParameter.docNumber = Request["docNumber"];
            oPaymentParameter.AdditionalInfoPayer = addInfoPayer;
            oPaymentParameter.items = items;

            PaymentHelper helperMercadoPago = new PaymentHelper();
            helperMercadoPago.GuardarPago(oPaymentParameter);

            //payment.Save();
            return View();
        }


        public ActionResult confirmacion(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        //CUPONERA
        public JsonResult ecommerce_uspBuscar_CuponesXCodigoPromocion(CuponesDTO request)
        {
            using (CuponesRepository oRepository = new CuponesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspBuscar_CuponesXCodigoPromocion(request), JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ecommerce_uspBuscar_CuponesXCodigoPromocion_TiendaVirtual(CuponesDTO request)
        {
            using (CuponesRepository oRepository = new CuponesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                request.UsuarioCreacion = "appsfit";//Commun.Usuario;
                return Json(oRepository.ecommerce_uspBuscar_CuponesXCodigoPromocion(request), JsonRequestBehavior.AllowGet);
            }
        }
        //FORMA PAGO YAPE
        public JsonResult ecommerce_uspBuscarFormaPago_Yape()
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
                return Json(row, JsonRequestBehavior.AllowGet);
            }

        }

        [VerifyPersonaTiendaVirtualAuthentication]
        public JsonResult ecommerce_uspBuscarFormaPago_Yape_TiendaVirtual()
        {
            PlantillaFormaPagoDTO row = new PlantillaFormaPagoDTO();

            using (PlantillaFormaPagoRepository CuponesRepository = new PlantillaFormaPagoRepository())
            {
                PlantillaFormaPagoDTO request = new PlantillaFormaPagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                row = CuponesRepository.ecommerce_uspBuscarFormaPago_Yape(request);
                if (row == null)
                {
                    row = new PlantillaFormaPagoDTO();
                    row.Yape_NroCelular = string.Empty;
                    row.Yape_CodigoQR = string.Empty;
                }
                return Json(row, JsonRequestBehavior.AllowGet);
            }

        }
        //FORMA CONTRA ENTREGA
        public JsonResult ecommerce_uspBuscarFormaPago_ContraEntrega()
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
                    row.Yape_NroCelular = string.Empty;
                    row.Yape_CodigoQR = string.Empty;
                }
                return Json(row, JsonRequestBehavior.AllowGet);
            }

        }

        [VerifyPersonaTiendaVirtualAuthentication]
        public JsonResult ecommerce_uspBuscarFormaPago_ContraEntrega_TiendaVitual()
        {
            PlantillaFormaPagoDTO row = new PlantillaFormaPagoDTO();

            using (PlantillaFormaPagoRepository CuponesRepository = new PlantillaFormaPagoRepository())
            {
                PlantillaFormaPagoDTO request = new PlantillaFormaPagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                row = CuponesRepository.ecommerce_uspBuscarFormaPago_ContraEntrega(request);
                if (row == null)
                {
                    row = new PlantillaFormaPagoDTO();
                    row.Yape_NroCelular = string.Empty;
                    row.Yape_CodigoQR = string.Empty;
                }
                return Json(row, JsonRequestBehavior.AllowGet);
            }

        }
        //ENVIO GRATIS
        public JsonResult ecommerce_uspBuscarEnvioGratis()
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
                    row.Valor = 0;
                }
            }
            return Json(row, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ecommerce_uspBuscarEnvioGratisXtiendaVirtual()
        {
            EnvioGratisDTO row = new EnvioGratisDTO();

            using (EnvioGratisRepository oRepository = new EnvioGratisRepository())
            {
                EnvioGratisDTO request = new EnvioGratisDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaTiendaVirtual;
                request.CodigoSede = Commun.CodigoSede_PersonaTiendaVirtual;
                row = oRepository.ecommerce_uspBuscarEnvioGratisXtienda(request);
                if (row == null)
                {
                    row = new EnvioGratisDTO();
                    row.Valor = 0;
                }
            }
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        //CATEGORIAS EN DURO

        public ActionResult cat_bancas(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult cat_trotadoras(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult cat_elipticas(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult cat_bicicletas(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult cat_remo(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult cat_homegym(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        public ActionResult cat_bancosmultiajustables(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }

        //PRODUCTOS

        public ActionResult cat_minigimnasio_multifuncionalmodelo001(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominioTienda", "error");
            }
            else
            {
                return View();
            }

        }


    }
}
