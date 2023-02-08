using BotComers.Helpers;
using BotComers.Repository.CentroEntrenamiento;
using BotComers.Repository.Ingresos;
using BotComers.Repository.Inventario;
using BotComers.ViewModels;
using BotComers.ViewModels.Ingresos;
using BotComers.ViewModels.Inventario;
using E_BusinessLayer.Gimnasio;

using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class posController : Controller
    {
        // GET: pos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult posfitness()
        {
            return View();
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

        public ActionResult ListarValorInventario_PuntoVenta(string Buscador, int CodigoAlmacen)
        {
            List<ItemsVentaViewModel> lista = new List<ItemsVentaViewModel>();

            using (ItemsVentaRepository oItemsVentaRepository = new ItemsVentaRepository())
            {
                ItemsVentaViewModel request = new ItemsVentaViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoAlmacen = CodigoAlmacen;
                request.Nombre = Buscador;
                return Json(oItemsVentaRepository.ecommerce_uspListarValorInventario_PuntoVenta(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarTipoComprobante(TipoComprobanteViewModel request)
        {
            if ((request.CodigoTipoDocumentoEmpresa == 1 || request.CodigoTipoDocumentoEmpresa == 2))
            {
                //Verificar Configuracion
                ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
                ConfiguracionDTO = BuscarConfiguracionServer();
                if (ConfiguracionDTO.TieneFacturacionElectronica)
                {
                    using (ComprobanteRepository oRepository = new ComprobanteRepository())
                    {
                        var listaResp = new List<TipoComprobanteViewModel>();
                        int CodigoSubDocumento = 0;

                        #region Obtener SubDocumento
                        List<SubTipoDocumentoDTO> lista = null;
                        SubTipoDocumentoDTO oSubTipoDocumentoDTO = new SubTipoDocumentoDTO();
                        oSubTipoDocumentoDTO.CodigoTipoDocumento = request.CodigoTipoDocumentoEmpresa;
                        oSubTipoDocumentoDTO.CodigoSede = Commun.CodigoSede;
                        oSubTipoDocumentoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                        ReqFilterSubTipoDocumentoDTO oReq = new ReqFilterSubTipoDocumentoDTO()
                        {
                            User = "Admin",
                            FilterCase = E_DataModel.Common.filterCaseSubTipoDocumento.ListarPorTipoDocumento,
                            Item = oSubTipoDocumentoDTO,
                            Paging = new E_DataModel.Common.Paging()
                            {
                                All = true,
                                PageRecords = 0
                            }
                        };
                        RespListSubTipoDocumentoDTO oResp = null;
                        using (SubTipoDocumentoLogic oSubTipoDocumentoLogic = new SubTipoDocumentoLogic())
                        {
                            oResp = oSubTipoDocumentoLogic.SubTipoDocumentoGetList(oReq);
                        }
                        if (oResp.Success)
                        {
                            lista = new List<SubTipoDocumentoDTO>();
                            lista = oResp.List;
                            CodigoSubDocumento = lista[0].Codigo;
                        }
                        #endregion

                        #region Documento
                        SeriesDTO oSeriesDTO = new SeriesDTO();
                        oSeriesDTO.flag = request.CodigoTipoDocumentoEmpresa;
                        oSeriesDTO.subFlag = CodigoSubDocumento;
                        oSeriesDTO.longitudSerie = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["longitudSerie"]);
                        oSeriesDTO.CodigoSede = Commun.CodigoSede;
                        oSeriesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio; ;
                        ReqFilterSeriesDTO oReq1 = new ReqFilterSeriesDTO()
                        {
                            FilterCase = E_DataModel.Common.filterCaseSeries.BuscarGenerarCorrelativo,
                            Item = oSeriesDTO,
                            User = "Admin"
                        };
                        RespItemSeriesDTO oResp1 = null;
                        using (SeriesLogic oSeriesLogic = new SeriesLogic())
                        {
                            oResp1 = oSeriesLogic.SeriesGetItem(oReq1);
                        }
                        if (oResp1.Success)
                        {
                            listaResp.Add(new TipoComprobanteViewModel()
                            {
                                Serie = string.Format("{0}_{1}", request.CodigoTipoDocumentoEmpresa,  oResp1.Item.NroCorrelativoActual),
                                CodigoTipoComprobante = request.CodigoTipoDocumentoEmpresa,
                                CodigoDocumentoSunat = true
                            });
                        }

                        #endregion

                        return Json(listaResp, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                else
                {
                    using (ComprobanteRepository oRepository = new ComprobanteRepository())
                    {
                        request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                        request.CodigoSede = Commun.CodigoSede;
                        return Json(oRepository.ecommerce_uspListarTipoComprobante(request), JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                using (ComprobanteRepository oRepository = new ComprobanteRepository())
                {
                    request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                    request.CodigoSede = Commun.CodigoSede;
                    return Json(oRepository.ecommerce_uspListarTipoComprobante(request), JsonRequestBehavior.AllowGet);
                }
            }
        }
        
        public ActionResult RegistrarComprobante(ComprobanteViewModel request)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarComprobante(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RegistrarClientes(ClientesViewModel request)
        {
            using (ClientesRepository oRepository = new ClientesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspRegistrarClientes(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscadorClientesPorIdentificacion(ClientesViewModel request)
        {
            using (ClientesRepository oRepository = new ClientesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspBuscadorClientesPorIdentificacion(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspBuscadorClientes(ClientesViewModel request)
        {
            using (ClientesRepository oRepository = new ClientesRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspBuscadorClientes(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspBuscarEmpresa_imprimirticket()
        {
            using (CentroEntrenamiento_ConfiguracionRepository oRepository = new CentroEntrenamiento_ConfiguracionRepository())
            {
                ConfiguracionDTO request = new ConfiguracionDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspBuscarEmpresa_imprimirticket(request), JsonRequestBehavior.AllowGet);
            }
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
    }
}