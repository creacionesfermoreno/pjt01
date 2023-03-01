using BotComers.Helpers;
using BotComers.Repository.CentroEntrenamiento;
using BotComers.Repository.Gimnasio;
using BotComers.Repository.Ingresos;
using BotComers.ViewModels;
using BotComers.ViewModels.Ingresos;
using E_BusinessLayer.CentroEntrenamiento;
//using E_DataModel;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using MimeKit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Operation = E_DataModel.Common.Operation;
using Path = System.IO.Path;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

using System.Web.Configuration;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Documents;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.pdf.qrcode;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System.Web.UI;
//using static NUnit.Framework.Constraints.Tolerance;
using Rectangle = iTextSharp.text.Rectangle;
using Document = iTextSharp.text.Document;
using PageSize = iTextSharp.text.PageSize;
using iTextSharp.text.pdf.draw;
using Paragraph = iTextSharp.text.Paragraph;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Font = iTextSharp.text.Font;
using System.Net.Mail;
using System.Web.WebPages;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text.html;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using MercadoPago.DataStructures.PaymentMethod;
using System.Web.Services.Description;
using System.Runtime.Remoting.Messaging;
using System.Globalization;
//using Quartz;
//using Quartz.Impl;
using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
using System.Threading;
//using Microsoft.Extensions.Logging;
using System.Collections.Specialized;

namespace BotComers.Controllers
{
    public class gestionceController : Controller
    {


        // GET: gestionce
        public ActionResult Index()
        {
            
            return View();
        }

        //MODULO CAJA REPOSITORY
        public ActionResult caja(string id)
        {
            return View();
        }

        #region Metodos CAJA REPOSITORY

        public ActionResult uspRegistrarAbrirCaja(int Codigo, decimal MontoApertura,
                                                  string Observacion, int TipoApertura, string Accion
                                              )


        {
            string mensaje = string.Empty;


            List<CajaAperturaCierreDTO> list = new List<CajaAperturaCierreDTO>();

            list.Add(new CajaAperturaCierreDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                Codigo = Codigo,
                MontoApertura = MontoApertura,
                MontoCierre = 0,
                Faltante = 0,
                Estado = 1,
                TotalMenbresias = 0,
                TotalProductos = 0,
                TotalLibres = 0,
                TotalEgresos = 0,
                TotalRopa = 0,
                TotalSuplementos = 0,
                TotalAccesorios = 0,
                SumaTotal = 0,
                TotalDeudas = 0,
                Total = 0,
                DineroDejadoCajaChica = 0,
                DineroRetirado = 0,
                DineroAjusteCaja = 0,
                Observacion = Observacion,
                TipoApertura = TipoApertura,
                Operation = Operation.uspRegistrarAbrirCaja,
            });

            ReqCajaAperturaCierreDTO oReq = new ReqCajaAperturaCierreDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespCajaAperturaCierreDTO oResp = null;
            using (CajaAperturaCierreLogic oCajaAperturaCierreLogic = new CajaAperturaCierreLogic())
            {
                oResp = oCajaAperturaCierreLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);

        }



        public ActionResult uspCerrarCaja(int Codigo,
                                          decimal MontoApertura, decimal MontoCierre, decimal Faltante,
                                          decimal TotalMenbresias, decimal TotalMenbresias_efectivo, decimal TotalMenbresias_debito, decimal TotalMenbresias_credito, decimal TotalMenbresias_deposito, decimal TotalMenbresias_web,
                                          decimal TotalProductos, decimal TotalProductos_efectivo, decimal TotalProductos_debito, decimal TotalProductos_credito, decimal TotalProductos_deposito, decimal TotalProductos_web,
                                          decimal TotalLibres, decimal TotalLibres_efectivo, decimal TotalLibres_debito, decimal TotalLibres_credito, decimal TotalLibres_deposito, decimal TotalLibres_web,
                                          decimal TotalRopa, decimal TotalRopa_efectivo, decimal TotalRopa_debito, decimal TotalRopa_credito, decimal TotalRopa_deposito, decimal TotalRopa_web,
                                          decimal TotalSuplementos, decimal TotalSuplementos_efectivo, decimal TotalSuplemento_debito, decimal TotalSuplementos_credito, decimal TotalSuplementos_deposito, decimal TotalSuplementos_web,
                                          decimal TotalAccesorio, decimal TotalAccesorio_efectivo, decimal TotalAccesorio_debito, decimal TotalAccesorio_credito, decimal TotalAccesorio_deposito, decimal TotalAccesorio_web,
                                          decimal TotalEgresos, decimal SumaTotal, decimal TotalDeudas, decimal Total,
                                          decimal DineroDejadoCajaChica, decimal DineroRetirado, decimal DineroAjusteCaja, string Accion)

        {

            string mensaje = string.Empty;

            List<CajaAperturaCierreDTO> list = new List<CajaAperturaCierreDTO>();
            list.Add(new CajaAperturaCierreDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Codigo = Codigo,
                MontoApertura = MontoApertura,
                MontoCierre = MontoCierre,
                Faltante = Faltante,

                TotalMenbresias = TotalMenbresias,
                TotalMenbresias_efectivo = TotalMenbresias_efectivo,
                TotalMenbresias_debito = TotalMenbresias_debito,
                TotalMenbresias_credito = TotalMenbresias_credito,
                TotalMenbresias_deposito = TotalMenbresias_deposito,
                TotalMenbresias_web = TotalMenbresias_web,

                TotalProductos = TotalProductos,
                TotalProductos_efectivo = TotalProductos_efectivo,
                TotalProductos_debito = TotalProductos_debito,
                TotalProductos_credito = TotalProductos_credito,
                TotalProductos_deposito = TotalProductos_deposito,
                TotalProductos_web = TotalProductos_web,

                TotalLibres = TotalLibres,
                TotalLibres_efectivo = TotalLibres_efectivo,
                TotalLibres_debito = TotalLibres_debito,
                TotalLibres_credito = TotalLibres_credito,
                TotalLibres_deposito = TotalLibres_deposito,
                TotalLibres_web = TotalLibres_web,

                TotalRopa = TotalRopa,
                TotalRopa_efectivo = TotalRopa_efectivo,
                TotalRopa_debito = TotalRopa_debito,
                TotalRopa_credito = TotalRopa_credito,
                TotalRopa_deposito = TotalRopa_deposito,
                TotalRopa_web = TotalRopa_web,

                TotalSuplementos = TotalSuplementos,
                TotalSuplementos_efectivo = TotalSuplementos_efectivo,
                TotalSuplemento_debito = TotalSuplemento_debito,
                TotalSuplementos_credito = TotalSuplementos_credito,
                TotalSuplementos_deposito = TotalSuplementos_deposito,
                TotalSuplementos_web = TotalSuplementos_web,

                TotalAccesorios = TotalAccesorio,
                TotalAccesorios_efectivo = TotalAccesorio_efectivo,
                TotalAccesorios_debito = TotalAccesorio_debito,
                TotalAccesorios_credito = TotalAccesorio_credito,
                TotalAccesorios_deposito = TotalAccesorio_deposito,
                TotalAccesorios_web = TotalAccesorio_web,

                TotalEgresos = TotalEgresos,
                SumaTotal = SumaTotal,
                TotalDeudas = TotalDeudas,
                Total = Total,
                DineroDejadoCajaChica = DineroDejadoCajaChica,
                DineroRetirado = DineroRetirado,
                DineroAjusteCaja = DineroAjusteCaja,
                Operation = Operation.UpdateAbrirCaja
            });

            ReqCajaAperturaCierreDTO oReq = new ReqCajaAperturaCierreDTO()
            {
                List = list,
                User = "Admin"
            };

