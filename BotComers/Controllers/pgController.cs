using BotComers.Filters;
using BotComers.Helpers;
using BotComers.Repository.CentroEntrenamiento;
using BotComers.Repository.Configuracion;
using BotComers.Repository.Corporativo;
using BotComers.Repository.Gimnasio;
using BotComers.ViewModels;
using BotComers.ViewModels.Configuracion;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BotComers.Controllers
{

    public class pgController : Controller
    {
        // GET: pg
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult inicio(string id)
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
                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaFit", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());
                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);

                        HttpCookie miCookieSeguridad_CodigoSede = new HttpCookie("_CodigoSede_PersonaFit", oEmpresaViewEditModel[0].CodigoSede.ToString());
                        miCookieSeguridad_CodigoSede.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_CodigoSede);

                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaFit", id);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                        {
                            CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                            requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                            requestPG.CodigoSede = Commun.CodigoSede_PersonaFit;
                            requestPG.UsuarioCreacion = "appsfit";
                            requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                            HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaFit", requestPG.ColorPrincipalPagina);
                            miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                            if (requestPG.logoPagina == string.Empty)
                            {
                                requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                            }

                            ViewBag.NombreComercial = requestPG.NombreComercial;

                            HttpCookie miCookieSeguridad_urlLogoTipo = new HttpCookie("_LogoTipo", requestPG.logoPagina);
                            miCookieSeguridad_urlLogoTipo.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlLogoTipo);

                            HttpCookie miCookieSeguridad_urlBannerReserva = new HttpCookie("_urlBannerReserva", requestPG.BannerReserva_FondoImagen);
                            miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                            HttpCookie miCookieSeguridad_urlBannerCentro = new HttpCookie("_urlBannerCentro", requestPG.BannerCentro_FondoImagen);
                            miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);

                            return View(requestPG);
                        }

                        // return View(oEmpresaViewEditModel);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }

        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult miperfil(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominio", "error");
            }
            else
            {
                using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                {
                    CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                    requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                    requestPG.CodigoSede = Commun.CodigoSede_PersonaFit;
                    requestPG.UsuarioCreacion = "appsfit";
                    requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                    HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaFit", requestPG.ColorPrincipalPagina);
                    miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                    if (requestPG.logoPagina == string.Empty)
                    {
                        requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                    }

                    ViewBag.NombreComercial = requestPG.NombreComercial;
                    ViewBag.logoPagina = requestPG.logoPagina;

                    HttpCookie miCookieSeguridad_urlLogoTipo = new HttpCookie("_LogoTipo", requestPG.logoPagina);
                    miCookieSeguridad_urlLogoTipo.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlLogoTipo);

                    HttpCookie miCookieSeguridad_urlBannerReserva = new HttpCookie("_urlBannerReserva", requestPG.BannerReserva_FondoImagen);
                    miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                    HttpCookie miCookieSeguridad_urlBannerCentro = new HttpCookie("_urlBannerCentro", requestPG.BannerCentro_FondoImagen);
                    miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);

                    return View(requestPG);
                }

            }

        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                int CodSede = Commun.CodigoSede_PersonaFit;
                string User = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo(CodigoUnidadNegocio, CodSede, User), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                int CodigoSede = Commun.CodigoSede_PersonaFit;
                string Correo = Commun.Usuario_PersonaFit;
                string User = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo(CodigoUnidadNegocio, CodigoSede, Correo, User), JsonRequestBehavior.AllowGet);
            }
        }


        [VerifyPersonaFitAuthenticationcs]
        public ActionResult reservaonline(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominio", "error");
            }
            else
            {
                using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                {
                    CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                    requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                    requestPG.CodigoSede = Commun.CodigoSede_PersonaFit;
                    requestPG.UsuarioCreacion = "appsfit";
                    requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                    HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaFit", requestPG.ColorPrincipalPagina);
                    miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                    HttpCookie miCookieSeguridad_ReservasNormativa = new HttpCookie("_ReservasNormativa_PersonaFit", requestPG.ReservasNormativa);
                    miCookieSeguridad_ReservasNormativa.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_ReservasNormativa);

                    HttpCookie miCookieSeguridad_ReservasNotas = new HttpCookie("_ReservasNotas_PersonaFit", requestPG.ReservasNotas);
                    miCookieSeguridad_ReservasNotas.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_ReservasNotas);

                    if (requestPG.logoPagina == string.Empty)
                    {
                        requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                    }

                    ViewBag.NombreComercial = requestPG.NombreComercial;
                    ViewBag.logoPagina = requestPG.logoPagina;

                    HttpCookie miCookieSeguridad_urlLogoTipo = new HttpCookie("_LogoTipo", requestPG.logoPagina);
                    miCookieSeguridad_urlLogoTipo.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlLogoTipo);

                    HttpCookie miCookieSeguridad_urlBannerReserva = new HttpCookie("_urlBannerReserva", requestPG.BannerReserva_FondoImagen);
                    miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                    HttpCookie miCookieSeguridad_urlBannerCentro = new HttpCookie("_urlBannerCentro", requestPG.BannerCentro_FondoImagen);
                    miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);

                    return View(requestPG);
                }

            }
        }


        [VerifyPersonaFitAuthenticationcs]
        public ActionResult uspListarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal(request), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(request), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(request), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(request), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio_grid()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                int CodigoSede = Commun.CodigoSede_PersonaFit;
                int CodigoSocio = Commun.CodigoSocio_PersonaFit;
                int CodigoMembresia = Commun.CodigoMembresia_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(CodigoUnidadNegocio, CodigoSede, CodigoSocio, CodigoMembresia), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion;
                request.CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal;
                request.CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias;
                request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(request), JsonRequestBehavior.AllowGet);
            }
        }


        //DESACTIVAR RESERVA
        [VerifyPersonaFitAuthenticationcs]
        public ActionResult UspRegistrarPresencial_HorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.CodigoMembresia = Commun.CodigoMembresia_PersonaFit;
                request.CodigoPaquete = Commun.CodigoPaquete_PersonaFit;
                request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                request.Correo = Commun.Usuario_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias(request), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias_grid(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion;
                request.CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal;
                request.CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias;
                request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(request), JsonRequestBehavior.AllowGet);
            }
        }


        [VerifyPersonaFitAuthenticationcs]
        public ActionResult GetMembresiaCodigo(int CodigoMembresia, int CodigoPaquete, int CodigoSocio)
        {
            HttpCookie miCookie_CodigoMembresia = new HttpCookie("_CodigoMembresia_PersonaFit", CodigoMembresia.ToString());
            miCookie_CodigoMembresia.Expires = DateTime.Now.AddDays(1);
            HttpContext.Response.SetCookie(miCookie_CodigoMembresia);

            HttpCookie miCookie_CodigoPaquete = new HttpCookie("_CodigoPaquete_PersonaFit", CodigoPaquete.ToString());
            miCookie_CodigoPaquete.Expires = DateTime.Now.AddDays(1);
            HttpContext.Response.SetCookie(miCookie_CodigoPaquete);

            HttpCookie miCookie_CodigoSocio = new HttpCookie("_CodigoSocio_PersonaFit", CodigoSocio.ToString());
            miCookie_CodigoSocio.Expires = DateTime.Now.AddDays(1);
            HttpContext.Response.SetCookie(miCookie_CodigoSocio);

            return Json(0, JsonRequestBehavior.AllowGet);

        }

        public ActionResult entrenaonline(string id)
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
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaFit", id);
                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaFit", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());

                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);

                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        return View(oEmpresaViewEditModel);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }
        }

        public ActionResult videoteca(string id)
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
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaFit", id);
                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaFit", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());

                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);

                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        return View(oEmpresaViewEditModel);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }
        }

        public ActionResult tienda(string id)
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
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaFit", id);
                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaFit", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());

                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);

                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        return View(oEmpresaViewEditModel);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }
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

                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaFit", id);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaFit", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());
                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);

                        HttpCookie miCookieSeguridad_CodigoSede = new HttpCookie("_CodigoSede_PersonaFit", oEmpresaViewEditModel[0].CodigoSede.ToString());
                        miCookieSeguridad_CodigoSede.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_CodigoSede);

                        using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                        {
                            CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                            requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                            requestPG.CodigoSede = Commun.CodigoSede_PersonaFit;
                            requestPG.UsuarioCreacion = "appsfit";
                            requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                            HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaFit", requestPG.ColorPrincipalPagina);
                            miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                            if (requestPG.logoPagina == string.Empty)
                            {
                                requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                            }

                            ViewBag.NombreComercial = requestPG.NombreComercial;
                            ViewBag.LogoCorporativo = requestPG.LogoCorporativo;

                            HttpCookie miCookieSeguridad_LogoTipo = new HttpCookie("_LogoTipo", requestPG.logoPagina);
                            miCookieSeguridad_LogoTipo.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_LogoTipo);

                            HttpCookie miCookieSeguridad_urlBannerReserva = new HttpCookie("_urlBannerReserva", requestPG.BannerReserva_FondoImagen);
                            miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                            HttpCookie miCookieSeguridad_urlBannerCentro = new HttpCookie("_urlBannerCentro", requestPG.BannerCentro_FondoImagen);
                            miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);

                            //return View(requestPG);
                        }

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

        public ActionResult ingresarPersonaFit(AspNetUsersViewModel request)
        {
            if (request.CodigoSede != 0 || request.UserName != string.Empty || request.PasswordHash != string.Empty)
            {
                AspNetUsersViewModel oAspNetUsersViewModel = new AspNetUsersViewModel();
                oAspNetUsersViewModel.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                oAspNetUsersViewModel.CodigoSede = request.CodigoSede;
                oAspNetUsersViewModel.UserName = request.UserName;
                oAspNetUsersViewModel.PasswordHash = request.PasswordHash;
                using (AspNetUsersRepository oItemsVentaRepository = new AspNetUsersRepository())
                {
                    oAspNetUsersViewModel = oItemsVentaRepository.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit(oAspNetUsersViewModel);
                }
                if (oAspNetUsersViewModel.LoginValidation == 1)
                {
                    HttpCookie miCookieSeguridad_Usuario = new HttpCookie("_Usuario_PersonaFit", oAspNetUsersViewModel.Id);
                    miCookieSeguridad_Usuario.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_Usuario);

                    HttpCookie miCookieSeguridad_CodigoSede = new HttpCookie("_CodigoSede_PersonaFit", request.CodigoSede.ToString());
                    miCookieSeguridad_CodigoSede.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.SetCookie(miCookieSeguridad_CodigoSede);

                    string SubDominio = Commun.SubDominio_PersonaFit;
                    FormsAuthentication.SetAuthCookie(oAspNetUsersViewModel.UserName, false);
                    //return Json(Url.Action("reservaonline", "pg", new { id = SubDominio }));
                    return Json(Url.Action("miperfil", "pg", new { id = SubDominio }));
                }
                else
                {
                    return Content(oAspNetUsersViewModel.LoginValidation.ToString());
                }
            }
            else
            {
                return Content("0");
            }
        }

        // [VerifyPersonaFitRegistrationcs]
        public ActionResult registrate(string id)
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
                        HttpCookie miCookieSeguridad_SubDominio = new HttpCookie("_SubDominio_PersonaFit", id);
                        miCookieSeguridad_SubDominio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_SubDominio);

                        HttpCookie miCookieSeguridad_UnidadNegocio = new HttpCookie("_CodigoUnidadNegocio_PersonaFit", oEmpresaViewEditModel[0].CodigoUnidadNegocio.ToString());
                        miCookieSeguridad_UnidadNegocio.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.SetCookie(miCookieSeguridad_UnidadNegocio);

                        using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                        {
                            CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                            requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                            requestPG.CodigoSede = Commun.CodigoSede_PersonaFit;
                            requestPG.UsuarioCreacion = "appsfit";
                            requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                            HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaFit", requestPG.ColorPrincipalPagina);
                            miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                            if (requestPG.logoPagina == string.Empty)
                            {
                                requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                            }

                            HttpCookie miCookieSeguridad_LogoTipo = new HttpCookie("_LogoTipo", requestPG.logoPagina);
                            miCookieSeguridad_LogoTipo.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_LogoTipo);

                            HttpCookie miCookieSeguridad_urlBannerReserva = new HttpCookie("_urlBannerReserva", requestPG.BannerReserva_FondoImagen);
                            miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                            HttpCookie miCookieSeguridad_urlBannerCentro = new HttpCookie("_urlBannerCentro", requestPG.BannerCentro_FondoImagen);
                            miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                            HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);

                            return View();
                        }
                    }
                    else
                    {
                        return RedirectToAction("ErrorPageNotDominio", "error");
                    }

                }

            }
        }

        [HttpPost]
        public ActionResult GuardarPersonaFit(AspNetUsersViewModel request)
        {
            using (AspNetUsersRepository oRepository = new AspNetUsersRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                request.CodigoCargo = 0;
                request.UserType = 1;
                request.UsuarioCreacion = "appsfit";
                //request.UsuarioCreacion = Commun.Usuario;
                int Validation = oRepository.ecommerce_uspRegistrar_AspNetUsers(request);
                if (Validation == 1)
                {
                    return Content("1");
                }
                else if (Validation == 0)
                {
                    HttpCookie _Usuario_PersonaFit = new HttpCookie("_Usuario_PersonaFit", request.Email.Trim());
                    HttpContext.Response.SetCookie(_Usuario_PersonaFit);
                    FormsAuthentication.SetAuthCookie(request.Email.Trim(), false);

                }
                return Content("0");
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public ActionResult CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oRquest = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                oRquest.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                oRquest.CodigoSede = Commun.CodigoSede_PersonaFit;
                oRquest.CodigoMembresia = Commun.CodigoMembresia_PersonaFit;
                oRquest.CodigoPaquete = Commun.CodigoPaquete_PersonaFit;
                oRquest.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                oRquest.UsuarioCreacion = Commun.Usuario_PersonaFit;
                oRquest.FechaHoraReserva = request.FechaHoraReserva;

                return Json(oRepository.CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(oRquest), JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyPersonaFitAuthenticationcs]
        public JsonResult uspListarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                request.CodigoSede = Commun.CodigoSede_PersonaFit;
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarSalaMaquinas_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }


        [VerifyPersonaFitAuthenticationcs]
        public ActionResult progreso(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorPageNotDominio", "error");
            }
            else
            {
                List<ControlMedidasDTO> lista = null;
                ControlMedidasDTO oControlMedidasDTO = new ControlMedidasDTO();
                oControlMedidasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                oControlMedidasDTO.CodigoSede = Commun.CodigoSede_PersonaFit;
                oControlMedidasDTO.CodigoCliente = Commun.CodigoSocio_PersonaFit;

                ReqFilterControlMedidasDTO oReq = new ReqFilterControlMedidasDTO()
                {
                    FilterCase = filterCaseControlMedidas.uspListarControlMedidas_Paginacion,
                    Item = oControlMedidasDTO,
                    User = Commun.Usuario_PersonaFit,
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = false,
                        PageRecords = 0,
                        PageNumber = Convert.ToUInt32(1)
                    }
                };

                RespListControlMedidasDTO oResp = null;

                using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
                {
                    oResp = oControlMedidasLogic.ControlMedidasGetList(oReq);
                }

                if (oResp.Success)
                {
                    lista = new List<ControlMedidasDTO>();
                    lista = oResp.List;
                }
                return View(lista);
                //  return Json(lista, JsonRequestBehavior.AllowGet);
                //using (CentroEntrenamiento_EditorPaginaWebRepository oRepositoryPG = new CentroEntrenamiento_EditorPaginaWebRepository())
                //{
                //    CentroEntrenamiento_EditorPaginaWebDTO requestPG = new CentroEntrenamiento_EditorPaginaWebDTO();
                //    requestPG.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio_PersonaFit;
                //    requestPG.CodigoSede = Commun.CodigoSede_PersonaFit;
                //    requestPG.UsuarioCreacion = "appsfit";
                //    requestPG = oRepositoryPG.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(requestPG);

                //    HttpCookie miCookieSeguridad_ColorEmpresa = new HttpCookie("_ColorEmpresa_PersonaFit", requestPG.ColorPrincipalPagina);
                //    miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                //    HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                //    if (requestPG.logoPagina == string.Empty)
                //    {
                //        requestPG.logoPagina = "/Content/appInicio/images/logogymdigital.png";
                //    }

                //    ViewBag.NombreComercial = requestPG.NombreComercial;
                //    ViewBag.logoPagina = requestPG.logoPagina;

                //    HttpCookie miCookieSeguridad_urlBannerReserva = new HttpCookie("_urlBannerReserva", requestPG.BannerReserva_FondoImagen);
                //    miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                //    HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                //    HttpCookie miCookieSeguridad_urlBannerCentro = new HttpCookie("_urlBannerCentro", requestPG.BannerCentro_FondoImagen);
                //    miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                //    HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);

                //    return View(requestPG);
                //}

            }

        }




    }
}