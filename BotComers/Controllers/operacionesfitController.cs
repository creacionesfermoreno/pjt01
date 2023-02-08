using BotComers.Helpers;
using BotComers.Repository.CentroEntrenamiento;
using BotComers.Repository.Ingresos;
using BotComers.ViewModels.CentroEntrenamiento;
using BotComers.ViewModels.Ingresos;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.qrcode;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using iTextSharp.tool.xml;
//using E_DataModel;

namespace BotComers.Controllers
{
    public class operacionesfitController : Controller
    {
        // GET: operacionesfit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult membresiaspagos()
        {

            return View();
        }

        #region Metodos MEMBRESIAS Y PAGOS       

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

        public ActionResult ListarUbicaciones(int tipo, string ubigeo, string buscador)
        {
            List<UbicacionesDTO> lista = null;
            UbicacionesDTO oUbicacionesDTO = new UbicacionesDTO();
            oUbicacionesDTO.Tipo = tipo;
            oUbicacionesDTO.Ubicaciones = ubigeo;
            oUbicacionesDTO.Buscador = buscador;
            ReqFilterUbicacionesDTO oReq = new ReqFilterUbicacionesDTO()
            {
                Item = oUbicacionesDTO,
                User = "appsfit",
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
        public ActionResult uspBuscarUbicacionesDefecto()
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                FilterCase = filterCaseConfiguracion.uspBuscarConfiguracion_apfitness,
                User = "admin"
            };
            RespItemConfiguracionDTO oResp = null;
            using (ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic())
            {
                oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            }
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }

            return Json(oConfiguracionDTO, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarUsuarioContrato()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
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
                lista = oResp.List.Where(x => x.CodigoPerfil == 4 && x.Estado == true).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarAsesorVentas()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuario_HacerContrato,
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
                lista.Insert(0, new UsuarioDTO() { Codigo = 0, NombreCompleto = "SIN VENDEDOR" });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SEGListarUsuarioResponsableSuplementos(string filtro)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
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

        public ActionResult ListaPaquetesContrato(int flag, string nombre)
        {
            List<PlanesDTO> lista = null;
            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                Item = new PlanesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    Descripcion = nombre,
                    CodigoSede = Commun.CodigoSede,
                    CodigoProfesor = 0
                },
                FilterCase = filterCasePlanes.filter_uspListarPaquetesPorProfesor,
                User = "Admin",
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
                lista = oResp.List.OrderBy(x => x.DesEstado).ToList();
            }

            lista.Insert(0, new PlanesDTO() { CodigoPaquete = 0, DescTipoPaquete = "Seleccione el Paquete Aqui", Descripcion = "", DesEstado = "" });
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BuscarPaquete(int codigo)
        {
            PlanesDTO oPaquetesDTO = new PlanesDTO();
            oPaquetesDTO.CodigoPaquete = codigo;
            oPaquetesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oPaquetesDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.porCodigo,
                Item = oPaquetesDTO,
                User = "appsfit"
            };
            RespItemPlanesDTO oResp = null;
            using (PlanesLogic oCargoLogic = new PlanesLogic())
            {
                oResp = oCargoLogic.PlanesGetItem(oReq);
            }
            if (oResp.Success)
            {
                oPaquetesDTO = oResp.Item;
            }

            return Json(oPaquetesDTO, JsonRequestBehavior.AllowGet);
        }

        //tipo de contratos---------------------------------


