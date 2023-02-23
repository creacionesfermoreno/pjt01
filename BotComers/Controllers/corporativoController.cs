using BotComers.Helpers;
using BotComers.Repository;
using BotComers.Repository.Corporativo;
using BotComers.ViewModels;
using BotComers.ViewModels.CentroEntrenamiento;
using E_BusinessLayer.CentroEntrenamiento;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BotComers.Controllers
{
    public class corporativoController : Controller
    {

        public ActionResult Index()
        {
            EmpresaViewModel viewEmpresa = new EmpresaViewModel();
            viewEmpresa.loadEmpresa = new EmpresaViewModelLoad();

            using (EmpresaRepository oEmpresaRepository = new EmpresaRepository())
            {
                viewEmpresa.loadEmpresa.listGridEmpresa = oEmpresaRepository.ecommerce_uspListarEmpresas_Paginacion(1);
                string filterEstado = "ecommerce_EstadoEmpresa";
                viewEmpresa.loadEmpresa.listEstadoEmpresa = oEmpresaRepository.ecommerce_uspListarMaestro(filterEstado);
                string filterPais = "ecommerce_Pais";
                viewEmpresa.loadEmpresa.listPaisEmpresa = oEmpresaRepository.ecommerce_uspListarMaestro(filterPais);
                string filterDocumentoEmpresa = "ecommerce_TipoDocumentoEmpresa";
                viewEmpresa.loadEmpresa.listTipoDocumentoEmpresa = oEmpresaRepository.ecommerce_uspListarMaestro(filterDocumentoEmpresa);
            }

            return View(viewEmpresa);
        }

        //[ErrorJsonFilter]
        public ActionResult RegistrarEmpresa(EmpresaViewInsertModel request)
        {
            using (EmpresaRepository oEmpresaRepository = new EmpresaRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = "admin";//Commun.Usuario;
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oEmpresaRepository.ecommerce_uspRegistrarEmpresa(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarEmpresa(EmpresaViewEditModel request)
        {
            using (EmpresaRepository oEmpresaRepository = new EmpresaRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                //request.UsuarioCreacion = "admin";//Commun.Usuario;
                //request.Accion = "N";
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oEmpresaRepository.ecommerce_uspBuscarEmpresas(request.CodigoUnidadNegocio, request.CodigoSede), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarIconosCategorias(MaestroViewModel request)
        {
            using (EmpresaRepository oEmpresaRepository = new EmpresaRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                //request.UsuarioCreacion = "admin";//Commun.Usuario;
                return Json(oEmpresaRepository.ecommerce_uspListarMaestro(request.Descripcion), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RegistrarCategorias(CategoriasProductosViewModel request)
        {
            using (CategoriasRepository oCategoriasRepository = new CategoriasRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = "admin";//Commun.Usuario;
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oCategoriasRepository.ecommerce_uspRegistrarCategorias(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarCategorias(CategoriasProductosViewModel request)
        {
            using (CategoriasRepository oCategoriasRepository = new CategoriasRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                request.UsuarioCreacion = "admin";//Commun.Usuario;
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oCategoriasRepository.ecommerce_uspEliminarCategorias(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uspListarCategoriasPrimario(CategoriasProductosViewModel request)
        {
            using (CategoriasRepository oCategoriasRepository = new CategoriasRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                //request.UsuarioCreacion = "admin";//Commun.Usuario;
                return Json(oCategoriasRepository.ecommerce_uspListarCategorias(request.CodigoUnidadNegocio, request.CodigoSede, request.CodigoMenuSuperior), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscarCategorias(CategoriasProductosViewModel request)
        {
            using (CategoriasRepository oCategoriasRepository = new CategoriasRepository())
            {
                //request.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                //request.CodigoSede = Commun.CodigoSede;
                //request.UsuarioCreacion = "admin";//Commun.Usuario;
                //request.Accion = "N";
                //request.FechaCompra = Convert.ToDateTime(request.FechaCompra.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                return Json(oCategoriasRepository.ecommerce_uspBuscarCategorias(request.CodigoUnidadNegocio, request.CodigoSede, request.CodigoMenuSuperior, request.CodigoMenu), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Empresas()
        {
            return View();
        }

        public ActionResult Empresas_add()
        {
            return View();
        }

        public ActionResult Empresas_edit(int idUN, int idSede)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = idUN;
            oConfiguracionDTO.CodigoSede = idSede;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                FilterCase = filterCaseConfiguracion.uspBuscarConfiguracion_apfitness,
                User = "appsift"
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

            return View(oConfiguracionDTO);
        }

        public ActionResult importardatos_solosocios() //int idun, int idsede
        {
          
            return View();
        }   

        [System.Web.Mvc.HttpPost]
        public ActionResult importardatos_solosocios(HttpPostedFileBase postedFile, int txtinicio, int txtfin) //int idun, int idsede
        {

            if (txtinicio == 0)
            {
                return View();
            }
            else if (txtfin == 0)
            {
                return View();
            }

            List<ClientesDTO> listaCLientes = new List<ClientesDTO>();
            string filePatch = string.Empty;
            string extension = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Content/files_excel_appsfit/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePatch = path + Path.GetFileName(postedFile.FileName);
                extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePatch);

                using (var stream = System.IO.File.Open(filePatch, FileMode.Open, FileAccess.Read))
                {
                    using (ExcelDataReader.IExcelDataReader reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                    {
                        if (reader.Read())
                        {
                            DataSet dataSet = reader.AsDataSet();
                            DataTable dataTable = new DataTable();
                            dataTable = dataSet.Tables[0];
                            for (int i = txtinicio; i <= txtfin; i++) //suma en 380 
                            {
                                ClientesDTO item = new ClientesDTO();
                                item.CodigoUnidadNegocio = 285;//GRUPO QUIME
                                item.CodigoSede = 1;
                                // item.CodigoSocio = Convert.ToInt32(dataTable.Rows[i][0]);
                                //CODIGO SOCIO
                                //NOMBRES
                                //APELLIDOS
                                //DNI
                                //DIRECCION
                                //CELULAR
                                //CORREO
                                //PLAN MIGRADO DEL ANTIGUO SISTEMA (OBSERVACION)
                                //FECHA INICIO
                                //FECHA FIN
                                //PLAN OBSERVACION
                                //COSTO
                                //A CUENTA

                                var names = dataTable.Rows[i][1].ToString().Split(' ');
                                if (names.Length == 1)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "");
                                    item.Apellidos = "";
                                }
                                else if (names.Length == 2)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "");
                                    item.Apellidos = names[1].ToString();
                                }
                                else if (names.Length == 3)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "");
                                    item.Apellidos = names[1].ToString() + " " + names[2].ToString();
                                }
                                else if (names.Length == 4)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString();
                                }
                                else if (names.Length == 5)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString();
                                }
                                else if (names.Length == 6)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString();
                                }
                                else if (names.Length == 7)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString();
                                }
                                else if (names.Length == 8)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString();
                                }
                                else if (names.Length == 9)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString();
                                }
                                else if (names.Length == 10)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString();
                                }
                                else if (names.Length == 11)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString();
                                }
                                else if (names.Length == 12)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString();
                                }
                                else if (names.Length == 13)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString();
                                }
                                else if (names.Length == 14)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString() + " " + names[13].ToString();
                                }
                                else if (names.Length == 15)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString() + " " + names[13].ToString() + " " + names[14].ToString();
                                }
                                else if (names.Length == 16)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString() + " " + names[13].ToString() + " " + names[14].ToString() + " " + names[15].ToString();
                                }
                                else if (names.Length == 17)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString() + " " + names[13].ToString() + " " + names[14].ToString() + " " + names[15].ToString() + " " + names[16].ToString();
                                }
                                else if (names.Length == 18)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString() + " " + names[13].ToString() + " " + names[14].ToString() + " " + names[15].ToString() + " " + names[16].ToString() + " " + names[17].ToString();
                                }
                                else if (names.Length == 19)
                                {
                                    item.Nombres = names[0].ToString().Replace(",", "") + " " + names[1].ToString().Replace(",", "");
                                    item.Apellidos = names[2].ToString() + " " + names[3].ToString() + " " + names[4].ToString() + " " + names[5].ToString() + " " + names[6].ToString() + " " + names[7].ToString() + " " + names[8].ToString() + " " + names[9].ToString() + " " + names[10].ToString() + " " + names[11].ToString() + " " + names[12].ToString() + " " + names[13].ToString() + " " + names[14].ToString() + " " + names[15].ToString() + " " + names[16].ToString() + " " + names[17].ToString() + " " + names[18].ToString();
                                }
                                item.CodigoSocio = Convert.ToInt32(dataTable.Rows[i][0]);

                                //item.Nombres = dataTable.Rows[i][1].ToString();
                                //item.Apellidos = dataTable.Rows[i][2].ToString();
                                item.TipoDocumento = 1;
                                item.DNI = dataTable.Rows[i][3].ToString();
                                item.Direccion = dataTable.Rows[i][2].ToString();
                                item.Celular = dataTable.Rows[i][4].ToString();
                                item.Correo = dataTable.Rows[i][5].ToString();

                                item.FechaNacimiento = DateTime.Now;
                                //string cumple = dataTable.Rows[i][8].ToString().Replace("-", "/").Replace("-", "/").Replace("-", "/");
                                //if (cumple.Split('/')[2].Length > 4)
                                //{
                                //    item.FechaNacimiento = Convert.ToDateTime(cumple);
                                //}
                                //else
                                //{
                                //    item.FechaNacimiento = new DateTime(Convert.ToInt32(cumple.Split('/')[2]), Convert.ToInt32(cumple.Split('/')[1]), Convert.ToInt32(cumple.Split('/')[0]));
                                //}

                                item.NombreMembresia = "MEMBRESIA GRIFO";//dataTable.Rows[i][5].ToString();

                                //string xi = dataTable.Rows[i][7].ToString().Replace("-", "/").Replace("-", "/").Replace("-", "/");
                                //string xf = dataTable.Rows[i][8].ToString().Replace("-", "/").Replace("-", "/").Replace("-", "/");
                                //DateTime f1 = new DateTime(Convert.ToInt32(xi.Split('/')[2]), Convert.ToInt32(xi.Split('/')[1]), Convert.ToInt32(xi.Split('/')[0]));//Convert.ToDateTime(xi);
                                //DateTime f2 = new DateTime(Convert.ToInt32(xf.Split('/')[2]), Convert.ToInt32(xf.Split('/')[1]), Convert.ToInt32(xf.Split('/')[0]));//Convert.ToDateTime(xf);

                                item.FechaInicio = Convert.ToDateTime(dataTable.Rows[i][7]);
                                item.FechaFinal = Convert.ToDateTime(dataTable.Rows[i][8]);
                                item.Costo = Convert.ToDecimal(dataTable.Rows[i][9]);
                                item.Pago = Convert.ToDecimal(dataTable.Rows[i][10]);
                                item.AsesorComercial = dataTable.Rows[i][11].ToString();
                                item.Vendedor = dataTable.Rows[i][11].ToString();
                                item.NroIngreso = 60;//Convert.ToInt32(dataTable.Rows[i][11]);
                                item.NroIngresoActual = 0;// Convert.ToInt32(dataTable.Rows[i][12]);
                                item.Ubicaciones = ""; //VILLA EL SALVADOR
                                item.NroComprobante = "";//dataTable.Rows[i][12].ToString();
                                item.NroContrato = "";//dataTable.Rows[i][13].ToString();

                                item.Operation = Operation.uspRegistrarSocios_ImportarExcel;
                                listaCLientes.Add(item);

                                //listaCLientes.Add(new ClientesDTO()
                                //{
                                //    CodigoSocio = Convert.ToInt32(dataTable.Rows[i][0]),
                                //    Nombre = dataTable.Rows[i][1].ToString(),
                                //    Correo = dataTable.Rows[i][2].ToString(),
                                //    Celular = dataTable.Rows[i][4].ToString()
                                //});

                            }
                        }
                    }
                }
            }

            ReqClientesDTO oReq = new ReqClientesDTO()
            {
                List = listaCLientes,
                User = "appsfit"
            };

            RespClientesDTO oResp = null;
            using (ClientesLogic oProductoLogic = new ClientesLogic())
            {
                oResp = oProductoLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {

            }

            return View(listaCLientes);
        }




        #region ADMINISTRACION GIMNASIOS

        public JsonResult uspListarConfiguracion_apfitness_Paginacion(int estado, int PageNumber, string filtro)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.filtro = filtro;
            oConfiguracionDTO.Estado = estado;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = filterCaseConfiguracion.uspListarConfiguracion_apfitness_Paginacion,
                Item = oConfiguracionDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
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

        public JsonResult uspListarConfiguracion_apfitness_NumeroRegistros(string filtro)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.filtro = filtro;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                FilterCase = filterCaseConfiguracion.uspListarConfiguracion_apfitness_NumeroRegistros,
                User = "appsfit"
            };
            RespItemConfiguracionDTO oResp = null;
            using (ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic())
            {
                oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            }
            if (oResp.Success)
            {
                oConfiguracionDTO = oResp.Item;
                oConfiguracionDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarConfiguracion_apfitness_NumeroRegistros"]);
            }
            return Json(oConfiguracionDTO, JsonRequestBehavior.AllowGet);
        }

        public JsonResult uspListarConfiguracionCuentas()
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = filterCaseConfiguracion.uspListarConfiguracionCuentas,
                Item = null,
                User = "appsfit",
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

        public JsonResult ListarUbicaciones(int tipo, string ubigeo, string buscador)
        {
            List<UbicacionesDTO> lista = null;
            UbicacionesDTO oUbicacionesDTO = new UbicacionesDTO();
            oUbicacionesDTO.Tipo = tipo;
            oUbicacionesDTO.Ubicaciones = ubigeo;
            oUbicacionesDTO.Buscador = buscador;
            ReqFilterUbicacionesDTO oReq = new ReqFilterUbicacionesDTO()
            {
                Item = oUbicacionesDTO,
                User = "ADMIN",
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


        public JsonResult GuardarConfiguracion_adFitness(int CodigoUnidadNegocio, int CodigoSede, string RazonSocial, string Pais, string Departamento, string Distrito, string Direccion, string Dominio, string Ruc, string Correo, string Telefono, string NombreComercial, int GenerarSerie, int GenerarComprobante, int Estado, int PermitirMuchasAsistenciasDia, int DescontarfreezengDisponiblesFlag, int DescontarfreezengDisponiblesNumero, int ClientesxVendedorAleatorio, int NumeroDiaMesEjecucionAleatorio, int NotificarDeudasxDia, int CantDiasDeudas, int MostrarVentasOtros, int ActivarCorreoBienvenida, int ActivarImprimirContrato, int TiempoMarcarAsistencia, int DiasCitasCaida, Decimal Igv, int TipoDescuento, int Tipo, string RutaCarpetaImagen, string Contrasenia, string ConexionDB, string LongitudSerie, string NombreTiquetera, DateTime FechaVencimiento, string Frase, string accion, DateTime FechaPago, string Ubicaciones, decimal MontoMensualidadPago, string CodigoCuenta, int CodigoPlan, string TipoMoneda,bool TieneFacturacionElectronica, string UrlAPISunafact, string TokenSunafact, string NombreGerente, string ContactoCobranza, string CelularCobranza, bool AplicacionDisponible, bool TiendaAplicacion, bool RutinasAplicacion)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = CodigoSede;
            oConfiguracionDTO.RazonSocial = RazonSocial;
            oConfiguracionDTO.Pais = Pais;
            oConfiguracionDTO.Departamento = Departamento;
            oConfiguracionDTO.Distrito = Distrito;
            oConfiguracionDTO.Direccion = Direccion;
            oConfiguracionDTO.SubDominio = Dominio;
            oConfiguracionDTO.Ruc = Ruc;
            oConfiguracionDTO.Correo = Correo;
            oConfiguracionDTO.Telefono = Telefono;
            oConfiguracionDTO.NombreComercial = NombreComercial;
            oConfiguracionDTO.GenerarSerie = Convert.ToBoolean(GenerarSerie);
            oConfiguracionDTO.GenerarComprobante = Convert.ToBoolean(GenerarComprobante);

            oConfiguracionDTO.PermitirMuchasAsistenciaPordia = PermitirMuchasAsistenciasDia;
            oConfiguracionDTO.DescontarFreezingDisponiblesFlag = DescontarfreezengDisponiblesFlag;
            oConfiguracionDTO.DescontarFreezingDisponiblesNumero = DescontarfreezengDisponiblesNumero;
            oConfiguracionDTO.ClientesxVendedorAleatorio = ClientesxVendedorAleatorio;
            oConfiguracionDTO.NumeroDiaMesEjecucionAleatorio = NumeroDiaMesEjecucionAleatorio;
            oConfiguracionDTO.NotificarDeudasXDia = Convert.ToBoolean(NotificarDeudasxDia);
            oConfiguracionDTO.CantDiasDeuda = CantDiasDeudas;
            oConfiguracionDTO.EstadoMostrarVentaOtros = MostrarVentasOtros;
            oConfiguracionDTO.EstadoActivarCorreoBienvenida = ActivarCorreoBienvenida;
            oConfiguracionDTO.EstadoImprimirContrato = ActivarImprimirContrato;
            oConfiguracionDTO.TiempoMarcarAsistencia = TiempoMarcarAsistencia;
            oConfiguracionDTO.DiaCitasCaida = DiasCitasCaida;
            oConfiguracionDTO.Igv = Igv;
            oConfiguracionDTO.TipoDescuento = TipoDescuento;
            oConfiguracionDTO.Tipo = Tipo;
            oConfiguracionDTO.RutaCarpetaImagen = RutaCarpetaImagen;
            oConfiguracionDTO.Contrasenia = Contrasenia;
            oConfiguracionDTO.ConexionDB = ConexionDB;
            oConfiguracionDTO.LongitudSerie = LongitudSerie;
            oConfiguracionDTO.NombreTiquetera = NombreTiquetera;

            oConfiguracionDTO.CodigoCuenta = CodigoCuenta;
            oConfiguracionDTO.CodigoPlan = CodigoPlan;
            oConfiguracionDTO.TipoMoneda = TipoMoneda;

            oConfiguracionDTO.Frase = Frase;
            oConfiguracionDTO.UsuarioCreacion = "appsfit";
            oConfiguracionDTO.UsuarioEdicion = "appsfit";
            oConfiguracionDTO.FechaPago = FechaPago;
            oConfiguracionDTO.MontoMensualidad = MontoMensualidadPago;
            oConfiguracionDTO.Ubicaciones = Ubicaciones;
            oConfiguracionDTO.TieneFacturacionElectronica = TieneFacturacionElectronica;
            oConfiguracionDTO.UrlAPISunafact = UrlAPISunafact;
            oConfiguracionDTO.TokenSunafact = TokenSunafact;

            oConfiguracionDTO.NombreGerente = NombreGerente;
            oConfiguracionDTO.ContactoCobranza = ContactoCobranza;
            oConfiguracionDTO.CelularCobranza = CelularCobranza;

            oConfiguracionDTO.AplicacionDisponible = AplicacionDisponible;
            oConfiguracionDTO.TiendaAplicacion = TiendaAplicacion;
            oConfiguracionDTO.RutinasAplicacion = RutinasAplicacion;

            oConfiguracionDTO.Operation = accion == "N" ? Operation.CreateConfiguracion_adFitness : Operation.UpdateConfiguracion_adFitness;

            oConfiguracionDTO.Estado = Estado;
            if (Estado == 1)
            {
                //si es activo la fecha vencimiento debe ser en 1 año mayor a hoy
                DateTime fechaHoy = new DateTime();
                fechaHoy = DateTime.Now.AddYears(1);
                oConfiguracionDTO.FechaVencimiento = fechaHoy;
            }
            else if (Estado == 2)
            {
                //si es prueba la fecha vencimiento debe ser en 7 dias mayor a hoy
                //DateTime fechaHoy = new DateTime();
                //fechaHoy = DateTime.Now.AddDays(7);
                oConfiguracionDTO.FechaVencimiento = FechaVencimiento;
            }
            else if (Estado == 3)
            {
                //si es inactivo la fecha vencimiento debe ser en 1 año menor a hoy
                DateTime fechaHoy = new DateTime();
                fechaHoy = DateTime.Now.AddYears(-1);
                oConfiguracionDTO.FechaVencimiento = fechaHoy;
            }
            else if (Estado == 4)
            {
                //si es retirado la fecha vencimiento debe ser en 1 año menor a hoy
                DateTime fechaHoy = new DateTime();
                fechaHoy = DateTime.Now.AddYears(-1);
                oConfiguracionDTO.FechaVencimiento = fechaHoy;
            }

            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            lista.Add(oConfiguracionDTO);

            ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
            {
                List = lista,
                User = "appsfit"
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

        public JsonResult GuardarConfiguracion_logo(CentroEntrenamiento_ConfiguracionViewModel request)
        {
            var file2 = request.file;
            string ruta;
            request.UrlUmagen = string.Empty;
            if (file2 != null)
            {
                var fileName = Path.GetFileName(file2.FileName);
                var extention = Path.GetExtension(file2.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file2.FileName);

                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { file2.FileName, file2.ContentType, file2.InputStream });

                ruta = UploadImgageAzure.UploadFilesAzure(obj, (request.CodigoUnidadNegocio.ToString() + request.CodigoSede.ToString() + extention), "empresas");
                request.UrlUmagen = ruta;
            }


            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = request.CodigoSede;
            oConfiguracionDTO.Logo = request.UrlUmagen;

            oConfiguracionDTO.Operation = Operation.uspActualizarConfiguracionLogo_adFitness;
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            lista.Add(oConfiguracionDTO);

            ReqConfiguracionDTO oReq = new ReqConfiguracionDTO()
            {
                List = lista,
                User = "appsfit"
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

        public ActionResult SEGListarUsuarioPorPerfil(int CodigoUnidadNegocio, int CodSede, int CodigoPerfil, bool Estado)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoPerfil = CodigoPerfil;
            oUsuarioDTO.Estado = Estado;

            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                FilterCase = filterCaseUsuario.SEGListarUsuarioPorPerfil,
                Item = oUsuarioDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 200
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


        #endregion

        #region MODULOS

        public ActionResult modulos()
        {
            return View();
        }

        public ActionResult CentroEntrenamiento_uspListarMenuPlantilla_ddl()
        {
            //List<CentroEntrenamiento_MenuPlantillaDTO> lista = null;
            CentroEntrenamiento_MenuPlantillaDTO oCentroEntrenamiento_MenuPlantillaDTO = new CentroEntrenamiento_MenuPlantillaDTO();
            //oCentroEntrenamiento_MenuPlantillaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            //oCentroEntrenamiento_MenuPlantillaDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarMenuPlantilla,
                User = "appsfit",
                Item = oCentroEntrenamiento_MenuPlantillaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_MenuPlantillaDTO oResp = null;

            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.CentroEntrenamiento_MenuPlantillaGetList(oReq);
            }

            List<CentroEntrenamiento_MenuPlantillaDTO> listaFiltro = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            listaFiltro.Add(new CentroEntrenamiento_MenuPlantillaDTO()
            {
                CodigoMenu = string.Empty,
                Descripcion = "Sin menú superior"
            });

            if (oResp.Success)
            {
                for (int i = 0; i < oResp.List.Count; i++)
                {
                    if (oResp.List[i].CodigoMenuSuperior == string.Empty)
                    {
                        listaFiltro.Add(new CentroEntrenamiento_MenuPlantillaDTO()
                        {
                            CodigoMenu = oResp.List[i].CodigoMenu,
                            Descripcion = oResp.List[i].Descripcion
                        });

                    }
                }

            }
            return Json(listaFiltro, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CentroEntrenamiento_uspBuscarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            CentroEntrenamiento_MenuPlantillaDTO oCentroEntrenamiento_MenuPlantillaDTO = new CentroEntrenamiento_MenuPlantillaDTO();
            //oCentroEntrenamiento_MenuPlantillaDTO.CodigoMenu = CodigoMenu;
            ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                Item = request,
                FilterCase = filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspBuscarMenuPlantilla,
                User = "appsift"
            };
            RespItemCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.CentroEntrenamiento_MenuPlantillaGetItem(oReq);
            }
            if (oResp.Success)
            {
                oCentroEntrenamiento_MenuPlantillaDTO = oResp.Item;
            }
            return Json(oCentroEntrenamiento_MenuPlantillaDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspListarMenuPlantilla()
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = null;
            CentroEntrenamiento_MenuPlantillaDTO oCentroEntrenamiento_MenuPlantillaDTO = new CentroEntrenamiento_MenuPlantillaDTO();
            //oCentroEntrenamiento_MenuPlantillaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            //oCentroEntrenamiento_MenuPlantillaDTO.CodigoSede = Commun.CodigoSede;

            ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarMenuPlantilla,
                User = "appsfit",
                Item = oCentroEntrenamiento_MenuPlantillaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_MenuPlantillaDTO oResp = null;

            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.CentroEntrenamiento_MenuPlantillaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspRegistrarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            int Codigo = 0;
            List<CentroEntrenamiento_MenuPlantillaDTO> list = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            list.Add(new CentroEntrenamiento_MenuPlantillaDTO()
            {
                CodigoMenu = request.CodigoMenu,
                CodigoMenuSuperior = request.CodigoMenuSuperior,
                Descripcion = request.Descripcion,
                Observacion = request.Observacion,
                IdControl = request.IdControl,
                Url = request.Url,
                Orden = request.Orden,
                Estado = request.Estado,
                UsuarioCreacion = "appsfit",
                Operation = Operation.Create

            });

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspActualizarMenuPlantilla(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            int Codigo = 0;
            List<CentroEntrenamiento_MenuPlantillaDTO> list = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            list.Add(new CentroEntrenamiento_MenuPlantillaDTO()
            {
                CodigoMenu = request.CodigoMenu,
                CodigoMenuSuperior = request.CodigoMenuSuperior,
                Descripcion = request.Descripcion,
                Observacion = request.Observacion,
                IdControl = request.IdControl,
                Url = request.Url,
                Orden = request.Orden,
                Estado = request.Estado,
                UsuarioCreacion = "appsfit",
                Operation = Operation.Update

            });

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CentroEntrenamiento_uspActualizarMenuPlantillaOrden(string listaDetalle)
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
                    CodigoMenu = nodo.ChildNodes[0].InnerText,
                    Orden = Convert.ToInt32(nodo.ChildNodes[1].InnerText),
                    UsuarioCreacion = "appsfit",
                    Operation = Operation.CentroEntrenamiento_uspActualizarMenuPlantillaOrden
                });
            }

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        //PLAN
        public ActionResult CentroEntrenamiento_uspListarSEG_Planes()
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = null;
            CentroEntrenamiento_MenuPlantillaDTO oCentroEntrenamiento_MenuPlantillaDTO = new CentroEntrenamiento_MenuPlantillaDTO();

            ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarSEG_Planes,
                User = "appsfit",
                Item = oCentroEntrenamiento_MenuPlantillaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_MenuPlantillaDTO oResp = null;

            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.CentroEntrenamiento_MenuPlantillaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //MENU PLAN
        public ActionResult CentroEntrenamiento_uspListarMenuPlantillaPlan(CentroEntrenamiento_MenuPlantillaDTO request)
        {
            List<CentroEntrenamiento_MenuPlantillaDTO> lista = null;

            ReqFilterCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqFilterCentroEntrenamiento_MenuPlantillaDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_MenuPlantilla.CentroEntrenamiento_uspListarMenuPlantillaPlan,
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

            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.CentroEntrenamiento_MenuPlantillaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //REGISTRAR MENU DEL PLAN
        public ActionResult CentroEntrenamiento_uspRegistrarMenuPlantillaPlan(string listaDetalle)
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
                    Planes_CodigoPlan = Convert.ToInt32(nodo.ChildNodes[0].InnerText),
                    CodigoMenu = nodo.ChildNodes[1].InnerText,
                    UsuarioCreacion = "appsfit",
                    Operation = Operation.CentroEntrenamiento_uspRegistrarMenuPlantillaPlan
                });
            }

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }

        //ELIMINAR MENU DEL PLAN
        public ActionResult CentroEntrenamiento_uspEliminarMenuPlantillaPlan(int Planes_CodigoPlan)
        {
            int Codigo = 0;
            List<CentroEntrenamiento_MenuPlantillaDTO> list = new List<CentroEntrenamiento_MenuPlantillaDTO>();

            list.Add(new CentroEntrenamiento_MenuPlantillaDTO()
            {
                Planes_CodigoPlan = Planes_CodigoPlan,
                Operation = Operation.CentroEntrenamiento_uspEliminarMenuPlantillaPlan
            });

            ReqCentroEntrenamiento_MenuPlantillaDTO oReq = new ReqCentroEntrenamiento_MenuPlantillaDTO()
            {
                List = list,
                User = "appsfit"
            };

            RespCentroEntrenamiento_MenuPlantillaDTO oResp = null;
            using (CentroEntrenamiento_MenuPlantillaLogic oCentroEntrenamiento_MenuPlantillaLogic = new CentroEntrenamiento_MenuPlantillaLogic())
            {
                oResp = oCentroEntrenamiento_MenuPlantillaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Codigo = oResp.MessageList[0].Codigo;
            }
            return Json(Codigo, JsonRequestBehavior.AllowGet);
        }




        public ActionResult planes()
        {
            return View();
        }

        #endregion


        #region COBRANZA

        public ActionResult cobranza()
        {
            return View();
        }



        public ActionResult uspListarConfiguracion_Cobranzas_Paginacion(int Anio, int Mes, int Tipo, int PageNumber, string filtro)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.filtro = filtro;
            oConfiguracionDTO.Anio = Anio;
            oConfiguracionDTO.MesEntero = Mes;
            oConfiguracionDTO.Tipo = Tipo;


            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = filterCaseConfiguracion.uspListarConfiguracion_Cobranzas_Paginacion,
                Item = oConfiguracionDTO,
                User = "ADMIN",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
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

        public ActionResult uspListarConfiguracion_Cobranzas_NumeroRegistros(int Anio, int Mes, int Tipo, string filtro)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.filtro = filtro;
            oConfiguracionDTO.Anio = Anio;
            oConfiguracionDTO.MesEntero = Mes;
            oConfiguracionDTO.Tipo = Tipo;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                FilterCase = filterCaseConfiguracion.uspListarConfiguracion_Cobranzas_NumeroRegistros,
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
                oConfiguracionDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarConfiguracion_apfitness_NumeroRegistros"]);
            }
            return Json(oConfiguracionDTO, JsonRequestBehavior.AllowGet);
        }

        public ActionResult uspRegistrarConfiguracionPagosMensualidades(int CodigoUnidadNegocio,
        int CodigoSede,
        int Anio,
        int Mes,
        int Dia,
        decimal MontoMes,
        decimal MontoAcuenta,
        string NroOperacion,
        string CodigoNroCuenta,
        string UsuarioCreacion)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = CodigoSede;
            oConfiguracionDTO.FechaPago = new DateTime(Anio, Mes, Dia);
            oConfiguracionDTO.MontoMes = MontoMes;
            oConfiguracionDTO.MontoAcuenta = MontoAcuenta;
            oConfiguracionDTO.NroOperacion = NroOperacion;
            oConfiguracionDTO.CodigoNroCuenta = CodigoNroCuenta;
            oConfiguracionDTO.UsuarioCreacion = UsuarioCreacion;
            oConfiguracionDTO.Operation = Operation.uspRegistrarConfiguracionPagosMensualidades;

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

        public ActionResult uspRegistrarConfiguracionMatriculas(
                    string Descripcion,
                    decimal MontoMatricula,
                    string NroOperacion,
                    string UsuarioCreacion)
        {
            string mensaje = string.Empty;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Descripcion = Descripcion;
            oConfiguracionDTO.MontoMatricula = MontoMatricula;
            oConfiguracionDTO.NroOperacion = NroOperacion;
            oConfiguracionDTO.UsuarioCreacion = UsuarioCreacion;
            oConfiguracionDTO.Operation = Operation.uspRegistrarConfiguracionMatriculas;

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

        public ActionResult uspListarConfiguracionMatriculas(int Anio, int Mes)
        {
            List<ConfiguracionDTO> lista = new List<ConfiguracionDTO>();
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Anio = Anio;
            oConfiguracionDTO.MesEntero = Mes;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = filterCaseConfiguracion.uspListarConfiguracionMatriculas,
                Item = oConfiguracionDTO,
                User = "ADMIN",
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

        #endregion

        public ActionResult mensajeswhatsapp()
        {
            return View();
        }

    }
}