            RespCajaAperturaCierreDTO oResp = null;
            using (CajaAperturaCierreLogic oCajaAperturaCierreLogic = new CajaAperturaCierreLogic())
            {
                oResp = oCajaAperturaCierreLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GuardarEgreso(int Codigo,
                                        string Responsable, string Descripcion,
                                        decimal MontoEgreso, string accion)

        {
            string mensaje = string.Empty;
            List<GastosDTO> list = new List<GastosDTO>();
            list.Add(new GastosDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                Codigo = Codigo,
                TipoEgreso = 100,
                Responsable = Responsable,
                Descripcion = Descripcion,
                MontoEgreso = MontoEgreso,
                NumeroRecibo = string.Empty,
                FechaCreacion = DateTime.Now,
                RUCProveedor = string.Empty,
                RZProveedor = string.Empty,
                SubTotal = 0,
                Igv = 0,
                OtrosTributos = 0,
                CodigoMedioPago = 1,
                NroOperacion = string.Empty,
                Observaciones = string.Empty,

                Operation = accion == "N" ? Operation.Create : Operation.Update
            });
            ReqGastosDTO oReq = new ReqGastosDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespGastosDTO oResp = null;
            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DetalleEgresosCaja(int CodigoAbrirCaja)
        {
            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = Commun.CodigoSede;
            oGastosDTO.UsuarioCreacion = Commun.Usuario;
            oGastosDTO.CodigoAbrirCaja = CodigoAbrirCaja;

            List<GastosDTO> lista = null;

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.ListarDetalleEgresosCaja,
                User = Commun.Usuario,
                Item = oGastosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SEGListarUsuarioResponsable_Gastos(string filtro)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;

            oUsuarioDTO.filtro = filtro;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuarioResponsableSuplementos,
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarDineroACaja(int CodigoIAc, int CodigoAbrirCaja, string Descripcion, decimal CantidadDinero)
        {
            string mensaje = string.Empty;
            List<IngresoAjustesCajaDTO> list = new List<IngresoAjustesCajaDTO>();

            list.Add(new IngresoAjustesCajaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoIAc = CodigoIAc,
                CodigoAbrirCaja = CodigoAbrirCaja,
                Descripcion = Descripcion,
                Cantidad = CantidadDinero,
                Operation = Operation.Create,

            });
            ReqIngresoAjustesCajaDTO oReq = new ReqIngresoAjustesCajaDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespIngresoAjustesCajaDTO oResp = null;
            using (IngresoAjustesCajaLogic oIngresoAjustesCajaLogic = new IngresoAjustesCajaLogic())
            {
                oResp = oIngresoAjustesCajaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetalleAjustesIngresoCaja(int CodigoAbrirCaja)
        {
            IngresoAjustesCajaDTO oIngresoAjustesCajaDTO = new IngresoAjustesCajaDTO();
            oIngresoAjustesCajaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oIngresoAjustesCajaDTO.CodigoSede = Commun.CodigoSede;
            oIngresoAjustesCajaDTO.CodigoAbrirCaja = CodigoAbrirCaja;

            List<IngresoAjustesCajaDTO> lista = null;

            ReqFilterIngresoAjustesCajaDTO oReq = new ReqFilterIngresoAjustesCajaDTO()
            {
                FilterCase = filterCaseIngresoAjustesCaja.ListarDetalleAjustesIngresoCaja,
                User = "Admin",
                Item = oIngresoAjustesCajaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListIngresoAjustesCajaDTO oResp = null;

            using (IngresoAjustesCajaLogic oIngresoAjustesCajaLogic = new IngresoAjustesCajaLogic())
            {
                oResp = oIngresoAjustesCajaLogic.IngresoAjustesCajaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspInformacionGeneralAbrirCaja()
        {
            CajaAperturaCierreDTO oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
            oCajaAperturaCierreDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oCajaAperturaCierreDTO.CodigoSede = Commun.CodigoSede;
            oCajaAperturaCierreDTO.UsuarioCreacion = Commun.Usuario;

            ReqFilterCajaAperturaCierreDTO oReq = new ReqFilterCajaAperturaCierreDTO()
            {
                FilterCase = filterCaseCajaAperturaCierre.uspInformacionGeneralAbrirCaja,
                Item = oCajaAperturaCierreDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemCajaAperturaCierreDTO oResp = null;
            using (CajaAperturaCierreLogic oCajaAperturaCierreLogic = new CajaAperturaCierreLogic())
            {
                oResp = oCajaAperturaCierreLogic.CajaAperturaCierreGetItem(oReq);
            }

            if (oResp.Success)
            {
                oCajaAperturaCierreDTO = oResp.Item;
            }
            return Json(oCajaAperturaCierreDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspInformacionGeneralAbrirCaja_otrasformaspago()
        {
            CajaAperturaCierreDTO oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
            oCajaAperturaCierreDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oCajaAperturaCierreDTO.CodigoSede = Commun.CodigoSede;
            oCajaAperturaCierreDTO.UsuarioCreacion = Commun.Usuario;

            ReqFilterCajaAperturaCierreDTO oReq = new ReqFilterCajaAperturaCierreDTO()
            {
                FilterCase = filterCaseCajaAperturaCierre.uspInformacionGeneralAbrirCaja_otrasformaspago,
                Item = oCajaAperturaCierreDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemCajaAperturaCierreDTO oResp = null;
            using (CajaAperturaCierreLogic oCajaAperturaCierreLogic = new CajaAperturaCierreLogic())
            {
                oResp = oCajaAperturaCierreLogic.CajaAperturaCierreGetItem(oReq);
            }

            if (oResp.Success)
            {
                oCajaAperturaCierreDTO = oResp.Item;
            }
            return Json(oCajaAperturaCierreDTO, JsonRequestBehavior.AllowGet);
        }

        //no se llama
        public ActionResult ListarCounters()
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.ListarCounters(CodigoUnidadNegocio, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarAsesoresComerciales()
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.ListarAsesoresComerciales(CodigoUnidadNegocio, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarTurnos()
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.ListarTurnos(CodigoUnidadNegocio, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarTipoIngreso()
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.ListarTipoIngreso(CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListaTiempoMembresia(string nombre)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                return Json(oRepository.ListaTiempoMembresia(nombre), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspTotalPagosVentasRangoFechas_Appsfit(string Fecha, string FechaFin, string Vendedor, int Turno,
                                                 int CodTiempoPaquete, string AsesorComercial, string TipoIngreso, int Tipo)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspTotalPagosVentasRangoFechas_Appsfit(CodigoUnidadNegocio, CodSede, Fecha, FechaFin, Vendedor, Turno,
                                                 CodTiempoPaquete, AsesorComercial, TipoIngreso, Tipo), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspCentroEntrenamiento_uspConsumoTotalPorCliente(int CodigoSocio, string DNI)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.CentroEntrenamiento_uspConsumoTotalPorCliente(CodigoUnidadNegocio, CodSede, CodigoSocio, DNI), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspCentroEntrenamiento_uspConsumoDetalladoPorCliente(int CodigoSocio, string DNI)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.CentroEntrenamiento_uspConsumoDetalladoPorCliente(CodigoUnidadNegocio, CodSede, CodigoSocio, DNI), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspTotalVentasTurnos_RangoFechas_Appsfit(string Fecha, string FechaFin, string Vendedor,
                                               int CodTiempoPaquete, string AsesorComercial, string TipoIngreso)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspTotalVentasTurnos_RangoFechas_Appsfit(CodigoUnidadNegocio, CodSede, Fecha, FechaFin, Vendedor,
                                                 CodTiempoPaquete, AsesorComercial, TipoIngreso), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TotalPagosVentas(string Fecha, string FechaFin, string Vendedor, int Turno,
                                                 int CodTiempoPaquete, string AsesorComercial, string TipoIngreso, int Tipo)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.TotalPagosVentas(CodigoUnidadNegocio, CodSede, Fecha, FechaFin, Vendedor, Turno,
                                                 CodTiempoPaquete, AsesorComercial, TipoIngreso, Tipo), JsonRequestBehavior.AllowGet);
            }
        }

        //no se llama en caja
        public ActionResult TotalPagosVentasCafeteria(string Fecha, string FechaFin, string Vendedor, int Turno,
                                                int CodTiempoPaquete, string AsesorComercial, string TipoIngreso, int Tipo)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.TotalPagosVentasCafeteria(CodigoUnidadNegocio, CodSede, Fecha, FechaFin, Vendedor, Turno,
                                                CodTiempoPaquete, AsesorComercial, TipoIngreso, Tipo), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion(VentasDetalleDTO oItem, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                oItem.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oItem.CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion(oItem, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult uspReporteVentasMembresiasRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasMembresiasRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasMembresiasRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasMembresiasRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int TipoIngresoMembresia, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, AsesorComercial, CodSede, TipoIngresoMembresia, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta llamando
        public ActionResult uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int TipoIngresoMembresia, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, AsesorComercial, CodSede, TipoIngresoMembresia, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta llamando
        public ActionResult uspReporteVentasProductosRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasProductosRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta llamando
        public ActionResult uspReporteVentasProductosRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasProductosRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta usando
        public ActionResult uspReporteVentasCafeteriaRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasCafeteriaRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta usando
        public ActionResult uspReporteVentasCafeteriaRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasCafeteriaRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasServiciosRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasServiciosRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasServiciosRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasServiciosRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta usando
        public ActionResult uspReporteVentasLibresRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasLibresRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }
        //no se esta usando
        public ActionResult uspReporteVentasLibresRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasLibresRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteEgresoRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int TipoEgreso, int TipoDocumento, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteEgresoRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, TipoEgreso, TipoDocumento, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteEgresoRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int TipoEgreso, int TipoDocumento)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteEgresoRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, TipoEgreso, TipoDocumento), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteEgresoRangoFechas_GastoCaja_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int TipoEgreso, int TipoDocumento, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteEgresoRangoFechas_GastosCaja_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, TipoEgreso, TipoDocumento, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteEgresoRangoFechas_GastoCaja_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int TipoEgreso, int TipoDocumento)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteEgresoRangoFechas_GastosCaja_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, TipoEgreso, TipoDocumento), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasNutricionRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasNutricionRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasNutricionRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasNutricionRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasPersonalizadoRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasPersonalizadoRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, Vendedor, CodSede, Turno, FormaPago, TipoIngresoMembresia, TipoCliente, Counter, AsesorComercial, CodigoTiempoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int TipoIngresoMembresia, int CodigoTiempoPaquete, int Tipo)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros(CodigoUnidadNegocio, FechaInicio, FechaFin, AsesorComercial, CodSede, TipoIngresoMembresia, CodigoTiempoPaquete, Tipo), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int TipoIngresoMembresia, int CodigoTiempoPaquete, int Tipo, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion(CodigoUnidadNegocio, FechaInicio, FechaFin, AsesorComercial, CodSede, TipoIngresoMembresia, CodigoTiempoPaquete, Tipo, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        // TIPO 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
        public ActionResult CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, string Counter, int FormaPago, int Turno, int Tipo, int PageNumber)
        {
            if (Counter == "Todos Counter")
            {
                Counter = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            using (ComprobanteDetalleRepository oRepository = new ComprobanteDetalleRepository())
            {
                E_DataModel.ComprobanteDetalleDTO request = new E_DataModel.ComprobanteDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.request_FechaInicio = FechaInicio;
                request.request_Fin = FechaFin;
                request.request_Vendedor = AsesorComercial;
                request.request_Counter = Counter;
                request.request_FormaPago = FormaPago;
                request.request_Tipo = Tipo;
                request.request_Turno = Turno;

                request.PageNumber = PageNumber;
                return Json(oRepository.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, string Counter, int FormaPago, int Turno, int Tipo)
        {
            if (Counter == "Todos Counter")
            {
                Counter = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            using (ComprobanteDetalleRepository oRepository = new ComprobanteDetalleRepository())
            {
                E_DataModel.ComprobanteDetalleDTO request = new E_DataModel.ComprobanteDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.request_FechaInicio = FechaInicio;
                request.request_Fin = FechaFin;
                request.request_Vendedor = AsesorComercial;
                request.request_Counter = Counter;
                request.request_FormaPago = FormaPago;

                request.request_Tipo = Tipo;
                request.request_Turno = Turno;
                return Json(oRepository.CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspTotalPagosProductosRangoFechas(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, string Counter, int Tipo, int Turno)
        {

            using (ComprobantePagoRepository oRepository = new ComprobantePagoRepository())
            {
                E_DataModel.ComprobantePagoDTO request = new E_DataModel.ComprobantePagoDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.request_FechaInicio = FechaInicio;
                request.request_Fin = FechaFin;
                request.request_Tipo = Tipo;
                request.request_Turno = Turno;

                if (Counter == "Vendedores")
                {
                    request.request_Counter = string.Empty;
                }
                else
                {
                    request.request_Counter = Counter;
                }

                if (AsesorComercial == "Vendedores")
                {
                    request.request_Vendedor = string.Empty;
                }
                else
                {
                    request.request_Vendedor = AsesorComercial;
                }

                return Json(oRepository.CentroEntrenamiento_uspTotalPagosProductosRangoFechas(request), JsonRequestBehavior.AllowGet);
            }
        }

        //FIN
        public ActionResult uspReporteVentasSuplementosTotalesRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasSuplementosTotalesRangoFechas_Paginacion(CodigoUnidadNegocio, CodSede, FechaInicio, FechaFin, Vendedor, Tipo, Turno, FormaPago, Counter, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros(CodigoUnidadNegocio, CodSede, FechaInicio, FechaFin, Vendedor, Tipo, Turno, FormaPago, Counter), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspTotalPagosSuplementosRangoFechas(string Fecha, string FechaFin, string Vendedor, int Tipo, int Turno, string Counter)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspTotalPagosSuplementosRangoFechas(CodigoUnidadNegocio, CodSede, Fecha, FechaFin, Vendedor, Tipo, Turno, Counter), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion(CodigoUnidadNegocio, CodSede, FechaInicio, FechaFin, AsesorComercial, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros(CodigoUnidadNegocio, CodSede, FechaInicio, FechaFin, AsesorComercial), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasRopasTotalesRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasRopasTotalesRangoFechas_Paginacion(CodigoUnidadNegocio, CodigoSede, FechaInicio, FechaFin, Vendedor, Tipo, Turno, FormaPago, Counter, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros(CodigoUnidadNegocio, CodigoSede, FechaInicio, FechaFin, Vendedor, Tipo, Turno, FormaPago, Counter), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspTotalPagosRopasRangoFechas(string Fecha, string FechaFin, string Vendedor, int Tipo, int Turno, string Counter)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspTotalPagosRopasRangoFechas(CodigoUnidadNegocio, CodSede, Fecha, FechaFin, Vendedor, Tipo, Turno, Counter), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion(CodigoUnidadNegocio, CodigoSede, FechaInicio, FechaFin, AsesorComercial, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros(CodigoUnidadNegocio, CodigoSede, FechaInicio, FechaFin, AsesorComercial), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarAperturaCaja_Paginacion(DateTime FechaCreacion, DateTime Fecha, string UsuarioCreacion, int PageNumber)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspListarAperturaCaja_Paginacion(CodigoUnidadNegocio, CodigoSede, UsuarioCreacion, FechaCreacion, Fecha, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarAperturaCaja_NumeroRegistros(DateTime FechaCreacion, DateTime Fecha, string UsuarioCreacion)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;

                return Json(oRepository.uspListarAperturaCaja_NumeroRegistros(CodigoUnidadNegocio, CodigoSede, UsuarioCreacion, FechaCreacion, Fecha), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspListarComprobante_Paginacion(DateTime b_FechaEmisionInicio, DateTime b_FechaEmisionFin, int PageNumber)
        {
            List<ComprobanteViewModel> lista = new List<ComprobanteViewModel>();

            E_DataModel.ReqFilterComprobanteDTO oReq = new E_DataModel.ReqFilterComprobanteDTO()
            {
                Item = new E_DataModel.ComprobanteDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoEstadoEntrega = null,
                    CodigoComprobante = null,
                    CodigoCliente = null,
                    b_FechaEmisionInicio = b_FechaEmisionInicio,
                    b_FechaEmisionFin = b_FechaEmisionFin,
                    Estado = null
                },
                FilterCase = filterCaseComprobante.ecommerce_uspListarComprobante,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            E_DataModel.RespListComprobanteDTO oResp = null;

            using (E_BusinessLayer.ComprobanteLogic oComprobanteLogic = new E_BusinessLayer.ComprobanteLogic())
            {
                oResp = oComprobanteLogic.ComprobanteGetList(oReq);
            }

            //if (oResp.Success)
            //{

            //foreach (E_DataModel.ComprobanteDTO item in oResp.List)
            //{
            //    lista.Add(new ComprobanteViewModel()
            //    {
            //        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
            //        CodigoSede = item.CodigoSede,
            //        CodigoComprobante = item.CodigoComprobante,
            //        CodigoTipoComprobante = item.CodigoTipoComprobante,
            //        TipoMoneda = item.TipoMoneda,
            //        CodigoAlmacen = item.CodigoAlmacen,
            //        Correlativo = item.Correlativo,
            //        CodigoCliente = item.CodigoCliente,
            //        NombresCliente = item.NombresCliente,
            //        Celular = item.Celular,
            //        CodigoVendedor = item.CodigoVendedor,
            //        FechaEmision = item.FechaEmision,
            //        DesFechaEmision = item.FechaEmision.ToString("dd/MM/yyyy"),
            //        CodigoPlazoPago = item.CodigoPlazoPago,
            //        FechaVencimiento = item.FechaVencimiento,
            //        FechaEmision_Texto = item.FechaEmision_Texto,
            //        DesFechaVencimiento = item.FechaVencimiento.ToString("dd/MM/yyyy"),
            //        ColorFechaVencimiento = item.ColorFechaVencimiento,
            //        TerminosCondiciones = item.TerminosCondiciones,
            //        Notas = item.Notas,
            //        Comentarios = item.Comentarios,
            //        SubTotal = item.SubTotal,
            //        Descuento = item.Descuento,
            //        SubTotal2 = item.SubTotal2,
            //        IGV = item.IGV,
            //        Total = item.Total,
            //        TotalCobrado = item.TotalCobrado,
            //        TotalPorCobrar = item.TotalPorCobrar,
            //        Estado = item.Estado,
            //        DesEstado = item.DesEstado,
            //        ColorEstado = item.ColorEstado,
            //        DesEstadoEntrega = item.DesEstadoEntrega,
            //        UsuarioCreacion = item.UsuarioCreacion,
            //        UrlPDF = item.UrlPDF
            //    });
            //}
            //}

            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        #endregion  

        #region Metodos CHECKING
        //MODULO checking
        public ActionResult checking(string id)
        {
            return View();
        }

        public ActionResult GuardarFotoCarnetVacunacionCliente(ClienteViewModel request)
        {
            var file2 = request.ImageFile;
            string ruta = string.Empty;
            if (file2 != null)
            {
                var fileName = Path.GetFileName(file2.FileName);
                var extention = Path.GetExtension(file2.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.SubDominio + "_" + request.CodigoSocio + extention), "carnetvacunacion");

                List<ClientesDTO> list = new List<ClientesDTO>();

                list.Add(new ClientesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoSocio = request.CodigoSocio,
                    ImagenUrl = ruta,
                    Operation = Operation.UpdateFotoCarnetVacunacion
                });

                ReqClientesDTO oReq = new ReqClientesDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespClientesDTO oResp = null;
                using (ClientesLogic oProductoLogic = new ClientesLogic())
                {
                    oResp = oProductoLogic.ExecuteTransac(oReq);
                }

                if (oResp.Success)
                {

                }
            }

            return Json(ruta, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarFotoCliente(ClienteViewModel request)
        {

            var file2 = request.ImageFile;
            string ruta = string.Empty;
            if (file2 != null)
            {
                var fileName = Path.GetFileName(file2.FileName);
                var extention = Path.GetExtension(file2.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.SubDominio + "_" + request.CodigoSocio + extention), "socios");

                List<ClientesDTO> list = new List<ClientesDTO>();

                list.Add(new ClientesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoSocio = request.CodigoSocio,
                    ImagenUrl = ruta,
                    Operation = Operation.UpdateFoto
                });

                ReqClientesDTO oReq = new ReqClientesDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespClientesDTO oResp = null;
                using (ClientesLogic oProductoLogic = new ClientesLogic())
                {
                    oResp = oProductoLogic.ExecuteTransac(oReq);
                }

                if (oResp.Success)
                {

                }
            }

            return Json(ruta, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GuardarFotoCliente_WebCam(ClienteViewModel request)
        {
            //string ImageFile_texto,int CodigoSocio, string SubDominio
            var file2 = request.ImageFile_texto;
            string ruta = string.Empty;
            if (file2 != null && file2 != string.Empty)
            {

                byte[] data = Convert.FromBase64String(file2);

                //var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                //var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { "nombre", "image/png", stream });
                var obj = ConstructHttpPostedFile(data, "nombre", "image/png");
                ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.SubDominio + "_" + request.CodigoSocio + ".png"), "socios");

                List<ClientesDTO> list = new List<ClientesDTO>();

                list.Add(new ClientesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoSocio = request.CodigoSocio,
                    ImagenUrl = ruta,
                    Operation = Operation.UpdateFoto
                });

                ReqClientesDTO oReq = new ReqClientesDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespClientesDTO oResp = null;
                using (ClientesLogic oProductoLogic = new ClientesLogic())
                {
                    oResp = oProductoLogic.ExecuteTransac(oReq);
                }

                if (oResp.Success)
                {

                }
            }

            return Json(ruta, JsonRequestBehavior.AllowGet);
        }

        public HttpPostedFile ConstructHttpPostedFile(byte[] data, string filename, string contentType)
        {
            // Get the System.Web assembly reference
            Assembly systemWebAssembly = typeof(HttpPostedFileBase).Assembly;
            // Get the types of the two internal types we need
            Type typeHttpRawUploadedContent = systemWebAssembly.GetType("System.Web.HttpRawUploadedContent");
            Type typeHttpInputStream = systemWebAssembly.GetType("System.Web.HttpInputStream");

            // Prepare the signatures of the constructors we want.
            Type[] uploadedParams = { typeof(int), typeof(int) };
            Type[] streamParams = { typeHttpRawUploadedContent, typeof(int), typeof(int) };
            Type[] parameters = { typeof(string), typeof(string), typeHttpInputStream };

            // Create an HttpRawUploadedContent instance
            object uploadedContent = typeHttpRawUploadedContent
              .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, uploadedParams, null)
              .Invoke(new object[] { data.Length, data.Length });

            // Call the AddBytes method
            typeHttpRawUploadedContent
              .GetMethod("AddBytes", BindingFlags.NonPublic | BindingFlags.Instance)
              .Invoke(uploadedContent, new object[] { data, 0, data.Length });

            // This is necessary if you will be using the returned content (ie to Save)
            typeHttpRawUploadedContent
              .GetMethod("DoneAddingBytes", BindingFlags.NonPublic | BindingFlags.Instance)
              .Invoke(uploadedContent, null);

            // Create an HttpInputStream instance
            object stream = (Stream)typeHttpInputStream
              .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, streamParams, null)
              .Invoke(new object[] { uploadedContent, 0, data.Length });

            // Create an HttpPostedFile instance
            HttpPostedFile postedFile = (HttpPostedFile)typeof(HttpPostedFile)
              .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, null)
              .Invoke(new object[] { filename, contentType, stream });

            return postedFile;
        }


        public ActionResult CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoSocio = request.CodigoSocio;
                return Json(oRepository.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oRquest = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                oRquest.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oRquest.CodigoSede = Commun.CodigoSede;
                oRquest.CodigoMembresia = request.CodigoMembresia;
                oRquest.CodigoPaquete = request.CodigoPaquete;
                oRquest.CodigoSocio = request.CodigoSocio;
                oRquest.UsuarioCreacion = Commun.Usuario;
                oRquest.FechaHoraReserva = request.FechaHoraReserva;

                return Json(oRepository.CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(oRquest), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository oRepository = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UspRegistrarPresencial_HorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion;
                request.CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal;
                request.CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias;
                //request.CodigoSocio = Commun.CodigoSocio_PersonaFit;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                //request.CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion;
                //request.CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal;
                //request.CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias;                              
                return Json(oRepository.CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspActualizarMenbresiasFechaInicio(int CodigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.uspActualizarMenbresiasFechaInicio(CodigoUnidadNegocio, CodSede, CodigoMenbresia, UsuarioCreacion), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarVentaDiario(
            int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                         string RUC_DNI, string Direccion, DateTime FechaVenta,
                                         int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                         string NroTarjeta, int TipoMoneda, int FormaPago,
                                         decimal SubTotal, decimal IGV, decimal TotalNeto,
                                         decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                         string Vendedor, decimal TotalDolares, decimal TotalAporte, int SubFormaPago, string tk, string latitud, string longitud

            )
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.GuardarVentaDiario(
                     CodigoUnidadNegocio, codigoSalida, CodigoSocio, RazonSocial_Sr,
                                          RUC_DNI, Direccion, FechaVenta,
                                          CodigoTipoComprobante, CodigoSubTipoComprobante, NroComprobante,
                                          NroTarjeta, TipoMoneda, FormaPago,
                                          SubTotal, IGV, TotalNeto,
                                          tipoCambio, listaDetalle, listaFormaPago,
                                          Vendedor, CodSede, TotalDolares, TotalAporte, SubFormaPago, tk, latitud, longitud, UsuarioCreacion
                    ), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarSuplementosVentasPorCategoria(int CodigoCategoria)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarSuplementosVentasPorCategoria(CodigoUnidadNegocio, CodSede, CodigoCategoria), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarProductoPorCategoriaVenta(int CodigoCategoria)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarProductoPorCategoriaVenta(CodigoUnidadNegocio, CodSede, CodigoCategoria), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarRopasVentas()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarRopasVentas(CodigoUnidadNegocio, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarProductoBuscadorPorNombre(int CodigoSocio, string Descripcion)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarProductoBuscadorPorNombre(CodigoUnidadNegocio, CodSede, CodigoSocio, Descripcion), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarFiadosSuplementos(
                                        int codigoSalida, int CodigoSocio,
                                           DateTime FechaVenta, string listaDetalle, string listaFormaPago, string Vendedor,
                                           string tk, string latitud, string longitud
            )
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.GuardarFiadosSuplementos(
                     CodigoUnidadNegocio, codigoSalida, CodigoSocio,
                                            FechaVenta, listaDetalle, listaFormaPago, Vendedor, CodSede,
                                            tk, latitud, longitud, UsuarioCreacion
                    ), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarVentaSuplementos(
            int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
            string RUC_DNI, string Direccion, DateTime FechaVenta,
            int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
            string NroTarjeta, int TipoMoneda, int FormaPago,
            decimal SubTotal, decimal IGV, decimal TotalNeto,
            decimal tipoCambio, string listaDetalle, string listaFormaPago,
            string Vendedor, decimal TotalDolares, decimal TotalAporte, int SubFormaPago, string tk, string latitud, string longitud
            )
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.GuardarVentaSuplementos(
                     CodigoUnidadNegocio, codigoSalida, CodigoSocio, RazonSocial_Sr,
             RUC_DNI, Direccion, FechaVenta,
             CodigoTipoComprobante, CodigoSubTipoComprobante, NroComprobante,
             NroTarjeta, TipoMoneda, FormaPago,
             SubTotal, IGV, TotalNeto,
             tipoCambio, listaDetalle, listaFormaPago,
             Vendedor, CodSede, TotalDolares, TotalAporte, SubFormaPago, tk, latitud, longitud, UsuarioCreacion
                    ), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SEGListarUsuarioResponsableSuplementos(string filtro)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.SEGListarUsuarioResponsableSuplementos(CodigoUnidadNegocio, CodSede, filtro), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDiarios()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarDiarios(CodigoUnidadNegocio, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListaSocios(string valor, int flag)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.ListaSocios(valor, flag, CodSede, CodigoUnidadNegocio), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarPerfilMenu()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                string Perfil = Commun.Perfil;

                return Json(oRepository.ListarPerfilMenu(CodigoUnidadNegocio, Perfil, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarAsistencia(int CodigoPersona, string TipoPersona, int CodigoPaquete, int CodigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.GuardarAsistencia(CodigoPersona, TipoPersona, CodigoPaquete, CodigoMenbresia, User, CodSede, CodigoUnidadNegocio), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ValidarBuscarDiasHorarioPaquete(int CodigoPaquete)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;

                return Json(oRepository.ValidarBuscarDiasHorarioPaquete(CodigoUnidadNegocio, CodigoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ActualizarNroIngresoMembresia(int codigoMembresia, int codigoSocio)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int codigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ActualizarNroIngresoMembresia(codigoMembresia, codigoSede, codigoSocio, CodigoUnidadNegocio, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ValidarIngresoDiaPaquete(int CodigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ValidarIngresoDiaPaquete(CodigoUnidadNegocio, CodigoSede, CodigoMenbresia, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarInformacionSociosPorCodigo(int codigo)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.BuscarInformacionSociosPorCodigo(CodigoUnidadNegocio, CodigoSede, codigo, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarInformacionSociosPorCodigoFiltro(string Filtro)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.BuscarInformacionSociosPorCodigoFiltro(CodigoUnidadNegocio, CodigoSede, Filtro, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarHistorialPagos(int codMembresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarHistorialPagos(CodigoUnidadNegocio, CodigoSede, codMembresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult listardllAsesoresVentas()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.listardllAsesoresVentas(CodigoUnidadNegocio, CodigoSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarAsistencia(string CodigoHorarioClasesConfiguracionAsistencias, int CodigoAsistencia, int CodigoMenbresia, string tk, string latitud, string longitud)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.EliminarAsistencia(CodigoHorarioClasesConfiguracionAsistencias, CodigoUnidadNegocio, CodigoAsistencia, CodigoMenbresia, CodSede, User, tk, latitud, longitud), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarAsistenciaEfectiva(int CodigoMenbresia, int CodigoSocio)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.BuscarAsistenciaEfectiva(CodigoMenbresia, CodigoSocio, User, CodSede, CodigoUnidadNegocio), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarMembresias(int codSocio)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarMembresias(CodigoUnidadNegocio, CodSede, codSocio), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(int codSocio, int CodigoMembresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                return Json(oRepository.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(CodigoUnidadNegocio, CodSede, codSocio, CodigoMembresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarMensajesMenbresia(int codigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarMensajesMenbresia(CodigoUnidadNegocio, codigoMenbresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarPedidosDelSocio(int codSocio)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarPedidosDelSocio(CodigoUnidadNegocio, CodigoSede, codSocio), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDetalleAsistenciaSocio_Paginacion(int CodigoMembresia, int PageNumber)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspListarDetalleAsistenciaSocio_Paginacion(CodigoUnidadNegocio, CodigoSede, CodigoMembresia, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDetalleAsistenciaSocio_NumeroRegistros(int CodigoMembresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspListarDetalleAsistenciaSocio_NumeroRegistros(CodigoUnidadNegocio, CodigoSede, CodigoMembresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDetalleAsistenciaSocio_Paginacion_TODOS(int CodigoMembresia, int PageNumber)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspListarDetalleAsistenciaSocio_Paginacion_TODOS(CodigoUnidadNegocio, CodigoSede, CodigoMembresia, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarDetalleAsistenciaSocio_NumeroRegistros_TODOS(int CodigoMembresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspListarDetalleAsistenciaSocio_NumeroRegistros_TODOS(CodigoUnidadNegocio, CodigoSede, CodigoMembresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarFreezing(int Codigo, int CodigoMenbresia, int CodigoSocio)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.EliminarFreezing(CodigoUnidadNegocio, Codigo, CodigoMenbresia, CodigoSocio, User, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarClientesMenbresiasCuotas(int CodigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspListarClientesMenbresiasCuotas(CodigoUnidadNegocio, CodigoSede, CodigoMenbresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarMensaje(int CodigoSocio, int codigoMembresia, string Mensaje, string tk, string latitud, string longitud)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.GuardarMensaje(CodigoUnidadNegocio, CodigoSocio, codigoMembresia, Mensaje, User, tk, latitud, longitud, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarAsesorVentas()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarAsesorVentas(CodigoUnidadNegocio, CodigoSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerInformacionFin(int codigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ObtenerInformacionFin(CodigoUnidadNegocio, CodigoSede, codigoMenbresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult VerInformacionMenbresias(int codigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.VerInformacionMenbresias(CodigoUnidadNegocio, CodigoSede, codigoMenbresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarConfiguracion()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.BuscarConfiguracion(CodigoUnidadNegocio, CodigoSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarDNIporRENIEC(string Number)//int id
        {
            try
            {
                string Type = "dni";
                //APIS.NET.PE
                string ApiToken = "apis-token-1979.daqZyoZDfaATqkB8NyJVPyKOig5WwNXd";
                string ApiUrl = "http://api.apis.net.pe/";
                var client = new RestClient(ApiUrl);
                var restRequest = new RestRequest("v1/" + Type + "?numero=" + Number, Method.GET);
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.AddParameter("Authorization", String.Format("Bearer " + ApiToken), ParameterType.HttpHeader);
                var restResponse = client.Execute(restRequest);

                if (restResponse.StatusDescription == "Not Found")
                {
                    return Json(restResponse.StatusDescription, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
                    oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                    oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
                    oConfiguracionDTO.UsuarioCreacion = Commun.Usuario;
                    oConfiguracionDTO.ConsultasNumeroDocumento_ConsultaNroDocumento = Number;
                    oConfiguracionDTO.Operation = Operation.uspRegistrarConfiguracionConsultaDocumentoPersonas_Log;


                    List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
                    lista.Add(oConfiguracionDTO);

                    ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
                    {
                        List = lista,
                        User = Commun.Usuario
                    };
                    RespConfiguracionDTO oResp = null;

                    ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
                    oResp = oConfiguracionLogic.ExecuteTransac(oReq);

                    if (oResp.Success)
                    {
                        return Json(restResponse.Content, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(null, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ListarUsuarioVendedor()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarUsuarioVendedor(CodigoUnidadNegocio, CodigoSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarHitorialFreezingPorMenbresia(int codigo)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarHitorialFreezingPorMenbresia(CodigoUnidadNegocio, CodigoSede, codigo), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarMembresia(int codigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.BuscarMembresia(CodigoUnidadNegocio, CodigoSede, codigoMenbresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarContratoMembresia(int codigoMenbresia)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarContratoMembresia(CodigoUnidadNegocio, CodigoSede, codigoMenbresia), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarSubTipoDocumentosPorTipoDocumento(int CodTipoDocumento)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ListarSubTipoDocumentosPorTipoDocumento(CodigoUnidadNegocio, CodTipoDocumento, CodigoSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerSerieGenarado(int tipoDocumento, int subTipoDocumento)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.ObtenerSerieGenarado(CodigoUnidadNegocio, tipoDocumento, subTipoDocumento, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarSalida(int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                         string RUC_DNI, string Direccion, DateTime FechaVenta,
                                         int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                         string NroTarjeta, int TipoMoneda, int FormaPago,
                                         decimal SubTotal, decimal IGV, decimal TotalNeto,
                                         decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                         decimal TotalDolares, string tk, string latitud, string longitud, string listaCuotas)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.GuardarSalida(CodigoUnidadNegocio, codigoSalida, CodigoSocio, RazonSocial_Sr,
                                          RUC_DNI, Direccion, FechaVenta,
                                          CodigoTipoComprobante, CodigoSubTipoComprobante, NroComprobante,
                                          NroTarjeta, TipoMoneda, FormaPago,
                                          SubTotal, IGV, TotalNeto,
                                          tipoCambio, listaDetalle, listaFormaPago,
                                          User, CodSede, TotalDolares, tk, latitud, longitud, listaCuotas), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarPagoFiado(int codigoSalida, int CodigoSocio, string RazonSocial_Sr,
                                        string RUC_DNI, string Direccion, DateTime FechaVenta,
                                        int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                        string NroTarjeta, int TipoMoneda, int FormaPago,
                                        decimal SubTotal, decimal IGV, decimal TotalNeto,
                                        decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                        decimal TotalDolares, string tk, string latitud, string longitud, string listaCuotas)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.GuardarPagoFiado(CodigoUnidadNegocio, codigoSalida, CodigoSocio, RazonSocial_Sr,
                                         RUC_DNI, Direccion, FechaVenta,
                                         CodigoTipoComprobante, CodigoSubTipoComprobante, NroComprobante,
                                         NroTarjeta, TipoMoneda, FormaPago,
                                         SubTotal, IGV, TotalNeto,
                                         tipoCambio, listaDetalle, listaFormaPago,
                                         User, CodSede, TotalDolares, tk, latitud, longitud, listaCuotas), JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult uspSeguridadObtenerUnidadNegocio()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspSeguridadObtenerUnidadNegocio(CodigoUnidadNegocio, CodSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarInformacionGeneralVentaPorCodigo(int codigo)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.BuscarInformacionGeneralVentaPorCodigo(CodigoUnidadNegocio, CodSede, codigo), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarAsignarDiasHorarioPaquete(int CodigoPaquete)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                return Json(oRepository.uspListarAsignarDiasHorarioPaquete(CodigoUnidadNegocio, CodigoPaquete), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GuardarMembresiaCongelamiento(int codigo, DateTime fechaInicio, DateTime fechaFin, int FrezenDisponibles, int NroDiasCongelar, DateTime fechaFreziing, DateTime fechaDesFreziing, int CodigoSocio, int NroDias, string NroSolicitud, string Motivo)
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {

                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;
                string User = Commun.Usuario;

                return Json(oRepository.GuardarMembresiaCongelamiento(CodigoUnidadNegocio, codigo, fechaInicio, fechaFin, FrezenDisponibles, NroDiasCongelar, fechaFreziing, fechaDesFreziing, CodSede, CodigoSocio, NroDias, NroSolicitud, Motivo, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspListarDeudasCliente(string Identificacion)
        {
            using (ComprobanteDetalleRepository oRepository = new ComprobanteDetalleRepository())
            {
                E_DataModel.ComprobanteDetalleDTO request = new E_DataModel.ComprobanteDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.Identificacion = Identificacion;
                return Json(oRepository.CentroEntrenamiento_uspListarDeudasCliente(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspRegistrarPagoComprobante(BotComers.ViewModels.Ingresos.ComprobanteViewModel request)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarPagoComprobante(request), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region CHECKING COLABORADORES

        public ActionResult uspListarPersonalClasesGrupales()
        {
            CentroEntrenamiento_ProfesorDTO request = new CentroEntrenamiento_ProfesorDTO();
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;

            ReqFilterCentroEntrenamiento_ProfesorDTO oReq = new ReqFilterCentroEntrenamiento_ProfesorDTO()
            {
                Item = request,
                User = Commun.Usuario,
                FilterCase = filterCaseCentroEntrenamiento_Profesor.CentroEntrenamiento_uspListarPresencial_uspListarPersonalClasesGrupales,
                Paging = new Paging() { }
            };

            RespListCentroEntrenamiento_ProfesorDTO oResp = null;
            using (CentroEntrenamiento_ProfesorLogic oPersonalAsistenciaLogic = new CentroEntrenamiento_ProfesorLogic())
            {
                oResp = oPersonalAsistenciaLogic.CentroEntrenamiento_ProfesorGetList(oReq);
            }
            if (oResp.Success)
            {
                //Codigo = oResp.MessageList[0].Codigo;
            }

            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPersonalFilter(PersonalAsistenciaDTO request)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;

            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = Commun.Usuario,
                FilterCase = filterCasePersonalAsistencia.FilterAutocomplete,
                Paging = new Paging() { }
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oPersonalAsistenciaLogic = new PersonalAsistenciaLogic())
            {
                oResp = oPersonalAsistenciaLogic.PersonalAsistenciaGetList(oReq);
            }
            if (oResp.Success)
            {
                //Codigo = oResp.MessageList[0].Codigo;
            }

            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPersonalAsistenciaGeneralPorNroDocumento(PersonalAsistenciaDTO request)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;

            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                },
                FilterCase = filterCasePersonalAsistencia.ListarTodasAsistenciaPorDNI
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oLogic = new PersonalAsistenciaLogic())
            {
                oResp = oLogic.PersonalAsistenciaGetList(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistrarAsistenciaPersonalAdministrativoPorConfiguracion(PersonalAsistenciaDTO request)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.UsuarioCreacion = Commun.Usuario;

            List<PersonalAsistenciaDTO> list = new List<PersonalAsistenciaDTO>();
            request.Operation = Operation.RegistrarAsistenciaPersonalAdministrativo;

            list.Add(request);
            ReqPersonalAsistenciaDTO oReq = new ReqPersonalAsistenciaDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oPersonalAsistenciaLogic = new PersonalAsistenciaLogic())
            {
                oResp = oPersonalAsistenciaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                //Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistrarAsistenciaProfesores(PersonalAsistenciaDTO request)
        {

            List<PersonalAsistenciaDTO> list = new List<PersonalAsistenciaDTO>();
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.UsuarioCreacion = Commun.Usuario;
            request.Operation = Operation.RegistrarAsistenciaProfesionalFitness;
            list.Add(request);
            ReqPersonalAsistenciaDTO oReq = new ReqPersonalAsistenciaDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oPersonalAsistenciaLogic = new PersonalAsistenciaLogic())
            {
                oResp = oPersonalAsistenciaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                //Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region AGENDA COMERCIAL

        public ActionResult agendacomercial(string id)
        {
            return View();
        }

        public ActionResult ListarAsesoresComercialesAgenda()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuario_AgendaComercial,
                User = "appsfit",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarInteresProspectos()
        {
            List<InteresProspectosDTO> lista = null;
            InteresProspectosDTO oInteresProspectosDTO = new InteresProspectosDTO();
            oInteresProspectosDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oInteresProspectosDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterInteresProspectosDTO oReq = new ReqFilterInteresProspectosDTO()
            {
                Item = oInteresProspectosDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListInteresProspectosDTO oResp = null;
            using (InteresProspectosLogic oInteresProspectosLogic = new InteresProspectosLogic())
            {
                oResp = oInteresProspectosLogic.InteresProspectosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTipoAgendaCliente()
        {
            List<TipoAgendaClienteDTO> lista = new List<TipoAgendaClienteDTO>();

            ReqFilterTipoAgendaClienteDTO oReq = new ReqFilterTipoAgendaClienteDTO()
            {
                FilterCase = filterCaseTipoAgendaCliente.Filter_uspListarTipoAgendaCliente,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListTipoAgendaClienteDTO oResp = null;

            using (TipoAgendaClienteLogic oTipoAgendaClienteLogic = new TipoAgendaClienteLogic())
            {
                oResp = oTipoAgendaClienteLogic.TipoAgendaClienteGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult usplistardllCreadoPor()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.usplistardllCreadoPor,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult usplistardllCreadoPor_ProspectosSinCita()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.usplistardllCreadoPor,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
                lista.Add(new UsuarioDTO()
                {
                    NombreCompleto = "OtrosVendedores"
                });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaPaquetesIAgenda(int flag, string nombre)
        {
            List<PlanesDTO> lista = null;

            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                Item = new PlanesDTO()
                {
                    Descripcion = nombre,
                    CodigoSede = Commun.CodigoSede,
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio
                },
                FilterCase = filterCasePlanes.ListarPaquetesTablaProspectos,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListPlanesDTO oResp = null;
            using (PlanesLogic oPaquetesLogic = new PlanesLogic())
            {
                oResp = oPaquetesLogic.PlanesGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            List<PlanesDTO> nuevoList = new List<PlanesDTO>();

            if (flag == 1)
            {
                foreach (PlanesDTO item in lista)
                {
                    DateTime f = new DateTime(item.FechaVencimiento.Year, item.FechaVencimiento.Month, item.FechaVencimiento.Day, 23, 30, 30);
                    if (f >= DateTime.Now)
                    {
                        nuevoList.Add(item);
                    }
                }
            }

            return Json(nuevoList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarTablaInvitados(int CodigoInvitado, string Nombres, string Apellidos,
           string DNI, string Telefono, string Celular, string Correo, DateTime FechaNacimiento,
           bool Estado, string Genero, string Direccion, string InvitadoPor, int Codigo_InvitadoPor,
           int NroDias, int NroDiasActual, DateTime FechaInicio, DateTime FechaFin, string Accion,
           int CodigoPaquete, string Vendedor, int TipoClienteInvitadoAgenda, int CodigoTiempo, decimal Precio, int CodigoSubProcedencia, int CodigoObjetivo)
        {
            int Codigo = 0;
            //codigo = 1 Es oblogatorio ingresar el nro de documento 
            //codigo = 2 Se guardo correctamente el prospecto
            //VALIDAR SI EL INGRESO DE DNI A PROSPECTOS ES OBLIGATORIO
            //VERIFICAR CONFIGURACION
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracionServer();

            if (ConfiguracionDTO.ObligatorioDNIProspectos && DNI == string.Empty)
            {
                Codigo = 1;
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            //VALIDAR NRO DOCUMENTO DE PROSPECTOS SI EXISTE Y OBTENER UNA LISTA
            if (DNI != string.Empty)
            {
                List<ProspectosTablaDTO> lista = null;
                ReqFilterProspectosDTO oReqList = new ReqFilterProspectosDTO()
                {
                    Item = new ProspectosTablaDTO()
                    {
                        CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                        CodigoSede = Commun.CodigoSede,
                        DNI = DNI
                    },
                    FilterCase = filterCaseTablaProspectos.uspListarProspectosValidadorExisteDNI,
                    User = Commun.Usuario,
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = false,
                        PageNumber = Convert.ToUInt32(0),
                        PageRecords = 0
                    }
                };
                RespListProspectosDTO oRespList = null;
                using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
                {
                    oRespList = oProspectosLogic.ProspectosGetList(oReqList);
                }
                if (oRespList.Success)
                {
                    lista = oRespList.List;
                    if (lista.Count > 0)
                    {
                        return Json(lista, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //SI EL DNI NO EXISTE ENTONCES SI SE PUEDE GUARDAR 
            List<InvitadosDTO> list = new List<InvitadosDTO>();
            var Imagen = "";

            if (Genero == "M")
            {
                Imagen = "../Imagenes/fitness/PerfilHombre.png";
            }
            else
            {
                Imagen = "../Imagenes/fitness/PerfilMujer.png";
            }

            list.Add(new InvitadosDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoInvitado = CodigoInvitado,
                Nombres = Nombres,
                Apellidos = Apellidos,
                DNI = DNI,
                Telefono = Telefono,
                Celular = Celular,
                Correo = Correo,
                FechaNacimiento = FechaNacimiento,
                ImagenUrl = Imagen,
                Estado = true,
                Genero = Genero,
                Facebook = "",
                Codigo_InvitadoPor = Codigo_InvitadoPor,
                InvitadoPor = InvitadoPor,
                Direccion = Direccion,
                Distrito = "",
                Ocupacion = "",
                TipoCliente = TipoClienteInvitadoAgenda,
                Ubicaciones = "",
                TipoDocumento = 1,
                Observacion = "",
                NroDias = NroDias,
                NroDiasActual = NroDiasActual,
                FechaInicio = FechaInicio,
                FechaFin = FechaFin,
                CodigoPaquete = CodigoPaquete,
                Vendedor = Vendedor,
                CodigoTiempo = CodigoTiempo,
                Precio = Precio,
                CodigoSubProcedencia = CodigoSubProcedencia,
                CodigoObjetivo = CodigoObjetivo,
                Operation = Operation.Create

            });

            ReqInvitadosDTO oReq = new ReqInvitadosDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespInvitadosDTO oResp = null;
            using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
            {
                oResp = oInvitadosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspRegistrarProspectoWeb(string Nombres, string Apellidos, string Telefono, string Celular, string Correo,
            string Genero, string Vendedor, int TipoClienteWebAgenda, int CodigoTiempo, decimal Precio, bool Estado)
        {
            int Codigo = 0;
            List<LlamadaEntranteDTO> list = new List<LlamadaEntranteDTO>();

            list.Add(new LlamadaEntranteDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoLlamadaE = 0,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Telefono = Telefono,
                Celular = Celular,
                Correo = Correo,
                Genero = Genero,
                Vendedor = Vendedor,
                TipoCliente = TipoClienteWebAgenda,
                CodigoTiempo = CodigoTiempo,
                Precio = Precio,
                Estado = true,
                Operation = Operation.uspRegistrarProspectoWeb
            });

            ReqLlamadaEntranteDTO oReq = new ReqLlamadaEntranteDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oReferidoLogic = new LlamadaEntranteLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }



        public ActionResult uspListarTablaWeb_Paginacion(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            List<LlamadaEntranteDTO> lista = null;
            ReqFilterLlamadaEntranteDTO oReq = new ReqFilterLlamadaEntranteDTO()
            {
                Item = new LlamadaEntranteDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Vendedor = User,
                    FiltroFechaInicio = FechaInicio,
                    FiltroFechaFin = FechaFin,
                    Nombres = Buscador

                },
                FilterCase = filterCaseLlamadaEntrante.uspListarTablaWeb_Paginacion,
                User = User,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.LlamadaEntranteGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTablaWeb_NumeroRegistros(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin)
        {
            LlamadaEntranteDTO oLlamadaEntranteDTO = new LlamadaEntranteDTO();
            oLlamadaEntranteDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oLlamadaEntranteDTO.CodigoSede = Commun.CodigoSede;
            oLlamadaEntranteDTO.Vendedor = User;
            oLlamadaEntranteDTO.FiltroFechaInicio = FechaInicio;
            oLlamadaEntranteDTO.FiltroFechaFin = FechaFin;
            oLlamadaEntranteDTO.Nombres = Buscador;
            ReqFilterLlamadaEntranteDTO oReq = new ReqFilterLlamadaEntranteDTO()
            {
                FilterCase = filterCaseLlamadaEntrante.uspListarTablaWeb_NumeroRegistros,
                Item = oLlamadaEntranteDTO,
                User = "appsfit"
            };
            RespItemLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.LlamadaEntranteGetItem(oReq);
            }
            if (oResp.Success)
            {
                oLlamadaEntranteDTO = oResp.Item;
                oLlamadaEntranteDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosLlamadaE_NumeroRegistros"]);
            }
            return Json(oLlamadaEntranteDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspBuscarPropectoWebPorCodigo(int CodigoLlamadaE)
        {
            LlamadaEntranteDTO oLlamadaEntranteDTO = new LlamadaEntranteDTO();
            oLlamadaEntranteDTO.CodigoLlamadaE = CodigoLlamadaE;
            oLlamadaEntranteDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oLlamadaEntranteDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterLlamadaEntranteDTO oReq = new ReqFilterLlamadaEntranteDTO()
            {
                FilterCase = filterCaseLlamadaEntrante.uspBuscarPropectoWebPorCodigo,
                Item = oLlamadaEntranteDTO,
                User = "appsfit"
            };

            RespItemLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.LlamadaEntranteGetItem(oReq);
            }
            if (oResp.Success)
            {
                oLlamadaEntranteDTO = oResp.Item;
            }

            return Json(oLlamadaEntranteDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTablaInvitados_Paginacion(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            List<InvitadosDTO> lista = null;
            ReqFilterInvitadosDTO oReq = new ReqFilterInvitadosDTO()
            {
                Item = new InvitadosDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Vendedor = User,
                    FiltroFechaInicio = FechaInicio,
                    FiltroFechaFin = FechaFin,
                    Nombres = Buscador
                },
                FilterCase = filterCaseInvitados.uspListarTablaInvitados_Paginacion,
                User = User,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListInvitadosDTO oResp = null;
            using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
            {
                oResp = oInvitadosLogic.InvitadosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListartablaInvitados_NumeroRegistros(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin)
        {
            InvitadosDTO oInvitadosDTO = new InvitadosDTO();
            oInvitadosDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oInvitadosDTO.CodigoSede = Commun.CodigoSede;
            oInvitadosDTO.Vendedor = User;
            oInvitadosDTO.FiltroFechaInicio = FechaInicio;
            oInvitadosDTO.FiltroFechaFin = FechaFin;
            oInvitadosDTO.Nombres = Buscador;
            ReqFilterInvitadosDTO oReq = new ReqFilterInvitadosDTO()
            {
                FilterCase = filterCaseInvitados.uspListarTablaInvitados_NumeroRegistros,
                Item = oInvitadosDTO,
                User = "appsfit"
            };
            RespItemInvitadosDTO oResp = null;
            using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
            {
                oResp = oInvitadosLogic.InvitadosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oInvitadosDTO = oResp.Item;
                oInvitadosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarTablaInvitados_NumeroRegistros"]);
            }
            return Json(oInvitadosDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspBuscarClientesDatosInvitadosPorCodigo(int CodigoInvitado)
        {
            InvitadosDTO oInvitadosDTO = new InvitadosDTO();
            oInvitadosDTO.CodigoInvitado = CodigoInvitado;
            oInvitadosDTO.CodigoSede = Commun.CodigoSede;
            oInvitadosDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterInvitadosDTO oReq = new ReqFilterInvitadosDTO()
            {
                FilterCase = filterCaseInvitados.uspBuscarClientesDatosInvitadosPorCodigo,
                Item = oInvitadosDTO,
                User = "appsfit"
            };

            RespItemInvitadosDTO oResp = null;
            using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
            {
                oResp = oInvitadosLogic.InvitadosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oInvitadosDTO = oResp.Item;
            }

            return Json(oInvitadosDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTablaReferidos_Paginacion(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            List<ReferidoDTO> lista = null;
            ReqFilterReferidoDTO oReq = new ReqFilterReferidoDTO()
            {
                Item = new ReferidoDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Vendedor = User,
                    FiltroFechaInicio = FechaInicio,
                    FiltroFechaFin = FechaFin,
                    Nombres = Buscador
                },
                FilterCase = filterCaseReferido.uspListarReferido_Paginacion,
                User = User,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListReferidoDTO oResp = null;
            using (ReferidoLogic oreferidoLogic = new ReferidoLogic())
            {
                oResp = oreferidoLogic.ReferidoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTablaReferidos_NumeroRegistros(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin)
        {
            ReferidoDTO oReferidoDTO = new ReferidoDTO();
            oReferidoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oReferidoDTO.CodigoSede = Commun.CodigoSede;
            oReferidoDTO.Vendedor = User;
            oReferidoDTO.FiltroFechaInicio = FechaInicio;
            oReferidoDTO.FiltroFechaFin = FechaFin;
            oReferidoDTO.Nombres = Buscador;

            ReqFilterReferidoDTO oReq = new ReqFilterReferidoDTO()
            {
                FilterCase = filterCaseReferido.uspListarTablaReferido_NumeroRegistros,
                Item = oReferidoDTO,
                User = "appsfit"
            };
            RespItemReferidoDTO oResp = null;
            using (ReferidoLogic oReferidoLogic = new ReferidoLogic())
            {
                oResp = oReferidoLogic.ReferidoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oReferidoDTO = oResp.Item;
                oReferidoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosReferido_NumeroRegistros"]);
            }
            return Json(oReferidoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspBuscarClientesDatosReferidosPorCodigo(int CodigoReferido)
        {
            ReferidoDTO oReferidoDTO = new ReferidoDTO();
            oReferidoDTO.CodigoReferido = CodigoReferido;
            oReferidoDTO.CodigoSede = Commun.CodigoSede;
            oReferidoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterReferidoDTO oReq = new ReqFilterReferidoDTO()
            {
                FilterCase = filterCaseReferido.uspBuscarPorCodigo,
                Item = oReferidoDTO,
                User = Commun.Usuario
            };

            RespItemReferidoDTO oResp = null;
            using (ReferidoLogic oReferidoLogic = new ReferidoLogic())
            {
                oResp = oReferidoLogic.ReferidoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oReferidoDTO = oResp.Item;
            }
            return Json(oReferidoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarAgendaSeguimientoTodos(int Codigo, int CodigoSocio, int Tipo, string DescTipo, string Asunto, string HoraInicio, string Color, string User, int Estado, int CodigoPaquete, int TipoActividad)
        {
            int mensaje = 0;
            string[] sHi = HoraInicio.Split('|');
            DateTime hInicio = new DateTime(Convert.ToInt32(sHi[0]), Convert.ToInt32(sHi[1]), Convert.ToInt32(sHi[2]), Convert.ToInt32(sHi[3]), Convert.ToInt32(sHi[4]), Convert.ToInt32(sHi[5]));
            DateTime hFin = new DateTime();
            hFin = hInicio.AddHours(1);
            List<AgendaSeguimientoDTO> list = new List<AgendaSeguimientoDTO>();
            list.Add(new AgendaSeguimientoDTO()
            {
                TipoActividad = TipoActividad,
                CodigoSocio = CodigoSocio,
                Codigo = Codigo,
                Tipo = Tipo,
                HoraInicio = hInicio,
                HoraFin = hFin,
                Asunto = Asunto,
                Color = Color,
                Estado = Estado,
                CodigoPaquete = CodigoPaquete,
                Vendedor = User,
                UsuarioCreacion = Commun.Usuario,
                CodigoSede = Commun.CodigoSede,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.Create_UspAgendaSeguimientoTodos
            });
            ReqAgendaSeguimientoDTO oReq = new ReqAgendaSeguimientoDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarReagendarAgendaSeguimientoTodosCaidos(int Codigo, int CodigoSocio, int Tipo, string DescTipo, string Asunto, string HoraInicio, string Color, string User, int Estado, int CodigoPaquete, int TipoActividad)
        {
            int mensaje = 0;
            string[] sHi = HoraInicio.Split('|');
            DateTime hInicio = new DateTime(Convert.ToInt32(sHi[0]), Convert.ToInt32(sHi[1]), Convert.ToInt32(sHi[2]), Convert.ToInt32(sHi[3]), Convert.ToInt32(sHi[4]), Convert.ToInt32(sHi[5]));
            DateTime hFin = new DateTime();
            hFin = hInicio.AddHours(1);
            List<AgendaSeguimientoDTO> list = new List<AgendaSeguimientoDTO>();
            list.Add(new AgendaSeguimientoDTO()
            {
                TipoActividad = TipoActividad,
                CodigoSocio = CodigoSocio,
                Codigo = Codigo,
                Tipo = Tipo,
                HoraInicio = hInicio,
                HoraFin = hFin,
                Asunto = Asunto,
                Color = Color,
                Estado = Estado,
                CodigoPaquete = CodigoPaquete,
                Vendedor = User,
                UsuarioCreacion = Commun.Usuario,
                CodigoSede = Commun.CodigoSede,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.Create_UspReagendarAgendaSeguimientoTodosCaidos
            });
            ReqAgendaSeguimientoDTO oReq = new ReqAgendaSeguimientoDTO()
            {
                List = list,
                User = User
            };
            RespAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarGridAgendaGeneral_Paginacion(string Buscador, DateTime FechaDesde, DateTime FechaHasta, int CodigoTipoAgenda, string UsuarioCreador, int CodTiempoPaquete, int TipoCliente, int PageNumber, int TipoActividad)
        {
            List<AgendaSeguimientoDTO> lista = null;

            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                Item = new AgendaSeguimientoDTO()
                {
                    Buscador = Buscador,
                    FechaDesde = FechaDesde,
                    FechaHasta = FechaHasta,
                    CodigoTipoAgenda = CodigoTipoAgenda,
                    UsuarioCreacion = UsuarioCreador,
                    CodSede = Commun.CodigoSede,
                    CodTiempoPaquete = CodTiempoPaquete,
                    TipoCliente = TipoCliente,
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    TipoActividad = TipoActividad
                },
                FilterCase = filterCaseAgendaSeguimiento.uspListarGridAgendaGeneral_Paginacion,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarGridAgendaGeneral_NumeroRegistros(string Buscador, DateTime FechaDesde, DateTime FechaHasta, int CodigoTipoAgenda, string UsuarioCreador, int CodTiempoPaquete, int TipoCliente, int TipoActividad)
        {
            AgendaSeguimientoDTO oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
            oAgendaSeguimientoDTO.Buscador = Buscador;
            oAgendaSeguimientoDTO.FechaDesde = FechaDesde;
            oAgendaSeguimientoDTO.FechaHasta = FechaHasta;
            oAgendaSeguimientoDTO.CodigoTipoAgenda = CodigoTipoAgenda;
            oAgendaSeguimientoDTO.UsuarioCreacion = UsuarioCreador;
            oAgendaSeguimientoDTO.CodSede = Commun.CodigoSede;
            oAgendaSeguimientoDTO.CodTiempoPaquete = CodTiempoPaquete;
            oAgendaSeguimientoDTO.TipoCliente = TipoCliente;
            oAgendaSeguimientoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAgendaSeguimientoDTO.TipoActividad = TipoActividad;

            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                FilterCase = filterCaseAgendaSeguimiento.uspListarGridAgendaGeneral_NumeroRegistros,
                Item = oAgendaSeguimientoDTO,
                User = Commun.Usuario
            };
            RespItemAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAgendaSeguimientoDTO = oResp.Item;
                oAgendaSeguimientoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarGridAgendaGeneral_NumeroRegistros"]);
            }

            return Json(oAgendaSeguimientoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarGridAgendaGeneralAuditoria_Paginacion(string Buscador, DateTime FechaDesde, DateTime FechaHasta, string UsuarioCreador, int PageNumber)
        {
            List<AgendaSeguimientoDTO> lista = null;

            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                Item = new AgendaSeguimientoDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodSede = Commun.CodigoSede,
                    Buscador = Buscador,
                    FechaDesde = FechaDesde,
                    FechaHasta = FechaHasta,
                    UsuarioCreacion = UsuarioCreador
                },
                FilterCase = filterCaseAgendaSeguimiento.uspListarGridAgendaGeneralAuditoria_Paginacion,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarGridAgendaGeneralAuditoria_NumeroRegistros(string Buscador, DateTime FechaDesde, DateTime FechaHasta, string UsuarioCreador)
        {
            AgendaSeguimientoDTO oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
            oAgendaSeguimientoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAgendaSeguimientoDTO.CodSede = Commun.CodigoSede;
            oAgendaSeguimientoDTO.Buscador = Buscador;
            oAgendaSeguimientoDTO.FechaDesde = FechaDesde;
            oAgendaSeguimientoDTO.FechaHasta = FechaHasta;
            oAgendaSeguimientoDTO.UsuarioCreacion = UsuarioCreador;

            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                FilterCase = filterCaseAgendaSeguimiento.uspListarGridAgendaGeneralAuditoria_NumeroRegistros,
                Item = oAgendaSeguimientoDTO,
                User = Commun.Usuario
            };
            RespItemAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAgendaSeguimientoDTO = oResp.Item;
                oAgendaSeguimientoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarGridAgendaGeneral_NumeroRegistros"]);
            }

            return Json(oAgendaSeguimientoDTO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor(DateTime FechaDesde, DateTime FechaHasta)
        {
            List<AgendaSeguimientoDTO> lista = null;

            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                Item = new AgendaSeguimientoDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodSede = Commun.CodigoSede,
                    FechaDesde = FechaDesde,
                    FechaHasta = FechaHasta
                },
                FilterCase = filterCaseAgendaSeguimiento.uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };
            RespListAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarCboVendedoresMigrador(char ind)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.indMigracion = ind;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.ListarVendedoresMigracion,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarCboVendedoresReceptor(char ind)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.indMigracion = ind;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.ListarVendedoresMigracion,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarSocios_PorVendedor_Paginacion(string Vendedor, string NombreCliente, int PageNumber)
        {
            List<ClientesDTO> oLista = null;
            ClientesDTO item = new ClientesDTO();
            item.Vendedor = Vendedor;
            item.CodigoSede = Commun.CodigoSede;
            item.NombreCompleto = NombreCliente;
            item.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                Item = item,
                User = Commun.Usuario,
                FilterCase = filterCaseClientes.uspListarSocios_PorVendedor_Paginacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }
            if (oResp.Success)
            {
                oLista = oResp.List;
            }
            return Json(oLista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarSocios_PorVendedor_NumeroRegistros(string Vendedor, string NombreCliente)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.NombreCompleto = NombreCliente;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarSocios_PorVendedor_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                if (oClientesDTO != null)
                {
                    oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarSocios_PorVendedor_Paginacion_NumeroRegistros"]);
                }

            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCantSocios_Migrador(string UserAsesorVenta)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.UserAsesorVenta = UserAsesorVenta;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.GetCantidadSociosPorVendedor,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCantSocios_Receptor(string UserAsesorVenta)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.UserAsesorVenta = UserAsesorVenta;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.GetCantidadSociosPorVendedor,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarAsesorComercial_Cliente(int CodigoSocio, string Vendedor)
        {
            int mensaje = 0;
            List<ClientesDTO> list = new List<ClientesDTO>();
            list.Add(new ClientesDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoSocio = CodigoSocio,
                Vendedor = Vendedor,
                Operation = Operation.ActualizarAsesorComercial_Cliente,
            });

            ReqClientesDTO oReq = new ReqClientesDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarMigrarAgenda(int tipMigracion, int cantMigracion, string UsuMigrador, string UsuReceptor)
        {
            int mensaje = 0;
            List<ClientesDTO> list = new List<ClientesDTO>();

            list.Add(new ClientesDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoSede = Commun.CodigoSede,
                UsuMigrador = UsuMigrador,
                UsuReceptor = UsuReceptor,
                TipMigracion = tipMigracion,
                CantParcial = cantMigracion,
                UsuarioEdicion = Commun.Usuario,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.RegistrarMigracion
            });

            ReqClientesDTO oReq = new ReqClientesDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        public ActionResult devx()
        {
            DateTime FechaInicio = Convert.ToDateTime("02/01/2019");
            DateTime FechaFin = Convert.ToDateTime("01/10/2023");

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);


            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.FiltroBusqueda = "";
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaCaidaDesde = Fi;
            oClientesDTO.FechaCaidaHasta = Ff;

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                Item = oClientesDTO,
                User = Commun.Usuario,
                FilterCase = filterCaseClientes.ListarSociosLibresAsesores_Paginacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 1,
                    PageRecords = 99999
                }
            };
            RespListClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarSociosLibresAsesores_Paginacion(string flagBusquedaCliente, DateTime FechaCaidaDesde, DateTime FechaCaidaHasta, int PageNumber)
        {
            List<ClientesDTO> oLista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.FiltroBusqueda = flagBusquedaCliente;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaCaidaDesde = FechaCaidaDesde;
            oClientesDTO.FechaCaidaHasta = FechaCaidaHasta;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                Item = oClientesDTO,
                User = Commun.Usuario,
                FilterCase = filterCaseClientes.ListarSociosLibresAsesores_Paginacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }
            if (oResp.Success)
            {
                oLista = oResp.List;
            }

            return Json(oLista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarSociosLibresAsesores_NumeroRegistros(string flagBusquedaCliente, DateTime FechaCaidaDesde, DateTime FechaCaidaHasta)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.FiltroBusqueda = flagBusquedaCliente;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaCaidaDesde = FechaCaidaDesde;
            oClientesDTO.FechaCaidaHasta = FechaCaidaHasta;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarSociosLibresAsesores_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosLibresAsesores_NumeroRegistros"]);
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarSeguimientoAgenda(int codSocio, int tipo)
        {
            List<AgendaSeguimientoDTO> lista = null;
            AgendaSeguimientoDTO oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
            oAgendaSeguimientoDTO.CodigoSocio = codSocio;
            oAgendaSeguimientoDTO.Tipo = tipo;
            oAgendaSeguimientoDTO.CodigoSede = Commun.CodigoSede;
            oAgendaSeguimientoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                User = Commun.Usuario,
                Item = oAgendaSeguimientoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarInformeCitaVendedores(DateTime FechaInicio, DateTime FechaFin)
        {
            List<AgendaSeguimientoDTO> oLista = null;
            AgendaSeguimientoDTO oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
            oAgendaSeguimientoDTO.CodSede = Commun.CodigoSede;
            oAgendaSeguimientoDTO.FechaDesde = FechaInicio;
            oAgendaSeguimientoDTO.FechaHasta = FechaFin;
            oAgendaSeguimientoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterAgendaSeguimientoDTO oReq = new ReqFilterAgendaSeguimientoDTO()
            {
                Item = oAgendaSeguimientoDTO,
                User = Commun.Usuario,
                FilterCase = filterCaseAgendaSeguimiento.ListarInformeCitaVendedores,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListAgendaSeguimientoDTO oResp = null;
            using (AgendaSeguimientoLogic oAgendaSeguimientoLogic = new AgendaSeguimientoLogic())
            {
                oResp = oAgendaSeguimientoLogic.AgendaSeguimientoGetList(oReq);
            }
            if (oResp.Success)
            {
                oLista = oResp.List;
            }
            return Json(oLista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspCerrarCitaClienteAgenda(int CodigoCita, int CodigoCliente, int Tipo, string User)
        {
            int existe = 0;
            using (AgendaSeguimientoLogic oSociosLogic = new AgendaSeguimientoLogic())
            {
                existe = oSociosLogic.uspCerrarCitaClienteAgenda(CodigoCita, CodigoCliente, Tipo, User, Commun.CodigoSede, Commun.CodigoUnidadNegocio);
            }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEncuestaNuevoDatos(int CodigoProspecto, int CodigoObjetivo, int CodigoComoConocioGym, string xml_Interes)
        {
            int mensaje = 0;
            //string mensaje = string.Empty;
            List<EncuestaNuevo1DTO> list = new List<EncuestaNuevo1DTO>();

            ///* Hospitales */
            List<EncuestaNuevo1DTO> Detalle_E = new List<EncuestaNuevo1DTO>();

            XmlDocument xmlDoc_EncuestaNuevo1 = new XmlDocument();
            xmlDoc_EncuestaNuevo1.LoadXml(xml_Interes);

            XmlNodeList detalles_EncuestaNuevo1 = xmlDoc_EncuestaNuevo1.GetElementsByTagName("ds");
            XmlNodeList detalle_E = ((XmlElement)detalles_EncuestaNuevo1[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalle_E)
            {
                EncuestaNuevo1DTO oitem = new EncuestaNuevo1DTO();
                oitem.CodigoInteres = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                Detalle_E.Add(oitem);
            }

            list.Add(new EncuestaNuevo1DTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEncuestaNuevo1 = 0,
                CodigoProspecto = CodigoProspecto,
                CodigoObjetivo = CodigoObjetivo,
                CodigoComoConocioGym = CodigoComoConocioGym,
                UsuarioCreacion = Commun.Usuario,
                ListaDetalle_E = Detalle_E,
                Operation = Operation.Create

            });
            ReqEncuestaNuevo1DTO oReq = new ReqEncuestaNuevo1DTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespEncuestaNuevo1DTO oResp = null;
            using (EncuestaNuevo1Logic oEncuestaNuevo1Logic = new EncuestaNuevo1Logic())
            {
                oResp = oEncuestaNuevo1Logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspActualizarProspectoASocio(int CodigoProspecto, string User)
        {
            int mensaje = 0;
            List<ProspectosTablaDTO> list = new List<ProspectosTablaDTO>();
            list.Add(new ProspectosTablaDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoProspecto = CodigoProspecto,
                UsuarioEdicion = User,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Operation = Operation.UpdateProspectoASocio
            });
            ReqProspectosDTO oReq = new ReqProspectosDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspActualizarInvitadoASocio(int CodigoInvitado, string User)
        {
            int mensaje = 0;
            List<InvitadosDTO> list = new List<InvitadosDTO>();
            list.Add(new InvitadosDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoInvitado = CodigoInvitado,
                UsuarioEdicion = User,
                CodigoSede = Commun.CodigoSede,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.UpdateInvitadoSocio
            });
            ReqInvitadosDTO oReq = new ReqInvitadosDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespInvitadosDTO oResp = null;
            using (InvitadosLogic oInvitadosLogic = new InvitadosLogic())
            {
                oResp = oInvitadosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspActualizarReferidoASocio(int CodigoReferido, string User)
        {
            int mensaje = 0;
            List<ReferidoDTO> list = new List<ReferidoDTO>();
            list.Add(new ReferidoDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoReferido = CodigoReferido,
                UsuarioEdicion = User,
                CodigoSede = Commun.CodigoSede,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.UpdateReferidoASocio
            });
            ReqReferidoDTO oReq = new ReqReferidoDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespReferidoDTO oResp = null;
            using (ReferidoLogic oReferidoLogic = new ReferidoLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspActualizarWebASocio(int CodigoLlamadaE, string User)
        {
            int mensaje = 0;
            List<LlamadaEntranteDTO> list = new List<LlamadaEntranteDTO>();
            list.Add(new LlamadaEntranteDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoLlamadaE = CodigoLlamadaE,
                UsuarioEdicion = User,
                CodigoSede = Commun.CodigoSede,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.uspActualizarProspectoWebASocio
            });
            ReqLlamadaEntranteDTO oReq = new ReqLlamadaEntranteDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspActualizarLlamadaEASocio(int CodigoLlamadaE, string User)
        {
            int mensaje = 0;
            List<LlamadaEntranteDTO> list = new List<LlamadaEntranteDTO>();
            list.Add(new LlamadaEntranteDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoLlamadaE = CodigoLlamadaE,
                UsuarioEdicion = User,
                CodigoSede = Commun.CodigoSede,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.UpdateLlamadaEASocio
            });
            ReqLlamadaEntranteDTO oReq = new ReqLlamadaEntranteDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspValidarExisteCita_Usuario_AgendaGeneral(int CodigoSocio, int CodigoTipoAgenda, string Usuario, string Clave)
        {
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSocio = CodigoSocio;
            oUsuarioDTO.CodigoTipoAgenda = CodigoTipoAgenda;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.Usuario = Usuario;
            oUsuarioDTO.Contrasenia = Clave;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.uspValidarExisteCita_Usuario_AgendaGeneral,
                Item = oUsuarioDTO,
                User = Commun.Usuario
            };

            RespItemUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetItem(oReq);
            }
            if (oResp.Success)
            {
                oUsuarioDTO = oResp.Item;
            }
            return Json(oUsuarioDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspValidarUsuarioIngresado(string VendedorGrillaRenovReins, string Clave)
        {
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.Vendedor = VendedorGrillaRenovReins;
            oUsuarioDTO.Contrasenia = Clave;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.uspValidarUsuarioIngresado,
                Item = oUsuarioDTO,
                User = Commun.Usuario
            };

            RespItemUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetItem(oReq);
            }
            if (oResp.Success)
            {
                oUsuarioDTO = oResp.Item;
            }

            return Json(oUsuarioDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarTablaLlamadaEntrante(int CodigoLlamadaE, string Nombres, string Apellidos, string DNI, string Telefono,
         string Celular, string Correo, DateTime FechaNacimiento, bool Estado, string Genero,
         string Direccion, string Accion, int CodigoPaquete, string Vendedor, int Hijos,
         int CantHijos, int TipoClienteLlamadaEAgenda, int CodigoTiempo, decimal Precio, int CodigoObjetivo)
        {
            int Codigo = 0;
            //codigo = 1 Es oblogatorio ingresar el nro de documento 
            //codigo = 2 Se guardo correctamente el prospecto
            //VALIDAR SI EL INGRESO DE DNI A PROSPECTOS ES OBLIGATORIO
            //VERIFICAR CONFIGURACION
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracionServer();

            if (ConfiguracionDTO.ObligatorioDNIProspectos && DNI == string.Empty)
            {
                Codigo = 1;
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            //VALIDAR NRO DOCUMENTO DE PROSPECTOS SI EXISTE Y OBTENER UNA LISTA
            if (DNI != string.Empty)
            {
                List<ProspectosTablaDTO> lista = null;
                ReqFilterProspectosDTO oReqList = new ReqFilterProspectosDTO()
                {
                    Item = new ProspectosTablaDTO()
                    {
                        CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                        CodigoSede = Commun.CodigoSede,
                        DNI = DNI
                    },
                    FilterCase = filterCaseTablaProspectos.uspListarProspectosValidadorExisteDNI,
                    User = Commun.Usuario,
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = false,
                        PageNumber = Convert.ToUInt32(0),
                        PageRecords = 0
                    }
                };
                RespListProspectosDTO oRespList = null;
                using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
                {
                    oRespList = oProspectosLogic.ProspectosGetList(oReqList);
                }
                if (oRespList.Success)
                {
                    lista = oRespList.List;
                    if (lista.Count > 0)
                    {
                        return Json(lista, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            //SI EL DNI NO EXISTE ENTONCES SI SE PUEDE GUARDAR
            List<LlamadaEntranteDTO> list = new List<LlamadaEntranteDTO>();
            var Imagen = "";

            if (Genero == "M")
            {
                Imagen = "../Imagenes/fitness/PerfilHombre.png";
            }
            else
            {
                Imagen = "../Imagenes/fitness/PerfilMujer.png";
            }

            list.Add(new LlamadaEntranteDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoLlamadaE = CodigoLlamadaE,
                Nombres = Nombres,
                Apellidos = Apellidos,
                DNI = DNI,
                Telefono = Telefono,
                Celular = Celular,
                Correo = Correo,
                FechaNacimiento = FechaNacimiento,
                ImagenUrl = Imagen,
                Estado = true,
                Genero = Genero,
                Facebook = "",
                Direccion = Direccion,
                Distrito = "",
                Ocupacion = "",
                TipoCliente = TipoClienteLlamadaEAgenda,
                Ubicaciones = "",
                TipoDocumento = 1,
                CodigoPaquete = CodigoPaquete,
                Vendedor = Vendedor,
                Hijos = Hijos,
                CantHijos = CantHijos,
                CodigoTiempo = CodigoTiempo,
                Precio = Precio,
                CodigoObjetivo = CodigoObjetivo,
                Operation = Operation.Create
            });

            ReqLlamadaEntranteDTO oReq = new ReqLlamadaEntranteDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oReferidoLogic = new LlamadaEntranteLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTablaLlamadaEntrante_Paginacion(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            List<LlamadaEntranteDTO> lista = null;
            ReqFilterLlamadaEntranteDTO oReq = new ReqFilterLlamadaEntranteDTO()
            {
                Item = new LlamadaEntranteDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Nombres = Buscador,
                    Vendedor = User,
                    FiltroFechaInicio = FechaInicio,
                    FiltroFechaFin = FechaFin
                },
                FilterCase = filterCaseLlamadaEntrante.uspListarTablaLlamadaEntrante_Paginacion,
                User = User,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.LlamadaEntranteGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarTablaLlamadaEntrante_NumeroRegistros(string Buscador, string User, DateTime FechaInicio, DateTime FechaFin)
        {
            LlamadaEntranteDTO oLlamadaEntranteDTO = new LlamadaEntranteDTO();
            oLlamadaEntranteDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oLlamadaEntranteDTO.CodigoSede = Commun.CodigoSede;
            oLlamadaEntranteDTO.Nombres = Buscador;
            oLlamadaEntranteDTO.Vendedor = User;
            oLlamadaEntranteDTO.FiltroFechaInicio = FechaInicio;
            oLlamadaEntranteDTO.FiltroFechaFin = FechaFin;
            ReqFilterLlamadaEntranteDTO oReq = new ReqFilterLlamadaEntranteDTO()
            {
                FilterCase = filterCaseLlamadaEntrante.uspListarTablaLlamadaEntrante_NumeroRegistros,
                Item = oLlamadaEntranteDTO,
                User = "appsfit"
            };
            RespItemLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.LlamadaEntranteGetItem(oReq);
            }
            if (oResp.Success)
            {
                oLlamadaEntranteDTO = oResp.Item;
                oLlamadaEntranteDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosLlamadaE_NumeroRegistros"]);
            }
            return Json(oLlamadaEntranteDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarClientesDatosLLamadaEPorCodigo(int CodigoLlamadaE)
        {
            LlamadaEntranteDTO oLlamadaEntranteDTO = new LlamadaEntranteDTO();
            oLlamadaEntranteDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oLlamadaEntranteDTO.CodigoSede = Commun.CodigoSede;
            oLlamadaEntranteDTO.CodigoLlamadaE = CodigoLlamadaE;
            ReqFilterLlamadaEntranteDTO oReq = new ReqFilterLlamadaEntranteDTO()
            {
                FilterCase = filterCaseLlamadaEntrante.uspBuscarPorCodigo,
                Item = oLlamadaEntranteDTO,
                User = "appsfit"
            };

            RespItemLlamadaEntranteDTO oResp = null;
            using (LlamadaEntranteLogic oLlamadaEntranteLogic = new LlamadaEntranteLogic())
            {
                oResp = oLlamadaEntranteLogic.LlamadaEntranteGetItem(oReq);
            }
            if (oResp.Success)
            {
                oLlamadaEntranteDTO = oResp.Item;
            }

            return Json(oLlamadaEntranteDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarTablaReferidos(int CodigoReferido, string Nombres, string Apellidos,
        string DNI, string Telefono, string Celular, string Correo,
        DateTime FechaNacimiento, bool Estado, string Genero, string Direccion,
        string ReferidoPor, int CodigoReferidoPor, string Accion,
        int CodigoPaquete, string Vendedor, int Hijos, int CantHijos,
        int TipoClienteRefidoAgenda, int CodigoTiempo, decimal Precio, int CodigoSubProcedencia, int CodigoObjetivo)
        {
            int Codigo = 0;
            //codigo = 1 Es oblogatorio ingresar el nro de documento 
            //codigo = 2 Se guardo correctamente el prospecto
            //VALIDAR SI EL INGRESO DE DNI A PROSPECTOS ES OBLIGATORIO
            //VERIFICAR CONFIGURACION
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracionServer();

            if (ConfiguracionDTO.ObligatorioDNIProspectos && DNI == string.Empty)
            {
                Codigo = 1;
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            //VALIDAR NRO DOCUMENTO DE PROSPECTOS SI EXISTE Y OBTENER UNA LISTA
            if (DNI != string.Empty)
            {
                List<ProspectosTablaDTO> lista = null;
                ReqFilterProspectosDTO oReqList = new ReqFilterProspectosDTO()
                {
                    Item = new ProspectosTablaDTO()
                    {
                        CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                        CodigoSede = Commun.CodigoSede,
                        DNI = DNI
                    },
                    FilterCase = filterCaseTablaProspectos.uspListarProspectosValidadorExisteDNI,
                    User = Commun.Usuario,
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = false,
                        PageNumber = Convert.ToUInt32(0),
                        PageRecords = 0
                    }
                };
                RespListProspectosDTO oRespList = null;
                using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
                {
                    oRespList = oProspectosLogic.ProspectosGetList(oReqList);
                }
                if (oRespList.Success)
                {
                    lista = oRespList.List;
                    if (lista.Count > 0)
                    {
                        return Json(lista, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            //SI EL DNI NO EXISTE ENTONCES SI SE PUEDE GUARDAR    
            List<ReferidoDTO> list = new List<ReferidoDTO>();
            var Imagen = "";

            if (Genero == "M")
            {
                Imagen = "../Imagenes/fitness/PerfilHombre.png";
            }
            else
            {
                Imagen = "../Imagenes/fitness/PerfilMujer.png";
            }

            list.Add(new ReferidoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoReferido = CodigoReferido,
                Nombres = Nombres,
                Apellidos = Apellidos,
                DNI = DNI,
                Telefono = Telefono,
                Celular = Celular,
                Correo = Correo,
                FechaNacimiento = FechaNacimiento,
                ImagenUrl = Imagen,
                Estado = true,
                Genero = Genero,
                Facebook = "",
                CodigoReferidoPor = CodigoReferidoPor,
                ReferidoPor = ReferidoPor,
                Direccion = Direccion,
                Distrito = "",
                Ocupacion = "",
                TipoCliente = TipoClienteRefidoAgenda,
                Ubicaciones = "",
                TipoDocumento = 1,
                CodigoPaquete = CodigoPaquete,
                Vendedor = Vendedor,
                Hijos = Hijos,
                CantHijos = CantHijos,
                CodigoTiempo = CodigoTiempo,
                Precio = Precio,
                CodigoSubProcedencia = CodigoSubProcedencia,
                CodigoObjetivo = CodigoObjetivo,
                Operation = Operation.Create
            });

            ReqReferidoDTO oReq = new ReqReferidoDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespReferidoDTO oResp = null;
            using (ReferidoLogic oReferidoLogic = new ReferidoLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public static ConfiguracionDTO BuscarConfiguracionServer()
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Codigo = Commun.CodigoSede;
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = Commun.Usuario,
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.BuscarPorCodigo
            };
            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }
            return oConfiguracionDTO;
        }

        public ActionResult GuardarTablaPropectos(string Vendedor, int CodigoOrigen, int CodigoProspecto,
         string Nombres, string Apellidos, string Telefono, string Celular, string Correo,
         string Genero, int TipoCliente, int CodigoTipoPaquete, int CodigoPaquete,
         int Hijos, int CantHijos, string DNI, string Accion, DateTime FechaNacimiento,
         int CodigoObjetivo, int CodigoComoConocioGym, string xml_Interes, int CodigoTiempo, decimal Precio)
        {
            int Codigo = 0;
            //codigo = 1 Es oblogatorio ingresar el nro de documento 
            //codigo = 2 Se guardo correctamente el prospecto
            //VALIDAR SI EL INGRESO DE DNI A PROSPECTOS ES OBLIGATORIO
            //VERIFICAR CONFIGURACION
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracionServer();

            if (ConfiguracionDTO.ObligatorioDNIProspectos && DNI == string.Empty)
            {
                Codigo = 1;
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            //VALIDAR NRO DOCUMENTO DE PROSPECTOS SI EXISTE Y OBTENER UNA LISTA
            if (DNI != string.Empty)
            {
                List<ProspectosTablaDTO> lista = null;
                ReqFilterProspectosDTO oReqList = new ReqFilterProspectosDTO()
                {
                    Item = new ProspectosTablaDTO()
                    {
                        CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                        CodigoSede = Commun.CodigoSede,
                        DNI = DNI
                    },
                    FilterCase = filterCaseTablaProspectos.uspListarProspectosValidadorExisteDNI,
                    User = Commun.Usuario,
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = false,
                        PageNumber = Convert.ToUInt32(0),
                        PageRecords = 0
                    }
                };
                RespListProspectosDTO oRespList = null;
                using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
                {
                    oRespList = oProspectosLogic.ProspectosGetList(oReqList);
                }
                if (oRespList.Success)
                {
                    lista = oRespList.List;
                    if (lista.Count > 0)
                    {
                        return Json(lista, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            //SI EL DNI NO EXISTE ENTONCES SI SE PUEDE GUARDAR                     
            List<EncuestaNuevo1DTO> Detalle_E = new List<EncuestaNuevo1DTO>();

            XmlDocument xmlDoc_EncuestaNuevo1 = new XmlDocument();
            xmlDoc_EncuestaNuevo1.LoadXml(xml_Interes);

            XmlNodeList detalles_EncuestaNuevo1 = xmlDoc_EncuestaNuevo1.GetElementsByTagName("ds");
            XmlNodeList detalle_E = ((XmlElement)detalles_EncuestaNuevo1[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalle_E)
            {
                EncuestaNuevo1DTO oitem = new EncuestaNuevo1DTO();
                oitem.CodigoInteres = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                Detalle_E.Add(oitem);
            }

            List<ProspectosTablaDTO> list = new List<ProspectosTablaDTO>();
            list.Add(new ProspectosTablaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoProspecto = CodigoProspecto,
                Vendedor = Vendedor,
                CodigoOrigen = CodigoOrigen,
                CodigoSP = 0,
                CodigoAE = 0,
                CodigoCCG = 0,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Telefono = Telefono,
                Celular = Celular,
                Genero = Genero,
                Facebook = "",
                TipoCliente = TipoCliente,
                CodigoTipoPaquete = CodigoTipoPaquete,
                CodigoPaquete = CodigoPaquete,
                Estado = true,
                FechaNacimiento = FechaNacimiento,
                Hijos = Hijos,
                CantHijos = CantHijos,
                DNI = DNI,
                Correo = Correo,
                Observacion = "",
                Ocupacion = "",
                TipoConversion = 0,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                CodigoObjetivo = CodigoObjetivo,
                CodigoComoConocioGym = CodigoComoConocioGym,
                ListaDetalle_E = Detalle_E,
                CodigoTiempo = CodigoTiempo,
                Precio = Precio,
                Operation = Operation.Create
            });


            ReqProspectosDTO oReq = new ReqProspectosDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarTablaPropectos(int CodigoProspecto)
        {
            string mensaje = string.Empty;
            int Codigo = 0;

            List<ProspectosTablaDTO> list = new List<ProspectosTablaDTO>();
            list.Add(new ProspectosTablaDTO()
            {
                CodigoProspecto = CodigoProspecto,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.Delete
            });

            ReqProspectosDTO oReq = new ReqProspectosDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarTablaPropectos_Paginacion(string descripcion, string User, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            List<ProspectosTablaDTO> lista = null;
            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                Item = new ProspectosTablaDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Nombres = descripcion,
                    Vendedor = User,
                    FiltroFechaInicio = FechaInicio,
                    FiltroFechaFin = FechaFin
                },
                FilterCase = filterCaseTablaProspectos.uspListarTablaPropectos_Paginacion,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListartablaProspectos_NumeroRegistros(string descripcion, string User, DateTime FechaInicio, DateTime FechaFin)
        {
            ProspectosTablaDTO oProspectosTablaDTO = new ProspectosTablaDTO();
            oProspectosTablaDTO.CodigoSede = Commun.CodigoSede;
            oProspectosTablaDTO.Nombres = descripcion;
            oProspectosTablaDTO.Vendedor = User;
            oProspectosTablaDTO.FiltroFechaInicio = FechaInicio;
            oProspectosTablaDTO.FiltroFechaFin = FechaFin;
            oProspectosTablaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;

            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                FilterCase = filterCaseTablaProspectos.uspListarTablaProspectos_NumeroRegistros,
                Item = oProspectosTablaDTO,
                User = Commun.Usuario
            };
            RespItemProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oProspectosTablaDTO = oResp.Item;
                oProspectosTablaDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListartablaProspectos_Paginacion"]);
            }
            return Json(oProspectosTablaDTO, JsonRequestBehavior.AllowGet);
        }




        public ActionResult BuscarClientesProspectosPorCodigo(int CodigoProspecto)
        {
            ProspectosTablaDTO oProspectosTablaDTO = new ProspectosTablaDTO();
            oProspectosTablaDTO.CodigoProspecto = CodigoProspecto;
            oProspectosTablaDTO.CodigoSede = Commun.CodigoSede;
            oProspectosTablaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                FilterCase = filterCaseTablaProspectos.uspBuscarClientesProspectosPorCodigo,
                Item = oProspectosTablaDTO,
                User = "appsfit"
            };

            RespItemProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oProspectosTablaDTO = oResp.Item;
            }
            return Json(oProspectosTablaDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspBuscarEncuesta1Nuevo(int CodigoProspecto)
        {
            EncuestaNuevo1DTO oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();
            oEncuestaNuevo1DTO.CodigoProspecto = CodigoProspecto;
            oEncuestaNuevo1DTO.CodigoSede = Commun.CodigoSede;
            oEncuestaNuevo1DTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterEncuestaNuevo1DTO oReq = new ReqFilterEncuestaNuevo1DTO()
            {
                FilterCase = filterCaseEncuestaNuevo1.uspBuscarEncuestaNuevo1,
                Item = oEncuestaNuevo1DTO,
                User = "appsfit"
            };

            RespItemEncuestaNuevo1DTO oResp = null;
            using (EncuestaNuevo1Logic oEncuestaNuevo1Logic = new EncuestaNuevo1Logic())
            {
                oResp = oEncuestaNuevo1Logic.EncuestaNuevo1GetItem(oReq);
            }
            if (oResp.Success)
            {
                oEncuestaNuevo1DTO = oResp.Item;
            }
            return Json(oEncuestaNuevo1DTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarEncuestaNuevo2(int CodigoProspecto, int CodigoOrigenProspecto)
        {
            List<EncuestaNuevo1DTO> lista = null;
            EncuestaNuevo1DTO oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();
            oEncuestaNuevo1DTO.CodigoOrigenProspecto = CodigoOrigenProspecto;
            oEncuestaNuevo1DTO.CodigoProspecto = CodigoProspecto;
            oEncuestaNuevo1DTO.CodigoSede = Commun.CodigoSede;
            oEncuestaNuevo1DTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterEncuestaNuevo1DTO oReq = new ReqFilterEncuestaNuevo1DTO()
            {
                FilterCase = filterCaseEncuestaNuevo1.uspListarEncuestaNuevo2,
                User = "appsfit",
                Item = oEncuestaNuevo1DTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListEncuestaNuevo1DTO oResp = null;
            using (EncuestaNuevo1Logic oEncuestaNuevo1Logic = new EncuestaNuevo1Logic())
            {
                oResp = oEncuestaNuevo1Logic.EncuestaNuevo1GetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion(string descripcion, int PageNumber)
        {
            List<ProspectosTablaDTO> lista = null;
            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                Item = new ProspectosTablaDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Nombres = descripcion
                },
                FilterCase = filterCaseTablaProspectos.uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro(string descripcion)
        {
            ProspectosTablaDTO oProspectosTablaDTO = new ProspectosTablaDTO();
            oProspectosTablaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oProspectosTablaDTO.CodigoSede = Commun.CodigoSede;
            oProspectosTablaDTO.Nombres = descripcion;

            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                FilterCase = filterCaseTablaProspectos.uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro,
                Item = oProspectosTablaDTO,
                User = Commun.Usuario
            };
            RespItemProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oProspectosTablaDTO = oResp.Item;
                oProspectosTablaDTO.TamanioPagina = 120;
            }
            return Json(oProspectosTablaDTO, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UspListarProspectosSinActividadAgendaComercial(string descripcion, string User, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            List<ProspectosTablaDTO> lista = null;
            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                Item = new ProspectosTablaDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    Nombres = descripcion,
                    Vendedor = User,
                    FiltroFechaInicio = FechaInicio,
                    FiltroFechaFin = FechaFin
                },
                FilterCase = filterCaseTablaProspectos.UspListarProspectosSinActividadAgendaComercial,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UspListarProspectosSinActividadAgendaComercial_NumeroRegistros(string descripcion, string User, DateTime FechaInicio, DateTime FechaFin)
        {
            ProspectosTablaDTO oProspectosTablaDTO = new ProspectosTablaDTO();
            oProspectosTablaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oProspectosTablaDTO.CodigoSede = Commun.CodigoSede;
            oProspectosTablaDTO.Nombres = descripcion;
            oProspectosTablaDTO.Vendedor = User;
            oProspectosTablaDTO.FiltroFechaInicio = FechaInicio;
            oProspectosTablaDTO.FiltroFechaFin = FechaFin;

            ReqFilterProspectosDTO oReq = new ReqFilterProspectosDTO()
            {
                FilterCase = filterCaseTablaProspectos.UspListarProspectosSinActividadAgendaComercial_NumeroRegistros,
                Item = oProspectosTablaDTO,
                User = Commun.Usuario
            };
            RespItemProspectosDTO oResp = null;
            using (ProspectosLogic oProspectosLogic = new ProspectosLogic())
            {
                oResp = oProspectosLogic.ProspectosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oProspectosTablaDTO = oResp.Item;
                oProspectosTablaDTO.TamanioPagina = 120;
            }
            return Json(oProspectosTablaDTO, JsonRequestBehavior.AllowGet);
        }


        //REAPARTIR INACTIVOS SIN CITA
        public ActionResult uspListarClientesInactivos(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivosSinCita,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarClientesInactivos_NumeroRegistros(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivosSinCita_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }

            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspAsignarClienteInactivosSinCitaAVendedores(bool flagRepartirEquitativamenteSegunMeta, DateTime FechaInicio, DateTime FechaFin, bool flagRepartirInactivos, bool flagRepartirRenovaciones, bool flagRepartirProspectosSinActividadVendedoresInactivos, bool flagRepartirProspectosSinActividadVendedoresActivos)
        {
            string mensaje = string.Empty;
            List<MetasDTO> lista = new List<MetasDTO>();
            MetasDTO oMetasDTO = new MetasDTO();
            oMetasDTO.CodigoEntidadNegocio = Commun.CodigoUnidadNegocio;
            oMetasDTO.CodigoSede = Commun.CodigoSede;
            oMetasDTO.UsuarioCreacion = Commun.Usuario;
            oMetasDTO.FechaInicio = FechaInicio;
            oMetasDTO.FechaFin = FechaFin;
            oMetasDTO.flagRepartirInactivos = flagRepartirInactivos;
            oMetasDTO.flagRepartirRenovaciones = flagRepartirRenovaciones;
            oMetasDTO.flagRepartirProspectosSinCitaVendedoresInactivos = flagRepartirProspectosSinActividadVendedoresInactivos;
            oMetasDTO.flagRepartirEquitativamenteSegunMeta = flagRepartirEquitativamenteSegunMeta;
            oMetasDTO.flagRepartirProspectosSinActividadVendedoresActivos = flagRepartirProspectosSinActividadVendedoresActivos;

            oMetasDTO.Operation = Operation.uspAsignarClienteInactivosSinCitaAVendedores;

            lista.Add(oMetasDTO);
            ReqMetasDTO oReq = new ReqMetasDTO()
            {
                List = lista,
                User = Commun.Usuario
            };
            RespMetasDTO oResp = null;
            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "se guardo correctamente.";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SEGListarUsuarioResponsable_RepartirInactivos(string filtro)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.filtro = filtro;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuarioResponsableSuplementos,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.Where(x => x.CodigoPerfil == 1 || x.CodigoPerfil == 6 || x.CodigoPerfil == 13).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SEGListarUsuarioResponsable_ConvertirActividadVenta(string filtro, string vendedor)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.filtro = filtro;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuarioResponsableSuplementos,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.Where(x => x.CodigoPerfil == 1 || x.CodigoPerfil == 6 || x.CodigoPerfil == 13).ToList();
                lista.Insert(0, new UsuarioDTO() { NombreCompleto = vendedor });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarClientesAgendaComercialRenovacion(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                           string EstadoAsistencia, string AsesorComercial, string Nombre,
                                                           DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesAgendaComercialRenovacion,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };
            RespListClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarClientesAgendaComercialRenovacion_NumeroRegistros(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string AsesorComercial, string Nombre, DateTime FechaInicio, DateTime FechaFin)
        {
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesAgendaComercialRenovacion_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarClientesAgendaComercialRenovacion"]);
            }

            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarClientesAgendaComercialRenovacionInscritos(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                          string EstadoAsistencia, string AsesorComercial, string Nombre,
                                                          DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesAgendaComercialRenovacionInscritos,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };
            RespListClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        public ActionResult uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                           string EstadoAsistencia, string AsesorComercial, string Nombre, DateTime FechaInicio, DateTime FechaFin)
        {
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarClientesAgendaComercialRenovacionInscritos"]);
            }

            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }


        //EFECTIVO
        public ActionResult uspListarEfectivadadCitasVendedores(DateTime FechaInicio, DateTime FechaFin)
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarEfectivadadCitasVendedores,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin

                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //no se usa este metodo
        public ActionResult uspListarVerificadorInformacionSociosComerciales_paginacion(DateTime FechaInicio, DateTime FechaFin, string Hi, string Hf, int CodigoTiempoMenbresia, string AsesorDeVentas, int TipoIngreso, int PageNumber)
        {
            List<MetasDTO> lista = new List<MetasDTO>();

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarVerificadorInformacionSociosComerciales_paginacion,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    AsesorDeVentas = AsesorDeVentas,
                    Verificador_TipoIngreso = TipoIngreso,
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //no se usa este metodo
        public ActionResult uspListarVerificadorInformacionSociosComerciales_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin, string AsesorDeVentas, int TipoIngreso)
        {
            MetasDTO oMetasDTO = new MetasDTO();
            oMetasDTO.CodigoEntidadNegocio = Commun.CodigoUnidadNegocio;
            oMetasDTO.CodigoSede = Commun.CodigoSede;
            oMetasDTO.FechaInicio = FechaInicio;
            oMetasDTO.FechaFin = FechaFin;
            oMetasDTO.AsesorDeVentas = AsesorDeVentas;
            oMetasDTO.Verificador_TipoIngreso = TipoIngreso;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarVerificadorInformacionSociosComerciales_NumeroRegistros,
                Item = oMetasDTO,
                User = Commun.Usuario
            };

            RespItemMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetItem(oReq);
            }

            if (oResp.Success)
            {
                oMetasDTO = oResp.Item;
                oMetasDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarVerificadorInformacionSociosComerciales_paginacion"]);
            }
            return Json(oMetasDTO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarMatriculadorAgendaComercial_paginacion(int CodigoMebresiaOrigen, string Nombres, DateTime FechaInicio, DateTime FechaFin, string Hi, string Hf, int CodTiempoMenbresia, string UsuarioCreacion, int PageNumber)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);

            fechaConsulta = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            fechaConsultaFin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, HoraFin.Hour, HoraFin.Minute, HoraFin.Second);

            List<ContratoDTO> lista = new List<ContratoDTO>();
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            oContratoDTO.CodigoMebresiaOrigen = CodigoMebresiaOrigen;
            oContratoDTO.Nombres = Nombres;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.CodTiempoMenbresia = CodTiempoMenbresia;
            oContratoDTO.UsuarioCreacion = UsuarioCreacion;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMatriculadorAgendaComercial_paginacion,
                Item = oContratoDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
                //oResp.Paging.TotalRecord;

            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportaruspListarMatriculadorAgendaComercial_paginacion(int CodigoMebresiaOrigen, string Nombres, DateTime FechaInicio, DateTime FechaFin, string Hi, string Hf, int CodTiempoMenbresia, string UsuarioCreacion)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);

            fechaConsulta = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            fechaConsultaFin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, HoraFin.Hour, HoraFin.Minute, HoraFin.Second);

            List<ContratoDTO> lista = new List<ContratoDTO>();
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            oContratoDTO.CodigoMebresiaOrigen = CodigoMebresiaOrigen;
            oContratoDTO.Nombres = Nombres;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.CodTiempoMenbresia = CodTiempoMenbresia;
            oContratoDTO.UsuarioCreacion = UsuarioCreacion;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.ExportaruspListarMatriculadorAgendaComercial_paginacion,
                Item = oContratoDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarMatriculadorAgendaComercial_NumeroRegistros(int CodigoMebresiaOrigen, string Nombres, DateTime FechaInicio, DateTime FechaFin, string Hi, string Hf, int CodTiempoMenbresia, string UsuarioCreacion)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);

            fechaConsulta = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            fechaConsultaFin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, HoraFin.Hour, HoraFin.Minute, HoraFin.Second);

            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            oContratoDTO.CodigoMebresiaOrigen = CodigoMebresiaOrigen;
            oContratoDTO.Nombres = Nombres;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.CodTiempoMenbresia = CodTiempoMenbresia;
            oContratoDTO.UsuarioCreacion = UsuarioCreacion;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMatriculadorAgendaComercial_NumeroRegistros,
                Item = oContratoDTO,
                User = Commun.Usuario

            };

            RespItemContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }

            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
                oContratoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarMembresiasSociosAcuenta_Paginacion"]);

            }
            return Json(oContratoDTO, JsonRequestBehavior.AllowGet);
        }


        //intereses
        public ActionResult uspListarInteres()
        {

            List<InteresProspectosDTO> lista = null;
            ReqFilterInteresProspectosDTO oReq = new ReqFilterInteresProspectosDTO()
            {
                Item = new InteresProspectosDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListInteresProspectosDTO oResp = null;
            using (InteresProspectosLogic oInteresProspectosLogic = new InteresProspectosLogic())
            {
                oResp = oInteresProspectosLogic.InteresProspectosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarInteres(string descripcion)
        {
            int Cod = 0;
            List<InteresProspectosDTO> list = new List<InteresProspectosDTO>();
            list.Add(new InteresProspectosDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoInteres = 0,
                Descripcion = descripcion,
                UsuarioCreacion = Commun.Usuario,
                Operation = Operation.Create
            });

            ReqInteresProspectosDTO oReq = new ReqInteresProspectosDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespInteresProspectosDTO oResp = null;
            using (InteresProspectosLogic oInteresProspectosLogic = new InteresProspectosLogic())
            {
                oResp = oInteresProspectosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Cod = oResp.MessageList[0].Codigo;
            }
            return Json(Cod, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspEliminarInteres(int CodigoInteres)
        {
            int Cod = 0;
            List<InteresProspectosDTO> list = new List<InteresProspectosDTO>();
            list.Add(new InteresProspectosDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoInteres = CodigoInteres,
                UsuarioCreacion = Commun.Usuario,
                Operation = Operation.Delete
            });
            ReqInteresProspectosDTO oReq = new ReqInteresProspectosDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespInteresProspectosDTO oResp = null;
            using (InteresProspectosLogic oInteresProspectosLogic = new InteresProspectosLogic())
            {
                oResp = oInteresProspectosLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Cod = oResp.MessageList[0].Codigo;
            }
            return Json(Cod, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region CRM APPSFIT

        //***inicio embudosventaplantilla
        public ActionResult CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla()
        {
            List<gimnasio_crm_1_embudosventaplantillaDTO> lista = null;

            gimnasio_crm_1_embudosventaplantillaDTO ogimnasio_crm_1_embudosventaplantillaDTO = new gimnasio_crm_1_embudosventaplantillaDTO();
            ogimnasio_crm_1_embudosventaplantillaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_1_embudosventaplantillaDTO.CodigoSede = Commun.CodigoSede;

            ReqFiltergimnasio_crm_1_embudosventaplantillaDTO oReq = new ReqFiltergimnasio_crm_1_embudosventaplantillaDTO()
            {
                FilterCase = filterCasegimnasio_crm_1_embudosventaplantilla.uspListar_gimnasio_crm_1_embudosventaplantilla,
                User = "appsfit",
                Item = ogimnasio_crm_1_embudosventaplantillaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListgimnasio_crm_1_embudosventaplantillaDTO oResp = null;

            using (gimnasio_crm_1_embudosventaplantillaLogic ogimnasio_crm_1_embudosventaplantillaLogic = new gimnasio_crm_1_embudosventaplantillaLogic())
            {
                oResp = ogimnasio_crm_1_embudosventaplantillaLogic.gimnasio_crm_1_embudosventaplantillaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspBuscar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO request)
        {
            gimnasio_crm_1_embudosventaplantillaDTO ogimnasio_crm_1_embudosventaplantillaDTO = new gimnasio_crm_1_embudosventaplantillaDTO();
            ogimnasio_crm_1_embudosventaplantillaDTO.CodigoEmbudoVenta = request.CodigoEmbudoVenta;
            ogimnasio_crm_1_embudosventaplantillaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_1_embudosventaplantillaDTO.CodigoSede = Commun.CodigoSede;

            ReqFiltergimnasio_crm_1_embudosventaplantillaDTO oReq = new ReqFiltergimnasio_crm_1_embudosventaplantillaDTO()
            {
                FilterCase = filterCasegimnasio_crm_1_embudosventaplantilla.uspBuscar_gimnasio_crm_1_embudosventaplantilla,
                Item = ogimnasio_crm_1_embudosventaplantillaDTO,
                User = "appsfit"
            };

            RespItemgimnasio_crm_1_embudosventaplantillaDTO oResp = null;
            using (gimnasio_crm_1_embudosventaplantillaLogic ogimnasio_crm_1_embudosventaplantillaLogic = new gimnasio_crm_1_embudosventaplantillaLogic())
            {
                oResp = ogimnasio_crm_1_embudosventaplantillaLogic.gimnasio_crm_1_embudosventaplantillaGetItem(oReq);
            }
            if (oResp.Success)
            {
                ogimnasio_crm_1_embudosventaplantillaDTO = oResp.Item;
            }

            return Json(ogimnasio_crm_1_embudosventaplantillaDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspRegistrar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO request)
        {
            int Codigo = 0;
            List<gimnasio_crm_1_embudosventaplantillaDTO> list = new List<gimnasio_crm_1_embudosventaplantillaDTO>();

            list.Add(new gimnasio_crm_1_embudosventaplantillaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                UsuarioCreacion = Commun.Usuario,
                Operation = Operation.Create
            });

            Reqgimnasio_crm_1_embudosventaplantillaDTO oReq = new Reqgimnasio_crm_1_embudosventaplantillaDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_1_embudosventaplantillaDTO oResp = null;
            using (gimnasio_crm_1_embudosventaplantillaLogic oReferidoLogic = new gimnasio_crm_1_embudosventaplantillaLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspActualizar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO Request)
        {
            int Codigo = 0;
            List<gimnasio_crm_1_embudosventaplantillaDTO> list = new List<gimnasio_crm_1_embudosventaplantillaDTO>();

            list.Add(new gimnasio_crm_1_embudosventaplantillaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEmbudoVenta = Request.CodigoEmbudoVenta,
                Nombre = Request.Nombre,
                Descripcion = Request.Descripcion,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.Update
            });

            Reqgimnasio_crm_1_embudosventaplantillaDTO oReq = new Reqgimnasio_crm_1_embudosventaplantillaDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_1_embudosventaplantillaDTO oResp = null;
            using (gimnasio_crm_1_embudosventaplantillaLogic oReferidoLogic = new gimnasio_crm_1_embudosventaplantillaLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspEliminar_gimnasio_crm_1_embudosventaplantilla(gimnasio_crm_1_embudosventaplantillaDTO Request)
        {
            int Codigo = 0;
            List<gimnasio_crm_1_embudosventaplantillaDTO> list = new List<gimnasio_crm_1_embudosventaplantillaDTO>();

            list.Add(new gimnasio_crm_1_embudosventaplantillaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEmbudoVenta = Request.CodigoEmbudoVenta,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.Delete
            });

            Reqgimnasio_crm_1_embudosventaplantillaDTO oReq = new Reqgimnasio_crm_1_embudosventaplantillaDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_1_embudosventaplantillaDTO oResp = null;
            using (gimnasio_crm_1_embudosventaplantillaLogic oReferidoLogic = new gimnasio_crm_1_embudosventaplantillaLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        //fin embudosventaplantilla
        //***inicio etapasplantilla
        public ActionResult CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO request)
        {
            List<gimnasio_crm_2_etapasplantillaDTO> lista = null;

            gimnasio_crm_2_etapasplantillaDTO ogimnasio_crm_2_etapasplantillaDTO = new gimnasio_crm_2_etapasplantillaDTO();
            ogimnasio_crm_2_etapasplantillaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_2_etapasplantillaDTO.CodigoSede = Commun.CodigoSede;
            ogimnasio_crm_2_etapasplantillaDTO.CodigoEmbudoVenta = request.CodigoEmbudoVenta;
            ReqFiltergimnasio_crm_2_etapasplantillaDTO oReq = new ReqFiltergimnasio_crm_2_etapasplantillaDTO()
            {
                FilterCase = filterCasegimnasio_crm_2_etapasplantilla.CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla,
                User = "appsfit",
                Item = ogimnasio_crm_2_etapasplantillaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListgimnasio_crm_2_etapasplantillaDTO oResp = null;

            using (gimnasio_crm_2_etapasplantillaLogic ogimnasio_crm_2_etapasplantillaLogic = new gimnasio_crm_2_etapasplantillaLogic())
            {
                oResp = ogimnasio_crm_2_etapasplantillaLogic.gimnasio_crm_2_etapasplantillaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspBuscar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO request)
        {
            gimnasio_crm_2_etapasplantillaDTO ogimnasio_crm_2_etapasplantillaDTO = new gimnasio_crm_2_etapasplantillaDTO();

            ogimnasio_crm_2_etapasplantillaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_2_etapasplantillaDTO.CodigoSede = Commun.CodigoSede;
            ogimnasio_crm_2_etapasplantillaDTO.CodigoEmbudoVenta = request.CodigoEmbudoVenta;
            ogimnasio_crm_2_etapasplantillaDTO.CodigoEtapa = request.CodigoEtapa;

            ReqFiltergimnasio_crm_2_etapasplantillaDTO oReq = new ReqFiltergimnasio_crm_2_etapasplantillaDTO()
            {
                FilterCase = filterCasegimnasio_crm_2_etapasplantilla.CentroEntrenamiento_uspBuscar_gimnasio_crm_2_etapasplantilla,
                Item = ogimnasio_crm_2_etapasplantillaDTO,
                User = "appsfit"
            };

            RespItemgimnasio_crm_2_etapasplantillaDTO oResp = null;
            using (gimnasio_crm_2_etapasplantillaLogic ogimnasio_crm_2_etapasplantillaLogic = new gimnasio_crm_2_etapasplantillaLogic())
            {
                oResp = ogimnasio_crm_2_etapasplantillaLogic.gimnasio_crm_2_etapasplantillaGetItem(oReq);
            }
            if (oResp.Success)
            {
                ogimnasio_crm_2_etapasplantillaDTO = oResp.Item;
            }

            return Json(ogimnasio_crm_2_etapasplantillaDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspRegistrar_gimnasio_crm_2_etapasplantilla(List<gimnasio_crm_2_etapasplantillaDTO> request)
        {
            int Codigo = 0;
            List<gimnasio_crm_2_etapasplantillaDTO> list = new List<gimnasio_crm_2_etapasplantillaDTO>();

            for (int i = 0; i < request.Count; i++)
            {
                list.Add(new gimnasio_crm_2_etapasplantillaDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoEmbudoVenta = request[i].CodigoEmbudoVenta,
                    CodigoEtapa = request[i].CodigoEtapa,
                    NombreEtapa = request[i].NombreEtapa,
                    OrdenEtapa = request[i].OrdenEtapa,
                    ProbabilidadNegocio = request[i].ProbabilidadNegocio,
                    NegocioEstancandose = request[i].NegocioEstancandose,
                    DiasAvisoInactividad = request[i].DiasAvisoInactividad,
                    UsuarioCreacion = Commun.Usuario,
                    UsuarioEdicion = Commun.Usuario,
                    Operation = request[i].CodigoEtapa == string.Empty || request[i].CodigoEtapa == null ? Operation.Create : Operation.Update
                });
            }

            Reqgimnasio_crm_2_etapasplantillaDTO oReq = new Reqgimnasio_crm_2_etapasplantillaDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_2_etapasplantillaDTO oResp = null;
            using (gimnasio_crm_2_etapasplantillaLogic oReferidoLogic = new gimnasio_crm_2_etapasplantillaLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspEliminar_gimnasio_crm_2_etapasplantilla(gimnasio_crm_2_etapasplantillaDTO request)
        {
            int Codigo = 0;
            List<gimnasio_crm_2_etapasplantillaDTO> list = new List<gimnasio_crm_2_etapasplantillaDTO>();

            list.Add(new gimnasio_crm_2_etapasplantillaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEmbudoVenta = request.CodigoEmbudoVenta,
                CodigoEtapa = request.CodigoEtapa,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.Delete
            });

            Reqgimnasio_crm_2_etapasplantillaDTO oReq = new Reqgimnasio_crm_2_etapasplantillaDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_2_etapasplantillaDTO oResp = null;
            using (gimnasio_crm_2_etapasplantillaLogic oReferidoLogic = new gimnasio_crm_2_etapasplantillaLogic())
            {
                oResp = oReferidoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }
        //fin etapasplantilla

        //inicio tratosprospecto
        public ActionResult CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            List<gimnasio_crm_3_tratosprospectoDTO> lista = null;

            gimnasio_crm_3_tratosprospectoDTO ogimnasio_crm_3_tratosprospectoDTO = new gimnasio_crm_3_tratosprospectoDTO();
            ogimnasio_crm_3_tratosprospectoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoSede = Commun.CodigoSede;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoEmbudoVenta = request.CodigoEmbudoVenta;
            ogimnasio_crm_3_tratosprospectoDTO.Vendedor = request.Vendedor ?? "Todos";
            ogimnasio_crm_3_tratosprospectoDTO.CodigoEstadoEtapa = request.CodigoEstadoEtapa;
            ogimnasio_crm_3_tratosprospectoDTO.Nombres = request.Nombres ?? String.Empty;

            ReqFiltergimnasio_crm_3_tratosprospectoDTO oReq = new ReqFiltergimnasio_crm_3_tratosprospectoDTO()
            {
                FilterCase = filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto,
                User = "appsfit",
                Item = ogimnasio_crm_3_tratosprospectoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListgimnasio_crm_3_tratosprospectoDTO oResp = null;

            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.gimnasio_crm_3_tratosprospectoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            gimnasio_crm_3_tratosprospectoDTO ogimnasio_crm_3_tratosprospectoDTO = new gimnasio_crm_3_tratosprospectoDTO();

            ogimnasio_crm_3_tratosprospectoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoSede = Commun.CodigoSede;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoEmbudoVenta = request.CodigoEmbudoVenta;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoTratoProspecto = request.CodigoTratoProspecto;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoOrigenProspecto = request.CodigoOrigenProspecto;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoProspecto = request.CodigoProspecto;

            ReqFiltergimnasio_crm_3_tratosprospectoDTO oReq = new ReqFiltergimnasio_crm_3_tratosprospectoDTO()
            {
                FilterCase = filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto,
                Item = ogimnasio_crm_3_tratosprospectoDTO,
                User = "appsfit"
            };

            RespItemgimnasio_crm_3_tratosprospectoDTO oResp = null;
            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.gimnasio_crm_3_tratosprospectoGetItem(oReq);
            }
            if (oResp.Success)
            {
                ogimnasio_crm_3_tratosprospectoDTO = oResp.Item;
            }

            return Json(ogimnasio_crm_3_tratosprospectoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto(gimnasio_crm_3_tratosprospectoDTO request)
        {

            gimnasio_crm_3_tratosprospectoDTO ogimnasio_crm_3_tratosprospectoDTO = new gimnasio_crm_3_tratosprospectoDTO();

            ogimnasio_crm_3_tratosprospectoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoSede = Commun.CodigoSede;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoOrigenProspecto = request.CodigoOrigenProspecto;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoProspecto = request.CodigoProspecto;

            ReqFiltergimnasio_crm_3_tratosprospectoDTO oReq = new ReqFiltergimnasio_crm_3_tratosprospectoDTO()
            {
                FilterCase = filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto,
                Item = ogimnasio_crm_3_tratosprospectoDTO,
                User = "appsfit"
            };

            RespItemgimnasio_crm_3_tratosprospectoDTO oResp = null;
            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.gimnasio_crm_3_tratosprospectoGetItem(oReq);
            }
            if (oResp.Success)
            {
                if (oResp.Item == null)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ogimnasio_crm_3_tratosprospectoDTO = oResp.Item;
                }

            }
            return Json(ogimnasio_crm_3_tratosprospectoDTO, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto(gimnasio_crm_3_tratosprospectoDTO request)
        {
            List<gimnasio_crm_3_tratosprospectoDTO> list = new List<gimnasio_crm_3_tratosprospectoDTO>();

            list.Add(new gimnasio_crm_3_tratosprospectoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEmbudoVenta = request.CodigoEmbudoVenta,
                CodigoEtapa = request.CodigoEtapa,
                CodigoTratoProspecto = request.CodigoTratoProspecto,
                NombreTrato = request.NombreTrato,
                CodigoEstadoEtapa = request.CodigoEstadoEtapa,
                FechaPrevistaCierre = request.FechaPrevistaCierre,
                CodigoMoneda = request.CodigoMoneda,
                Valor = request.Valor,
                Nota = request.Nota ?? String.Empty,
                CodigoProspecto = request.CodigoProspecto,
                CodigoOrigenProspecto = request.CodigoOrigenProspecto,
                Vendedor = request.Vendedor.Trim(),
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                Operation = request.CodigoTratoProspecto == string.Empty || request.CodigoTratoProspecto == null ? Operation.Create : Operation.Update
            });

            Reqgimnasio_crm_3_tratosprospectoDTO oReq = new Reqgimnasio_crm_3_tratosprospectoDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_3_tratosprospectoDTO oResp = null;
            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspActualizar_gimnasio_crm_3_tratosprospectoEstado(gimnasio_crm_3_tratosprospectoDTO request)
        {
            int Codigo = 0;
            List<gimnasio_crm_3_tratosprospectoDTO> list = new List<gimnasio_crm_3_tratosprospectoDTO>();

            list.Add(new gimnasio_crm_3_tratosprospectoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEmbudoVenta = request.CodigoEmbudoVenta,
                CodigoTratoProspecto = request.CodigoTratoProspecto,
                CodigoEstadoEtapa = request.CodigoEstadoEtapa,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.UpdateEstado
            });

            Reqgimnasio_crm_3_tratosprospectoDTO oReq = new Reqgimnasio_crm_3_tratosprospectoDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_3_tratosprospectoDTO oResp = null;
            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspActualizar_gimnasio_crm_3_tratosprospectoEtapa(gimnasio_crm_3_tratosprospectoDTO request)
        {
            List<gimnasio_crm_3_tratosprospectoDTO> list = new List<gimnasio_crm_3_tratosprospectoDTO>();

            list.Add(new gimnasio_crm_3_tratosprospectoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoEmbudoVenta = request.CodigoEmbudoVenta,
                CodigoEtapa = request.CodigoEtapa,
                CodigoTratoProspecto = request.CodigoTratoProspecto,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.UpdateEtapa
            });

            Reqgimnasio_crm_3_tratosprospectoDTO oReq = new Reqgimnasio_crm_3_tratosprospectoDTO()
            {
                List = list,
                User = "appsfit"
            };
            Respgimnasio_crm_3_tratosprospectoDTO oResp = null;
            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.ExecuteTransac(oReq);
            }

            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial(gimnasio_crm_3_tratosprospectoDTO request)
        {
            List<gimnasio_crm_3_tratosprospectoDTO> lista = null;

            gimnasio_crm_3_tratosprospectoDTO ogimnasio_crm_3_tratosprospectoDTO = new gimnasio_crm_3_tratosprospectoDTO();
            ogimnasio_crm_3_tratosprospectoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoSede = Commun.CodigoSede;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoEmbudoVenta = request.CodigoEmbudoVenta;
            ogimnasio_crm_3_tratosprospectoDTO.CodigoTratoProspecto = request.CodigoTratoProspecto;

            ReqFiltergimnasio_crm_3_tratosprospectoDTO oReq = new ReqFiltergimnasio_crm_3_tratosprospectoDTO()
            {
                FilterCase = filterCasegimnasio_crm_3_tratosprospecto.CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial,
                User = "appsfit",
                Item = ogimnasio_crm_3_tratosprospectoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListgimnasio_crm_3_tratosprospectoDTO oResp = null;

            using (gimnasio_crm_3_tratosprospectoLogic ogimnasio_crm_3_tratosprospectoLogic = new gimnasio_crm_3_tratosprospectoLogic())
            {
                oResp = ogimnasio_crm_3_tratosprospectoLogic.gimnasio_crm_3_tratosprospectoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        //fin tratosprospecto
        #endregion

        #region HISTORIAL RESERVAS
        public ActionResult historialreservas(string id)
        {
            return View();
        }

        public JsonResult uspListarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult uspListarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarSalaMaquinas_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult uspRegistrarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_SalaRepository oRepository = new CentroEntrenamiento_Presencial_SalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult uspListarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO request)
        {
            using (CentroEntrenamiento_Presencial_DisciplinaSalaRepository oRepository = new CentroEntrenamiento_Presencial_DisciplinaSalaRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarDisciplinaSala_Presencial(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult uspListarPresencial_HorarioClasesConfiguracionGestion(string Buscador_filtro, int CodigoSala, string fechaInicio, string fechaFin, string Hi, string Hf, bool Estado, int PageNumber)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }

            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    Buscador_filtro = Buscador_filtro,
                    CodigoSala = CodigoSala,
                    FechaHoraReservaInicio_filtro = fechaConsulta,
                    FechaHoraReservaFin_filtro = fechaConsultaFin,
                    Estado = Estado
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros(string Buscador_filtro, int CodigoSala, string fechaInicio, string fechaFin, string Hi, string Hf, bool Estado)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }

            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    Buscador_filtro = Buscador_filtro,
                    CodigoSala = CodigoSala,
                    FechaHoraReservaInicio_filtro = fechaConsulta,
                    FechaHoraReservaFin_filtro = fechaConsultaFin,
                    Estado = Estado
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return Json(oItem, JsonRequestBehavior.AllowGet);
        }

        //LISTA DE CLASES CREADAS EN TIEMPO REAL CLASES GRUPALES Y DE MAQUINAS
        public JsonResult CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES(int TipoSala, DateTime FechaHoraReservaInicio_filtro, DateTime FechaHoraReservaFin_filtro, int PageNumber)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.FechaHoraReservaInicio_filtro = FechaHoraReservaInicio_filtro;
                request.FechaHoraReservaFin_filtro = FechaHoraReservaFin_filtro;
                request.TipoSala = TipoSala;
                request.UsuarioCreacion = Commun.Usuario;

                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES(request, PageNumber), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros(int TipoSala, DateTime FechaHoraReservaInicio_filtro, DateTime FechaHoraReservaFin_filtro)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.FechaHoraReservaInicio_filtro = FechaHoraReservaInicio_filtro;
                request.FechaHoraReservaFin_filtro = FechaHoraReservaFin_filtro;
                request.TipoSala = TipoSala;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros(request), JsonRequestBehavior.AllowGet);
            }
        }


        //LISTA DE PERSONAS RESERVARON SE VISUALIZARA EN EL MODULO CLIENTES
        public JsonResult CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking()
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //LISTAR USUARIOS FIT
        public JsonResult CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion(string Buscador_filtro, int PageNumber)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    Buscador_filtro = Buscador_filtro
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros(string Buscador_filtro)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO ItemResponse = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    Buscador_filtro = Buscador_filtro
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                ItemResponse = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                ItemResponse = oResp.Item;
            }
            return Json(ItemResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarPresencial_HorarioClasesAsistenciasGestion(string CodigoHorarioClasesConfiguracionTiempoReal)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoHorarioClasesConfiguracionTiempoReal = CodigoHorarioClasesConfiguracionTiempoReal;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking(string CodigoHorarioClasesConfiguracion)
        {
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository oRepository = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository())
            {
                CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoHorarioClasesConfiguracion = CodigoHorarioClasesConfiguracion;
                return Json(oRepository.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking(request), JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        public ActionResult informesfit(string id)
        {
            return View();
        }

        #region INFORMES
        public JsonResult uspListarClientesPorTodos(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                        string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                        string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {
            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorTodos,
                Item = oClientesDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public JsonResult uspListarClientesPorTodos_NumeroRegistros(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorTodos_NumeroRegistros,
                Item = oClientesDTO,
                User = "appsfit"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_Todos"]);
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspListarClientesActivos(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesActivos,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        public List<ClientesDTO> uspListarClientesActivosEmail(DateTime FechaInicio, DateTime FechaFin, string gender)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = 0;
            oClientesDTO.Genero = gender;
            oClientesDTO.EdadRango1 = 0;
            oClientesDTO.EdadRango2 = 100;
            oClientesDTO.EstadoDeuda = 3;
            oClientesDTO.EstadoAsistencia = "Todos";
            oClientesDTO.Ubicaciones = "0";
            oClientesDTO.AsesorComercial = "Ninguno";
            oClientesDTO.Nombre = "";
            oClientesDTO.Apellidos = "";
            oClientesDTO.CodigoS = 0;
            oClientesDTO.DNI = "";
            oClientesDTO.Telefono = "";
            oClientesDTO.Celular = "";
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesActivos,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 999999,
                    PageNumber = 1,
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return lista;
        }



        public List<ClientesDTO> uspListarClientesInactivosEmail(DateTime FechaInicio, DateTime FechaFin, string gender)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = 0;
            oClientesDTO.Genero = gender;
            oClientesDTO.EdadRango1 = 0;
            oClientesDTO.EdadRango2 = 100;
            oClientesDTO.EstadoDeuda = 3;
            oClientesDTO.EstadoAsistencia = "Todos";
            oClientesDTO.Ubicaciones = "0";
            oClientesDTO.AsesorComercial = "Ninguno";
            oClientesDTO.Nombre = "";
            oClientesDTO.Apellidos = "";
            oClientesDTO.CodigoS = 0;
            oClientesDTO.DNI = "";
            oClientesDTO.Telefono = "";
            oClientesDTO.Celular = "";
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivos,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 999999,
                    PageNumber = 1,
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return lista;
        }


        public List<ClientesDTO> uspListarClientesPorVencerEmail(DateTime FechaInicio, DateTime FechaFin, string gender)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            //oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = 0;
            oClientesDTO.Genero = gender;
            oClientesDTO.EdadRango1 = 0;
            oClientesDTO.EdadRango2 = 100;
            oClientesDTO.EstadoDeuda = 3;
            oClientesDTO.EstadoAsistencia = "Todos";
            oClientesDTO.Ubicaciones = "0";
            oClientesDTO.AsesorComercial = "Ninguno";
            oClientesDTO.Nombre = "";
            oClientesDTO.Apellidos = "";
            oClientesDTO.CodigoS = 0;
            oClientesDTO.DNI = "";
            oClientesDTO.Telefono = "";
            oClientesDTO.Celular = "";
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorVencer,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 999999,
                    PageNumber = 1,
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return lista;
        }

        public JsonResult uspListarClientesActivos_NumeroRegistros(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            // oClientesDTO.CodigoPaquete = CodigoPaquete;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesActivos_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }

            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspListarClientesInactivos_informe(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                           string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                           string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivos,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarClientesInactivos_NumeroRegistros_informe(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesInactivos_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspListarClientesPorVencer(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                          string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                          string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos, int PageNumber)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 0);

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorVencer,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public JsonResult uspListarClientesPorVencer_NumeroRegistros(int CodigoTiempoMenbresia, string Genero, int EdadRango1, int EdadRango2, int EstadoDeuda,
                                                            string EstadoAsistencia, string Ubicaciones, string AsesorComercial, string Nombre,
                                                            string Apellidos, int CodigoS, string DNI, string Telefono, string Celular, DateTime FechaInicio, DateTime FechaFin, int CheckTodos)
        {

            DateTime Fi = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
            DateTime Ff = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 30, 0);

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.CodTiempoPaquete = CodigoTiempoMenbresia;
            oClientesDTO.Genero = Genero;
            oClientesDTO.EdadRango1 = EdadRango1;
            oClientesDTO.EdadRango2 = EdadRango2;
            oClientesDTO.EstadoDeuda = EstadoDeuda;
            oClientesDTO.EstadoAsistencia = EstadoAsistencia;
            oClientesDTO.Ubicaciones = Ubicaciones;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.Nombre = Nombre;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.CodigoS = CodigoS;
            oClientesDTO.DNI = DNI;
            oClientesDTO.Telefono = Telefono;
            oClientesDTO.Celular = Celular;
            oClientesDTO.FechaInicio = Fi;
            oClientesDTO.FechaFinal = Ff;
            oClientesDTO.CheckTodos = CheckTodos;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesPorVencer_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }


        //ASISTENCIAS
        public JsonResult ListarAsistenciaTodosFiltro(string fechaInicio, string fechaFin, string Hi, string Hf, string Buscador, int PageNumber)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }

            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();

            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = Commun.CodigoSede;
            oAsistenciaDTO.TipoPersona = "S";
            oAsistenciaDTO.FechaIngreso = fechaConsulta;
            oAsistenciaDTO.FechaFinalizo = fechaConsultaFin;
            oAsistenciaDTO.HoraInicioAsistencia = HoraInicio;
            oAsistenciaDTO.HoraFinAsistencia = HoraFin;
            oAsistenciaDTO.Nombres = Buscador;

            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.ListarAsistenciaTodosFiltro_Paginacion,
                Item = oAsistenciaDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarAsistenciaTodosFiltro_NumeroRegistros(string fechaInicio, string fechaFin, string Hi, string Hf, string Buscador)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);
            if (fechaInicio == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fechaInicio.Split('/')[2]), Convert.ToInt32(fechaInicio.Split('/')[1]), Convert.ToInt32(fechaInicio.Split('/')[0]), HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]), HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
            }

            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            oAsistenciaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = Commun.CodigoSede;
            oAsistenciaDTO.TipoPersona = "S";
            oAsistenciaDTO.FechaIngreso = fechaConsulta;
            oAsistenciaDTO.FechaFinalizo = fechaConsultaFin;
            oAsistenciaDTO.HoraInicioAsistencia = HoraInicio;
            oAsistenciaDTO.HoraFinAsistencia = HoraFin;
            oAsistenciaDTO.Nombres = Buscador;

            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                FilterCase = filterCaseAsistencia.uspListarAsistenciaTodosFiltro_NumeroRegistros,
                Item = oAsistenciaDTO,
                User = Commun.Usuario
            };
            RespItemAsistenciaDTO oResp = null;
            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAsistenciaDTO = oResp.Item;
                oAsistenciaDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarAsistenciaTodosFiltro_Paginacion"]);
            }
            return Json(oAsistenciaDTO, JsonRequestBehavior.AllowGet);
        }

        //INASISTENCIAS      
        public JsonResult uspListar_Socios_Inasistencias(string NumeroDiasAtras, string Vendedor, string Buscador, int PageNumber)
        {
            List<AsistenciaDTO> lista = new List<AsistenciaDTO>();
            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            int DiasAtras;
            if (NumeroDiasAtras == "")
            {
                DiasAtras = 1000;
            }
            else
            {
                try
                {
                    DiasAtras = Convert.ToInt32(NumeroDiasAtras);
                }
                catch
                {
                    DiasAtras = 1000;
                }
            }

            if (Vendedor == "")
            {
                Vendedor = "Todos";
            }
            oAsistenciaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = Commun.CodigoSede;
            oAsistenciaDTO.DiasAtras = DiasAtras;
            oAsistenciaDTO.Vendedor = Vendedor;
            oAsistenciaDTO.Nombres = Buscador;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                Item = oAsistenciaDTO,
                User = Commun.Usuario,
                FilterCase = filterCaseAsistencia.uspListar_Socios_Inasistencias_Paginacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListar_Socios_Inasistencias_NumeroRegistro(string NumeroDiasAtras, string Vendedor, string Buscador)
        {
            AsistenciaDTO oAsistenciaDTO = new AsistenciaDTO();
            int DiasAtras;
            if (NumeroDiasAtras == "")
            {
                DiasAtras = 1000;
            }
            else
            {
                try
                {
                    DiasAtras = Convert.ToInt32(NumeroDiasAtras);
                }
                catch
                {
                    DiasAtras = 1000;
                }
            }
            if (Vendedor == "")
            {
                Vendedor = "Todos";
            }
            oAsistenciaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAsistenciaDTO.CodigoSede = Commun.CodigoSede;
            oAsistenciaDTO.DiasAtras = DiasAtras;
            oAsistenciaDTO.Vendedor = Vendedor;
            oAsistenciaDTO.Nombres = Buscador;
            ReqFilterAsistenciaDTO oReq = new ReqFilterAsistenciaDTO()
            {
                Item = oAsistenciaDTO,
                User = Commun.Usuario,
                FilterCase = filterCaseAsistencia.uspListar_Socios_Inasistencias_NumeroRegistro

            };

            RespItemAsistenciaDTO oResp = null;

            using (AsistenciaLogic oAsistenciaLogic = new AsistenciaLogic())
            {
                oResp = oAsistenciaLogic.AsistenciaGetItem(oReq);
            }

            if (oResp.Success)
            {
                oAsistenciaDTO = oResp.Item;
                oAsistenciaDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListar_Socios_Inasistencias_NumeroRegistro"]);

            }
            return Json(oAsistenciaDTO, JsonRequestBehavior.AllowGet);
        }

        //DEUDAS
        public JsonResult uspListarMembresiasSociosAcuenta_Paginacion(string Vendedor, string fecha, string fechaFin, int PageNumber, string Buscador)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;
            if (fecha == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fecha.Split('/')[2]), Convert.ToInt32(fecha.Split('/')[1]), Convert.ToInt32(fecha.Split('/')[0]));
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]));
            }

            List<ContratoDTO> lista = new List<ContratoDTO>();
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            oContratoDTO.UsuarioCreacion = Vendedor;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.Nombres = Buscador;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasSociosAcuenta_Paginacion,
                Item = oContratoDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarMembresiasSociosAcuenta_NumeroRegistro(string Vendedor, string fecha, string fechaFin, string Buscador)
        {
            DateTime fechaConsulta;
            DateTime fechaConsultaFin;
            if (fecha == string.Empty)
            {
                fechaConsulta = DateTime.Now;
            }
            else
            {
                fechaConsulta = new DateTime(Convert.ToInt32(fecha.Split('/')[2]), Convert.ToInt32(fecha.Split('/')[1]), Convert.ToInt32(fecha.Split('/')[0]));
            }

            if (fechaFin == string.Empty)
            {
                fechaConsultaFin = DateTime.Now;
            }
            else
            {
                fechaConsultaFin = new DateTime(Convert.ToInt32(fechaFin.Split('/')[2]), Convert.ToInt32(fechaFin.Split('/')[1]), Convert.ToInt32(fechaFin.Split('/')[0]));
            }

            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            oContratoDTO.UsuarioCreacion = Vendedor;
            oContratoDTO.FechaInicio = fechaConsulta;
            oContratoDTO.FechaFin = fechaConsultaFin;
            oContratoDTO.Nombres = Buscador;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasSociosAcuenta_NumeroRegistro,
                Item = oContratoDTO,
                User = Commun.Usuario

            };

            RespItemContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }

            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
                oContratoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarMembresiasSociosAcuenta_Paginacion"]);

            }
            return Json(oContratoDTO, JsonRequestBehavior.AllowGet);
        }

        //CUOTAS
        public JsonResult uspVerMasClientesComprometidosPagosCuotas_Paginacion(int Tipo, string Vendedor, string Buscador, int PageNumber)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.Tipo = Tipo;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Nombre = Buscador;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspVerMasClientesComprometidosPagosCuotas_Paginacion,
                Item = oClientesDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<ClientesDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros(int Tipo, string Vendedor, string Buscador)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.Tipo = Tipo;
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Nombre = Buscador;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros"]);
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        //CUMPLEAÑOS

        public JsonResult uspNotificacionCumpleaniosSocios_Paginacion(int flag, int PageNumber)
        {
            List<ClientesDTO> lista = new List<ClientesDTO>();
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.flag = flag;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspNotificacionCumpleaniosSocios_Paginacion,
                User = Commun.Usuario,
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public JsonResult uspNotificacionCumpleaniosSocios_NumeroRegistros(int flag)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.flag = flag;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspNotificacionCumpleaniosSocios_NumeroRegistros,
                Item = oClientesDTO,
                User = Commun.Usuario
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
                oClientesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarGridCumpleanios_NumeroRegistros"]);
            }
            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }



        #endregion

        public ActionResult nutricion(string id)
        {
            return View();
        }

        #region NUTRICION

        public ActionResult uspListarControlMedidas_Paginacion(int CodigoCliente, int PageNumber)
        {
            List<ControlMedidasDTO> lista = null;
            ControlMedidasDTO oControlMedidasDTO = new ControlMedidasDTO();
            oControlMedidasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oControlMedidasDTO.CodigoSede = Commun.CodigoSede;
            oControlMedidasDTO.CodigoCliente = CodigoCliente;

            ReqFilterControlMedidasDTO oReq = new ReqFilterControlMedidasDTO()
            {
                FilterCase = filterCaseControlMedidas.uspListarControlMedidas_Paginacion,
                Item = oControlMedidasDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
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

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarControlMedida(
                int CodigoCliente,
                int CodigoMedida,
                DateTime FechaVencimiento,
                string AntecedentesMedicos,
                string Observacion,
                string ExpEntrenamiento,
                int Edad,
                decimal Estatura,
                decimal PesoCorporal,
                decimal PesoGraso,
                decimal PorcentajeGrasa,
                decimal IMC,
                decimal Cuello,
                decimal CirdelMom,
                decimal CirdelTorax,
                decimal Cintura,
                decimal CadA,
                decimal CadB,
                decimal MusloSuperior,
                decimal MusloBajo,
                decimal Pantorrilla,
                decimal BrazoNormal,
                decimal GrasaVisceral,
                decimal BrazoFlexionado,
                decimal AntreBrazo,
                decimal Munieca,
                string Comentario,
                DateTime FechaCreacion,
                string Accion,
                string hora,
                string minuto,
                string segundo,
                string milisegundo,
                int CodigoObjetivo,
                decimal Gluteos
            )
        {
            string mensaje = string.Empty;

            List<ControlMedidasDTO> list = new List<ControlMedidasDTO>();

            list.Add(new ControlMedidasDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoCliente = CodigoCliente,
                CodigoMedida = CodigoMedida,
                FechaVencimiento = FechaVencimiento,
                TiempoMedicion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(hora), Convert.ToInt32(minuto), Convert.ToInt32(segundo), Convert.ToInt32(milisegundo)),
                AntecedentesMedicos = AntecedentesMedicos,
                Observacion = Observacion,
                ExpEntrenamiento = ExpEntrenamiento,
                Edad = Edad,
                Estatura = Estatura,
                PesoCorporal = PesoCorporal,
                PesoGraso = PesoGraso,
                PorcentajeGrasa = PorcentajeGrasa,
                IMC = IMC,
                Cuello = Cuello,
                CirdelMom = CirdelMom,
                CirdelTorax = CirdelTorax,
                Cintura = Cintura,
                CadA = CadA,
                CadB = CadB,
                MusloSuperior = MusloSuperior,
                MusloBajo = MusloBajo,
                Pantorrilla = Pantorrilla,
                BrazoNormal = BrazoNormal,
                GrasaVisceral = GrasaVisceral,
                BrazoFlexionado = BrazoFlexionado,
                AntreBrazo = AntreBrazo,
                Munieca = Munieca,
                Comentario = Comentario,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                FechaCreacion = FechaCreacion,
                CodigoObjetivo = CodigoObjetivo,
                Gluteos = Gluteos,
                Operation = Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqControlMedidasDTO oReq = new ReqControlMedidasDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespControlMedidasDTO oResp = null;
            using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
            {
                oResp = oControlMedidasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ActualizarControlMedida(
                int CodigoCliente,
                int CodigoMedida,
                DateTime FechaVencimiento,
                string AntecedentesMedicos,
                string Observacion,
                string ExpEntrenamiento,
                int Edad,
                decimal Estatura,
                decimal PesoCorporal,
                decimal PesoGraso,
                decimal PorcentajeGrasa,
                decimal IMC,
                decimal Cuello,
                decimal CirdelMom,
                decimal CirdelTorax,
                decimal Cintura,
                decimal CadA,
                decimal CadB,
                decimal MusloSuperior,
                decimal MusloBajo,
                decimal Pantorrilla,
                decimal BrazoNormal,
                decimal GrasaVisceral,
                decimal BrazoFlexionado,
                decimal AntreBrazo,
                decimal Munieca,
                string Comentario,
                DateTime FechaCreacion,
                string Accion,
                string hora,
                string minuto,
                string segundo,
                string milisegundo,
                int CodigoObjetivo,
                decimal Gluteos
            )
        {
            string mensaje = string.Empty;

            List<ControlMedidasDTO> list = new List<ControlMedidasDTO>();

            list.Add(new ControlMedidasDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoCliente = CodigoCliente,
                CodigoMedida = CodigoMedida,
                FechaVencimiento = FechaVencimiento,
                TiempoMedicion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(hora), Convert.ToInt32(minuto), Convert.ToInt32(segundo), Convert.ToInt32(milisegundo)),
                AntecedentesMedicos = AntecedentesMedicos,
                Observacion = Observacion,
                ExpEntrenamiento = ExpEntrenamiento,
                Edad = Edad,
                Estatura = Estatura,
                PesoCorporal = PesoCorporal,
                PesoGraso = PesoGraso,
                PorcentajeGrasa = PorcentajeGrasa,
                IMC = IMC,
                Cuello = Cuello,
                CirdelMom = CirdelMom,
                CirdelTorax = CirdelTorax,
                Cintura = Cintura,
                CadA = CadA,
                CadB = CadB,
                MusloSuperior = MusloSuperior,
                MusloBajo = MusloBajo,
                Pantorrilla = Pantorrilla,
                BrazoNormal = BrazoNormal,
                GrasaVisceral = GrasaVisceral,
                BrazoFlexionado = BrazoFlexionado,
                AntreBrazo = AntreBrazo,
                Munieca = Munieca,
                Comentario = Comentario,
                UsuarioCreacion = Commun.Usuario,
                FechaCreacion = FechaCreacion,
                CodigoObjetivo = CodigoObjetivo,
                Gluteos = Gluteos,
                Operation = Accion == "A" ? Operation.Update : Operation.Create
            });

            ReqControlMedidasDTO oReq = new ReqControlMedidasDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespControlMedidasDTO oResp = null;
            using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
            {
                oResp = oControlMedidasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspBuscarControlMedidasPorCodigo(int CodigoCliente, int CodigoMedida)
        {
            ControlMedidasDTO oControlMedidasDTO = new ControlMedidasDTO();
            oControlMedidasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oControlMedidasDTO.CodigoSede = Commun.CodigoSede;
            oControlMedidasDTO.CodigoCliente = CodigoCliente;
            oControlMedidasDTO.CodigoMedida = CodigoMedida;
            ReqFilterControlMedidasDTO oReq = new ReqFilterControlMedidasDTO()
            {
                FilterCase = filterCaseControlMedidas.uspBuscarControlMedidasPorCodigo,
                Item = oControlMedidasDTO,
                User = Commun.Usuario
            };
            RespItemControlMedidasDTO oResp = null;
            using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
            {
                oResp = oControlMedidasLogic.ControlMedidasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oControlMedidasDTO = oResp.Item;
            }
            return Json(oControlMedidasDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarControlMedidas(int CodigoCliente, int CodigoMedida)
        {
            string mensaje = string.Empty;

            List<ControlMedidasDTO> list = new List<ControlMedidasDTO>();
            list.Add(new ControlMedidasDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoCliente = CodigoCliente,
                CodigoMedida = CodigoMedida,
                Operation = Operation.Delete
            });
            ReqControlMedidasDTO oReq = new ReqControlMedidasDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespControlMedidasDTO oResp = null;
            using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
            {
                oResp = oControlMedidasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Los datos han sido eliminados correctamente.";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SEGListarUsuario_NutricionistasActivos()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuario_NutricionistasActivos,
                User = Commun.Usuario,
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
                lista.Insert(0, new UsuarioDTO() { Codigo = 0, NombreCompleto = "Todos" });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarAgendaNutricionalGeneralHistorial_Paginacion(string UsuarioCreacion,
                   DateTime FechaInicio_Filtro, DateTime FechaFin_Filtro, string Buscador, int PageNumber)
        {

            List<ControlMedidasDTO> lista = null;
            ControlMedidasDTO oControlMedidasDTO = new ControlMedidasDTO();
            oControlMedidasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oControlMedidasDTO.CodigoSede = Commun.CodigoSede;
            oControlMedidasDTO.UsuarioCreacion = UsuarioCreacion;
            oControlMedidasDTO.FechaInicio_Filtro = FechaInicio_Filtro;
            oControlMedidasDTO.FechaFin_Filtro = FechaFin_Filtro;
            oControlMedidasDTO.Buscador = Buscador;

            ReqFilterControlMedidasDTO oReq = new ReqFilterControlMedidasDTO()
            {
                FilterCase = filterCaseControlMedidas.uspListarAgendaNutricionalGeneralHistorial_Paginacion,
                Item = oControlMedidasDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
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
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros(string UsuarioCreacion,
                   DateTime FechaInicio_Filtro, DateTime FechaFin_Filtro, string Buscador)
        {
            ControlMedidasDTO oControlMedidasDTO = new ControlMedidasDTO();
            oControlMedidasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oControlMedidasDTO.CodigoSede = Commun.CodigoSede;
            oControlMedidasDTO.UsuarioCreacion = UsuarioCreacion;
            oControlMedidasDTO.FechaInicio_Filtro = FechaInicio_Filtro;
            oControlMedidasDTO.FechaFin_Filtro = FechaFin_Filtro;
            oControlMedidasDTO.Buscador = Buscador;

            ReqFilterControlMedidasDTO oReq = new ReqFilterControlMedidasDTO()
            {
                FilterCase = filterCaseControlMedidas.uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros,
                Item = oControlMedidasDTO,
                User = Commun.Usuario
            };
            RespItemControlMedidasDTO oResp = null;
            using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
            {
                oResp = oControlMedidasLogic.ControlMedidasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oControlMedidasDTO = oResp.Item;
                oControlMedidasDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlMedidas_NumeroRegistros"]);
            }
            return Json(oControlMedidasDTO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarAgendaNutricionalGeneral_Paginacion(string UsuarioCreacion,
               DateTime FechaInicio_Filtro, DateTime FechaFin_Filtro, string Buscador, int TipoActividad, int PageNumber)
        {
            List<AgendaNutricionalDTO> lista = null;
            AgendaNutricionalDTO oAgendaNutricionalDTO = new AgendaNutricionalDTO();
            oAgendaNutricionalDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAgendaNutricionalDTO.CodigoSede = Commun.CodigoSede;
            oAgendaNutricionalDTO.UsuarioCreacion = UsuarioCreacion;
            oAgendaNutricionalDTO.FechaInicio_Filtro = FechaInicio_Filtro;
            oAgendaNutricionalDTO.FechaFin_Filtro = FechaFin_Filtro;
            oAgendaNutricionalDTO.Buscador = Buscador;
            oAgendaNutricionalDTO.TipoActividad = TipoActividad;

            ReqFilterAgendaNutricionalDTO oReq = new ReqFilterAgendaNutricionalDTO()
            {
                FilterCase = filterCaseAgendaNutricional.uspListarAgendaNutricionalGeneral_Paginacion,
                Item = oAgendaNutricionalDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber)
                }
            };

            RespListAgendaNutricionalDTO oResp = null;

            using (AgendaNutricionalLogic oAgendaNutricionalLogic = new AgendaNutricionalLogic())
            {
                oResp = oAgendaNutricionalLogic.AgendaNutricionalGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<AgendaNutricionalDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarAgendaNutricionalGeneral_NumeroRegistros(string UsuarioCreacion,
                   DateTime FechaInicio_Filtro, DateTime FechaFin_Filtro, string Buscador, int TipoActividad)
        {
            AgendaNutricionalDTO oAgendaNutricionalDTO = new AgendaNutricionalDTO();
            oAgendaNutricionalDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAgendaNutricionalDTO.CodigoSede = Commun.CodigoSede;
            oAgendaNutricionalDTO.UsuarioCreacion = UsuarioCreacion;
            oAgendaNutricionalDTO.FechaInicio_Filtro = FechaInicio_Filtro;
            oAgendaNutricionalDTO.FechaFin_Filtro = FechaFin_Filtro;
            oAgendaNutricionalDTO.Buscador = Buscador;
            oAgendaNutricionalDTO.TipoActividad = TipoActividad;

            ReqFilterAgendaNutricionalDTO oReq = new ReqFilterAgendaNutricionalDTO()
            {
                FilterCase = filterCaseAgendaNutricional.uspListarAgendaNutricionalGeneral_NumeroRegistros,
                Item = oAgendaNutricionalDTO,
                User = Commun.Usuario
            };
            RespItemAgendaNutricionalDTO oResp = null;
            using (AgendaNutricionalLogic oAgendaNutricionalLogic = new AgendaNutricionalLogic())
            {
                oResp = oAgendaNutricionalLogic.AgendaNutricionalGetItem(oReq);
            }
            if (oResp.Success)
            {
                oAgendaNutricionalDTO = oResp.Item;
                oAgendaNutricionalDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAgendaNutricionalGeneral_NumeroRegistros"]);
            }

            return Json(oAgendaNutricionalDTO, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspValidarHorariosOcupadosCitasNutricionales(string UsuarioCreacion, DateTime FechaInicio_Filtro)
        {
            List<AgendaNutricionalDTO> lista = null;
            AgendaNutricionalDTO oAgendaNutricionalDTO = new AgendaNutricionalDTO();
            oAgendaNutricionalDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oAgendaNutricionalDTO.CodigoSede = Commun.CodigoSede;
            oAgendaNutricionalDTO.UsuarioCreacion = UsuarioCreacion;
            oAgendaNutricionalDTO.HoraInicio = FechaInicio_Filtro;

            ReqFilterAgendaNutricionalDTO oReq = new ReqFilterAgendaNutricionalDTO()
            {
                FilterCase = filterCaseAgendaNutricional.uspValidarHorariosOcupadosCitasNutricionales,
                Item = oAgendaNutricionalDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(1)
                }
            };

            RespListAgendaNutricionalDTO oResp = null;

            using (AgendaNutricionalLogic oAgendaNutricionalLogic = new AgendaNutricionalLogic())
            {
                oResp = oAgendaNutricionalLogic.AgendaNutricionalGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<AgendaNutricionalDTO>();
                lista = oResp.List;
            }

            if (lista.Count == 0)
            {
                return Json("Ninguno", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(lista, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GuardarAgendaNutricional(int Codigo, int CodigoSocio,
            string HoraInicio, string Asunto, int Estado, string UsuarioCreacion, int TipoActividad)

        {
            string mensaje = string.Empty;

            List<AgendaNutricionalDTO> list = new List<AgendaNutricionalDTO>();
            string[] sHi = HoraInicio.Split('|');
            DateTime hInicio = new DateTime(Convert.ToInt32(sHi[0]), Convert.ToInt32(sHi[1]), Convert.ToInt32(sHi[2]), Convert.ToInt32(sHi[3]), Convert.ToInt32(sHi[4]), Convert.ToInt32(sHi[5]));

            list.Add(new AgendaNutricionalDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Codigo = Codigo,
                CodigoSocio = CodigoSocio,
                HoraInicio = hInicio,
                Asunto = Asunto,
                Estado = Estado,
                TipoActividad = TipoActividad,
                UsuarioCreacion = UsuarioCreacion,
                Operation = Operation.Create
            });

            ReqAgendaNutricionalDTO oReq = new ReqAgendaNutricionalDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespAgendaNutricionalDTO oResp = null;
            using (AgendaNutricionalLogic oAgendaNutricionalLogic = new AgendaNutricionalLogic())
            {
                oResp = oAgendaNutricionalLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }




        #endregion

        #region ESTADISTICAS
        public ActionResult estadisticas(string id)
        {
            return View();
        }

        public JsonResult uspEstadisticaDashboar(DateTime FechaInicio, DateTime FechaFinal)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = FechaInicio;
            oClientesDTO.FechaFinal = FechaFinal;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspEstadisticaDashboar,
                Item = oClientesDTO,
                User = "appsfit"
            };
            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return Json(oClientesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult estadisticasventas(string id)
        {
            return View();
        }

        public JsonResult uspListarEstadistica_VentasDiarios(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = string.Empty;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspListarEstadistica_VentasDiarios,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = string.Empty;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspEstadisticaVentasPorTiempoMembresia_Ventas(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = AsesorComercial;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaVentasPorTiempoMembresia_Ventas,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspEstadisticaMatriculadosPorNombrePlan(DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = AsesorComercial;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaMatriculadosPorNombrePlan,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspEstadisticaVentasPorDiaSemana_Ventas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = string.Empty;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaVentasPorDiaSemana_Ventas,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.OrderBy(x => x.Dia).ToList();
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspEstadisticaVentasPorDia_Ventas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = string.Empty;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaVentasPorDia_Ventas,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List.OrderBy(x => x.Dia).ToList();
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspEstadisticaVentasPorHoras_Ventas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = string.Empty;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaVentasPorHoras_Ventas,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public JsonResult uspEstadisticaVentasPorFormaPago_Ventas(DateTime FechaInicio, DateTime FechaFin)
        {
            List<VentasDTO> lista = new List<VentasDTO>();
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;
            oVentasDTO.AsesorComercial = string.Empty;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspEstadisticaVentasPorFormaPago_Ventas,
                Item = oVentasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oVentasLogic = new VentasLogic())
            {
                oResp = oVentasLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult estadisticasmarketing(string id)
        {
            return View();
        }

        public JsonResult uspListarEncuestaEstadisticaObjetivos(DateTime fechaInicio, DateTime fechaFin)
        {
            List<EncuestaNuevo1DTO> lista = null;
            EncuestaNuevo1DTO oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();

            oEncuestaNuevo1DTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oEncuestaNuevo1DTO.CodigoSede = Commun.CodigoSede;
            oEncuestaNuevo1DTO.fehaInicio = fechaInicio;
            oEncuestaNuevo1DTO.fehaFin = fechaFin;

            ReqFilterEncuestaNuevo1DTO oReq = new ReqFilterEncuestaNuevo1DTO()
            {
                FilterCase = filterCaseEncuestaNuevo1.uspListarEncuestaEstadisticaObjetivos,
                User = "appsfit",
                Item = oEncuestaNuevo1DTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListEncuestaNuevo1DTO oResp = null;

            using (EncuestaNuevo1Logic oEncuestaNuevo1Logic = new EncuestaNuevo1Logic())
            {
                oResp = oEncuestaNuevo1Logic.EncuestaNuevo1GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarEstadisticaComoConocioGym(DateTime fechaInicio, DateTime fechaFin)
        {
            List<EncuestaNuevo1DTO> lista = null;
            EncuestaNuevo1DTO oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();

            oEncuestaNuevo1DTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oEncuestaNuevo1DTO.CodigoSede = Commun.CodigoSede;
            oEncuestaNuevo1DTO.fehaInicio = fechaInicio;
            oEncuestaNuevo1DTO.fehaFin = fechaFin;

            ReqFilterEncuestaNuevo1DTO oReq = new ReqFilterEncuestaNuevo1DTO()
            {
                FilterCase = filterCaseEncuestaNuevo1.uspListarEstadisticaComoConocioGym,
                User = "appsfit",
                Item = oEncuestaNuevo1DTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListEncuestaNuevo1DTO oResp = null;

            using (EncuestaNuevo1Logic oEncuestaNuevo1Logic = new EncuestaNuevo1Logic())
            {
                oResp = oEncuestaNuevo1Logic.EncuestaNuevo1GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarEstadisticaInteres(DateTime fechaInicio, DateTime fechaFin)
        {
            List<EncuestaNuevo1DTO> lista = null;
            EncuestaNuevo1DTO oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();

            oEncuestaNuevo1DTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oEncuestaNuevo1DTO.CodigoSede = Commun.CodigoSede;
            oEncuestaNuevo1DTO.fehaInicio = fechaInicio;
            oEncuestaNuevo1DTO.fehaFin = fechaFin;

            ReqFilterEncuestaNuevo1DTO oReq = new ReqFilterEncuestaNuevo1DTO()
            {
                FilterCase = filterCaseEncuestaNuevo1.uspListarEstadisticaInteres,
                User = "appsfit",
                Item = oEncuestaNuevo1DTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListEncuestaNuevo1DTO oResp = null;

            using (EncuestaNuevo1Logic oEncuestaNuevo1Logic = new EncuestaNuevo1Logic())
            {
                oResp = oEncuestaNuevo1Logic.EncuestaNuevo1GetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarClientesHombres_MujeresEstadistica(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesHombres_MujeresEstadistica,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarTotalDia_TardeEstadistica(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarTotalDia_TardeEstadistica,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspListarEstadistica_AsistenciaporRangoEdades(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarEstadistica_AsistenciaporRangoEdades,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarEstadistica_AsistenciaporHorarios(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarEstadistica_AsistenciaporHorarios,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarEstadistica_AsistenciaporSemana(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarEstadistica_AsistenciaporSemana,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public JsonResult uspListarClientesAsistenciaEfectiva_Estadistica(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();

            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarClientesAsistenciaEfectiva_Estadistica,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarEstadisticaTipoContrato(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = fechaInicio;
            oClientesDTO.FechaFinaliza = fechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarEstadisticaTipoContrato,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarEstadisticaTiempoMenbresia(DateTime FechaInicio, DateTime FechaFin)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.FechaInicio = FechaInicio;
            oClientesDTO.FechaFinal = FechaFin;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarEstadisticaTiempoMenbresia,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region METAS
        public ActionResult metascomerciales(string id)
        {
            return View();
        }

        public ActionResult uspListarHistorialMetas(int CodigoMeta, DateTime FechaInicio, DateTime FechaFin, int NumeroPagina)
        {
            List<MetasDTO> lista = new List<MetasDTO>();
            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarHistorialMetas,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = CodigoMeta,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(NumeroPagina),
                    PageRecords = 0
                }
            };
            RespListMetasDTO oResp = null;
            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspBuscarMetaVendedorPorMesActual(int CodigoMeta)
        {
            MetasDTO oMetasDTO = new MetasDTO();
            oMetasDTO.CodigoEntidadNegocio = Commun.CodigoUnidadNegocio;
            oMetasDTO.CodigoSede = Commun.CodigoSede;
            oMetasDTO.CodigoMeta = CodigoMeta;
            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspBuscarMetaVendedorPorMesActual,
                Item = oMetasDTO,
                User = Commun.Usuario
            };
            RespItemMetasDTO oResp = null;
            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oMetasDTO = oResp.Item;
            }
            return Json(oMetasDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarMetasDetalle_VentasAvance(int CodigoMeta)
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarMetasDetalle_VentasAvance,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = CodigoMeta
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarMetasDetalle_EstadisticaVenta(int CodigoMeta)
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarMetasDetalle_EstadisticaVenta,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = CodigoMeta
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarMetricas_ConversionLeads_Totales()
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarMetricas_ConversionLeads_Totales,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarMetasDetalle_ConversionLeads_Totales(int CodigoMeta)
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarMetasDetalle_ConversionLeads_Totales,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = CodigoMeta
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarMetasDetalle_CuadroComisiones(int CodigoMeta)
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarMetasDetalle_CuadroComisiones,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = CodigoMeta
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //PRODUCTIVIDAD
        public ActionResult uspBuscarMetaVendedorPorCodigo(int CodigoMeta)
        {
            MetasDTO oMetasDTO = new MetasDTO();
            oMetasDTO.CodigoEntidadNegocio = Commun.CodigoUnidadNegocio;
            oMetasDTO.CodigoSede = Commun.CodigoSede;
            oMetasDTO.CodigoMeta = CodigoMeta;
            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspBuscarMetaVendedorPorCodigo,
                Item = oMetasDTO,
                User = Commun.Usuario
            };
            RespItemMetasDTO oResp = null;
            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oMetasDTO = oResp.Item;
            }
            return Json(oMetasDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarProductividad_AreaComercial(int CodigoMeta, DateTime FechaInicio, DateTime FechaFin)
        {
            List<MetasDTO> lista = null;

            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.uspListarProductividad_AreaComercial,
                Item = new MetasDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = CodigoMeta,
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin
                },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListMetasDTO oResp = null;

            using (MetasLogic oMetasLogic = new MetasLogic())
            {
                oResp = oMetasLogic.MetasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        #endregion

        public ActionResult marketing(string id)
        {
            DateTime now = DateTime.Now;
            DateTime firtsDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = firtsDay.AddMonths(1).AddDays(-1);
           
            ViewBag.firtsDay = firtsDay.ToString("yyyy-MM-dd");
            ViewBag.lastDay = lastDay.ToString("yyyy-MM-dd");

            //Debug.WriteLine("hola todo el mundo nose");
            return View();
        }

        #region marketing

        public ConfiguracionDTO getConfiguracion()
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Codigo = Commun.CodigoSede;
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = Commun.Usuario,
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.BuscarPorCodigo
            };
            RespItemConfiguracionDTO oResp = null;
            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }
            return oConfiguracionDTO;
        }

        [ValidateInput(false)]

        public JsonResult marketing_enviarcorreo(int emailgroup_to_personas, string email_asunto, string email_mensajecorreo, string FechaInicio, string FechaFin, string gender, string addressee = "")
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (emailgroup_to_personas == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio un grupo de personas.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (email_asunto == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un asunto.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (email_mensajecorreo == "")
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un mensaje.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (gender == "")
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un sexo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }


            List<ClientesDTO> lista = new List<ClientesDTO>();

            if (emailgroup_to_personas == 1)
            {
                //activos
                lista = uspListarClientesActivosEmail(DateTime.Parse(FechaInicio), DateTime.Parse(FechaFin), gender);
            }
            else if (emailgroup_to_personas == 2)
            {
                //por vencer
                lista = uspListarClientesPorVencerEmail(DateTime.Parse(FechaInicio), DateTime.Parse(FechaFin), gender);
            }
            else if (emailgroup_to_personas == 3)
            {
                //vencidos
                lista = uspListarClientesInactivosEmail(DateTime.Parse(FechaInicio), DateTime.Parse(FechaFin), gender);
            }

            else if (emailgroup_to_personas == 100)
            {
                //prueba
                if (!Commun.IsValidEmail(addressee))
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "Es obligatorio el destinatario.";
                    _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                    validadorParametros = false;
                }


                if (!validadorParametros)
                {
                    return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
                }
                //add addressee
                ClientesDTO oClientesDTO = new ClientesDTO();
                oClientesDTO.Correo = addressee;
                oClientesDTO.Nombre = addressee;
                lista.Add(oClientesDTO);
            }

            try
            {
                string path = Server.MapPath("~/Content/assets/images/");
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                HttpFileCollectionBase files = Request.Files;
                Random random = new Random();
                List<ImageSend> filesArray = new List<ImageSend>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string extension = Path.GetExtension(file.FileName);
                    string name = random.Next() + extension;
                    file.SaveAs(path + name);
                    filesArray.Add(new ImageSend(name: name, origin: file.FileName));
                }


                ResponseModel resp = new ResponseModel();


                // resp = SendEmailMassive(lista, email_asunto, email_mensajecorreo, filesArray);


                if (lista.Count > 30)
                {
                    var newList = SplitList(lista, 30);
                    for (int i = 0; i < newList.Count; i++)
                    {
                        resp = SendEmailMassive(newList[i], email_asunto, email_mensajecorreo, filesArray);
                    }
                }
                else
                {
                    resp = SendEmailMassive(lista, email_asunto, email_mensajecorreo, filesArray);
                }




                //remove files
                foreach (ImageSend a in filesArray)
                {
                    Commun.removeFile(path + a.Name);
                }

                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = resp.Message1;
                _objResponseModel.Message2 = resp.Message2;


            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }





       



        private ResponseModel SendEmailMassive(List<ClientesDTO> person, string email_asunto, string email_mensajecorreo, List<ImageSend> attachments)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            // ConfigurationManager.AppSettings["RecordNumForPage_ListarConfiguracion_apfitness_NumeroRegistros"]
            try
            {
                String servidor = "mail.saavedraromoabogados.pe";
                int puerto = 587;

                String GmailUser = "noreply@saavedraromoabogados.pe";
                String GmailPass = "GmoV&yOTI38";
                var config = getConfiguracion();
                if (!String.IsNullOrEmpty(config.EmailHost) && !String.IsNullOrEmpty(config.EmailPort) && !String.IsNullOrEmpty(config.EmailUser) && !String.IsNullOrEmpty(config.EmailKey))
                {
                    servidor = config.EmailHost;
                    puerto = Int32.Parse(config.EmailPort);
                    GmailUser = config.EmailUser;
                    GmailPass = config.EmailKey;

                }

                MimeMessage mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress(config.NombreComercial ?? "Appsfit", GmailUser));
                //mensaje.To.Add(new MailboxAddress(person.Nombre, person.Correo)); 

                InternetAddressList list = new InternetAddressList();

                foreach (ClientesDTO c in person)
                {
                    bool valid = Commun.IsValidEmail(c.Correo);
                    if (valid)
                    {
                        list.Add(new MailboxAddress(c.Nombre, c.Correo));
                    }
                }
               
               // mensaje.To.Add(new MailboxAddress("dev", GmailUser));
                mensaje.Cc.AddRange(list);
               // mensaje.Bcc.AddRange(list);
               

                mensaje.Subject = email_asunto;

                BodyBuilder CuerpoMensaje = new BodyBuilder();
                string path = Server.MapPath("~/Content/assets/images/");
                foreach (ImageSend a in attachments)
                {
                    CuerpoMensaje.Attachments.Add(path + a.Name);
                }

                CuerpoMensaje.HtmlBody = email_mensajecorreo;

                mensaje.Body = CuerpoMensaje.ToMessageBody();
                SmtpClient ClienteSmtp = new SmtpClient();
                ClienteSmtp.CheckCertificateRevocation = false;
                ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                ClienteSmtp.Authenticate(GmailUser, GmailPass);
                ClienteSmtp.Send(mensaje);
                ClienteSmtp.Disconnect(true);


                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "correo enviado correctamente.";


            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return _objResponseModel;
        }



        private ResponseModel SendEmailSimple(ClientesDTO person, string email_asunto, string email_mensajecorreo)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                String servidor = null;
                int puerto = 0;

                String GmailUser = null;
                String GmailPass = null;
                var config = getConfiguracion();
                if (!String.IsNullOrEmpty(config.EmailHost) && !String.IsNullOrEmpty(config.EmailPort) && !String.IsNullOrEmpty(config.EmailUser) && !String.IsNullOrEmpty(config.EmailKey))
                {
                    servidor = config.EmailHost;
                    puerto = Int32.Parse(config.EmailPort);
                    GmailUser = config.EmailUser;
                    GmailPass = config.EmailKey;

                    MimeMessage mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress(config.NombreComercial, GmailUser));
                    mensaje.To.Add(new MailboxAddress(person.Nombre, person.Correo));
                    mensaje.Subject = email_asunto;
                    BodyBuilder CuerpoMensaje = new BodyBuilder();


                    CuerpoMensaje.HtmlBody = email_mensajecorreo;
                    mensaje.Body = CuerpoMensaje.ToMessageBody();


                    SmtpClient ClienteSmtp = new SmtpClient();
                    ClienteSmtp.CheckCertificateRevocation = false;
                    ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                    ClienteSmtp.Authenticate(GmailUser, GmailPass);
                    ClienteSmtp.Send(mensaje);
                    ClienteSmtp.Disconnect(true);

                    _objResponseModel.Status = 0;
                    _objResponseModel.Message1 = "correo enviado correctamente.";

                }
                else
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "No se se envió el correo, tienes que tener una cuenta personalizada, comunícate con centro de ayuda.";
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return _objResponseModel;
        }




        private ResponseModel SendEmailSimpleToAccount(ClientesDTO person, string email_asunto, string email_mensajecorreo,List<ImageSend> attachments)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            try
            {
                String servidor = null;
                int puerto = 0;

                String GmailUser = null;
                String GmailPass = null;
                var config = getConfiguracion();
                if (!String.IsNullOrEmpty(config.EmailHost) && !String.IsNullOrEmpty(config.EmailPort) && !String.IsNullOrEmpty(config.EmailUser) && !String.IsNullOrEmpty(config.EmailKey))
                {
                    servidor = config.EmailHost;
                    puerto = Int32.Parse(config.EmailPort);
                    GmailUser = config.EmailUser;
                    GmailPass = config.EmailKey;



                    MimeMessage mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress(config.NombreComercial, GmailUser));
                    mensaje.To.Add(new MailboxAddress(person.Nombre, person.Correo)); 

                    //InternetAddressList list = new InternetAddressList();

                    //foreach (ClientesDTO c in person)
                    //{
                    //    bool valid = Commun.IsValidEmail(c.Correo);
                    //    if (valid)
                    //    {
                    //        list.Add(new MailboxAddress(c.Nombre, c.Correo));
                    //    }
                    //}
                    //mensaje.To.AddRange(list);

                    mensaje.Subject = email_asunto;

                    BodyBuilder CuerpoMensaje = new BodyBuilder();
                    string path = Server.MapPath("~/Content/assets/pdf/");
                    foreach (ImageSend a in attachments)
                    {
                        CuerpoMensaje.Attachments.Add(path + a.Name);
                    }

                    CuerpoMensaje.HtmlBody = email_mensajecorreo;

                    mensaje.Body = CuerpoMensaje.ToMessageBody();
                    SmtpClient ClienteSmtp = new SmtpClient();
                    ClienteSmtp.CheckCertificateRevocation = false;
                    ClienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                    ClienteSmtp.Authenticate(GmailUser, GmailPass);
                    ClienteSmtp.Send(mensaje);
                    ClienteSmtp.Disconnect(true);


                    //remove pdf
                    foreach (ImageSend a in attachments)
                    {
                        Commun.removeFile(path + a.Name);
                    }


                    _objResponseModel.Status = 0;
                    _objResponseModel.Message1 = "correo enviado correctamente.";

                }
                else
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "No se se envió el correo, tienes que tener una cuenta personalizada, comunícate con centro de ayuda.";
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return _objResponseModel;
        }

        //remove Pdf 

        public JsonResult removePDF(string name)
        {
            string path = Server.MapPath("~/Content/assets/pdf/");
            ResponseModel responseModel= new ResponseModel();
            bool validadorParametros = true;

            if (name == String.Empty)
            {
                responseModel.Status = 1;
                responseModel.Message2 = "Nombre del archivo es requerido";
                validadorParametros = false;
            }


            if (!validadorParametros)
            {
                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            try
            {
                
             bool rev =  Commun.removeFile(path + name);
                if(rev)
                {
                    responseModel.Status = 0;
                    responseModel.Message2 = "Archivo eliminado";
                }
                else
                {
                    responseModel.Status = 1;
                    responseModel.Message2 = "No se pudo eliminar el archivo.";
                }
                
            }
            catch(Exception ex)
            {
                responseModel.Status = 2;
                responseModel.Message1 = ex.Message;
                responseModel.Message2 = "No se pudo eliminar el archivo.";
            }
            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }



        //send email clausula TC
        public JsonResult sendEmailTC(string clausula, string to)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (clausula == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio un clausula.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (to == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un correo electronico destino.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (!Commun.IsValidEmail(to))
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un correo electronico destino.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }


            ClientesDTO clientesDTO = new ClientesDTO();
            clientesDTO.Nombre = "amador";
            clientesDTO.Correo = to;
            try
            {

                var resp = SendEmailSimple(clientesDTO, "clausula", clausula);
                _objResponseModel.Status = resp.Status;
                _objResponseModel.Message1 = resp.Message1;
                _objResponseModel.Message2 = clausula;

            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }


        //send pdf venta
        public JsonResult sendEmailVentaPdf(string name, string to,string asunto  = "Ticket")
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (name == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio un nombre del archivo." +name;
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (to == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un correo electronico destino.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (!Commun.IsValidEmail(to))
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un correo electronico destino.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }


            ClientesDTO clientesDTO = new ClientesDTO();
            clientesDTO.Nombre = "amador";
            clientesDTO.Correo = to;
            try
            {

                List<ImageSend> filesArray = new List<ImageSend>();
                filesArray.Add(new ImageSend(name: name, origin: name));

                var resp = SendEmailSimpleToAccount(clientesDTO, asunto,"" ,filesArray);
                _objResponseModel.Status = resp.Status;
                _objResponseModel.Message1 = resp.Message1;
               

            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Message2 = "Error de envio de correo.";

            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }



        public ActionResult actualizarConfigHostEmail(string host, string port, string user, string pass)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.EmailHost = host;
            oConfiguracionDTO.EmailPort = port;
            oConfiguracionDTO.EmailUser = user;
            oConfiguracionDTO.EmailKey = pass;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.uspActualizarConfiguracion_HostEnvioEmail;

            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            lista.Add(oConfiguracionDTO);

            ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
            {
                List = lista,
                User = "Admin"
            };
            RespConfiguracionDTO oResp = null;

            ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic();
            oResp = oConfiguracionLogic.ExecuteTransac(oReq);

            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult pagospersonal(string id)
        {
            return View();
        }

        #region PERSONALPAGOS

        public ActionResult ListarAsistenciaPersonalAdministrativo(PersonalAsistenciaDTO request, int PageNumber, int PageRecords)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = Convert.ToUInt32(PageRecords)
                },
                FilterCase = filterCasePersonalAsistencia.AsistenciaPersonalAdministrativo
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oLogic = new PersonalAsistenciaLogic())
            {
                oResp = oLogic.PersonalAsistenciaGetList(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarInformeProfesores(PersonalAsistenciaDTO request, int PageNumber, int PageRecords)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            List<PersonalAsistenciaDTO> list = new List<PersonalAsistenciaDTO>();
            list.Add(request);
            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = Commun.Usuario,
                FilterCase = filterCasePersonalAsistencia.AsistenciaProfesores,
                Paging = new Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = Convert.ToUInt32(PageRecords)
                }
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oPersonalAsistenciaLogic = new PersonalAsistenciaLogic())
            {
                oResp = oPersonalAsistenciaLogic.PersonalAsistenciaGetList(oReq);
            }
            if (oResp.Success)
            {
                //Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarAsistenciaPersonalAdministrativoResumen(PersonalAsistenciaDTO request, int PageNumber, int PageRecords)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            ReqFilterPersonalAsistenciaDTO oReq = new ReqFilterPersonalAsistenciaDTO()
            {
                Item = request,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = Convert.ToUInt32(PageRecords)
                },
                FilterCase = filterCasePersonalAsistencia.ListarAsistenciaPersonalAdministrativoResumen
            };

            RespListPersonalAsistenciaDTO oResp = null;
            using (PersonalAsistenciaLogic oLogic = new PersonalAsistenciaLogic())
            {
                oResp = oLogic.PersonalAsistenciaGetList(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region PERMISOS

        public ActionResult SEGListarPerfilMenu()
        {
            CentroEntrenamiento_MenuPlantillaDTO request = new CentroEntrenamiento_MenuPlantillaDTO();
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.CodigoPerfil = Commun.CodigoPerfil;

            List<CentroEntrenamiento_MenuPlantillaDTO> lista = null;

            ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_MenuPlantilla.SEGListarPerfilMenu,
                User = "appsfit",
                Item = request,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_MenuPlantillaDTO oResp = null;

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

        public ActionResult uspUpdateEstadoMembresias()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                int flag = 1;// --1 = activaciones membresias, 2 = citas caidas
                return Json(oRepository.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(CodigoUnidadNegocio, CodigoSede, flag, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspUpdateCitasCaidas()
        {
            using (ModuloCheckingRepository oRepository = new ModuloCheckingRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodigoSede = Commun.CodigoSede;
                string User = Commun.Usuario;
                int flag = 2;// --1 = activaciones membresias, 2 = citas caidas
                return Json(oRepository.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(CodigoUnidadNegocio, CodigoSede, flag, User), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspValidarPagosClientes_AdFitness()
        {
            int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            int CodigoSede = Commun.CodigoSede;
            AdFitnessAtencionAlClienteDTO oAdFitnessAtencionAlClienteDTO = new AdFitnessAtencionAlClienteDTO();

            using (AdFitnessAtencionAlClienteLogic oAdFitnessAtencionAlClienteLogic = new AdFitnessAtencionAlClienteLogic())
            {
                oAdFitnessAtencionAlClienteDTO = oAdFitnessAtencionAlClienteLogic.uspValidarPagosClientes_AdFitness(CodigoUnidadNegocio, CodigoSede);
            }

            return Json(oAdFitnessAtencionAlClienteDTO, JsonRequestBehavior.AllowGet);
        }

        #region Finanzas

        /**
         * Author: Angel Rojas
         * Date: 18/01/2021 11:14
         * Update: 20/01/2021 17:20
         */

        public ActionResult finanzas(string id)
        {
            return View();
        }

        public ActionResult ListarEgresosTotal(DateTime fecha_inicio, DateTime fecha_fin)
        {
            List<GastosDTO> lista = new List<GastosDTO>();

            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = Commun.CodigoSede;
            oGastosDTO.FechaInicio = fecha_inicio;
            oGastosDTO.FechaFin = fecha_fin;

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.ListarEgresosTotal,
                Item = oGastosDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListGastosDTO oResp = null;

            using (GastosLogic oGastosLogic = new GastosLogic())
            {
                oResp = oGastosLogic.GastosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarVentasTotal(DateTime fecha_inicio, DateTime fecha_fin)
        {
            using (ModuloCajaRepository oRepository = new ModuloCajaRepository())
            {
                int CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                int CodSede = Commun.CodigoSede;

                return Json(oRepository.uspListarVentasTotal(CodigoUnidadNegocio, CodSede, fecha_inicio, fecha_fin), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ListarTipoEgreso()
        {
            List<TipoEgresoDTO> lista = null;
            ReqFilterTipoEgresoDTO oReq = new ReqFilterTipoEgresoDTO()
            {
                User = "admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListTipoEgresoDTO oResp = null;
            using (TipoEgresoLogic oTipoEgresoLogic = new TipoEgresoLogic())
            {
                oResp = oTipoEgresoLogic.TipoEgresoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        #endregion



        public List<VentasDTO> DetalleDeVentaPorCodigo(int CodigoVenta)
        {
            List<VentasDTO> lista = null;
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoVenta = CodigoVenta;
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.BuscarInformacionDetalleDeVentaPorCodigo,
                Item = oVentasDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = Commun.Usuario
            };

            RespListVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<VentasDTO>();
                lista = oResp.List;
            }
            return lista;
        }

        //list venta diario
        public List<VentasDTO> VentaDiarioPorCodigo(string nrocomprobante)
        {
            List<VentasDTO> lista = null;
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.NroComprobante = nrocomprobante;
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.ventaDiariaByCodigo,
                Item = oVentasDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = Commun.Usuario
            };

            RespListVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<VentasDTO>();
                lista = oResp.List;
            }
            return lista;
        }

        public VentasDTO BuscarVentaPorCodigo(int codigo)
        {
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoVenta = codigo;
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.BuscarInfoGeneralVentaPorCodigo,
                Item = oVentasDTO,
                User = Commun.Usuario
            };
            RespItemVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.VentasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oVentasDTO = oResp.Item;
            }
            return oVentasDTO;
        }


        //generate pdf format ticket by codigo venta (memebresia)
        public ActionResult generatePDF(int codigo)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            try
            {
                //configuration 
                ModuloCheckingRepository moduloChecking = new ModuloCheckingRepository();
                var config = moduloChecking.BuscarConfiguracion(Commun.CodigoUnidadNegocio, Commun.CodigoSede);
                var vent = BuscarVentaPorCodigo(codigo);
                HeaderItem hearder = new HeaderItem();
                hearder.ruc = config.Ticket_RUC;
                hearder.name = config.Ticket_RazonSocial;
                hearder.phone = config.Ticket_Telefono;
                hearder.cell = config.Ticket_Celular;


                hearder.nro = vent.NroComprobante;
                hearder.date = vent.DescFechaVenta;
                hearder.hour = vent.DescHoraVenta;

                hearder.FormaPago = vent.DescFormaPago;

                hearder.customer = vent.NombreCliente;
                hearder.dni = vent.RUC_DNI;
                hearder.address = vent.DireccionDistritoCliente;

                hearder.Total = vent.CostoPlan;
                hearder.SubTotal = vent.TotalNeto;
                hearder.Debe = vent.Debe;
                hearder.created = vent.UsuarioCreacion;
                hearder.Frase = vent.Frase;
                var detail = DetalleDeVentaPorCodigo(codigo);

                List<DetailV> list = null;
                DetailV prod = new DetailV();

                foreach (VentasDTO v in detail)
                {
                    prod.Cantidad = v.Cantidad;
                    prod.Descripcion = v.DescripcionProducto;
                    prod.Precio = v.ImporteProducto;
                    list = new List<DetailV>();
                    list.Add(prod);
                }

                var generate = generatePDFTicket(hearder,list);

                if (generate.Status == 0)
                {
                    _objResponseModel.Status = 0;
                    _objResponseModel.Message1 = generate.Message1;

                    _objResponseModel.Message2 = generate.Message2;
                }
                else
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = generate.Message1;
                }


            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = ex.Message;
            }

            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }
        

        //para ventas diarias
        public ActionResult generatePDFSimple(string nrocomprobante)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            try
            {
                //configuration 
                ModuloCheckingRepository moduloChecking = new ModuloCheckingRepository();
                var config = moduloChecking.BuscarConfiguracion(Commun.CodigoUnidadNegocio, Commun.CodigoSede);
                var vent = VentaDiarioPorCodigo(nrocomprobante);

                if(vent.Count < 1)
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "No se encontro detalles de venta";
                    return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
                }

                HeaderItem hearder = new HeaderItem();
                hearder.ruc = config.Ticket_RUC;
                hearder.name = config.Ticket_RazonSocial;
                hearder.phone = config.Ticket_Telefono;
                hearder.cell = config.Ticket_Celular;


                hearder.nro = vent[0].NroComprobante;
                hearder.date = vent[0].DescFechaVenta;
                hearder.hour = vent[0].DescHoraVenta;

                hearder.FormaPago = vent[0].DescFormaPago;

                hearder.customer = vent[0].NombreCliente;
                hearder.dni = vent[0].RUC_DNI;
                hearder.address = vent[0].DireccionDistritoCliente;

                hearder.Total = 0;
             
                hearder.created = vent[0].UsuarioCreacion;
                hearder.Frase = vent[0].Frase;
               

                

                List<DetailV> list = new List<DetailV>();
                DetailV prod = new DetailV();

                foreach (VentasDTO v in vent)
                {
                    
                    
                    list.Add(new DetailV()
                    {
                    Cantidad = v.Cantidad,
                    Descripcion = v.DescripcionProducto,
                    Precio = v.TotalNeto,
                    });
                }

                var generate = generatePDFTicketSimple(hearder, list);

                if (generate.Status == 0)
                {
                    _objResponseModel.Status = 0;
                    _objResponseModel.Message1 = generate.Message1;

                    _objResponseModel.Message2 = generate.Message2;
                    _objResponseModel.Message3 ="" + list.Count;
                }
                else
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = generate.Message1;
                }


            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = ex.Message;
            }

            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }


        //deuda membresia
       

        public ResponseModel generatePDFTicket(HeaderItem item, List<DetailV> list)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            var MyTempPath = Server.MapPath("~/Content/assets/pdf/");
            if (!Directory.Exists(MyTempPath))
            {
                Directory.CreateDirectory(MyTempPath);
            }


            Random random = new Random();
            var OutputPath = random.Next() + ".pdf";
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            //StringReader sr = new StringReader(data);
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Path.Combine(MyTempPath, OutputPath), FileMode.Create));
            //pdfDoc.Open();
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //pdfDoc.Close();

            try
            {
                Document document = new Document(new iTextSharp.text.Rectangle(216, 600), 5f, 5f, 5f, 5f);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(MyTempPath, OutputPath), FileMode.Create));
                document.Open();

                //font custom
                Font bold = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);
                Font normal = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
                Font trasparent = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.GRAY);

                var gym = ITextPdf.ParagraphCustom(item.name, bold, 1);
                document.Add(gym);

                PdfPTable table = new PdfPTable(1);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(ITextPdf.cellCustom("RUC : " + item.ruc, bold, 1));
                table.AddCell(ITextPdf.cellCustom("CELULAR : " + item.cell, bold, 1));
                table.AddCell(ITextPdf.cellCustom("TELEFONO : " + item.phone, bold, 1));
                document.Add(table);

                Chunk linebreak = new Chunk(new DottedLineSeparator());
                document.Add(linebreak);

                PdfPTable table2 = new PdfPTable(1);
                table2.DefaultCell.Border = Rectangle.NO_BORDER;
                table2.HorizontalAlignment = Element.ALIGN_CENTER;

                table2.AddCell(ITextPdf.cellCustom("CLIENTE : " + item.customer, bold, 1));
                table2.AddCell(ITextPdf.cellCustom("RUC/DNI/CE : " + item.dni, bold, 1));
                table2.AddCell(ITextPdf.cellCustom("DIRECCION : " + item.address, bold, 1));
                document.Add(table2);

                Chunk linebreak2 = new Chunk(new DottedLineSeparator());
                document.Add(linebreak2);


                //
                PdfPTable table3 = new PdfPTable(2);
                table2.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell ticket = new PdfPCell(new Phrase(item.nro, bold));
                ticket.Border = 0;
                ticket.Colspan = 3;
                ticket.HorizontalAlignment = Element.ALIGN_CENTER;
                table3.AddCell(ticket);

                table3.AddCell(ITextPdf.cellCustom("FECHA : " + item.date, normal, 1));
                table3.AddCell(ITextPdf.cellCustom("HORA :  " + item.hour, normal, 1));
                document.Add(table3);
                Chunk linebreak3 = new Chunk(new DottedLineSeparator());
                document.Add(linebreak3);


                //table
                PdfPTable table4 = new PdfPTable(3);
                float[] anchoDeColumnas = new float[] { 17f, 60f, 23f };
                table4.SetWidths(anchoDeColumnas);
                table4.WidthPercentage = 100f;
                table4.HorizontalAlignment = 1;
           
                //table4.DefaultCell.Border = Rectangle.BOTTOM_BORDER;


                table4.AddCell(ITextPdf.cellCustom("Cant.", bold, 0, 0, true, 6));
                table4.AddCell(ITextPdf.cellCustom("Descripción", bold, 0, 0, true, 6));
                table4.AddCell(ITextPdf.cellCustom("Importe", bold, 2, 0, true, 6));
                foreach (DetailV det in list)
                {
                    table4.AddCell(ITextPdf.cellCustom(""+det.Cantidad, normal, 0, 0, true, 6));
                    table4.AddCell(ITextPdf.cellCustom(""+det.Descripcion, normal, 0, 0, true, 6));
                    table4.AddCell(ITextPdf.cellCustom(""+det.Precio, normal,2, 0, true, 3));  
                }

                table4.AddCell(ITextPdf.cellCustom(item.FormaPago, bold, 2, 2, false, 3));
                table4.AddCell(ITextPdf.cellCustom("" + item.SubTotal, bold, 2, 0, false, 3));

                table4.AddCell(ITextPdf.cellCustom("TOTAL", bold, 2, 2, false, 3));
                table4.AddCell(ITextPdf.cellCustom("" + item.Total, bold, 2, 0, false, 3));

                table4.AddCell(ITextPdf.cellCustom("A CUENTA", bold, 2, 2, false, 3));
                table4.AddCell(ITextPdf.cellCustom("" + item.SubTotal, bold, 2, 0, false, 3));

                table4.AddCell(ITextPdf.cellCustom("DEBE", bold, 2, 2, false, 3));
                table4.AddCell(ITextPdf.cellCustom(""+item.Debe, bold, 2, 0, false, 3));
                document.Add(table4);

                var user = ITextPdf.ParagraphCustom("Responsable : " + item.created, normal, 1);
                user.SpacingBefore = 20;
                document.Add(user);

                
                var txtEnd = ITextPdf.ParagraphCustom(Commun.HTMLToText(item.Frase), bold, 1);
                document.Add(txtEnd);

                Chunk linebreak4 = new Chunk(new DottedLineSeparator());
                document.Add(linebreak4);

                document.Close();

                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "" + baseUrl + "Content/assets/pdf/" + OutputPath;
                _objResponseModel.Message2 = OutputPath;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "" + ex;
            }

            return _objResponseModel;
        }

        public ResponseModel generatePDFTicketSimple(HeaderItem item, List<DetailV> list)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            var MyTempPath = Server.MapPath("~/Content/assets/pdf/");
            if (!Directory.Exists(MyTempPath))
            {
                Directory.CreateDirectory(MyTempPath);
            }


            Random random = new Random();
            var OutputPath = random.Next() + ".pdf";
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            //StringReader sr = new StringReader(data);
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Path.Combine(MyTempPath, OutputPath), FileMode.Create));
            //pdfDoc.Open();
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //pdfDoc.Close();

            try
            {
                Document document = new Document(new iTextSharp.text.Rectangle(216, 600), 5f, 5f, 5f, 5f);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(MyTempPath, OutputPath), FileMode.Create));
                document.Open();

                //font custom
                Font bold = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);
                Font normal = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
                Font trasparent = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.GRAY);

                var gym = ITextPdf.ParagraphCustom(item.name, bold, 1);
                document.Add(gym);

                PdfPTable table = new PdfPTable(1);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(ITextPdf.cellCustom("RUC : " + item.ruc, bold, 1));
                table.AddCell(ITextPdf.cellCustom("CELULAR : " + item.cell, bold, 1));
                table.AddCell(ITextPdf.cellCustom("TELEFONO : " + item.phone, bold, 1));
                document.Add(table);

                Chunk linebreak = new Chunk(new DottedLineSeparator());
                document.Add(linebreak);

                PdfPTable table2 = new PdfPTable(1);
                table2.DefaultCell.Border = Rectangle.NO_BORDER;
                table2.HorizontalAlignment = Element.ALIGN_CENTER;

                table2.AddCell(ITextPdf.cellCustom("CLIENTE : " + item.customer, bold, 1));
                table2.AddCell(ITextPdf.cellCustom("RUC/DNI/CE : " + item.dni, bold, 1));
                table2.AddCell(ITextPdf.cellCustom("DIRECCION : " + item.address, bold, 1));
                document.Add(table2);

                Chunk linebreak2 = new Chunk(new DottedLineSeparator());
                document.Add(linebreak2);


                //
                PdfPTable table3 = new PdfPTable(2);
                table2.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell ticket = new PdfPCell(new Phrase(item.nro, bold));
                ticket.Border = 0;
                ticket.Colspan = 3;
                ticket.HorizontalAlignment = Element.ALIGN_CENTER;
                table3.AddCell(ticket);

                table3.AddCell(ITextPdf.cellCustom("FECHA : " + item.date, normal, 1));
                table3.AddCell(ITextPdf.cellCustom("HORA :  " + item.hour, normal, 1));
                document.Add(table3);
                Chunk linebreak3 = new Chunk(new DottedLineSeparator());
                document.Add(linebreak3);


                //table
                PdfPTable table4 = new PdfPTable(3);
                float[] anchoDeColumnas = new float[] { 17f, 60f, 23f };
                table4.SetWidths(anchoDeColumnas);
                table4.WidthPercentage = 100f;
                table4.HorizontalAlignment = 1;

                //table4.DefaultCell.Border = Rectangle.BOTTOM_BORDER;


                table4.AddCell(ITextPdf.cellCustom("Cant.", bold, 0, 0, true, 6));
                table4.AddCell(ITextPdf.cellCustom("Descripción", bold, 0, 0, true, 6));
                table4.AddCell(ITextPdf.cellCustom("Importe", bold, 2, 0, true, 6));
                decimal total = 0;
                foreach (DetailV det in list)
                {
                    table4.AddCell(ITextPdf.cellCustom("" + det.Cantidad, normal, 0, 0, true, 6));
                    table4.AddCell(ITextPdf.cellCustom("" + det.Descripcion, normal, 0, 0, true, 6));
                    table4.AddCell(ITextPdf.cellCustom("" + det.Precio, normal, 2, 0, true, 3));
                    total += det.Precio;
                }

                table4.AddCell(ITextPdf.cellCustom(item.FormaPago, bold, 2, 2, false, 3));
                table4.AddCell(ITextPdf.cellCustom("" + total, bold, 2, 0, false, 3));

                table4.AddCell(ITextPdf.cellCustom("TOTAL", bold, 2, 2, false, 3));
                table4.AddCell(ITextPdf.cellCustom("" + total, bold, 2, 0, false, 3));

                document.Add(table4);

                var user = ITextPdf.ParagraphCustom("Responsable : " + item.created, normal, 1);
                user.SpacingBefore = 20;
                document.Add(user);


                var txtEnd = ITextPdf.ParagraphCustom(Commun.HTMLToText(item.Frase), bold, 1);
                document.Add(txtEnd);

                Chunk linebreak4 = new Chunk(new DottedLineSeparator());
                document.Add(linebreak4);

                document.Close();

                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "" + baseUrl + "Content/assets/pdf/" + OutputPath;
                _objResponseModel.Message2 = OutputPath;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "" + ex;
            }

            return _objResponseModel;
        }






        //return pdf
        public FileResult generatePDFxx()
        {
            var MyTempPath = Server.MapPath("~/Content/assets/pdf/");
            string GridHtml = "sdfsdf";



            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }

        public void GeneratePdfbbb()
        {

            var html = @"
<table width=""80%"" align=""center"" cellspacing>
      <tbody>
        <tr>
          <td>
            <div style=""padding: 2px; text-align: center"">
              <h2>NOMBRE DEL GYM</h2>
              <div>
                <strong>Celular : </strong> <strong> 89889898</strong><br />
                <strong>Telefono : </strong> <strong> 89889898</strong><br />
              </div>
            </div>
          </td>
        </tr>
        <tr>
          <td>
            <div style=""border: 1px solid rgb(61, 61, 61)""></div>
<hr/>
          </td>
        </tr>
        <tr>
          <td>
            <div style=""padding: 2px; text-align: center"">
              <div
                style=""display: flex; direction: row; justify-content: center""
              >
                <h4 style=""text-transform: uppercase"">
                  CLIENTE : Amador ccasani arche (565)
                </h4>
              </div>
              <div style=""margin-top: -15px"">
                <strong>RUC/DNI/CE : </strong> <strong> 89889898</strong><br />
                <strong>DIRECCION : </strong> <strong> 89889898</strong><br />
              </div>
            </div>
          </td>
        </tr>
        <tr>
          <td colspan=""3"">
           
<hr/>
          </td>
        </tr>

        <tr style=""text-align: center"">
          <td>
            <h4>TICKET NR° 656565656565</h4>
            <span style=""margin-right: 20px"">FECHA: 2/5258/55 </span>
            <span style=""margin-left: 20px""> HORA: 1528:25:</span>
          </td>
        </tr>
        <tr>
          <td>
            
<hr/>
          </td>
        </tr>
      </tbody>
    </table>
    <table width=""80%"" align=""center"" style=""border-collapse: collapse"">
      <thead>
        <tr>
          <th width=""15%"">Cant</th>
          <th align=""left"">Descripcion</th>
          <th align=""right"">Importe</th>
        </tr>
      </thead>
      <tbody>
        <tr class='trbr'>
          <td width=""15%"">1</td>
          <td align=""left"">descripton de un producto</td>
          <td align=""right"">100</td>
        </tr>
        <tr class=""trbr"">
          <td colspan=""2"" align=""right""><strong>EFECTIVO</strong></td>
          <td align=""right""><strong>1600</strong></td>
        </tr>
        <tr style=""border-bottom: 1px solid rgb(138, 137, 137, 0.5)"">
          <td colspan=""2"" align=""right""><strong>TOTAL</strong></td>
          <td align=""right""><strong>1600</strong></td>
        </tr>
        <tr style=""border-bottom: 1px solid rgb(138, 137, 137, 0.5)"">
          <td colspan=""2"" align=""right"">
            <strong style=""color: rgb(126, 123, 123)"">A CUENTA</strong>
          </td>
          <td align=""right"">
            <strong style=""color: rgb(126, 123, 123)"">1600</strong>
          </td>
        </tr>
        <tr style=""border-bottom: 1px solid rgb(138, 137, 137, 0.5)"">
          <td colspan=""2"" align=""right"">
            <strong style=""color: rgb(126, 123, 123)"">DEBE</strong>
          </td>
          <td align=""right"">
            <strong style=""color: rgb(126, 123, 123)"">1600</strong>
          </td>
        </tr>
      </tbody>
    </table>
    <table width=""80%"" align=""center"" style=""margin-top: 20px;border-collapse: collapse"">
      <tbody>
        <tr style=""text-align: center"">
          <td>
            <span>Responsable: Usauario</span> <br />
            <h4 style=""margin-top: 5px"">MUCHAS GRACIAS, VUELVA PRONTO</h4>
           <hr/>
          </td>
        </tr>
      </tbody>
    </table>


";

            var MyTempPath = Server.MapPath("~/Content/assets/pdf/");
            Document document = new Document(PageSize.A4);
            List<string> css = new List<string>();
            css.Add("~/Content/pdf.css");
            Random random = new Random();
            var namefile = random.Next() + ".pdf";
            using (var ms = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(MyTempPath, namefile), FileMode.Create));
                writer.CloseStream = false;
                document.Open();
                HtmlPipelineContext htmlPipeline = new HtmlPipelineContext(null);
                htmlPipeline.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                //CSS
                ICSSResolver cSSResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                css.ForEach(i => cSSResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath(i), true));

                //
                IPipeline pipeline = new CssResolverPipeline(cSSResolver, new HtmlPipeline(htmlPipeline, new PdfWriterPipeline(document, writer)));

                //
                XMLWorker worker = new XMLWorker(pipeline, true);
                XMLParser xMLParser = new XMLParser(worker);
                xMLParser.Parse(new MemoryStream(Encoding.UTF8.GetBytes(html)));
                document.Close();
            }


        }


        //NOTIFICACIONES APP
        //listado
        public ActionResult ListarNotificacionesApp()
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();

            NotificacionDTO oNotificacionDTO = new NotificacionDTO();
            oNotificacionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oNotificacionDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()
            
            {
                FilterCase = filterCaseNotificacionApp.Listar,
                Item = oNotificacionDTO,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListNotificacionDTO oResp = null;
            

            using (NotificacionappLogic oNotificacionappLogic = new NotificacionappLogic())
            {
                oResp = oNotificacionappLogic.NotificacionappGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult NotificacionAppByCodigo(string codigo)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (codigo == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }


            NotificacionDTO oNotificacionDTO = new NotificacionDTO();
            oNotificacionDTO.CodigoSede = Commun.CodigoSede;
            oNotificacionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oNotificacionDTO.CodigoNotificacionesApp = codigo;
            ReqFilterNotificacionDTO oReq = new ReqFilterNotificacionDTO()
            {
                FilterCase = filterCaseNotificacionApp.BuscarPorCodigo,
                Item = oNotificacionDTO,
                User = Commun.Usuario,
            };
            RespItemNotificacionDTO oResp = null;
            using (NotificacionappLogic oNotificacionappLogic = new NotificacionappLogic())
            {
                oResp = oNotificacionappLogic.NotificacionAppGetItem(oReq);
            }
            if (oResp.Success)
            {
                _objResponseModel.Date = oResp.Item;
                _objResponseModel.Status = 0;
            }

      
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }

        //register notiapp
        public ActionResult RegisterNotificacionApp(int tipo, string asunto, string date, string message, bool recurrente,int grupo)
        {

           
            ResponseModel _objResponseModel = new ResponseModel();
            List<NotificacionDTO> list = new List<NotificacionDTO>();


            bool validadorParametros = true;
            if (tipo == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio tipo envio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (grupo == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio un grupo de personas.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (asunto == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un asunto.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (message == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un mensaje.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (date.Trim() == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un fecha.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }



            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoNotificacionesApp = "0",
                TipoEnvio = tipo,
                Asunto = asunto,
                FechaHoraEnvio = DateTime.Parse(date),
                Mensaje =message,
                Recurrente = recurrente,
                GrupoPersonas = grupo,
                Operation = Operation.Create,
            }); ;
            ReqNotificacionDTO oReq = new ReqNotificacionDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespNotificacionDTO oResp = null;
            using (NotificacionappLogic logic = new NotificacionappLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }

        //update
        public ActionResult ActualizarNotificacionApp(string codigo , int tipo, string asunto, string date, string message, bool recurrente, int grupo,bool estado)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<NotificacionDTO> list = new List<NotificacionDTO>();


            bool validadorParametros = true;
            if (tipo == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio tipo envio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (grupo == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio un grupo de personas.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (asunto == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un asunto.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (message == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un mensaje.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (date.Trim() == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un fecha.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }

            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoNotificacionesApp = codigo,
                UsuarioCreacion = Commun.Usuario,
                TipoEnvio = tipo,
                Asunto = asunto,
                FechaHoraEnvio = DateTime.Parse(date),
                Mensaje = message,
                Recurrente = recurrente,
                Estado=estado,
                GrupoPersonas = grupo,
                UsuarioEdicion = Commun.Usuario,
                Operation = Operation.Update,
            }); 
            ReqNotificacionDTO oReq = new ReqNotificacionDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespNotificacionDTO oResp = null;
            using (NotificacionappLogic logic = new NotificacionappLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }


        public ActionResult DeleteNotificacionApp(string codigo)
        {

            ResponseModel _objResponseModel = new ResponseModel();
            List<NotificacionDTO> list = new List<NotificacionDTO>();

            bool validadorParametros = true;
         
             if (codigo == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio agregar un codigo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
            }
            list.Add(new NotificacionDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoNotificacionesApp = codigo,
              
                Operation = Operation.Eliminarfiltro,
            }); ;
            ReqNotificacionDTO oReq = new ReqNotificacionDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespNotificacionDTO oResp = null;
            using (NotificacionappLogic logic = new NotificacionappLogic())
            {
                oResp = logic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Status = 0;
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);

        }


        public List<List<ClientesDTO>> SplitList(List<ClientesDTO> locations, int nSize = 30)
        {
            var list = new List<List<ClientesDTO>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }
    }


    public class ResponseModel
    {
        public string Message1 { set; get; }
        public string Message2 { set; get; }
        public string Message3 { set; get; }
        public int Status { set; get; }
        public bool Success { set; get; }
        public object Date { set; get; }
    }


    //SE USA PARA ARMAR EL TICKET HEADER
    public class HeaderItem
    {
        public string ruc { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }

        public string nro { get; set; }
        public string date { get; set; }
        public string hour { get; set; }
        public string customer { get; set; }
        public string address { get; set; }
        public string dni { get; set; }

        public string created { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Debe { get; set; }
        public string Frase { get; set; }
        public string FormaPago { get; set; }
    }

    //SE USA PARA ARMAR EL TICKET BODY
    public class DetailV{
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }

    public class ImageSend
    {
        public string Origin { get; set; }
        public string Name { get; set; }
        public ImageSend(string name, string origin)
        {
            Name = name;
            Origin = origin;

        }
    }

}