        public ActionResult RegistrarTipoContratos(string clausula, string description, string serie, string correlativo)
        {

            List<TipoContratoDTO> list = new List<TipoContratoDTO>();
            TipoContratoDTO tipoContrato = new TipoContratoDTO();
            tipoContrato.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            tipoContrato.CodigoSede = Commun.CodigoSede;
            tipoContrato.UsuarioCreacion = Commun.Usuario;
            tipoContrato.Clausula = clausula;
            tipoContrato.Descripcion = description;
            tipoContrato.Codigo = 0;
            //serie
            tipoContrato.NroSerie = serie;
            tipoContrato.NroCorrelativoActual = correlativo;
            tipoContrato.Operation = Operation.Create;
            list.Add(tipoContrato);

            ReqTipoContratoDTO oReq = new ReqTipoContratoDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespTipoContratoDTO oResp = null;
            using (TipoContratoLogic tipoContratoLogic = new TipoContratoLogic())
            {
                oResp = tipoContratoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                // Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActualizarTipoContratos(int codigo, string clausula, string description, string serie, string correlativo)
        {

            List<TipoContratoDTO> list = new List<TipoContratoDTO>();
            TipoContratoDTO tipoContrato = new TipoContratoDTO();
            tipoContrato.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            tipoContrato.CodigoSede = Commun.CodigoSede;
            tipoContrato.UsuarioEdicion = Commun.Usuario;
            tipoContrato.Clausula = clausula;
            tipoContrato.Descripcion = description;
            tipoContrato.Codigo = codigo;

            //serie
            tipoContrato.NroSerie = serie;
            tipoContrato.NroCorrelativoActual = correlativo;
            tipoContrato.Operation = Operation.Update;
            list.Add(tipoContrato);

            ReqTipoContratoDTO oReq = new ReqTipoContratoDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespTipoContratoDTO oResp = null;
            using (TipoContratoLogic tipoContratoLogic = new TipoContratoLogic())
            {
                oResp = tipoContratoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                // Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarTipoContratos(int codigo)
        {

            List<TipoContratoDTO> list = new List<TipoContratoDTO>();
            TipoContratoDTO tipoContrato = new TipoContratoDTO();
            tipoContrato.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            tipoContrato.CodigoSede = Commun.CodigoSede;
            tipoContrato.UsuarioEdicion = Commun.Usuario;
            tipoContrato.Codigo = codigo;
            tipoContrato.Operation = Operation.Delete;
            list.Add(tipoContrato);

            ReqTipoContratoDTO oReq = new ReqTipoContratoDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespTipoContratoDTO oResp = null;
            using (TipoContratoLogic tipoContratoLogic = new TipoContratoLogic())
            {
                oResp = tipoContratoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                // Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTipoContratoPDF(int codigo,string cdclient, string customer, string doc,string membresia,string ncontrato, string finicio, string ffin)
        {
            ResponseModel responseModel= new ResponseModel();

            TipoContratoDTO tipoContrato = new TipoContratoDTO();
            tipoContrato.Codigo = codigo;
            tipoContrato.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterTipoContratoDTO oReq = new ReqFilterTipoContratoDTO()
            {
                FilterCase = filterCaseTipoContrato.porCodigo,
                Item = tipoContrato,
                User = Commun.Usuario
            };

            RespItemTipoContratoDTO oResp = null;
            using (TipoContratoLogic tipoContratoLogic = new TipoContratoLogic())
            {
                oResp = tipoContratoLogic.TipoContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                //transform Text
                List<ParmTC> parms = new List<ParmTC>();
                parms.Add(new ParmTC(name: "codigocliente", code: "{codigocliente}", value: cdclient));
                parms.Add(new ParmTC(name: "nombrecompletocliente", code: "{nombrecompletocliente}", value: customer));
                parms.Add(new ParmTC(name: "nrodocumentocliente", code: "{nrodocumentocliente}", value: doc));
                parms.Add(new ParmTC(name: "nombremembresia", code: "{nombremembresia}", value: membresia));
                parms.Add(new ParmTC(name: "nrocontrato", code: "{nrocontrato}", value: ncontrato));
                parms.Add(new ParmTC(name: "fechainicio", code: "{fechainicio}", value: finicio));
                parms.Add(new ParmTC(name: "fechafin", code: "{fechafin}", value: ffin));

                string replaces = Commun.TextReplace(oResp.Item.Clausula, parms);
                var generatePff = PDFHTMl(replaces);
                responseModel.Status = 0;
                responseModel.Message1 = replaces;
                responseModel.Message2 = generatePff.Message3;
                responseModel.Message3 = generatePff.Message2;
               
            }

            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTipoContrato(int codigo)
        {
           

            TipoContratoDTO tipoContrato = new TipoContratoDTO();
            tipoContrato.Codigo = codigo;
            tipoContrato.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterTipoContratoDTO oReq = new ReqFilterTipoContratoDTO()
            {
                FilterCase = filterCaseTipoContrato.porCodigo,
                Item = tipoContrato,
                User = Commun.Usuario
            };

            RespItemTipoContratoDTO oResp = null;
            using (TipoContratoLogic tipoContratoLogic = new TipoContratoLogic())
            {
                oResp = tipoContratoLogic.TipoContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                tipoContrato = oResp.Item;
            }

            return Json(tipoContrato, JsonRequestBehavior.AllowGet);
        }

        private ResponseModel PDFHTMl(string text)
        {
            ResponseModel responseModel = new ResponseModel();
            var MyTempPath = Server.MapPath("~/Content/assets/pdf/");

            if (!Directory.Exists(MyTempPath))
            {
                Directory.CreateDirectory(MyTempPath);
            }

            Random random = new Random();
            var OutputPath = random.Next() + ".pdf";
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            try
            {
              
                StringReader sr = new StringReader(text);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                pdfDoc.SetMargins(50, 50, 10, 40);

                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Path.Combine(MyTempPath, OutputPath), FileMode.Create));
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();


                responseModel.Status = 0;
                responseModel.Message2 = baseUrl + "Content/assets/pdf/" + OutputPath;
                responseModel.Message3 = OutputPath;
            }
            catch(Exception ex)
            {
                responseModel.Status = 2;
                responseModel.Message1 = ex.Message;
            }
            return responseModel;
        }
        

        

        public ActionResult ListaTipoContratos()
        {
            List<TipoContratoDTO> lista = null;
            TipoContratoDTO oTipoContratoDTO = new TipoContratoDTO();
            oTipoContratoDTO.CodigoSede = Commun.CodigoSede;
            oTipoContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterTipoContratoDTO oReq = new ReqFilterTipoContratoDTO()
            {
                User = "appsfit",
                Item = oTipoContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListTipoContratoDTO oResp = null;
            using (TipoContratoLogic oTipoContratoLogic = new TipoContratoLogic())
            {
                oResp = oTipoContratoLogic.TipoContratoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<TipoContratoDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Listar_TipoIngreso()
        {
            List<TipoIngresoDTO> lista = null;
            TipoIngresoDTO oTipoIngresoDTO = new TipoIngresoDTO();
            oTipoIngresoDTO.CodigoSede = Commun.CodigoSede; ;
            ReqFilterTipoIngresoDTO oReq = new ReqFilterTipoIngresoDTO()
            {
                User = "appsfit",
                Item = oTipoIngresoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListTipoIngresoDTO oResp = null;
            using (TipoIngresoLogic oTipoIngresoLogic = new TipoIngresoLogic())
            {
                oResp = oTipoIngresoLogic.TipoIngresoGetList(oReq);
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
                User = "appfit",
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

        public ActionResult EliminarSocio(int codigo)
        {
            string mensaje = string.Empty;
            string Cod = string.Empty;
            int elimina = 0;
            List<ClientesDTO> oList = new List<ClientesDTO>();
            oList.Add(new ClientesDTO()
            {

                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoSocio = codigo,
                UsuarioCreacion = Commun.Usuario,
                Operation = E_DataModel.Common.Operation.Delete
            });

            ReqClientesDTO oReq = new ReqClientesDTO()
            {
                List = oList,
                User = Commun.Usuario
            };

            RespClientesDTO oResp = null;
            using (ClientesLogic oRutinaLogic = new ClientesLogic())
            {
                oResp = oRutinaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Los datos han sido eliminados correctamente.";
                Cod = (oResp.MessageList[0].Codigo).ToString();
                elimina = 0;
            }
            else
            {
                mensaje = oResp.MessageList[0].Detalle;
                Cod = (oResp.MessageList[0].Codigo).ToString();
                elimina = 1;
            }
            return Json(mensaje + "|" + Cod, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ObtenerSerieGenaradoTipoContrato(int tipoContrato)
        {
            string nro = string.Empty;
            if (tipoContrato > 1)
            {
                SeriesContratoDTO oSeriesContratoDTO = new SeriesContratoDTO();
                oSeriesContratoDTO.flag = tipoContrato;
                oSeriesContratoDTO.longitudSerie = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["longitudSerie"]);
                oSeriesContratoDTO.CodigoSede = Commun.CodigoSede;
                oSeriesContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                ReqFilterSeriesContratoDTO oReq = new ReqFilterSeriesContratoDTO()
                {
                    FilterCase = E_DataModel.Common.filterCaseSeriesContrato.BuscarGenerarCorrelativo,
                    Item = oSeriesContratoDTO,
                    User = "Admin"
                };
                RespItemSeriesContratoDTO oResp = null;
                using (SeriesContratoLogic oSeriesContratoLogic = new SeriesContratoLogic())
                {
                    oResp = oSeriesContratoLogic.SeriesContratoGetItem(oReq);
                }
                if (oResp.Success)
                {
                    oSeriesContratoDTO = oResp.Item;
                    nro = oResp.Item.NroCorrelativoActual;
                }
            }
            return Json(nro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarSubTipoDocumentosPorTipoDocumento(int CodTipoDocumento)
        {
            List<SubTipoDocumentoDTO> lista = null;
            SubTipoDocumentoDTO oSubTipoDocumentoDTO = new SubTipoDocumentoDTO();
            oSubTipoDocumentoDTO.CodigoTipoDocumento = CodTipoDocumento;
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
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerSerieGenarado(int tipoDocumento, int subTipoDocumento)
        {
            string nro = string.Empty;
            if (tipoDocumento != 0)
            {
                SeriesDTO oSeriesDTO = new SeriesDTO();
                oSeriesDTO.flag = tipoDocumento;
                oSeriesDTO.subFlag = subTipoDocumento;
                oSeriesDTO.longitudSerie = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["longitudSerie"]);
                oSeriesDTO.CodigoSede = Commun.CodigoSede;
                oSeriesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio; ;
                ReqFilterSeriesDTO oReq = new ReqFilterSeriesDTO()
                {
                    FilterCase = E_DataModel.Common.filterCaseSeries.BuscarGenerarCorrelativo,
                    Item = oSeriesDTO,
                    User = "Admin"
                };
                RespItemSeriesDTO oResp = null;
                using (SeriesLogic oSeriesLogic = new SeriesLogic())
                {
                    oResp = oSeriesLogic.SeriesGetItem(oReq);
                }
                if (oResp.Success)
                {
                    oSeriesDTO = oResp.Item;
                    nro = oResp.Item.NroCorrelativoActual;
                }
            }
            return Json(nro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarMembresiasTraspaso(int codSocio)
        {
            List<ContratoDTO> lista = null;
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoSocio = codSocio;
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.ListarMembresiasTraspasoSocios,
                User = "Admin",
                Item = oContratoDTO,
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
                lista = oResp.List.Take(6).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarMembresia(int codigoMenbresia)
        {
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoMenbresia = codigoMenbresia;
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.porCodigo,
                Item = oContratoDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "appsfit"
            };

            RespItemContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
            }
            return Json(oContratoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarMembresia(int codigo)
        {
            string mensaje = string.Empty;

            List<ContratoDTO> oList = new List<ContratoDTO>();
            oList.Add(new ContratoDTO()
            {
                CodigoMenbresia = codigo,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = E_DataModel.Common.Operation.Delete
            });

            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = oList,
                User = "Admin"
            };

            RespContratoDTO oResp = null;
            using (ContratoLogic oRutinaLogic = new ContratoLogic())
            {
                oResp = oRutinaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Membresia eliminada correctamente.";
            }
            else
            {
                mensaje = "El  cliente Posee Pagos, Anule la venta";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarSociosConFiltrosTransferenciaContrato(int codigo, string Nombres, string Apellidos, string Dni, int CodigoSede)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSocio = codigo;
            oClientesDTO.Nombre = Nombres;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.DNI = Dni;
            oClientesDTO.CodigoSede = CodigoSede;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspBuscarSociosConFiltrosTransferenciaContrato,
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

        public ActionResult GuardarMembresiaContrato(int codigo, int codigoSocio, int codigoPaquete, DateTime fechaInicio, DateTime fechaFin, string Hi, string Hf, decimal costo, int nroIngreso, int nroIngresoActual, string nroContrato, int MatriculadoPor, int FrezenDisponibles, int NroDiasCongelar, DateTime fechaFreziing, DateTime fechaDesFreziing, int estado, string action, string User, int TipoMembresia, int OrigenContrato, string ObservacionTraspaso, int tipoDescuento, decimal montoDescuento, string observacion, string AsesorComercial, int TipoIngreso, string indTraspaso, int tipoContrato, int OrigenSociosTraspaso, int OrigenMembresiaTraspaso, int CodigoProfesor, int CodTiempoMenbresia)
        {
            string mensaje = string.Empty;
            int codigoMembresia = 0;
            DateTime fechaCongelacionProgramada = DateTime.Now;
            fechaCongelacionProgramada = fechaCongelacionProgramada.AddDays(-1);
            DateTime fechaDesCongelacion;
            if (action == "N")
            {
                fechaCongelacionProgramada = DateTime.Now.AddDays(-1);
            }
            else
            {
                if (estado == 1)
                {
                    if (fechaFreziing == DateTime.Now)
                    {
                        fechaCongelacionProgramada = fechaFreziing.AddDays(-1);
                    }
                    else if (fechaFreziing > DateTime.Now)
                    {
                        fechaCongelacionProgramada = fechaFreziing;
                    }

                }
                else
                {
                    fechaCongelacionProgramada = fechaFreziing;
                }
            }

            if (estado == 0)
            {
                if (fechaFreziing == null)
                {
                    fechaDesCongelacion = fechaDesFreziing;// DateTime.Now.AddDays(NroDiasCongelar);
                }
                else
                {
                    fechaDesCongelacion = fechaDesFreziing;// Convert.ToDateTime(fechaFreziing).AddDays(NroDiasCongelar);
                }
            }
            else
            {
                if (fechaFreziing > DateTime.Now)
                {
                    fechaDesCongelacion = fechaDesFreziing;// Convert.ToDateTime(fechaFreziing).AddDays(NroDiasCongelar);
                }
                else
                {
                    fechaDesCongelacion = DateTime.Now.AddDays(-1);
                }
            }

            DateTime HoraInicio = Convert.ToDateTime(Hi);
            DateTime HoraFin = Convert.ToDateTime(Hf);

            List<ContratoDTO> list = new List<ContratoDTO>();
            list.Add(new ContratoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoMenbresia = codigo,
                CodigoSocio = codigoSocio,
                CodigoResponsable = 1,
                CodigoPaquete = codigoPaquete,
                FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second),
                FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, HoraFin.Hour, HoraFin.Minute, HoraFin.Second),
                Estado = estado,
                Costo = costo,
                NroIngreso = nroIngreso,
                NroContrato = nroContrato,
                NroIngresoActual = nroIngresoActual,
                FrezenDisponibles = FrezenDisponibles,
                FechaDesCongelacion = fechaDesCongelacion,
                FechaCongelacionProgramada = fechaCongelacionProgramada,
                MatriculadoPor = MatriculadoPor,
                UsuarioCreacion = User.Replace('#', ' '),
                UsuarioEdicion = User.Replace('#', ' '),
                TipoMembresia = TipoMembresia,
                CodigoMebresiaOrigen = OrigenContrato,
                ObservacionTraspaso = ObservacionTraspaso,
                CodigoSede = Commun.CodigoSede,
                TipoDescuento = tipoDescuento,
                MontoDescuento = montoDescuento,
                Observacion = observacion,
                AsesorComercial = AsesorComercial,
                TipoIngreso = TipoIngreso,
                IndTraspaso = indTraspaso,
                TipoContrato = tipoContrato,
                OrigenSociosTraspaso = OrigenSociosTraspaso,
                OrigenMembresiaTraspaso = OrigenMembresiaTraspaso,
                UsuarioEmisor = Commun.Usuario,
                CodigoProfesor = CodigoProfesor,
                CodTiempoMenbresia = CodTiempoMenbresia,
                Operation = action == "N" ? Operation.Create : Operation.Update
            });
            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = list,
                User = User.Replace('#', ' ')
            };
            RespContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Membresia guardada correctamente !!!";
                codigoMembresia = oResp.MessageList[0].Codigo;
            }
            return Json(codigoMembresia, JsonRequestBehavior.AllowGet);
        }

        public ActionResult buscarConfiguracionImprimirContrato()
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = "Admin",
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.buscarConfiguracionImprimirContrato
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

        public ActionResult ActualizarTraspasoMembresia(int codigoMembresia, int CodigoSocioReceptor)
        {
            List<ContratoDTO> list = new List<ContratoDTO>();
            list.Add(new ContratoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoMenbresia = codigoMembresia,
                Estado = 3,
                UsuarioResponsable = Commun.Usuario,
                CodigoSocioReceptor = CodigoSocioReceptor,
                CodigoSede = Commun.CodigoSede,
                Operation = Operation.UpdateEstadoTraspaso,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario
            });
            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = list,
                User = "Admin"
            };
            RespContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidarDNI(string dni)
        {
            int existe = 0;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                existe = oSociosLogic.ValidarDni(Commun.CodigoUnidadNegocio, dni);
            }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarUsuarioVendedor()
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
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
                lista = oResp.List.Where(x => x.CodigoPerfil == 4 && x.Estado == true).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarMembresiasContrato(int codSocio)
        {
            List<ContratoDTO> lista = null;
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoSocio = codSocio;
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasContrato,
                User = "appsfit",
                Item = oContratoDTO,
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

        public ActionResult ListarContratoMembresia(int codigoMenbresia)
        {
            ContratoFolioDTO oContratoMembresiaDTO = new ContratoFolioDTO();
            oContratoMembresiaDTO.codigo_Membresia = codigoMenbresia;
            oContratoMembresiaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoMembresiaDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterContratoFolioDTO oReq = new ReqFilterContratoFolioDTO()
            {
                FilterCase = filterCaseContratoFolioDTO.porCodigo,
                Item = oContratoMembresiaDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };
            RespItemContratoFolioDTO oResp = null;
            using (ContratoFolioLogic oContratoMembresiaLogic = new ContratoFolioLogic())
            {
                oResp = oContratoMembresiaLogic.ContratoFolioGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoMembresiaDTO = oResp.Item;
            }
            return Json(oContratoMembresiaDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarMembresiaEditar(int codigoMenbresia)
        {
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoMenbresia = codigoMenbresia;
            oContratoDTO.CodigoSede = Commun.CodigoSede;
            oContratoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.porCodigo,
                Item = oContratoDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "appsfit"
            };
            RespItemContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
            }
            return Json(oContratoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarCuotas(int codigo)
        {
            List<ContratoCuotaDTO> lista = null;
            ContratoCuotaDTO oContratoCuotaDTO = new ContratoCuotaDTO();
            oContratoCuotaDTO.CodigoMembresia = codigo;
            oContratoCuotaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oContratoCuotaDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterContratoCuotaDTO oReq = new ReqFilterContratoCuotaDTO()
            {
                User = "Admin",
                Item = oContratoCuotaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListContratoCuotaDTO oResp = null;
            using (ContratoCuotaLogic oMembresiasCuotaLogic = new ContratoCuotaLogic())
            {
                oResp = oMembresiasCuotaLogic.ContratoCuotaGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarPagoMembresia_Anulados(DateTime FechaInicio, DateTime FechaFin)
        {
            List<PagosContratoDTO> lista = null;
            PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
            oPagoMembresiaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oPagoMembresiaDTO.CodigoSede = Commun.CodigoSede;

            oPagoMembresiaDTO.FechaInicio = FechaInicio;
            oPagoMembresiaDTO.FechaFin = FechaFin;

            ReqFilterPagosContratoDTO oReq = new ReqFilterPagosContratoDTO()
            {
                FilterCase = filterCasePagosContrato.uspListarPagoMembresia_Anulados,
                User = "Admin",
                Item = oPagoMembresiaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListPagosContratoDTO oResp = null;
            using (PagosContratoLogic oPagoMembresiaLogic = new PagosContratoLogic())
            {
                oResp = oPagoMembresiaLogic.PagosContratoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarHistorialPagos(int codMembresia)
        {
            List<PagosContratoDTO> lista = null;
            PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
            oPagoMembresiaDTO.CodigoMembresia = codMembresia;
            oPagoMembresiaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oPagoMembresiaDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterPagosContratoDTO oReq = new ReqFilterPagosContratoDTO()
            {
                FilterCase = filterCasePagosContrato.ListarPagosFormaPago,
                User = "Admin",
                Item = oPagoMembresiaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListPagosContratoDTO oResp = null;
            using (PagosContratoLogic oPagoMembresiaLogic = new PagosContratoLogic())
            {
                oResp = oPagoMembresiaLogic.PagosContratoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarSociosConFiltro_Contrato(int codigo, string Nombres, string Apellidos, string Dni)
        {
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoSocio = codigo;
            oClientesDTO.Nombre = Nombres;
            oClientesDTO.Apellidos = Apellidos;
            oClientesDTO.DNI = Dni;
            oClientesDTO.CodigoSede = Commun.CodigoSede;
            oClientesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspBuscarSociosConFiltro_Contrato,
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

        public ActionResult GuardarSocioContrato(int Codigo, string Nombres, string Apellido, int TipoDocumento, string Dni, string FechaNacimiento, string Genero, int EstadoCivil, string Correo, string Distrito, bool Estado, string Telefono, string Celular, string UrlFacebook, string Ocupacion, string DireccionTrabajo, int TipoCliente, string TelefonoTrabajo, int Referido, string Ubicaciones, int Hijos, string Direccion, int TipoClienteSocioAgenda, string Nota)
        {

            //CodigoResultado = 0 // Es oblogatorio ingresar el nro de documento 
            //CodigoResultado = mayor igual a 1 // Se guardo correctamente el socio
            //CodigoResultado = "error" // Se guardo correctamente el socio
            //VALIDAR SI EL INGRESO DE DNI A PROSPECTOS ES OBLIGATORIO
            //VERIFICAR CONFIGURACION
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracionServer();

            if (ConfiguracionDTO.ObligatorioDNIProspectos && Dni == string.Empty)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            if (Codigo == 0)
            {
                //VALIDAR NRO DOCUMENTO DE PROSPECTOS SI EXISTE Y OBTENER UNA LISTA
                if (Dni != string.Empty)
                {
                    List<ProspectosTablaDTO> lista = null;
                    ReqFilterProspectosDTO oReqList = new ReqFilterProspectosDTO()
                    {
                        Item = new ProspectosTablaDTO()
                        {
                            CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                            CodigoSede = Commun.CodigoSede,
                            DNI = Dni
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
            }

            //SI EL DNI NO EXISTE ENTONCES SI SE PUEDE GUARDAR          
            DateTime? fechaNaci = null;
            if (FechaNacimiento != "null")
            {
                fechaNaci = new DateTime(Convert.ToInt32(FechaNacimiento.Split('/')[2]), Convert.ToInt32(FechaNacimiento.Split('/')[0]), Convert.ToInt32(FechaNacimiento.Split('/')[1]));
            }
            else
            {
                fechaNaci = DateTime.Now;
            }
            List<ClientesDTO> list = new List<ClientesDTO>();
            list.Add(new ClientesDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoS = 0,
                CodigoSocio = Codigo,
                Nombres = Nombres,
                Apellidos = Apellido,
                DNI = Dni,
                Telefono = Telefono,
                Celular = Celular,
                Correo = Correo,
                FechaNacimiento = fechaNaci,
                ImagenUrl = Genero == "M" ? "../Imagenes/fitness/PerfilHombre.png" : "../Imagenes/fitness/PerfilMujer.png",
                Estado = Estado,
                Genero = Genero,
                UsuarioCreacion = Commun.Usuario,
                UrlFacebook = UrlFacebook,
                ReferidoPor = Referido,
                Direccion = Direccion,
                Distrito = Distrito,
                Ocupacion = Ocupacion,
                TipoCliente = TipoCliente,
                Ubicaciones = Ubicaciones,
                TipoDocumento = TipoDocumento,
                DireccionTrabajo = DireccionTrabajo,
                EstadoCivil = EstadoCivil,
                EstadoHijos = Hijos,
                TelefonoTrabajo = TelefonoTrabajo,
                TipoClienteAgenda = TipoClienteSocioAgenda,
                Nota = Nota,
                Operation = Operation.Create // : Operation.Update,
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
                if (oResp.MessageList[0].Codigo == 0)
                {
                    return Json("errorcodigo", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(oResp.MessageList[0].Codigo, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult uspValidarNroComprobante(int CodigoTipoComprobante, int CodigoSubTipoDocumento, string NroComprobante)
        {
            VentasDTO oVentasDTO = new VentasDTO();

            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;
            oVentasDTO.CodigoTipoComprobante = CodigoTipoComprobante;
            oVentasDTO.CodigoSubTipoDocumento = CodigoSubTipoDocumento;
            oVentasDTO.NroComprobante = NroComprobante;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspValidarNroComprobante,
                Item = oVentasDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
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
            return Json(oVentasDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarSalida(int codigoSalida, int CodigoSocio, int CodigoTipoDocumentoSocio, string RazonSocial_Sr,
                                       string RUC_DNI, string Direccion, DateTime FechaVenta,
                                       int CodigoTipoComprobante, int CodigoSubTipoComprobante, string NroComprobante,
                                       string NroTarjeta, int TipoMoneda, int FormaPago,
                                       decimal SubTotal, decimal IGV, decimal TotalNeto,
                                       decimal tipoCambio, string listaDetalle, string listaFormaPago,
                                       decimal TotalDolares, string listaCuotas, string RucContribuyente, string RazonContribuyente, string DireccionFiscal, string Observaciones)
        {
            string mensaje = "";

            //DETALLE SALIDA Y SALIDA
            List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);
            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            List<printProductos> listaProductosEmprimir = new List<printProductos>();

            foreach (XmlElement nodo in detalle)
            {
                VentasDetalleDTO oitem = new VentasDetalleDTO();
                printProductos oitemPrint = new printProductos();
                oitem.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oitem.CodigoSalidaDetalle = 0;
                oitem.CodigoProducto = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Tipo = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                oitem.Cantidad = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                oitemPrint.cantidad = nodo.ChildNodes[2].InnerText; //cantidad
                oitem.Descripcion = nodo.ChildNodes[3].InnerText;
                oitemPrint.producto = nodo.ChildNodes[3].InnerText.Split('-')[0]; //producto
                oitem.codigoDetalle_Ingreso = Convert.ToInt32(nodo.ChildNodes[6].InnerText);
                oitem.CodigoPedido = Convert.ToInt32(nodo.ChildNodes[7].InnerText);
                oitem.AsesorComercial = nodo.ChildNodes[8].InnerText;
                oitem.TipoIngresoMembre = nodo.ChildNodes[9].InnerText;
                string pruebaPrecioU = nodo.ChildNodes[4].InnerText;//.Replace(".", ",");
                string pruebaImporte = nodo.ChildNodes[5].InnerText;//.Replace(".", ",");
                decimal newImporte = Convert.ToDecimal(pruebaImporte);
                oitem.PrecioUnitario = decimal.Parse(pruebaPrecioU);
                oitem.Importe = decimal.Parse(pruebaImporte);
                oitem.CodigoSede = Commun.CodigoSede;
                oitemPrint.precio = Convert.ToString(newImporte);
                Detalle.Add(oitem);
                listaProductosEmprimir.Add(oitemPrint);
            }

            //FORMA DE PAGO
            List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.LoadXml(listaFormaPago);
            XmlNodeList detallesFP = xmlDoc2.GetElementsByTagName("ds");
            XmlNodeList detalleFP = ((XmlElement)detallesFP[0]).GetElementsByTagName("d");
            foreach (XmlElement nodo in detalleFP)
            {
                ControlSalidaFormaPagoDTO oItemFP = new ControlSalidaFormaPagoDTO();
                oItemFP.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oItemFP.Codigo = 0;
                oItemFP.TipoMoneda = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                string FP_Monto = nodo.ChildNodes[1].InnerText;//.Replace(".", ",");
                oItemFP.Monto = decimal.Parse(FP_Monto);
                string FP_TipoCambio = nodo.ChildNodes[2].InnerText;//.Replace(".", ",");
                oItemFP.TipoCambio = decimal.Parse(FP_TipoCambio);

                oItemFP.FormaPago = Convert.ToInt32(nodo.ChildNodes[3].InnerText);
                oItemFP.SubFormaPago = Convert.ToInt32(nodo.ChildNodes[4].InnerText);
                oItemFP.NroBoucher = nodo.ChildNodes[5].InnerText.ToString();
                oItemFP.UrlBoucher = "";
                FPDetalle.Add(oItemFP);
            }
            //Cuotas
            List<ContratoCuotaDTO> FPCuotas = new List<ContratoCuotaDTO>();
            XmlDocument xmlDoc3 = new XmlDocument();
            xmlDoc3.LoadXml(listaCuotas);
            XmlNodeList detallesCuotas = xmlDoc3.GetElementsByTagName("ds");
            XmlNodeList detalleCuotas = ((XmlElement)detallesCuotas[0]).GetElementsByTagName("d");
            foreach (XmlElement nodo in detalleCuotas)
            {
                ContratoCuotaDTO oItemCuotas = new ContratoCuotaDTO();
                oItemCuotas.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oItemCuotas.CodigoSede = Commun.CodigoSede;
                oItemCuotas.CodigoCuota = 0;
                int anio = Convert.ToInt32(nodo.ChildNodes[0].InnerText.ToString().Split('/')[2]);
                int mes = Convert.ToInt32(nodo.ChildNodes[0].InnerText.ToString().Split('/')[1]);
                int dia = Convert.ToInt32(nodo.ChildNodes[0].InnerText.ToString().Split('/')[0]);
                DateTime Fecha = new DateTime(anio, mes, dia);

                oItemCuotas.Fecha = Fecha;//Convert.ToDateTime(nodo.ChildNodes[0].InnerText.ToString());

                string FP_Monto = nodo.ChildNodes[1].InnerText;//.Replace(".", ",");
                oItemCuotas.Monto = decimal.Parse(FP_Monto);
                oItemCuotas.CodigoMembresia = Convert.ToInt32(nodo.ChildNodes[2].InnerText);
                FPCuotas.Add(oItemCuotas);
            }

            //Verificar Configuracion
            ConfiguracionDTO ConfiguracionDTO = new ConfiguracionDTO();
            ConfiguracionDTO = BuscarConfiguracionServer();

            if (CodigoTipoComprobante == 3)
            {
                ConfiguracionDTO.TieneFacturacionElectronica = false;
            }

            List<VentasDTO> lista = new List<VentasDTO>();
            lista.Add(new VentasDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoIngreso = codigoSalida,
                CodigoSocio = CodigoSocio,
                RazonSocial_Sr = RazonSocial_Sr,
                RUC_DNI = RUC_DNI,
                Direccion = Direccion,
                FechaVenta = FechaVenta,
                CodigoTipoComprobante = CodigoTipoComprobante,
                CodigoSubTipoDocumento = CodigoSubTipoComprobante,
                NroComprobante = NroComprobante,
                NroTarjeta = NroTarjeta,
                TipoMoneda = TipoMoneda,
                FormaPago = FormaPago,
                SubTotal = SubTotal,
                IGV = IGV,
                TotalNeto = TotalNeto,
                tipoCambio = tipoCambio,
                UsuarioCreacion = Commun.Usuario,
                User = Commun.Usuario,
                ListaDetalle = Detalle,
                ListaFormaPago = FPDetalle,
                ListaCuotas = FPCuotas,
                Comentario = Observaciones,
                Estado = true,
                SerieComprobante = NroComprobante.Split('-')[0],
                Operation = E_DataModel.Common.Operation.Create,
                TotalDolares = TotalDolares,
                GenerarSerie = ConfiguracionDTO.GenerarSerie,
                TieneFacturacionElectronica = ConfiguracionDTO.TieneFacturacionElectronica,


                CodigoTipoDocumentoSocio = CodigoTipoDocumentoSocio,
                RazonSocialContribuyente = RazonContribuyente,
                RucContribuyente = RucContribuyente,
                DireccionFiscalEmpresa = DireccionFiscal
            });

            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                User = Commun.Usuario,
                List = lista
            };
            RespVentasDTO oResp = null;

            var rptaFact = new Respuesta();
            if (ConfiguracionDTO.TieneFacturacionElectronica)
            {
                lista[0].Comentario = Observaciones;

                using (Helpers.NubefactService facturacion = new Helpers.NubefactService())
                {
                    rptaFact = facturacion.EjecutarWebService(lista[0], ConfiguracionDTO);
                }
                if (string.IsNullOrEmpty(rptaFact.errors))
                {
                    using (VentasLogic oControlSalidaLogic = new VentasLogic())
                    {
                        oReq.List[0].NroComprobante = string.Format("{0}-{1}", rptaFact.serie, rptaFact.numero);
                        oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                    }

                    if (oResp.Success)
                    {
                        mensaje = (oResp.MessageList[0].Codigo).ToString();
                        string data_base64_pdf = "";
                        if (!string.IsNullOrEmpty(rptaFact.enlace_del_pdf))
                        {
                            using (var client = new WebClient())
                            {
                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                var bytes = client.DownloadData(rptaFact.enlace_del_pdf);
                                data_base64_pdf = Convert.ToBase64String(bytes);
                            }
                        }
                        //return Json((mensaje + "|" + "2" + "|" + rptaFact.enlace_del_pdf ?? string.Empty), JsonRequestBehavior.AllowGet);
                        return Json((mensaje + "|" + "2" + "|" + data_base64_pdf ?? string.Empty), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json((mensaje + "|" + "4" + "|" + (oResp.MessageList.Count > 0 ? oResp.MessageList[0].Detalle ?? string.Empty : string.Empty)), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json((mensaje + "|" + "3" + "|" + rptaFact.errors ?? string.Empty), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {//Metodo Cristofer (Sin facturacion)

                using (VentasLogic oControlSalidaLogic = new VentasLogic())
                {
                    oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    mensaje = (oResp.MessageList[0].Codigo).ToString();
                }
                if (ConfiguracionDTO.GenerarComprobante)
                {
                    return Json((mensaje + "|" + "1"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json((mensaje + "|" + "0"), JsonRequestBehavior.AllowGet);
                }

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

        public ActionResult BuscarConfiguracion()
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
            return Json(oConfiguracionDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarInformacionGeneralVentaPorCodigo(int codigo)
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
            return Json(oVentasDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarFormaPagoDeVentaPorCodigo(int CodigoVenta)
        {
            List<ControlSalidaFormaPagoDTO> lista = null;
            ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
            oControlSalidaFormaPagoDTO.CodigoIngreso = CodigoVenta;
            oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterControlSalidaFormaPagoDTO oReq = new ReqFilterControlSalidaFormaPagoDTO()
            {
                FilterCase = filterCaseControlSalidaFormaPago.Filter_uspListarControlSalidaFormaPago,
                Item = oControlSalidaFormaPagoDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = Commun.Usuario
            };
            RespListControlSalidaFormaPagoDTO oResp = null;
            using (ControlSalidaFormaPagoLogic oControlSalidaFormaPagoLogic = new ControlSalidaFormaPagoLogic())
            {
                oResp = oControlSalidaFormaPagoLogic.ControlSalidaFormaPagoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<ControlSalidaFormaPagoDTO>();
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarInformacionDetalleDeVentaPorCodigo(int CodigoVenta)
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
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AnularPago(int CodigoPagoMembresia, string Comentario)
        {
            int Cod = 0;
            List<PagosContratoDTO> oList = new List<PagosContratoDTO>();
            oList.Add(new PagosContratoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoPagoMembresia = CodigoPagoMembresia,
                Comentario = Comentario,
                UsuarioCreacion = Commun.Usuario,
                Operation = E_DataModel.Common.Operation.Delete
            });

            ReqPagosContratoDTO oReq = new ReqPagosContratoDTO()
            {
                List = oList,
                User = Commun.Usuario
            };

            RespPagosContratoDTO oResp = null;
            using (PagosContratoLogic oPagoMembresiaLogic = new PagosContratoLogic())
            {
                oResp = oPagoMembresiaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Cod = oResp.MessageList[0].Codigo;
            }
            return Json(Cod, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarCuota(int codigo)
        {
            int mensaje = 0;
            List<ContratoCuotaDTO> oList = new List<ContratoCuotaDTO>();
            oList.Add(new ContratoCuotaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoCuota = codigo,
                Operation = E_DataModel.Common.Operation.Delete,
                UsuarioCreacion = Commun.Usuario
            });
            ReqContratoCuotaDTO oReq = new ReqContratoCuotaDTO()
            {
                List = oList,
                User = Commun.Usuario
            };
            RespContratoCuotaDTO oResp = null;
            using (ContratoCuotaLogic oRutinaLogic = new ContratoCuotaLogic())
            {
                oResp = oRutinaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarCuota(int codigoMembresia, DateTime fecha, decimal monto)
        {
            int mensaje = 0;
            List<ContratoCuotaDTO> list = new List<ContratoCuotaDTO>();
            list.Add(new ContratoCuotaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoCuota = 0,
                CodigoMembresia = codigoMembresia,
                Monto = monto,
                Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day),
                Operation = Operation.Create
            });
            ReqContratoCuotaDTO oReq = new ReqContratoCuotaDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespContratoCuotaDTO oResp = null;
            using (ContratoCuotaLogic oMembresiasCuotaLogic = new ContratoCuotaLogic())
            {
                oResp = oMembresiasCuotaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        #endregion

        public ActionResult planesmembresias()
        {

            return View();
        }

        #region Metodos PLANES MEMBRESIAS PRESENCIAL


        public ActionResult listaTipoPaquete()
        {
            List<TipoPaqueteDTO> lista = null;
            TipoPaqueteDTO oTipoPaqueteDTO = new TipoPaqueteDTO();
            oTipoPaqueteDTO.CodigoSede = Commun.CodigoSede;
            oTipoPaqueteDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterTipoPaqueteDTO oReq = new ReqFilterTipoPaqueteDTO()
            {
                User = Commun.Usuario,
                Item = oTipoPaqueteDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListTipoPaqueteDTO oResp = null;
            using (TipoPaqueteLogic oTipoPaqueteLogic = new TipoPaqueteLogic())
            {
                oResp = oTipoPaqueteLogic.TipoPaqueteGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaTiempoMembresia(string nombre)
        {
            nombre = nombre == "undefined" ? string.Empty : nombre;
            List<TiempoMembresiaDTO> lista = null;
            ReqFilterTiempoMembresiaDTO oReq = new ReqFilterTiempoMembresiaDTO()
            {
                Item = new TiempoMembresiaDTO() { Descripcion = nombre },
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListTiempoMembresiaDTO oResp = null;
            using (TiempoMembresiaLogic oTiempoMembresiaLogic = new TiempoMembresiaLogic())
            {
                oResp = oTiempoMembresiaLogic.TiempoMembresiaGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult listarddlTipoPaquete()
        {
            List<TipoPaqueteDTO> lista = null;
            ReqFilterTipoPaqueteDTO oReq = new ReqFilterTipoPaqueteDTO()
            {
                Item = new TipoPaqueteDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede
                },
                FilterCase = filterCaseTipoPaquete.FilteruspListaDllTipoPaquete,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListTipoPaqueteDTO oResp = null;
            using (TipoPaqueteLogic oTipoPaqueteLogic = new TipoPaqueteLogic())
            {
                oResp = oTipoPaqueteLogic.TipoPaqueteGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarPaquetesMenbresiasCursos_Paginacion(bool Estado, int TipoPaquete, string Busqueda, int PageNumber)
        {
            List<PlanesDTO> lista = null;
            PlanesDTO oPaquetesDTO = new PlanesDTO();
            oPaquetesDTO.CodigoSede = Commun.CodigoSede;
            oPaquetesDTO.Estado = Estado;
            oPaquetesDTO.CodigoTipoPaquete = TipoPaquete;
            oPaquetesDTO.Busqueda = Busqueda;
            oPaquetesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.uspListarPaquetesMenbresiasCursos_Paginacion,
                User = "Admin",
                Item = oPaquetesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
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
            foreach (PlanesDTO item in lista)
            {
                DateTime f = new DateTime(item.FechaVencimiento.Year, item.FechaVencimiento.Month, item.FechaVencimiento.Day, 23, 30, 30);
                if (f >= DateTime.Now)
                {
                    item.DesEstado = "Vigente";
                    nuevoList.Add(item);
                }
                else
                {
                    item.DesEstado = "Finalizado";
                    nuevoList.Add(item);
                }
            }
            return Json(nuevoList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarPaquetesMenbresiasCursos_NumeroRegistros(bool Estado, int TipoPaquete, string Busqueda)
        {
            PlanesDTO oPaquetesDTO = new PlanesDTO();
            oPaquetesDTO.CodigoSede = Commun.CodigoSede;
            oPaquetesDTO.CodigoTipoPaquete = TipoPaquete;
            oPaquetesDTO.Busqueda = Busqueda;
            oPaquetesDTO.Estado = Estado;
            oPaquetesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.uspListarPaquetesMenbresiasCursos_NumeroRegistros,
                Item = oPaquetesDTO,
                User = Commun.Usuario
            };
            RespItemPlanesDTO oResp = null;
            using (PlanesLogic oPaquetesLogic = new PlanesLogic())
            {
                oResp = oPaquetesLogic.PlanesGetItem(oReq);
            }
            if (oResp.Success)
            {
                oPaquetesDTO = oResp.Item;
                oPaquetesDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarPaquetesMenbresiasCursos_Paginacion"]);
            }
            return Json(oPaquetesDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarPaqueteAdministracion(int codigo)
        {
            PlanesDTO oPaquetesDTO = new PlanesDTO();
            oPaquetesDTO.CodigoPaquete = codigo;
            oPaquetesDTO.CodigoSede = Commun.CodigoSede;
            oPaquetesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.porCodigo,
                Item = oPaquetesDTO,
                User = Commun.Usuario
            };
            RespItemPlanesDTO oResp = null;
            using (PlanesLogic oCargoLogic = new PlanesLogic())
            {
                oResp = oCargoLogic.PlanesGetItem(oReq);
            }
            if (oResp.Success)
            {
                oPaquetesDTO = oResp.Item;
            }
            return Json(oPaquetesDTO, JsonRequestBehavior.AllowGet);
        }
        public ActionResult uspListarHoraPaquete(int codigoHP)
        {
            List<HorarioPaqueteDetalleDTO> lista = null;
            HorarioPaqueteDetalleDTO oHorarioPaqueteDetalleDTO = new HorarioPaqueteDetalleDTO();
            oHorarioPaqueteDetalleDTO.CodigoHP = codigoHP;
            oHorarioPaqueteDetalleDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            ReqFilterHorarioPaqueteDetalleDTO oReq = new ReqFilterHorarioPaqueteDetalleDTO()
            {
                Item = oHorarioPaqueteDetalleDTO,
                FilterCase = filterCaseHorarioPaqueteDetalle.Filter_uspListarHoraPaquete,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListHorarioPaqueteDetalleDTO oResp = null;
            using (HorarioPaqueteDetalleLogic oHorarioPaqueteDetalleLogic = new HorarioPaqueteDetalleLogic())
            {
                oResp = oHorarioPaqueteDetalleLogic.HorarioPaqueteDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult verificarExiteDiaSemanaCurso(int CodigoPaquete, int dia)
        {
            int existe = 0;
            using (HorarioPaqueteLogic oHorarioPaqueteLogic = new HorarioPaqueteLogic())
            {
                existe = oHorarioPaqueteLogic.verificarExiteDiaSemanaCurso(Commun.CodigoUnidadNegocio, CodigoPaquete, dia);
            }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarAsignarDiasHorarioPaquete(int CodigoPaquete)
        {
            List<HorarioPaqueteDTO> lista = null;
            ReqFilterHorarioPaqueteDTO oReq = new ReqFilterHorarioPaqueteDTO()
            {
                Item = new HorarioPaqueteDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoPaquete = CodigoPaquete
                },
                FilterCase = filterCaseHorarioPaquete.uspListarDiasHorarioPaquete_visualizar,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };
            RespListHorarioPaqueteDTO oResp = null;
            using (HorarioPaqueteLogic oHorarioPaqueteLogic = new HorarioPaqueteLogic())
            {
                oResp = oHorarioPaqueteLogic.HorarioPaqueteGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspListarSedesPorSedesPermisos(int CodigoPaquete)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoPaquete = CodigoPaquete;
            List<ConfiguracionDTO> lista = null;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = filterCaseConfiguracion.uspListarSedesPorSedesPermisos,
                User = Commun.Usuario,
                Item = oConfiguracionDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListConfiguracionDTO oResp = null;

            using (ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic())
            {
                oResp = oConfiguracionLogic.ConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarHoraPaquete(int codigoHPD, int CodigoHP, DateTime HoraInicio, DateTime HoraFin, string accion)
        {
            string mensaje = string.Empty;
            List<HorarioPaqueteDetalleDTO> list = new List<HorarioPaqueteDetalleDTO>();
            list.Add(new HorarioPaqueteDetalleDTO()
            {
                CodigoHPD = codigoHPD,
                CodigoHP = CodigoHP,
                HoraInicio = HoraInicio,
                horaFin = HoraFin,
                UsuarioCreacion = Commun.Usuario,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = Operation.Create
            });
            ReqHorarioPaqueteDetalleDTO oReq = new ReqHorarioPaqueteDetalleDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespHorarioPaqueteDetalleDTO oResp = null;
            using (HorarioPaqueteDetalleLogic oHorarioPaqueteDetalleLogic = new HorarioPaqueteDetalleLogic())
            {
                oResp = oHorarioPaqueteDetalleLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarPaquete(int codigo, string descripcion, int valor, int FrezenDisponibles, decimal costo, string accion, bool estado, DateTime FechaVencimiento, int CodTipCli, int CodigoTipoPaquete, int TiempoMembresia, int NroCupo, DateTime FechaInicio, int EstadoFecha, int NroIngresoDia, bool EstadoMembresiaInterdiaria,bool showapp)
        {
            int mensaje = 0;
            DateTime fechaAyer = DateTime.Now;
            fechaAyer = fechaAyer.AddDays(-1);
            DateTime fechaFutura = DateTime.Now;
            fechaFutura = fechaFutura.AddMonths(+3);

            List<PlanesDTO> list = new List<PlanesDTO>();
            if (estado == true)
            {
                if (FechaVencimiento >= DateTime.Now)
                {
                    list.Add(new PlanesDTO()
                    {
                        CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                        CodigoPaquete = codigo,
                        Descripcion = descripcion,
                        valor = valor,
                        CongelamientoVigente = FrezenDisponibles,
                        Costo = costo,
                        Estado = estado,
                        FechaVencimiento = FechaVencimiento,
                        Operation = accion == "N" ? Operation.Create : Operation.Update,
                        UsuarioCreacion = Commun.Usuario,
                        UsuarioEdicion = Commun.Usuario,
                        CodigoSede = Commun.CodigoSede,
                        CodigoTipoCliente = CodTipCli,
                        CodigoTipoPaquete = CodigoTipoPaquete,
                        TiempoMembresia = TiempoMembresia,
                        NroCupo = NroCupo,
                        FechaInicio = FechaInicio,
                        EstadoFecha = EstadoFecha,
                        NroIngresoDia = NroIngresoDia,
                        EstadoMembresiaInterdiaria = EstadoMembresiaInterdiaria,
                        ShowApp = showapp,
                    });
                }
                else
                {
                    list.Add(new PlanesDTO()
                    {
                        CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                        CodigoPaquete = codigo,
                        Descripcion = descripcion,
                        valor = valor,
                        CongelamientoVigente = FrezenDisponibles,
                        Costo = costo,
                        Estado = estado,
                        FechaVencimiento = FechaVencimiento, /*desactivado por pedido de JPFitnes(ojo no puede guardar)*///fechaFutura,
                        Operation = accion == "N" ? Operation.Create : Operation.Update,
                        UsuarioCreacion = Commun.Usuario,
                        UsuarioEdicion = Commun.Usuario,
                        CodigoSede = Commun.CodigoSede,
                        CodigoTipoCliente = CodTipCli,
                        CodigoTipoPaquete = CodigoTipoPaquete,
                        TiempoMembresia = TiempoMembresia,
                        NroCupo = NroCupo,
                        FechaInicio = FechaInicio,
                        EstadoFecha = EstadoFecha,
                        NroIngresoDia = NroIngresoDia,
                        EstadoMembresiaInterdiaria = EstadoMembresiaInterdiaria,
                        ShowApp = showapp,
                    });
                }

            }
            else
            {
                list.Add(new PlanesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoPaquete = codigo,
                    Descripcion = descripcion,
                    valor = valor,
                    CongelamientoVigente = FrezenDisponibles,
                    Costo = costo,
                    Estado = estado,
                    FechaVencimiento = fechaAyer,
                    Operation = accion == "N" ? Operation.Create : Operation.Update,
                    UsuarioCreacion = Commun.Usuario,
                    UsuarioEdicion = Commun.Usuario,
                    CodigoSede = Commun.CodigoSede,
                    CodigoTipoCliente = CodTipCli,
                    CodigoTipoPaquete = CodigoTipoPaquete,
                    TiempoMembresia = TiempoMembresia,
                    NroCupo = NroCupo,
                    FechaInicio = FechaInicio,
                    EstadoFecha = EstadoFecha,
                    NroIngresoDia = NroIngresoDia,
                    EstadoMembresiaInterdiaria = EstadoMembresiaInterdiaria,
                    ShowApp = showapp,
                });
            }
            ReqPlanesDTO oReq = new ReqPlanesDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespPlanesDTO oResp = null;
            using (PlanesLogic oPaquetesLogic = new PlanesLogic())
            {
                oResp = oPaquetesLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarHoraPaquete(int CodHPD)
        {
            string mensaje = string.Empty;
            List<HorarioPaqueteDetalleDTO> oList = new List<HorarioPaqueteDetalleDTO>();
            oList.Add(new HorarioPaqueteDetalleDTO()
            {
                CodigoHPD = CodHPD,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                Operation = E_DataModel.Common.Operation.Delete
            });
            ReqHorarioPaqueteDetalleDTO oReq = new ReqHorarioPaqueteDetalleDTO()
            {
                List = oList,
                User = "appsfit"
            };
            RespHorarioPaqueteDetalleDTO oResp = null;
            using (HorarioPaqueteDetalleLogic oHorarioPaqueteDetalleLogic = new HorarioPaqueteDetalleLogic())
            {
                oResp = oHorarioPaqueteDetalleLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Los datos se eliminaron correctamente.";
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarPaquete(int codigo)
        {
            int mensaje = 0;
            List<PlanesDTO> oList = new List<PlanesDTO>();
            oList.Add(new PlanesDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoPaquete = codigo,
                Operation = E_DataModel.Common.Operation.Delete
            });
            ReqPlanesDTO oReq = new ReqPlanesDTO()
            {
                List = oList,
                User = "appsfit"
            };
            RespPlanesDTO oResp = null;
            using (PlanesLogic oRutinaLogic = new PlanesLogic())
            {
                oResp = oRutinaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarHorarioPaquete(int CodigoPaquete, string xml_HorarioPaquete, string Accion)
        {

            string mensaje = string.Empty;
            List<HorarioPaqueteDTO> list = new List<HorarioPaqueteDTO>();
            /* Colaborador */
            List<HorarioPaqueteDTO> Detalle_H = new List<HorarioPaqueteDTO>();

            XmlDocument xmlDoc_HorarioPaquete = new XmlDocument();
            xmlDoc_HorarioPaquete.LoadXml(xml_HorarioPaquete);

            XmlNodeList detalles_HorarioPaquete = xmlDoc_HorarioPaquete.GetElementsByTagName("ds");
            XmlNodeList detalle_H = ((XmlElement)detalles_HorarioPaquete[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalle_H)
            {
                HorarioPaqueteDTO oitem = new HorarioPaqueteDTO();
                oitem.Dia = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Estado = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                Detalle_H.Add(oitem);

            }
            list.Add(new HorarioPaqueteDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoPaquete = CodigoPaquete,
                Operation = Operation.Create,
                UsuarioCreacion = Commun.Usuario,
                ListaDetalle_H = Detalle_H,
            });
            ReqHorarioPaqueteDTO oReq = new ReqHorarioPaqueteDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespHorarioPaqueteDTO oResp = null;
            using (HorarioPaqueteLogic oHorarioPaqueteLogic = new HorarioPaqueteLogic())
            {
                oResp = oHorarioPaqueteLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspRegistrarPaqueteSedePermiso(int CodigoPaquete, string xml_SedePaquete)
        {
            int mensaje = 0;
            List<ConfiguracionDTO> list = new List<ConfiguracionDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml_SedePaquete);
            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalle)
            {
                ConfiguracionDTO oitem = new ConfiguracionDTO();

                oitem.CodigoPermiso = 0;
                oitem.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oitem.UsuarioCreacion = Commun.Usuario;
                oitem.CodigoPaquete = CodigoPaquete;
                oitem.Operation = Operation.uspRegistrarPaqueteSedePermiso;
                oitem.CodigoSede = Convert.ToInt32(nodo.ChildNodes[0].InnerText);
                oitem.Estado = Convert.ToInt32(nodo.ChildNodes[1].InnerText);
                list.Add(oitem);
            }

            List<ConfiguracionDTO> cabezera = new List<ConfiguracionDTO>();

            cabezera.Add(new ConfiguracionDTO()
            {
                CodigoPermiso = 0,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoPaquete = CodigoPaquete,
                Operation = Operation.uspRegistrarPaqueteSedePermiso,
                UsuarioCreacion = Commun.Usuario,
                Lista = list
            });

            ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
            {
                List = cabezera,
                User = "appsfit"
            };

            RespConfiguracionDTO oResp = null;
            using (ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic())
            {
                oResp = oConfiguracionLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region METAS COMERCIALES

        public ActionResult historialmetas()
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


        public ActionResult uspListarMetasDetalle(int CodigoMeta)
        {
            List<MetasDetalleDTO> lista = null;
            ReqFilterMetasDetalleDTO oReq = new ReqFilterMetasDetalleDTO()
            {
                FilterCase = filterCaseMetasDetalle.uspListarMetasDetalle,
                Item = new MetasDetalleDTO()
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
            RespListMetasDetalleDTO oResp = null;
            using (MetasDetalleLogic oMetasDetalleLogic = new MetasDetalleLogic())
            {
                oResp = oMetasDetalleLogic.MetasDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarVendedores()
        {
            List<MetasDTO> lista = null;
            ReqFilterMetasDTO oReq = new ReqFilterMetasDTO()
            {
                FilterCase = filterCaseMetas.ListarporVendedores,
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

        public ActionResult uspEliminarMetaVendedor(int CodigoMeta)
        {
            List<MetasDTO> lista = new List<MetasDTO>();
            MetasDTO oMetasDTO = new MetasDTO();
            oMetasDTO.CodigoEntidadNegocio = Commun.CodigoUnidadNegocio;
            oMetasDTO.CodigoSede = Commun.CodigoSede;
            oMetasDTO.CodigoMeta = CodigoMeta;
            oMetasDTO.UsuarioCreacion = Commun.Usuario;
            oMetasDTO.Operation = Operation.Delete;
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
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarMeta(int codigoMeta,
                                      decimal Meta, int CantidadVendedores, decimal Bono,
                                      DateTime FechaInicio, DateTime FechaFin, int CodigoSupervisorVenta,
                                      decimal B_TicketPromedio_MontoMinimo, decimal B_TicketPromedio_Bono, int B_ContratosAnuales_CantidadMinima, decimal B_ContratosAnuales_Bono,
                                      int B_Nuevos_PorcentajeMinimo, decimal B_Nuevos_Bono, decimal B_Reinscripciones_MontoMinimo, decimal B_Reinscripciones_Bono,
                                      int B_Renovaciones_PorcentajeMinimo, decimal B_Renovaciones_Bono, decimal B_VentaSemanal_Bono,
                                      decimal B_VentaAdicionalMeta10porciento_Bono, int B_AmpliacionContrato_Cantidad, decimal B_AmpliacionContrato_Bono,
                                      decimal metaSemanal,
                                      decimal txtComision1a, decimal txtComision1b, decimal txtComision1porc,
                                      decimal txtComision2a, decimal txtComision2b, decimal txtComision2porc,
                                      decimal txtComision3a, decimal txtComision3b, decimal txtComision3porc,
                                      decimal txtComision4a, decimal txtComision4b, decimal txtComision4porc,
                                      decimal txtComision5a, decimal txtComision5b, decimal txtComision5porc,
                                      decimal txtComision6a, decimal txtComision6b, decimal txtComision6porc,
                                      string xmlMeta, string Accion,
                                      decimal txtPorcenBonoAdicional1, decimal txtMonto_BonoAdicional1, decimal txtPorcenBonoAdicional2, decimal txtMonto_BonoAdicional2,
                                      decimal txtPorcenBonoAdicional3, decimal txtMonto_BonoAdicional3, decimal txtPorcenBonoAdicional4, decimal txtMonto_BonoAdicional4,
                                      decimal txtPorcenBonoAdicional5, decimal txtMonto_BonoAdicional5, decimal txtPorcenBonoAdicional6, decimal txtMonto_BonoAdicional6,
                                      DateTime txtFechaSemanal1a, DateTime txtFechaSemanal1b, decimal txtCuotaSemanalBono1,
                                      DateTime txtFechaSemanal2a, DateTime txtFechaSemanal2b, decimal txtCuotaSemanalBono2,
                                      DateTime txtFechaSemanal3a, DateTime txtFechaSemanal3b, decimal txtCuotaSemanalBono3,
                                      DateTime txtFechaSemanal4a, DateTime txtFechaSemanal4b, decimal txtCuotaSemanalBono4, decimal txtMetaMinimaPorc)
        {
            string mensaje = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlMeta);

            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("e");

            List<MetasDetalleDTO> listDetalle = new List<MetasDetalleDTO>();

            foreach (XmlElement nodo in detalle)
            {
                MetasDTO oitem = new MetasDTO();

                //string newMeta = nodo.ChildNodes[2].InnerText.Replace('.', ',');
                //string newMetaSemanal = nodo.ChildNodes[3].InnerText.Replace('.', ',');

                string newMeta = nodo.ChildNodes[2].InnerText.Replace(',', '.');
                string newMetaSemanal = nodo.ChildNodes[3].InnerText.Replace(',', '.');

                listDetalle.Add(new MetasDetalleDTO()
                {
                    CodigoEntidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoMeta = codigoMeta,
                    Codigo = Convert.ToInt32(nodo.ChildNodes[0].InnerText),
                    CodigoVendedor = Convert.ToInt32(nodo.ChildNodes[1].InnerText),
                    Meta = Decimal.Parse(newMeta),
                    MetaSemanal = Decimal.Parse(newMetaSemanal),
                    CantidadMetaPlan = Convert.ToInt32(nodo.ChildNodes[4].InnerText),
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    UsuarioCreacion = Commun.Usuario,
                    Operation = Convert.ToInt32(nodo.ChildNodes[0].InnerText) == 0 ? Operation.Create : Operation.Update
                });

            }

            List<MetasDTO> lista = new List<MetasDTO>();
            MetasDTO oMetasDTO = new MetasDTO();
            oMetasDTO.CodigoEntidadNegocio = Commun.CodigoUnidadNegocio;
            oMetasDTO.CodigoSede = Commun.CodigoSede;
            oMetasDTO.CodigoMeta = codigoMeta;
            oMetasDTO.Meta = Meta;
            oMetasDTO.Bono = Bono;
            oMetasDTO.FechaInicio = FechaInicio;
            oMetasDTO.FechaFin = FechaFin;
            oMetasDTO.CantidadVendedores = CantidadVendedores;
            oMetasDTO.CodigoSupervisorVenta = CodigoSupervisorVenta;
            oMetasDTO.B_TicketPromedio_MontoMinimo = B_TicketPromedio_MontoMinimo;
            oMetasDTO.B_TicketPromedio_Bono = B_TicketPromedio_Bono;
            oMetasDTO.B_Nuevos_PorcentajeMinimo = B_Nuevos_PorcentajeMinimo;
            oMetasDTO.B_Nuevos_Bono = B_Nuevos_Bono;
            oMetasDTO.B_Reinscripciones_MontoMinimo = B_Reinscripciones_MontoMinimo;
            oMetasDTO.B_Reinscripciones_Bono = B_Reinscripciones_Bono;
            oMetasDTO.B_Renovaciones_PorcentajeMinimo = B_Renovaciones_PorcentajeMinimo;
            oMetasDTO.B_Renovaciones_Bono = B_Renovaciones_Bono;
            oMetasDTO.B_ContratosAnuales_CantidadMinima = B_ContratosAnuales_CantidadMinima;
            oMetasDTO.B_ContratosAnuales_Bono = B_ContratosAnuales_Bono;
            oMetasDTO.B_VentaSemanal_Bono = B_VentaSemanal_Bono;
            oMetasDTO.B_VentaAdicionalMeta10porciento_Bono = B_VentaAdicionalMeta10porciento_Bono;
            oMetasDTO.B_AmpliacionContrato_Cantidad = B_AmpliacionContrato_Cantidad;
            oMetasDTO.B_AmpliacionContrato_Bono = B_AmpliacionContrato_Bono;

            oMetasDTO.MetaSemanal = metaSemanal;
            oMetasDTO.Comision1a = txtComision1a;
            oMetasDTO.Comision1b = txtComision1b;
            oMetasDTO.Comision1porc = txtComision1porc;

            oMetasDTO.Comision2a = txtComision2a;
            oMetasDTO.Comision2b = txtComision2b;
            oMetasDTO.Comision2porc = txtComision2porc;
            oMetasDTO.Comision3a = txtComision3a;
            oMetasDTO.Comision3b = txtComision3b;
            oMetasDTO.Comision3porc = txtComision3porc;
            oMetasDTO.Comision4a = txtComision4a;
            oMetasDTO.Comision4b = txtComision4b;
            oMetasDTO.Comision4porc = txtComision4porc;
            oMetasDTO.Comision5a = txtComision5a;
            oMetasDTO.Comision5b = txtComision5b;
            oMetasDTO.Comision5porc = txtComision5porc;
            oMetasDTO.Comision6a = txtComision6a;
            oMetasDTO.Comision6b = txtComision6b;
            oMetasDTO.Comision6porc = txtComision6porc;

            oMetasDTO.PorcenBonoAdicional1 = txtPorcenBonoAdicional1;
            oMetasDTO.Monto_BonoAdicional1 = txtMonto_BonoAdicional1;
            oMetasDTO.PorcenBonoAdicional2 = txtPorcenBonoAdicional2;
            oMetasDTO.Monto_BonoAdicional2 = txtMonto_BonoAdicional2;
            oMetasDTO.PorcenBonoAdicional3 = txtPorcenBonoAdicional3;
            oMetasDTO.Monto_BonoAdicional3 = txtMonto_BonoAdicional3;
            oMetasDTO.PorcenBonoAdicional4 = txtPorcenBonoAdicional4;
            oMetasDTO.Monto_BonoAdicional4 = txtMonto_BonoAdicional4;
            oMetasDTO.PorcenBonoAdicional5 = txtPorcenBonoAdicional5;
            oMetasDTO.Monto_BonoAdicional5 = txtMonto_BonoAdicional5;
            oMetasDTO.PorcenBonoAdicional6 = txtPorcenBonoAdicional6;
            oMetasDTO.Monto_BonoAdicional6 = txtMonto_BonoAdicional6;

            oMetasDTO.FechaSemanal1a = txtFechaSemanal1a;
            oMetasDTO.FechaSemanal1b = txtFechaSemanal1b;
            oMetasDTO.CuotaSemanalBono1 = txtCuotaSemanalBono1;
            oMetasDTO.FechaSemanal2a = txtFechaSemanal2a;
            oMetasDTO.FechaSemanal2b = txtFechaSemanal2b;
            oMetasDTO.CuotaSemanalBono2 = txtCuotaSemanalBono2;
            oMetasDTO.FechaSemanal3a = txtFechaSemanal3a;
            oMetasDTO.FechaSemanal3b = txtFechaSemanal3b;
            oMetasDTO.CuotaSemanalBono3 = txtCuotaSemanalBono3;
            oMetasDTO.FechaSemanal4a = txtFechaSemanal4a;
            oMetasDTO.FechaSemanal4b = txtFechaSemanal4b;
            oMetasDTO.CuotaSemanalBono4 = txtCuotaSemanalBono4;
            oMetasDTO.MetaMinimaPorc = txtMetaMinimaPorc;


            oMetasDTO.UsuarioCreacion = Commun.Usuario;
            oMetasDTO.ListaDetalle = listDetalle;
            oMetasDTO.Operation = Accion == "N" ? Operation.Create : Operation.Update;
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
                codigoMeta = oResp.MessageList[0].Codigo;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        #endregion

        public ActionResult personalfijo()
        {

            return View();
        }

        #region PERSONAL_FIJO

        public ActionResult ListarPersonalAdministrativo()
        {
            PersonalAdministrativoDTO request = new PersonalAdministrativoDTO();
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;

            ReqFilterPersonalAdministrativoDTO oReq = new ReqFilterPersonalAdministrativoDTO()
            {
                Item = request,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oLogic = new PersonalAdministrativoLogic())
            {
                oResp = oLogic.PersonalAdministrativoGetList(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPersonalAdministrativoPorFiltros(PersonalAdministrativoDTO request)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            ReqFilterPersonalAdministrativoDTO oReq = new ReqFilterPersonalAdministrativoDTO()
            {
                Item = request,
                User = Commun.Usuario,
                FilterCase = filterCasePersonalAdministrativo.ListarPorFiltros,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oLogic = new PersonalAdministrativoLogic())
            {
                oResp = oLogic.PersonalAdministrativoGetList(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarPersonalAdministrativoGlobalPorNumeroDoc(PersonalAdministrativoDTO request)
        {
            ReqFilterPersonalAdministrativoDTO oReq = new ReqFilterPersonalAdministrativoDTO()
            {
                Item = request,
                User = Commun.Usuario,
                FilterCase = filterCasePersonalAdministrativo.BuscarPorNumeroDocumentoGlobal,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespItemPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oLogic = new PersonalAdministrativoLogic())
            {
                oResp = oLogic.PersonalAdministrativoGetItem(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarPersonalAdministrativoPorNumeroDocumento(PersonalAdministrativoDTO request)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            ReqFilterPersonalAdministrativoDTO oReq = new ReqFilterPersonalAdministrativoDTO()
            {
                Item = request,
                User = Commun.Usuario,
                FilterCase = filterCasePersonalAdministrativo.BuscarAsistenciaConfiguracionPorNumeroDocumento,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespItemPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oLogic = new PersonalAdministrativoLogic())
            {
                oResp = oLogic.PersonalAdministrativoGetItem(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistrarPersonalAdministrativo(PersonalAdministrativoDTO request, string Accion)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.UsuarioCreacion = Commun.Usuario;
            List<PersonalAdministrativoDTO> list = new List<PersonalAdministrativoDTO>();
            request.Operation = Accion == "N" ? Operation.Create : Operation.Update;
            request.FechaInicioEmpresa = DateTime.UtcNow.AddHours(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["numeroZonaHorario"]));
            list.Add(request);
            ReqPersonalAdministrativoDTO oReq = new ReqPersonalAdministrativoDTO()
            {
                List = list,
                User = Commun.Usuario,
            };

            RespPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oPersonalAdministrativoLogic = new PersonalAdministrativoLogic())
            {
                oResp = oPersonalAdministrativoLogic.ExecuteTransac(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivarPersonalAdministrativo(PersonalAdministrativoDTO request, string Accion)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.UsuarioCreacion = Commun.Usuario;

            List<PersonalAdministrativoDTO> list = new List<PersonalAdministrativoDTO>();
            request.Operation = Operation.ActivarPersonalAdministrativo;
            list.Add(request);
            ReqPersonalAdministrativoDTO oReq = new ReqPersonalAdministrativoDTO()
            {
                List = list,
                User = Commun.Usuario,
            };

            RespPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oPersonalAdministrativoLogic = new PersonalAdministrativoLogic())
            {
                oResp = oPersonalAdministrativoLogic.ExecuteTransac(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DesactivarPersonalAdministrativo(PersonalAdministrativoDTO request, string Accion)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;
            request.UsuarioCreacion = Commun.Usuario;

            List<PersonalAdministrativoDTO> list = new List<PersonalAdministrativoDTO>();
            request.Operation = Operation.CesarPersonalAdministrativo;
            request.FechaCese = DateTime.UtcNow.AddHours(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["numeroZonaHorario"]));
            list.Add(request);
            ReqPersonalAdministrativoDTO oReq = new ReqPersonalAdministrativoDTO()
            {
                List = list,
                User = Commun.Usuario,
            };

            RespPersonalAdministrativoDTO oResp = null;
            using (PersonalAdministrativoLogic oPersonalAdministrativoLogic = new PersonalAdministrativoLogic())
            {
                oResp = oPersonalAdministrativoLogic.ExecuteTransac(oReq);
            }
            return Json(oResp, JsonRequestBehavior.AllowGet);
        }


        #endregion

        public ActionResult gastos()
        {

            return View();
        }

        #region GASTOS ADMINISTRATIVOS

        public ActionResult SEGListarUsuarioResponsable_Gastos(string filtro)
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
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
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

        public ActionResult ListarTipoEgreso()
        {
            List<TipoEgresoDTO> lista = null;
            ReqFilterTipoEgresoDTO oReq = new ReqFilterTipoEgresoDTO()
            {
                User = Commun.Usuario,
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

        public ActionResult GuardarEgreso_admin(int Codigo,
                                         string Responsable, string Descripcion, int Tipo,
                                         int TipoDocumento, string NroDocumento,
                                         decimal MontoEgreso, string accion,
                                         string RUCProveedor,
                                         string RZProveedor,
                                         decimal SubTotal,
                                         decimal Igv,
                                         decimal OtrosTributos,
                                         int CodigoMedioPago,
                                         string NroOperacion,
                                         string Observaciones,
                                         DateTime txtGasto_fechaGasto
                                         )

        {
            string mensaje = string.Empty;
            List<GastosDTO> list = new List<GastosDTO>();
            list.Add(new GastosDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Codigo = Codigo,
                TipoEgreso = Tipo,
                TipoMoneda = TipoDocumento, //tipo de moneda es el tipo de Documento
                NumeroRecibo = NroDocumento,
                Responsable = Responsable,
                Descripcion = Descripcion,
                MontoEgreso = MontoEgreso,
                UsuarioCreacion = Commun.Usuario,
                FechaCreacion = txtGasto_fechaGasto,
                RUCProveedor = RUCProveedor,
                RZProveedor = RZProveedor,
                SubTotal = SubTotal,
                Igv = Igv,
                OtrosTributos = OtrosTributos,
                CodigoMedioPago = CodigoMedioPago,
                NroOperacion = NroOperacion,
                Observaciones = Observaciones,

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


        public ActionResult uspEliminarEgresos(int Codigo
                                       )

        {
            string mensaje = string.Empty;
            List<GastosDTO> list = new List<GastosDTO>();
            list.Add(new GastosDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Codigo = Codigo,
                Operation = Operation.Delete
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

        #endregion

        public ActionResult usuarios()
        {

            return View();
        }

        #region Metodos Usuarios

        public ActionResult SEGListarUsuarioPorPerfil(int CodigoPerfil, bool Estado)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoPerfil = CodigoPerfil;
            oUsuarioDTO.Estado = Estado;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuarioPorPerfil,
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
                lista = oResp.List.Where(x => x.CodigoPerfil != 2).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarUsuario(int codigo)
        {
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = Commun.CodigoSede;
            oUsuarioDTO.CodigoUsuario = codigo;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.porCodigo,
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

        public ActionResult uspValidarExisteUsuario(string NombreCompleto)
        {
            int existe = 0;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                existe = oUsuarioLogic.uspValidarExisteUsuario(Commun.CodigoUnidadNegocio, Commun.CodigoSede, NombreCompleto);
            }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CambiarClaveUsuario(int codigo, string antiguo, string nuevo)
        {
            string mensaje = "";
            string MensajeValidacionAcceso = "";
            List<UsuarioDTO> oList = new List<UsuarioDTO>();
            oList.Add(new UsuarioDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoUsuario = codigo,
                PasswordNuevo = nuevo,
                PasswordAntiguo = antiguo,
                Operation = E_DataModel.Common.Operation.UpdateClave
            });
            ReqUsuarioDTO oReq = new ReqUsuarioDTO()
            {
                List = oList,
                User = "appsfit"
            };
            RespUsuarioDTO oResp = null;
            using (UsuarioLogic oRutinaLogic = new UsuarioLogic())
            {
                oResp = oRutinaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = (oResp.MessageList[0].Codigo).ToString();
                MensajeValidacionAcceso = (oResp.MessageList[0].Detalle);
            }
            return Json(mensaje + "|" + MensajeValidacionAcceso, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarNuevoUsuario(int CodigoUsuario,
         int CodigoPerfil, string NombreCompleto, string Password, string Correo, string Telefono,
         string Accion, bool Estado, string Nombres, string Apellidos, string Dni)
        {
            //int mensaje = 0;
            string MensajeValidacionAcceso = "";
            List<UsuarioDTO> list = new List<UsuarioDTO>();
            list.Add(new UsuarioDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                CodigoUsuario = CodigoUsuario,
                NombreCompleto = NombreCompleto.Trim(),
                Password = Password,
                Mail = Correo,
                Telefono = Telefono,
                Estado = Estado,
                CodigoPerfil = CodigoPerfil,
                imagenUrl = "../Imagenes/fitness/PerfilHombre.png",
                Nombres = Nombres,
                Apellidos = Apellidos,
                DNI = Dni,
                Operation = Accion == "N" ? Operation.Create : Operation.Update

            });
            ReqUsuarioDTO oReq = new ReqUsuarioDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespUsuarioDTO oResp = null;
            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                MensajeValidacionAcceso = (oResp.MessageList[0].Detalle);
            }
            return Json(MensajeValidacionAcceso, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EliminarUsuario(int codigo, int flag)
        {
            string mensaje = string.Empty;
            List<UsuarioDTO> oList = new List<UsuarioDTO>();
            oList.Add(new UsuarioDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoUsuario = codigo,
                flag = flag,
                Operation = E_DataModel.Common.Operation.Delete
            });

            ReqUsuarioDTO oReq = new ReqUsuarioDTO()
            {
                List = oList,
                User = Commun.Usuario
            };
            RespUsuarioDTO oResp = null;
            using (UsuarioLogic oRutinaLogic = new UsuarioLogic())
            {
                oResp = oRutinaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Metodos PAGINA WEB

        public ActionResult aparienciapaginaweb()
        {
            using (CentroEntrenamiento_EditorPaginaWebRepository oRepository = new CentroEntrenamiento_EditorPaginaWebRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDTO request = new CentroEntrenamiento_EditorPaginaWebDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request = oRepository.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(request);

                System.Web.HttpCookie miCookieSeguridad_ColorEmpresa = new System.Web.HttpCookie("_ColorEmpresa_PersonaFit", request.ColorPrincipalPagina);
                miCookieSeguridad_ColorEmpresa.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_ColorEmpresa);

                System.Web.HttpCookie miCookieSeguridad_LogoTipo = new System.Web.HttpCookie("_LogoTipo", request.logoPagina);
                miCookieSeguridad_LogoTipo.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_LogoTipo);

                System.Web.HttpCookie miCookieSeguridad_urlBannerReserva = new System.Web.HttpCookie("_urlBannerReserva", request.BannerReserva_FondoImagen);
                miCookieSeguridad_urlBannerReserva.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerReserva);

                System.Web.HttpCookie miCookieSeguridad_urlBannerCentro = new System.Web.HttpCookie("_urlBannerCentro", request.BannerCentro_FondoImagen);
                miCookieSeguridad_urlBannerCentro.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(miCookieSeguridad_urlBannerCentro);


                return View(request);
            }

        }

        public ActionResult aparienciapaginaweb_trainners()
        {
            return View();
        }

        public ActionResult aparienciapaginaweb_servicios()
        {
            return View();
        }

        public ActionResult aparienciapaginaweb_planes()
        {
            return View();
        }

        public ActionResult aparienciapaginaweb_bannerreserva()
        {
            return View();
        }

        public ActionResult CentroEntrenamiento_uspActualizarEdicionPaginaWeb(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebRepository oRepository = new CentroEntrenamiento_EditorPaginaWebRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspActualizarEdicionPaginaWeb(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspActualizarEdicionPaginaWeb_Foto(string Codigo, int TipoFoto, string UrlImagen)
        {
            using (CentroEntrenamiento_EditorPaginaWebRepository oRepository = new CentroEntrenamiento_EditorPaginaWebRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDTO request = new CentroEntrenamiento_EditorPaginaWebDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoPagina = Codigo;
                request.TipoFoto = TipoFoto;
                request.UrlImagen = UrlImagen;

                return Json(oRepository.CentroEntrenamiento_uspActualizarEdicionPaginaWeb_Foto(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspListarEdicionPaginaWebDetalle_Trinner(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.Tipo = "TRAINNER";
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspListarEdicionPaginaWebDetalle(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspListarEdicionPaginaWebDetalle_Planes(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.Tipo = "PLANES";
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspListarEdicionPaginaWebDetalle(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspListarEdicionPaginaWebDetalle_Servicios(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.Tipo = "SERVICIOS";
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspListarEdicionPaginaWebDetalle(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspBuscarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspBuscarEdicionPaginaWebDetalle(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspRegistrarEdicionPaginaWebDetalle_Trinner(CentroEntrenamiento_EditorPaginaWebViewModel oParameter)
        {
            //public HttpPostedFileWrapper ImageFile2 { get; set; }
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDetalleDTO request = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Tipo = "trainner";
                request.Codigo = oParameter.Codigo;
                request.Titulo = oParameter.Titulo;
                request.SubTitulo = oParameter.SubTitulo;
                request.UrlUmagen = oParameter.UrlUmagen;

                if (oParameter.Codigo == null || oParameter.Codigo == string.Empty)
                {
                    request.UrlUmagen = string.Empty;
                    oParameter.Codigo = oRepository.ecommerce_uspRegistrarEdicionPaginaWebDetalle(request);

                    var file2 = oParameter.file;
                    string ruta;
                    request.UrlUmagen = string.Empty;
                    if (file2 != null)
                    {
                        var fileName = Path.GetFileName(file2.FileName);
                        var extention = Path.GetExtension(file2.FileName);
                        var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                        var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                        var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                        ruta = UploadImgageAzure.UploadFilesAzure(obj, (oParameter.Codigo + extention), "paginatrainners");
                        request.UrlUmagen = ruta;
                    }
                    request.Codigo = oParameter.Codigo;
                    oRepository.ecommerce_uspActualizarEdicionPaginaWebDetalle(request);
                }
                else
                {
                    var file2 = oParameter.file;
                    string ruta;
                    request.UrlUmagen = string.Empty;
                    if (file2 != null)
                    {
                        var fileName = Path.GetFileName(file2.FileName);
                        var extention = Path.GetExtension(file2.FileName);
                        var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                        var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                        var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                        ruta = UploadImgageAzure.UploadFilesAzure(obj, (oParameter.Codigo + extention), "paginatrainners");
                        request.UrlUmagen = ruta;
                    }

                    oRepository.ecommerce_uspActualizarEdicionPaginaWebDetalle(request);
                }

                //GUARDAR IMAGEN

                return Json(oParameter.Codigo, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspRegistrarEdicionPaginaWebDetalle_Planes(CentroEntrenamiento_EditorPaginaWebViewModel oParameter)
        {
            //public HttpPostedFileWrapper ImageFile2 { get; set; }
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDetalleDTO request = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Tipo = "planes";
                request.Codigo = oParameter.Codigo;
                request.Titulo = oParameter.Titulo;
                request.SubTitulo = oParameter.SubTitulo;
                request.LinkPago = oParameter.LinkPago == null ? string.Empty : oParameter.LinkPago;
                request.UrlUmagen = string.Empty;

                if (oParameter.Codigo == null || oParameter.Codigo == string.Empty)
                {
                    request.UrlUmagen = string.Empty;
                    oParameter.Codigo = oRepository.ecommerce_uspRegistrarEdicionPaginaWebDetalle(request);
                }
                else
                {
                    request.UrlUmagen = string.Empty;
                    oRepository.ecommerce_uspActualizarEdicionPaginaWebDetalle(request);
                }

                //GUARDAR IMAGEN

                return Json(oParameter.Codigo, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ecommerce_uspRegistrarEdicionPaginaWebDetalle_Servicios(CentroEntrenamiento_EditorPaginaWebViewModel oParameter)
        {
            //public HttpPostedFileWrapper ImageFile2 { get; set; }
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDetalleDTO request = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Tipo = "servicios";
                request.Codigo = oParameter.Codigo;
                request.Titulo = oParameter.Titulo;
                request.SubTitulo = oParameter.SubTitulo;
                request.UrlUmagen = oParameter.UrlUmagen;

                if (oParameter.Codigo == null || oParameter.Codigo == string.Empty)
                {
                    request.UrlUmagen = string.Empty;
                    oParameter.Codigo = oRepository.ecommerce_uspRegistrarEdicionPaginaWebDetalle(request);

                    var file2 = oParameter.file;
                    string ruta;
                    request.UrlUmagen = string.Empty;
                    if (file2 != null)
                    {
                        var fileName = Path.GetFileName(file2.FileName);
                        var extention = Path.GetExtension(file2.FileName);
                        var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                        var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                        var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                        ruta = UploadImgageAzure.UploadFilesAzure(obj, (oParameter.Codigo + extention), "paginaservicios");
                        request.UrlUmagen = ruta;
                    }
                    request.Codigo = oParameter.Codigo;
                    oRepository.ecommerce_uspActualizarEdicionPaginaWebDetalle(request);
                }
                else
                {
                    var file2 = oParameter.file;
                    string ruta;
                    request.UrlUmagen = string.Empty;
                    if (file2 != null)
                    {
                        var fileName = Path.GetFileName(file2.FileName);
                        var extention = Path.GetExtension(file2.FileName);
                        var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                        var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                        var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                        ruta = UploadImgageAzure.UploadFilesAzure(obj, (oParameter.Codigo + extention), "paginaservicios");
                        request.UrlUmagen = ruta;
                    }

                    oRepository.ecommerce_uspActualizarEdicionPaginaWebDetalle(request);
                }

                //GUARDAR IMAGEN

                return Json(oParameter.Codigo, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ecommerce_uspActualizarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebViewModel oParameter)
        {

            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                CentroEntrenamiento_EditorPaginaWebDetalleDTO request = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Tipo = "servicios";
                request.Codigo = oParameter.Codigo;
                request.Titulo = oParameter.Titulo == null ? string.Empty : oParameter.Titulo;
                request.SubTitulo = oParameter.SubTitulo == null ? string.Empty : oParameter.SubTitulo;
                request.UrlUmagen = oParameter.UrlUmagen;

                oRepository.ecommerce_uspActualizarEdicionPaginaWebDetalle(request);
                return Json(oParameter.Codigo, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult ecommerce_uspEliminarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            using (CentroEntrenamiento_EditorPaginaWebDetalleRepository oRepository = new CentroEntrenamiento_EditorPaginaWebDetalleRepository())
            {
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.ecommerce_uspEliminarEdicionPaginaWebDetalle(request), JsonRequestBehavior.AllowGet);
            }
        }

        //GALERIA FITNESS

        public ActionResult CentroEntrenamiento_uspRegistrarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessViewModel oParameter)
        {
            //public HttpPostedFileWrapper ImageFile2 { get; set; }
            using (CentroEntrenamiento_GaleriaFitnessRepository oRepository = new CentroEntrenamiento_GaleriaFitnessRepository())
            {
                CentroEntrenamiento_GaleriaFitnessDTO request = new CentroEntrenamiento_GaleriaFitnessDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Tipo = oParameter.Tipo; //BANER PAGINA RESERVA
                request.Privacidad = 2; //PUBLICO
                request.UrlImagen = string.Empty;
                request.Estado = true;
                request.Codigo = string.Empty;
                oParameter.Codigo = oRepository.CentroEntrenamiento_uspRegistrarGaleriaFitness(request);

                if (oParameter.Codigo != string.Empty)
                {
                    var file2 = oParameter.file;
                    string ruta;
                    if (file2 != null)
                    {
                        var fileName = Path.GetFileName(file2.FileName);
                        var extention = Path.GetExtension(file2.FileName);
                        var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                        var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                        var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                        ruta = UploadImgageAzure.UploadFilesAzure(obj, (oParameter.Codigo + extention), "galeriafitness");
                        request.UrlImagen = ruta;
                        request.Codigo = oParameter.Codigo;
                        oRepository.CentroEntrenamiento_uspActualizarGaleriaFitness(request);
                    }
                }

                //GUARDAR IMAGEN

                return Json(oParameter.Codigo, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspEliminarGaleriaFitness(string Codigo)
        {
            //public HttpPostedFileWrapper ImageFile2 { get; set; }
            using (CentroEntrenamiento_GaleriaFitnessRepository oRepository = new CentroEntrenamiento_GaleriaFitnessRepository())
            {
                CentroEntrenamiento_GaleriaFitnessDTO request = new CentroEntrenamiento_GaleriaFitnessDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.Codigo = Codigo;

                return Json(oRepository.CentroEntrenamiento_uspEliminarGaleriaFitness(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspListarGaleriaFitness(int Tipo)
        {
            using (CentroEntrenamiento_GaleriaFitnessRepository oRepository = new CentroEntrenamiento_GaleriaFitnessRepository())
            {
                CentroEntrenamiento_GaleriaFitnessDTO request = new CentroEntrenamiento_GaleriaFitnessDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.Privacidad = 2;
                request.Tipo = Tipo;
                request.UsuarioCreacion = Commun.Usuario;
                return Json(oRepository.CentroEntrenamiento_uspListarGaleriaFitness(request), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region DIARIOS

        public ActionResult uspListarProductoElaboradoPorFiltro_Paginacion(string Busqueda, int PageNumber)
        {
            List<ProductoElaboradoDTO> lista = null;
            ProductoElaboradoDTO oProductoElaboradoDTO = new ProductoElaboradoDTO();
            oProductoElaboradoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oProductoElaboradoDTO.CodigoSede = Commun.CodigoSede;
            oProductoElaboradoDTO.Busqueda = Busqueda;
            ReqFilterProductoElaboradoDTO oReq = new ReqFilterProductoElaboradoDTO()
            {
                FilterCase = filterCaseProductoElaborado.uspListarProductoElaboradoPorFiltro_Paginacion,
                User = "Admin",
                Item = oProductoElaboradoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListProductoElaboradoDTO oResp = null;
            using (ProductoElaboradoLogic oProductoLogic = new ProductoElaboradoLogic())
            {
                oResp = oProductoLogic.ProductoElaboradoGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarProductoElaboradoPorFiltro_NumeroRegistros(string Busqueda)
        {
            ProductoElaboradoDTO oProductoElaboradoDTO = new ProductoElaboradoDTO();
            oProductoElaboradoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oProductoElaboradoDTO.CodigoSede = Commun.CodigoSede;
            oProductoElaboradoDTO.Busqueda = Busqueda;

            ReqFilterProductoElaboradoDTO oReq = new ReqFilterProductoElaboradoDTO()
            {
                FilterCase = filterCaseProductoElaborado.uspListarProductoElaboradoPorFiltro_NumeroRegistros,
                Item = oProductoElaboradoDTO,
                User = "admin"
            };
            RespItemProductoElaboradoDTO oResp = null;
            using (ProductoElaboradoLogic oProductoElaboradoLogic = new ProductoElaboradoLogic())
            {
                oResp = oProductoElaboradoLogic.ProductoElaboradoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oProductoElaboradoDTO = oResp.Item;
                oProductoElaboradoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarProductoElaboradoPorFiltro_NumeroRegistros"]);
            }
            return Json(oProductoElaboradoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarProductoElaborado(int CodigoProducto)
        {
            ProductoElaboradoDTO oProductoElaboradoDTO = new ProductoElaboradoDTO();
            oProductoElaboradoDTO.CodigoProducto = CodigoProducto;
            oProductoElaboradoDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oProductoElaboradoDTO.CodigoSede = Commun.CodigoSede;
            ReqFilterProductoElaboradoDTO oReq = new ReqFilterProductoElaboradoDTO()
            {
                FilterCase = filterCaseProductoElaborado.BuscarPorCodigo,
                Item = oProductoElaboradoDTO,
                User = "Admin"
            };
            RespItemProductoElaboradoDTO oResp = null;
            using (ProductoElaboradoLogic oProductoElaboradoLogic = new ProductoElaboradoLogic())
            {
                oResp = oProductoElaboradoLogic.ProductoElaboradoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oProductoElaboradoDTO = oResp.Item;
            }
            return Json(oProductoElaboradoDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarProductoElaborados(int CodigoProducto, string Descripcion, decimal PrecioVenta, int CodigoCategoria, string Accion)
        {
            int mensaje = 0;

            List<ProductoElaboradoDTO> list = new List<ProductoElaboradoDTO>();
            list.Add(new ProductoElaboradoDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoProducto = CodigoProducto,
                CodigoCategoria = CodigoCategoria,
                Descripcion = Descripcion,
                PrecioVenta = PrecioVenta,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                Operation = Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqProductoElaboradoDTO oReq = new ReqProductoElaboradoDTO()
            {
                List = list,
                User = Commun.Usuario
            };
            RespProductoElaboradoDTO oResp = null;
            using (ProductoElaboradoLogic oProductoElaboradoLogic = new ProductoElaboradoLogic())
            {
                oResp = oProductoElaboradoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarProductoElaborado(int CodigoProducto)
        {
            int mensaje = 0;
            List<ProductoElaboradoDTO> list = new List<ProductoElaboradoDTO>();
            list.Add(new ProductoElaboradoDTO()
            {
                UsuarioCreacion = Commun.Usuario,
                CodigoProducto = CodigoProducto,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Operation = Operation.Delete
            });
            ReqProductoElaboradoDTO oReq = new ReqProductoElaboradoDTO()
            {
                List = list,
                User = "Admin"
            };
            RespProductoElaboradoDTO oResp = null;
            using (ProductoElaboradoLogic oProductoElaboradoLogic = new ProductoElaboradoLogic())
            {
                oResp = oProductoElaboradoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;

            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }



        #endregion


        public ActionResult anularventas()
        {

            return View();
        }

        #region ANULAR VENTAS

        //ANULAR PRODUCTOS
        public ActionResult ecommerce_uspListarComprobanteParaAnular(DateTime FechaInicio, DateTime FechaFin)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                ComprobanteViewModel request = new ComprobanteViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.b_FechaEmisionInicio = FechaInicio;
                request.b_FechaEmisionFin = FechaFin;
                request.PageNumber = 1;
                return Json(oRepository.ecommerce_uspListarComprobanteParaAnular(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspListarComprobanteDetalleParaAnular(int CodigoComprobante)
        {
            using (ComprobanteDetalleRepository oRepository = new ComprobanteDetalleRepository())
            {
                E_DataModel.ComprobanteDetalleDTO request = new E_DataModel.ComprobanteDetalleDTO();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.CodigoComprobante = CodigoComprobante;

                return Json(oRepository.CentroEntrenamiento_uspListarComprobanteDetalleParaAnular(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CentroEntrenamiento_uspEliminarComprobante(int CodigoComprobante)
        {
            using (ComprobanteRepository oRepository = new ComprobanteRepository())
            {
                ComprobanteViewModel request = new ComprobanteViewModel();
                request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = Commun.Usuario;
                request.CodigoComprobante = CodigoComprobante;
                return Json(oRepository.CentroEntrenamiento_uspEliminarComprobante(request), JsonRequestBehavior.AllowGet);
            }
        }

        //ANULAR DIARIOS

        public ActionResult uspListarVentasRapidasAnular_Paginacion(DateTime FechaInicio, DateTime FechaFin, int PageNumber)
        {
            //DateTime fechaConsulta = new DateTime(Convert.ToInt32(fecha.Split('/')[2]), Convert.ToInt32(fecha.Split('/')[1]), Convert.ToInt32(fecha.Split('/')[0]));
            List<VentasDTO> lista = null;
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;

            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                Item = oVentasDTO,
                User = "ADMIN",
                FilterCase = filterCaseVentas.uspListarVentasRapidasAnular_Paginacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = Convert.ToUInt32(PageNumber),
                }
            };
            RespListVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.VentasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspListarVentasRapidasAnular_NumeroRegistros(DateTime FechaInicio, DateTime FechaFin)
        {
            //DateTime fechaConsulta = new DateTime(Convert.ToInt32(fecha.Split('/')[2]), Convert.ToInt32(fecha.Split('/')[1]), Convert.ToInt32(fecha.Split('/')[0]));
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = Commun.CodigoSede;

            oVentasDTO.FechaInicio = FechaInicio;
            oVentasDTO.FechaFin = FechaFin;

            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.uspListarVentasRapidasAnular_NumeroRegistros,
                Item = oVentasDTO,
                User = "admin"
            };
            RespItemVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.VentasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oVentasDTO = oResp.Item;
                oVentasDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlSalidaPorFechaAnular_Paginacion"]);
            }

            return Json(oVentasDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarDetalleVentas(int codigoSalida)
        {
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = Commun.CodigoSede;
            oVentasDetalleDTO.CodigoSalida = codigoSalida;
            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                Item = oVentasDetalleDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnularVenta(int codigoSalida)
        {
            int mensaje = 0;
            List<VentasDTO> oList = new List<VentasDTO>();
            oList.Add(new VentasDTO()
            {
                CodigoIngreso = codigoSalida,
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                Operation = E_DataModel.Common.Operation.Delete
            });
            ReqVentasDTO oReq = new ReqVentasDTO()
            {
                List = oList,
                User = "Admin"
            };
            RespVentasDTO oResp = null;
            using (VentasLogic oControlSalidaLogic = new VentasLogic())
            {
                oResp = oControlSalidaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        #endregion

        public ActionResult permisos(string id)
        {
            return View();
        }

        #region PERMISOS


        //LISTAR PERFIL MENU
        public ActionResult SEGListarPerfilMenu(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            request.CodigoSede = Commun.CodigoSede;

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

        //REGISTRAR MENU PERFIL
        public ActionResult CentroEntrenamiento_uspRegistrarPerfilMenu(string listaDetalle)
        {
            int Codigo = 0;
            List<CentroEntrenamiento_MenuPlantillaDTO> list = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(listaDetalle);
            XmlNodeList detalles = xmlDoc.GetElementsByTagName("ds");
            XmlNodeList detalle = ((XmlElement)detalles[0]).GetElementsByTagName("d");

            foreach (XmlElement nodo in detalle)
            {
                list.Add(new CentroEntrenamiento_MenuPlantillaDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoPerfil = Convert.ToInt32(nodo.ChildNodes[0].InnerText),
                    CodigoMenu = nodo.ChildNodes[1].InnerText,
                    UsuarioCreacion = "appsfit",
                    Operation = Operation.CentroEntrenamiento_uspRegistrarPerfilMenu
                });
            }

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (E_BusinessLayer.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new E_BusinessLayer.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        //ELIMINAR MENU PERFIL
        public ActionResult CentroEntrenamiento_uspEliminarPerfilMenu(int CodigoPerfil)
        {
            int Codigo = 0;
            List<CentroEntrenamiento_MenuPlantillaDTO> list = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            list.Add(new CentroEntrenamiento_MenuPlantillaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                CodigoPerfil = CodigoPerfil,
                Operation = Operation.CentroEntrenamiento_uspEliminarPerfilMenu
            });

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (E_BusinessLayer.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new E_BusinessLayer.CentroEntrenamiento.CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        //CONTROLA LOS MODULOS DE LAS PANTALLAS DEL SISTEMA
        public ActionResult SEGListarPerfilMenu_Control()
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


        #region CONFIGURACION

        public ActionResult configuracion(string id)
        {
            return View();
        }

        public ActionResult BuscarSerie(int codigo)
        {
            SeriesDTO oSeriesDTO = new SeriesDTO();
            oSeriesDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oSeriesDTO.CodigoSede = Commun.CodigoSede;
            oSeriesDTO.CodigoSerie = codigo;

            ReqFilterSeriesDTO oReq = new ReqFilterSeriesDTO()
            {
                FilterCase = E_DataModel.Common.filterCaseSeries.BuscarPorCodigo,
                Item = oSeriesDTO,
                User = "appsfit"
            };
            RespItemSeriesDTO oResp = null;
            using (SeriesLogic oSeriesLogic = new SeriesLogic())
            {
                oResp = oSeriesLogic.SeriesGetItem(oReq);
            }
            if (oResp.Success)
            {
                oSeriesDTO = oResp.Item;
            }

            return Json(oSeriesDTO, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GuardarSerie(int CodigoSerie, int tipoDocumento, int subTipoDocumento, string nroSerie, string nroActual, bool estado, bool chkGenerarSerie, bool chkGenerarComprobante)
        {
            string mensaje = string.Empty;

            List<SeriesDTO> list = new List<SeriesDTO>();

            list.Add(new SeriesDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                UsuarioCreacion = Commun.Usuario,
                CodigoSerie = CodigoSerie,
                TipoDocumento = tipoDocumento,
                SubTipoDocumento = subTipoDocumento,
                NroSerie = nroSerie,
                NroCorrelativoActual = nroActual,
                NroCorrelativoFinal = string.Empty,
                Estado = estado,
                chkGenerarSerie = chkGenerarSerie,
                chkGenerarComprobante = chkGenerarComprobante,
                Operation = E_DataModel.Common.Operation.Create
            });

            ReqSeriesDTO oReq = new ReqSeriesDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespSeriesDTO oResp = null;
            using (SeriesLogic oSeriesLogic = new SeriesLogic())
            {
                oResp = oSeriesLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }


        public ActionResult uspActualizarConfiguracionDatosFormatoTicket(CentroEntrenamiento_ConfiguracionViewModel request)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Ticket_RazonSocial = request.Ticket_RazonSocial;
            oConfiguracionDTO.Ticket_RUC = request.Ticket_RUC;
            oConfiguracionDTO.Ticket_Direccion = request.Ticket_Direccion;
            oConfiguracionDTO.Ticket_Celular = request.Ticket_Celular;
            oConfiguracionDTO.Ticket_Telefono = request.Ticket_Telefono;
            oConfiguracionDTO.Frase = request.Ticket_Frase;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.uspActualizarConfiguracionDatosFormatoTicket;

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
                mensaje = "Datos Guardados Correctamente";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarTurno(int Codigo, string Inicio, string Fin)
        {
            string mensaje = string.Empty;
            string[] sHi = Inicio.Split('|');
            string[] sHf = Fin.Split('|');

            DateTime hInicio = new DateTime(Convert.ToInt32(sHi[0]), Convert.ToInt32(sHi[1]), Convert.ToInt32(sHi[2]), Convert.ToInt32(sHi[3]), Convert.ToInt32(sHi[4]), Convert.ToInt32(sHi[5]));
            DateTime hFin = new DateTime(Convert.ToInt32(sHf[0]), Convert.ToInt32(sHf[1]), Convert.ToInt32(sHf[2]), Convert.ToInt32(sHf[3]), Convert.ToInt32(sHf[4]), Convert.ToInt32(sHf[5]));
            List<TurnosEmpresaDTO> list = new List<TurnosEmpresaDTO>();

            list.Add(new TurnosEmpresaDTO()
            {
                CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                CodigoSede = Commun.CodigoSede,
                Codigo = Codigo,
                HoraInicio = hInicio,
                HoraFin = hFin,
                UsuarioCreacion = Commun.Usuario,
                UsuarioEdicion = Commun.Usuario,
                Operation = E_DataModel.Common.Operation.Create,
            });

            ReqTurnosEmpresaDTO oReq = new ReqTurnosEmpresaDTO()
            {
                List = list,
                User = Commun.Usuario
            };

            RespTurnosEmpresaDTO oResp = null;
            using (TurnosEmpresaLogic oTurnosEmpresaLogic = new TurnosEmpresaLogic())
            {
                oResp = oTurnosEmpresaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarTurnosEmpresa()
        {
            List<TurnosEmpresaDTO> lista = new List<TurnosEmpresaDTO>();
            TurnosEmpresaDTO oTurnosEmpresaDTO = new TurnosEmpresaDTO();
            oTurnosEmpresaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oTurnosEmpresaDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterTurnosEmpresaDTO oReq = new ReqFilterTurnosEmpresaDTO()
            {
                User = Commun.Usuario,
                Item = oTurnosEmpresaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListTurnosEmpresaDTO oResp = null;

            using (TurnosEmpresaLogic oTurnosEmpresaLogic = new TurnosEmpresaLogic())
            {
                oResp = oTurnosEmpresaLogic.TurnosEmpresaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);

        }


        public ActionResult BuscarConfiguracionDiasCitasCaida()
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = E_DataModel.Common.filterCaseConfiguracion.BuscarConfiguracionDiasCitasCaida,
                Item = oConfiguracionDTO,
                User = Commun.Usuario
            };
            RespItemConfiguracionDTO oResp = null;
            using (ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic())
            {
                oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            }
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
            }
            return Json(oConfiguracionDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarDiasCitasCaida(int DiaCitasCaida)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.DiaCitasCaida = DiaCitasCaida;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateConfiguracionCitasCaidas;

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
        public ActionResult ActualizarObligarMarcarClaseAsistencia(bool ObligatorioMarcarIngresoSalaClase)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.ObligatorioMarcarIngresoSalaClase = ObligatorioMarcarIngresoSalaClase;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateObligarMarcarClaseAsistencia;

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

        public ActionResult GuardarIngresoDNIObligatorio(bool ObligatorioDNIProspectos)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.ObligatorioDNIProspectos = ObligatorioDNIProspectos;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateObligatorioIngresoDNI;

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

        public ActionResult ActualizarPermitirMuchasAsistenciasPorCliente(bool EvitarMuchasAsistencias)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.PermitirMuchasAsistenciaPordia = Convert.ToInt32(EvitarMuchasAsistencias);
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdatePermitirMuchasAsistenciaPordia;

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

        public ActionResult ActualizarGenerarCodigoclienteAutomatico(bool CodigoClienteAuto)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.CodigoClienteAuto = CodigoClienteAuto;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateGenerarCodigoclienteAutomatico;

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

        public ActionResult ActualizarGenerarContratoAutomatico(bool activarImprimir)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            oConfiguracionDTO.ActivarGenerarContratoMembresias = activarImprimir;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateGenerarContratoAutomatico;

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



        public ActionResult uspActualizarConsultasNumeroDocumentoEntidades(bool ConsultasNumeroDocumentoEntidades, decimal ConsultasNumeroDocumento_PrecioAnual)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = Commun.CodigoSede;
            //oConfiguracionDTO.ConsultasNumeroDocumentoEntidades = ConsultasNumeroDocumentoEntidades;
            oConfiguracionDTO.UsuarioEdicion = Commun.Usuario;
            oConfiguracionDTO.ConsultasNumeroDocumento_Correo = string.Empty;
            oConfiguracionDTO.ConsultasNumeroDocumento_Clave = string.Empty;
            oConfiguracionDTO.ConsultasNumeroDocumento_ApiUrl = @"https://api.apis.net.pe/";
            oConfiguracionDTO.ConsultasNumeroDocumento_ApiToken = "apis-token-1979.daqZyoZDfaATqkB8NyJVPyKOig5WwNXd";
            oConfiguracionDTO.ConsultasNumeroDocumento_PrecioAnual = ConsultasNumeroDocumento_PrecioAnual;
            oConfiguracionDTO.ConsultasNumeroDocumento_UsuarioCreacion = Commun.Usuario;
            oConfiguracionDTO.ConsultasNumeroDocumento_Estado = ConsultasNumeroDocumentoEntidades;

            oConfiguracionDTO.Operation = E_DataModel.Common.Operation.UpdateConsultasNumeroDocumentoEntidades;

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
    }
}