using BotComers.Helpers;
using BotComers.Repository.CentroEntrenamiento;
using BotComers.Repository.Configuracion;
using BotComers.Repository.Corporativo;
using BotComers.ViewModels;
using BotComers.ViewModels.Configuracion;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BotComers.Controllers
{
    public class accessController : Controller
    {
        // GET: access
        public ActionResult Index(string un)
        {
            return View();
        }

        public ActionResult uspSeguridadObtenerUnidadNegocio(string SubDominio)
        {

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.SubDominio = SubDominio;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = "Admin",
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.uspSeguridadObtenerUnidadNegocio_SubDominio
            };

            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);

            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }

            return Json(oConfiguracionDTO, JsonRequestBehavior.AllowGet);

        }

        public ActionResult login(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominio", "error");
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
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_Business", id);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_Business", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());
                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);

                        return View(oEmpresaViewEditModel);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }
        }

        public ActionResult logintiendaonline(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominio", "error");
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
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_Business", id);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_Business", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());
                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);

                        return View(oEmpresaViewEditModel);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }
        }

        [HttpPost]
        public ActionResult ingresar(string user, string password, int SelectedValue)
        {
            AspNetUsersViewModel oAspNetUsersViewModel = new AspNetUsersViewModel();
            oAspNetUsersViewModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAspNetUsersViewModel.CodigoSede = SelectedValue;
            oAspNetUsersViewModel.UserName = user;
            oAspNetUsersViewModel.PasswordHash = password;
            using (AspNetUsersRepository oItemsVentaRepository = new AspNetUsersRepository())
            {
                oAspNetUsersViewModel.LoginValidation = oItemsVentaRepository.ecommerce_AspNetUsers_ValidarUsuarioBusiness(oAspNetUsersViewModel);
            }
            if (oAspNetUsersViewModel.LoginValidation == 1)
            {
                HttpCookie miCookieSeguridad_Usuario = new HttpCookie("_Usuario_Business", oAspNetUsersViewModel.UserName);
                miCookieSeguridad_Usuario.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_Usuario);

                //HttpCookie miCookieSeguridad_Sede = new HttpCookie("_CodigoSede_Business", oAspNetUsersViewModel.CodigoSede.ToString());
                HttpCookie miCookieSeguridad_Sede = new HttpCookie("_CodigoSede_Business", SelectedValue.ToString());
                miCookieSeguridad_Sede.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_Sede);

                FormsAuthentication.SetAuthCookie(oAspNetUsersViewModel.UserName, false);
                string SubDominio = Commun.SubDominio;

                using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                {
                    CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                    requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                    requestPG.CodigoSede = SelectedValue;
                    requestPG.UsuarioCreacion = Commun.Usuario;
                    requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarLogoCorporativo(requestPG);

                    if (requestPG.logoPagina == string.Empty)
                    {
                        requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                    }

                    //ViewBag.NombreComercial = requestPG.NombreComercial;

                    HttpCookie miCookieSeguridad_urlCorporativo_Logo = new HttpCookie("_urlCorporativo_Logo", requestPG.logoPagina);
                    miCookieSeguridad_urlCorporativo_Logo.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlCorporativo_Logo);

                    HttpCookie miCookieSeguridad_urlCorporativo_NombreComercial = new HttpCookie("_urlCorporativo_NombreComercial", requestPG.NombreComercial);
                    miCookieSeguridad_urlCorporativo_NombreComercial.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlCorporativo_NombreComercial);

                    HttpCookie miCookieSeguridad_urlCorporativo_PlanEmpresa = new HttpCookie("_urlCorporativo_PlanEmpresa", requestPG.DesPlanEmpresa);
                    miCookieSeguridad_urlCorporativo_PlanEmpresa.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlCorporativo_PlanEmpresa);

                    HttpCookie miCookieSeguridad_urlCorporativo_EstadoEmpresa = new HttpCookie("_urlCorporativo_EstadoEmpresa", requestPG.EstadoEmpresa);
                    miCookieSeguridad_urlCorporativo_EstadoEmpresa.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlCorporativo_EstadoEmpresa);

                    HttpCookie miCookieSeguridad_urlCorporativo_SubDominio = new HttpCookie("_urlCorporativo_SubDominio", requestPG.SubDominio);
                    miCookieSeguridad_urlCorporativo_SubDominio.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlCorporativo_SubDominio);

                    HttpCookie miCookieSeguridad_CodigoPerfil = new HttpCookie("_CodigoPerfil_Business", requestPG.CodigoPerfil.ToString());
                    miCookieSeguridad_CodigoPerfil.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_CodigoPerfil);

                    return Json(Url.Action("checking", "gestionce", new { id = SubDominio }));
                    //  return View(requestPG);
                }


            }
            else
            {
                return Content("0");
            }
        }

        [HttpPost]
        public ActionResult ingresartiendaonline(string user, string password, int SelectedValue)
        {
            AspNetUsersViewModel oAspNetUsersViewModel = new AspNetUsersViewModel();
            oAspNetUsersViewModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAspNetUsersViewModel.CodigoSede = SelectedValue;
            oAspNetUsersViewModel.UserName = user;
            oAspNetUsersViewModel.PasswordHash = password;
            using (AspNetUsersRepository oItemsVentaRepository = new AspNetUsersRepository())
            {
                oAspNetUsersViewModel.LoginValidation = oItemsVentaRepository.ecommerce_AspNetUsers_ValidarUsuarioBusiness(oAspNetUsersViewModel);
            }
            if (oAspNetUsersViewModel.LoginValidation == 1)
            {
                HttpCookie miCookieSeguridad_Usuario = new HttpCookie("_Usuario_Business", oAspNetUsersViewModel.UserName);
                miCookieSeguridad_Usuario.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_Usuario);

                HttpCookie miCookieSeguridad_Sede = new HttpCookie("_CodigoSede_Business", oAspNetUsersViewModel.CodigoSede.ToString());
                miCookieSeguridad_Sede.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_Sede);

                FormsAuthentication.SetAuthCookie(oAspNetUsersViewModel.UserName, false);
                string SubDominio = Commun.SubDominio;
                return Json(Url.Action("index", "management", new { id = SubDominio }));
            }
            else
            {
                return Content("0");
            }
        }



    }
}