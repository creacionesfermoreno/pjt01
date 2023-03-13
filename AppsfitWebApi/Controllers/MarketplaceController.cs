using AppsfitWebApi.Helpers;
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository;
using AppsfitWebApi.Repository.CulqiServices;
using AppsfitWebApi.Repository.Services;
using AppsfitWebApi.ViewModels;
using E_BusinessLayer;
using E_BusinessLayer.CentroEntrenamiento;
using E_BusinessLayer.Gimnasio;
using E_DataModel;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/marketplace")]
    public class MarketplaceController : ApiController
    {


        //*********************************************** PLANES **********************************************

        [HttpPost]
        [Route("listarplanes")] //CodigoUnidadNegocio,CodigoSede
        public HttpResponseMessage getListPlanesApp(PlanesDTO request)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            bool validadorParametros = true;

            if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Codigo de sede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                return HttpResponseJson(_objResponseModel);
            }

            try
            {
                List<PlanesApiModel> list = new List<PlanesApiModel>();
                PlanesDTO oPlanesDTO = new PlanesDTO();
                oPlanesDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
                oPlanesDTO.CodigoSede = request.CodigoSede;
                ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
                {
                    FilterCase = filterCasePlanes.listApp,
                    Item = oPlanesDTO,
                    User = "appsfit",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 0,
                        PageRecords = 9999
                    }
                };
                RespListPlanesDTO oResp = null;
                using (PlanesLogic oPlanesLogic = new PlanesLogic())
                {
                    oResp = oPlanesLogic.PlanesGetList(oReq);
                }
                if (oResp.Success)
                {

                    foreach (PlanesDTO item in oResp.List)
                    {
                        PlanesApiModel modelApi = new PlanesApiModel();
                        modelApi.CodigoPaquete = item.CodigoPaquete;
                        modelApi.CodigoTipoPaquete = item.CodigoTipoPaquete;
                        modelApi.DesTiempoPlan = item.DesTiempoMembresia;
                        modelApi.Descripcion = item.Descripcion;
                        modelApi.EstadoMembresiaInterdiaria = item.EstadoMembresiaInterdiaria;
                        modelApi.CongelamientoVigente = item.CongelamientoVigente;
                        modelApi.ValorTiempoPlan = item.ValorTiempoPlan;
                        modelApi.ValorSesiones = item.ValorSesiones;
                        modelApi.Costo = item.Costo;
                        modelApi.NroCupo = item.NroCupo;
                        modelApi.FechaVencimiento = item.FechaVencimiento;
                        modelApi.DesNomProfesor = item.desNomProfesor;
                        modelApi.DesTipoPaquete = item.DesTipoPaquete;
                        modelApi.UrlImage = item.UrlImage;
                        modelApi.Suscripcion = item.Suscripcion;
                        list.Add(modelApi);
                    }

                }
                _objResponseModel.Date = list;
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message;
            }
            return HttpResponseJson(_objResponseModel);
        }

        //***************************************************************   MEMBRESIA PAY ***********************************


        //culqi - membresia
        [HttpPost]
        [Route("membresia/buy/culqi")]
        public async Task<HttpResponseMessage> BuyMembresiaCulqi([FromBody] RequestCharge request)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                _objResponseModel.Success = false;
                _objResponseModel.Status = 2;
                _objResponseModel.Errors = allErrors;
            }
            else
            {

                CulqiService services = new CulqiService();
                PasarelaEmpresaApiRepository repository = new PasarelaEmpresaApiRepository();

                try
                {
                    //search acount pasarela
                    HomeRepository homeRepository = new HomeRepository();
                    var validAccount = await homeRepository.ValidPasarelaRepo(request.DefaultKeyEmpresa, request.CodigoPlantillaFormaPago);

                    if (!validAccount.Success)
                    {
                        return HttpResponseJson(validAccount);
                    }

                    //validate card return token id
                    request.card.email = request.Email;
                    var validate = await services.validateCardTokenService(request.card, validAccount?.Message1);

                    if (validate.Success == true)
                    {
                        ChargeAPI ocharge = new ChargeAPI()
                        {
                            amount = request.charge.amount,
                            currency_code = "PEN",
                            email = request.Email,
                            source_id = validate?.Message1,
                            capture = false,
                            metadata = new MetadataCharge
                            {
                                documentNumber = request.sale.RUC_DNI,
                            }
                        };

                        //charge culqi
                        var charge = await services.chargeService(ocharge, validAccount?.Message2);


                        if (charge.Success == true)
                        {
                            //********************* last process charge culqi********************

                            MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
                            AspNetHelper oHelper = new AspNetHelper();

                            //save membresia
                            int value = oHelper.ValidateInputMembresia(request.membresia.FechaFinMembresia);
                            request.membresia.TipoIngreso = value;
                            request.membresia.CodigoMebresiaOrigen = value;
                            request.membresia.DefaultKeyEmpresa = request.DefaultKeyEmpresa;
                            request.membresia.CodigoSocio = request.CodigoSocio;
                            int IdMembresia = repositoryMem.GuardarMembresiaContratoRepository(request.membresia);

                            //pay
                            MembresiaAPI membresiaAPI = new MembresiaAPI();
                            membresiaAPI.CodigoMembresia = IdMembresia;
                            membresiaAPI.Costo = request.membresia.Costo;
                            membresiaAPI.Descripcion = request.membresia.Descripcion;

                            VentasDTO oVentasDTO = new VentasDTO();
                            oVentasDTO.DefaultKeyEmpresa = request.DefaultKeyEmpresa;
                            oVentasDTO.CodigoSocio = request.CodigoSocio;
                            oVentasDTO.RazonSocial_Sr = request.sale.RazonSocial_Sr;
                            oVentasDTO.RUC_DNI = request.sale.RUC_DNI;
                            oVentasDTO.Direccion = request.sale.Direccion;
                            oVentasDTO.TotalNeto = request.membresia.Costo;
                            oVentasDTO.Comentario = request.sale.Comentario;
                            oVentasDTO.NroTarjeta = "";
                            oVentasDTO.NroBoucher = request.sale.NroBoucher;

                            var pay = repositoryMem.PayMembresiaRepository(oVentasDTO, request.CodigoSede, request.CodigoUnidadNegocio, membresiaAPI);
                            if (pay.Success == true)
                            {

                                //************************* BackgroundJob *********************
                                var Request = HttpContext.Current.Request;
                                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                                _ = JobApiHelper.SendReceiptJob("", request.CodigoSede, request.CodigoUnidadNegocio, int.Parse(pay.Message1), request.Email, baseUrl, 2);
                                //************************* BackgroundJob *********************
                                _objResponseModel.Message1 = "Su compra ha sido exitosa.";
                                _objResponseModel.Success = true;
                                _objResponseModel.Status = 0;
                            }
                            else
                            {
                                _objResponseModel.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                                _objResponseModel.Success = false;
                                _objResponseModel.Status = 1;
                            }

                        }
                        else
                        {
                            _objResponseModel.Message1 = charge.Message1;
                            _objResponseModel.Message2 = charge.Message2;
                            _objResponseModel.Success = false;
                            _objResponseModel.Status = 1;
                        }
                    }
                    else
                    {
                        //token card invalid
                        _objResponseModel.Message1 = validate.Message1;
                        _objResponseModel.Message2 = validate.Message2;
                        _objResponseModel.Success = false;
                        _objResponseModel.Status = 1;
                    }

                }
                catch (Exception ex)
                {
                    _objResponseModel.Message1 = ex.Message;
                    _objResponseModel.Success = false;
                    _objResponseModel.Status = 1;
                }
            }

            return HttpResponseJson(_objResponseModel);

        }


        //membresia - Paypal
        [HttpPost]
        [Route("membresia/buy/paypal")]
        public async Task<HttpResponseMessage> BuyMembresiaPaypal(RequestCapture req)
        {
            ResponseApi responseApi = new ResponseApi();

            PaypalRepository paypalRepository = new PaypalRepository();

            //HttpContent requestContent = Request.Content;
            //string jsonContent =  requestContent.ReadAsStringAsync().Result;

            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                var Request = HttpContext.Current.Request;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                responseApi = await paypalRepository.CaptureOrderRepo(req, baseUrl);
            }

            return HttpResponseJson(responseApi);
        }


        //---Start Mercado Pago ---
        //membresia generate preferences - Mercado Pago
        [HttpPost]
        [Route("mercadopago/preferences")]
        public async Task<HttpResponseMessage> MPagoPrefMembresia([FromBody] RequestMPagoPref req)
        {
            ResponseApi responseApi = new ResponseApi();
            if (!ModelState.IsValid || req == null)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                //search acount pasarela
                HomeRepository homeRepository = new HomeRepository();
                var validAccount = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);

                if (!validAccount.Success)
                {
                    return HttpResponseJson(validAccount);
                }


                MercadoPagoService service = new MercadoPagoService();
                MPagoModel mPagoModel = new MPagoModel();

                //set membresia
                List<ItemMP> items = new List<ItemMP>() {
                (new ItemMP() { id = "1", title = req.membresia.name, currency_id = validAccount?.Message2, description = req.membresia.Descripcion, category_id = validAccount?.Message2, quantity = 1, unit_price = req.membresia.Costo })

                    };

                mPagoModel.payer = new Payer()
                {
                    name = req.payer.Name,
                    surname = req.payer.Surname,
                    email = req.Email,
                    identification = new Identification()
                    {
                        type = req.payer.Type_doc,
                        number = req.payer.Number_doc
                    }
                };
                mPagoModel.back_urls = new BackUrls() { success = "www.example.com/success", failure = "www.example.com/failure", pending = "www.example.com/pending" };
                mPagoModel.auto_return = "approved";
                mPagoModel.external_reference = AspNetHelper.RandomString(10);
                mPagoModel.statement_descriptor = "Software AppsFit";
                mPagoModel.items = items;

                responseApi = await service.PreferencesMpagoServ(mPagoModel, validAccount?.Message1);

            }

            return HttpResponseJson(responseApi);
        }


        //membresia - Mercado Pago
        [HttpPost]
        [Route("membresia/buy/mercadopago")]
        public async Task<HttpResponseMessage> BuyMembresiaMPago([FromBody] RequestMPagoPayment req)
        {
            ResponseApi responseApi = new ResponseApi();
            if (!ModelState.IsValid || req == null)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                //search acount pasarela
                HomeRepository homeRepository = new HomeRepository();
                var validAccount = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);

                if (!validAccount.Success)
                {
                    return HttpResponseJson(validAccount);
                }

                //state preferences
                MercadoPagoService service = new MercadoPagoService();
                var statePref = await service.StatePaymentMpagoServ(token: validAccount?.Message1, payment: req.PaymentId);
                if (statePref.Success && statePref.Message1 == "approved")
                {
                    //processs finish

                    //********************* last process register bd ********************

                    MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
                    AspNetHelper oHelper = new AspNetHelper();

                    var membresia = req.membresia;

                    //save membresia
                    int value = oHelper.ValidateInputMembresia(membresia.FechaFinMembresia);
                    membresia.TipoIngreso = value;
                    membresia.CodigoMebresiaOrigen = value;
                    membresia.DefaultKeyEmpresa = req.DefaultKeyEmpresa;
                    membresia.CodigoSocio = req.CodigoSocio;
                    int IdMembresia = repositoryMem.GuardarMembresiaContratoRepository(membresia);

                    //pay
                    MembresiaAPI membresiaAPI = new MembresiaAPI();
                    membresiaAPI.CodigoMembresia = IdMembresia;
                    membresiaAPI.Costo = membresia.Costo;
                    membresiaAPI.Descripcion = membresia.Descripcion;

                    VentasDTO oVentasDTO = new VentasDTO();
                    oVentasDTO.DefaultKeyEmpresa = req.DefaultKeyEmpresa;
                    oVentasDTO.CodigoSocio = req.CodigoSocio;
                    oVentasDTO.RazonSocial_Sr = req.payer.Name;
                    oVentasDTO.RUC_DNI = req.payer.Number_doc;
                    oVentasDTO.Direccion = req.payer.Address ?? "--";
                    oVentasDTO.TotalNeto = membresia.Costo;
                    oVentasDTO.Comentario = $"Mercado Pago, IdTransacion :{req.PaymentId}, Correo:{req.Email}";
                    oVentasDTO.NroTarjeta = "Mercado Pago";
                    oVentasDTO.NroBoucher = req.PaymentId;

                    var pay = repositoryMem.PayMembresiaRepository(oVentasDTO, req.CodigoSede, req.CodigoUnidadNegocio, membresiaAPI);
                    if (pay.Success == true)
                    {

                        //************************* BackgroundJob *********************
                        var Request = HttpContext.Current.Request;
                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                        _ = JobApiHelper.SendReceiptJob("", req.CodigoSede, req.CodigoUnidadNegocio, int.Parse(pay.Message1), req.Email, baseUrl, 2);
                        //************************* BackgroundJob *********************
                        responseApi.Message1 = "Su compra ha sido exitosa.";
                        responseApi.Success = true;
                        responseApi.Status = 0;
                    }
                    else
                    {
                        responseApi.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                        responseApi.Success = false;
                        responseApi.Status = 1;
                    }

                }
                else
                {
                    responseApi = statePref;
                }
            }

            return HttpResponseJson(responseApi);
        }


        //product - generate preferences - mercado pago
        [HttpPost]
        [Route("mercadopago/preferences/product")]
        public async Task<HttpResponseMessage> MPagoPrefProduct([FromBody] ReqMPRefProduct req)
        {
            ResponseApi responseApi = new ResponseApi();
            if (!ModelState.IsValid || req == null)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                //search acount pasarela
                HomeRepository homeRepository = new HomeRepository();
                var validAccount = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);

                if (!validAccount.Success)
                {
                    return HttpResponseJson(validAccount);
                }


                MercadoPagoService service = new MercadoPagoService();
                MPagoModel mPagoModel = new MPagoModel();

                //set membresia
                List<ItemMP> items = new List<ItemMP>();

                int index = 1;
                foreach (ComprobanteDetalleRequestAPI prod in req.listaDetalle)
                {
                    items.Add(new ItemMP() { id = $"{index}", title = prod.Descripcion, currency_id = validAccount?.Message2, description = prod.Descripcion, category_id = validAccount?.Message2, quantity = prod.Cantidad, unit_price = prod.Precio });
                    index++;
                }

                mPagoModel.payer = new Payer()
                {
                    name = req.payer.Name,
                    surname = req.payer.Surname,
                    email = req.Email,
                    identification = new Identification()
                    {
                        type = req.payer.Type_doc,
                        number = req.payer.Number_doc
                    }
                };
                mPagoModel.back_urls = new BackUrls() { success = "www.example.com/success", failure = "www.example.com/failure", pending = "www.example.com/pending" };
                mPagoModel.payment_methods = new PaymentMethods()
                {
                    installments = 1,
                };
                mPagoModel.auto_return = "approved";
                mPagoModel.external_reference = AspNetHelper.RandomString(10);
                mPagoModel.statement_descriptor = "Software AppsFit";
                mPagoModel.items = items;

                responseApi = await service.PreferencesMpagoServ(mPagoModel, validAccount?.Message1);

            }

            return HttpResponseJson(responseApi);
        }


        //product - Mercado Pago
        [HttpPost]
        [Route("product/buy/mercadopago")]
        public async Task<HttpResponseMessage> BuyProductMPago([FromBody] ReqProductMP req)
        {
            ResponseApi responseApi = new ResponseApi();
            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                //search acount pasarela
                HomeRepository homeRepository = new HomeRepository();
                var validAccount = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);

                if (!validAccount.Success)
                {
                    return HttpResponseJson(validAccount);
                }

                //state preferences
                MercadoPagoService service = new MercadoPagoService();
                var statePref = await service.StatePaymentMpagoServ(token: validAccount?.Message1, payment: req.PaymentId);

                if (statePref.Success && statePref.Message1 == "approved")
                {
                    //********************* last process register bd ********************

                    //save post bd
                    PostApiRepository repository = new PostApiRepository();
                    decimal total = 0;
                    foreach (ComprobanteDetalleRequestAPI prodItem in req.listaDetalle)
                    {
                        total += prodItem.Total;
                    }

                    ComprobanteRequestModel compro = new ComprobanteRequestModel();
                    compro.DefaultKeyEmpresa = req.DefaultKeyEmpresa;
                    compro.CodigoPlantillaFormaPago = req.CodigoPlantillaFormaPago;
                    compro.CodigoUnidadNegocio = req.CodigoUnidadNegocio;
                    compro.CodigoSede = req.CodigoSede;
                    compro.CodigoAlmacen = req.CodigoAlmacen;
                    compro.Total = total;
                    compro.Estado = req.Estado;
                    compro.NroIdentificacion = req.payer.Number_doc;
                    compro.Email = req.Email;
                    compro.listaDetalle = req.listaDetalle;
                    var codePost = repository.RegisterComprobanteRepo(compro);
                    if (codePost > 0)
                    {
                        var Request = HttpContext.Current.Request;
                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                        //************************* BackgroundJob *********************
                        _ = JobApiHelper.SendReceiptJob(req.DefaultKeyEmpresa, req.CodigoSede, req.CodigoUnidadNegocio, codePost, req.Email, baseUrl, 1);
                        //************************* BackgroundJob *********************

                        responseApi.Message1 = "Su compra ha sido exitosa.";
                        responseApi.Message2 = codePost.ToString();
                        responseApi.Success = true;
                        responseApi.Status = 0;
                    }
                    else
                    {
                        responseApi.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                        responseApi.Success = false;
                        responseApi.Status = 1;
                    }
                }
                else
                {
                    responseApi = statePref;
                }

            }

            return HttpResponseJson(responseApi);
        }

        //---End Mercado Pago ---
        //***************************************************************   MEMBRESIA PAY ***********************************

        [HttpGet]
        [Route("categories")]
        public HttpResponseMessage categories()
        {
            ResponseApi responseApi = new ResponseApi();
            List<CategoryApiModel> lista = null;

            ReqFilterCategoriasDTO oReq = new ReqFilterCategoriasDTO()
            {
                Item = new CategoriasDTO() { },
                FilterCase = filterCaseCategorias.api_listCategories,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCategoriasDTO oResp = null;

            using (CategoriasLogic oCategoriasLogic = new CategoriasLogic())
            {
                oResp = oCategoriasLogic.CategoriasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CategoryApiModel>();
                foreach (CategoriasDTO item in oResp.List)
                {
                    lista.Add(new CategoryApiModel()
                    {
                        CodigoMenu = item.CodigoMenu,
                        CodigoMenuSuperior = item.CodigoMenuSuperior,
                        Descripcion = item.Descripcion,
                        UrlUbicacion = item.UrlUbicacion,
                        UrlImagen = item.UrlImagen,
                        CodigoImagenPortada = item.CodigoImagenPortada,
                    });
                }
            }
            responseApi.Date = lista;
            return HttpResponseJson(responseApi);
        }


        [HttpPost]
        [Route("products/category")]
        public HttpResponseMessage productByCategory(RequestBodyFilterProduct req)
        {
            ResponseApi _objResponseModel = new ResponseApi();

            if (req.CodigoUnidadNegocio == 0 || req.CodigoSede == 0 || req.CodigoMenu == 0)
            {
                _objResponseModel.Message1 = "Campos requeridos CodigoUnidadNegocio, CodigoSede, CodigoMenu";
                _objResponseModel.Status = 2;
                _objResponseModel.Success = false;
                return HttpResponseJson(_objResponseModel);
            }

            List<ItemsVentaDTO> list = null;
            try
            {
                ItemsVentaDTO oItemsVentaDTO = new ItemsVentaDTO();
                oItemsVentaDTO.CodigoUnidadNegocio = req.CodigoUnidadNegocio;
                oItemsVentaDTO.CodigoSede = req.CodigoSede;
                oItemsVentaDTO.CodigoMenu = req.CodigoMenu;
                ReqFilterItemsVentaDTO oReq = new ReqFilterItemsVentaDTO()
                {
                    FilterCase = filterCaseItemsVenta.ecommerce_productbycate,
                    Item = oItemsVentaDTO,
                    User = "appsfit",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 0,
                        PageRecords = 9999
                    }
                };
                RespListItemsVentaDTO oResp = null;
                using (ItemsVentaLogic oItemsVentaLogic = new ItemsVentaLogic())
                {
                    oResp = oItemsVentaLogic.ItemsVentaGetList(oReq);
                }
                if (oResp.Success)
                {
                    _objResponseModel.Success = true;
                    _objResponseModel.Total = oResp.List.Count();
                    list = oResp.List;

                }
                _objResponseModel.Date = list;

            }
            catch (Exception ex)
            {
                _objResponseModel.Success = false;
                _objResponseModel.Message1 = ex.Message;
                _objResponseModel.Date = list;
            }
            return HttpResponseJson(_objResponseModel);
        }


        //product - culqi
        [HttpPost]
        [Route("products/buy/culqi")]
        public async Task<HttpResponseMessage> BuyProductCulqi([FromBody] ComprobanteRequestModel req)
        {
            ResponseApi _objResponseModel = new ResponseApi();

            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                _objResponseModel.Success = false;
                _objResponseModel.Status = 2;
                _objResponseModel.Errors = allErrors;
            }
            else
            {

                CulqiService services = new CulqiService();

                //search acount pasarela
                HomeRepository homeRepository = new HomeRepository();
                var validAccount = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);

                if (!validAccount.Success)
                {
                    return HttpResponseJson(validAccount);
                }

                //validate card return token id
                req.card.email = req.Email;
                var validate = await services.validateCardTokenService(req.card, validAccount?.Message1);

                if (validate.Success == true)
                {
                    ChargeAPI ocharge = new ChargeAPI()
                    {
                        amount = (int)(req.Total * 100),
                        currency_code = "PEN",
                        email = req.Email,
                        source_id = validate?.Message1,
                        capture = false,
                        metadata = new MetadataCharge
                        {
                            documentNumber = req.NroIdentificacion,
                        }
                    };


                    //charge culqi
                    var charge = await services.chargeService(ocharge, validAccount?.Message2);

                    if (charge.Success == true)
                    {
                        //********************* last process charge culqi********************

                        //save post bd
                        PostApiRepository repository = new PostApiRepository();
                        var codePost = repository.RegisterComprobanteRepo(req);
                        if (codePost > 0)
                        {

                            //************************* BackgroundJob *********************
                            var Request = HttpContext.Current.Request;
                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                            _ = JobApiHelper.SendReceiptJob(req.DefaultKeyEmpresa, req.CodigoSede, req.CodigoUnidadNegocio, codePost, req.Email, baseUrl, 1);
                            //************************* BackgroundJob *********************

                            _objResponseModel.Message1 = "Su compra ha sido exitosa.";
                            _objResponseModel.Message2 = codePost.ToString();
                            _objResponseModel.Success = true;
                            _objResponseModel.Status = 0;
                        }
                        else
                        {
                            _objResponseModel.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                            _objResponseModel.Success = false;
                            _objResponseModel.Status = 1;
                        }

                    }
                    else
                    {
                        _objResponseModel.Message1 = charge.Message1;
                        _objResponseModel.Message2 = charge.Message2;
                        _objResponseModel.Success = false;
                        _objResponseModel.Status = 1;
                    }
                }
                else
                {
                    //token card invalid
                    _objResponseModel.Message1 = validate.Message1;
                    _objResponseModel.Message2 = validate.Message2;
                    _objResponseModel.Success = false;
                    _objResponseModel.Status = 1;
                }
            }

            return HttpResponseJson(_objResponseModel);
        }


        //product - paypal
        [HttpPost]
        [Route("products/buy/paypal")]
        public async Task<HttpResponseMessage> BuyProductPaypal([FromBody] RequestProductCapturePaypal req)
        {
            ResponseApi responseApi = new ResponseApi();

            PaypalRepository paypalRepository = new PaypalRepository();

            //HttpContent requestContent = Request.Content;
            //string jsonContent =  requestContent.ReadAsStringAsync().Result;

            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                var Request = HttpContext.Current.Request;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                responseApi = await paypalRepository.CaptureOrderProductRepo(req, baseUrl);
            }

            return HttpResponseJson(responseApi);

        }


        //generar order membresia - paypal
        [HttpPost]
        [Route("paypal/order")]
        public async Task<HttpResponseMessage> PaypalOrderMembresia([FromBody] RequestOrder req)
        {
            ResponseApi responseApi = new ResponseApi();

            PaypalService paypalService = new PaypalService();
            PaypalHelper paypalHelper = new PaypalHelper();

            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                HomeRepository homeRepository = new HomeRepository();

                var token = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);
                if (token.Success == true)
                {
                    //order
                    List<ItemPaypal> items = new List<ItemPaypal>();
                    UnitAmount unitAmount = new UnitAmount() { currency_code = "USD", value = req.membresia.Costo };
                    items.Add(new ItemPaypal() { name = req.membresia.name, description = req.membresia.Descripcion, quantity = 1, unit_amount = unitAmount });
                    var data = paypalHelper.OrderHelper(items);
                    var orderServ = await paypalService.PaypalOrderService(data, token?.Message1, token.Production);

                    if (orderServ.Success)
                    {
                        responseApi = orderServ;
                        var links = (List<Link>)orderServ.Date;
                        if (links.Count > 0) { responseApi.Date = links.Where(e => e.rel == "approve"); }
                    }
                    else
                    {
                        responseApi = orderServ;
                    }

                }
                else { responseApi = token; }
            }

            return HttpResponseJson(responseApi);
        }


        //generar order product - paypal
        [HttpPost]
        [Route("paypal/order/product")]
        public async Task<HttpResponseMessage> PaypalOrderProduct([FromBody] RequestProductOrderPaypal req)
        {
            ResponseApi responseApi = new ResponseApi();

            PaypalService paypalService = new PaypalService();
            PaypalHelper paypalHelper = new PaypalHelper();

            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                HomeRepository homeRepository = new HomeRepository();

                var token = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);
                if (token.Success == true)
                {
                    //order
                    List<ItemPaypal> items = new List<ItemPaypal>();

                    foreach (ComprobanteDetalleRequestAPI item in req.listaDetalle)
                    {
                        UnitAmount unitAmount = new UnitAmount() { currency_code = "USD", value = item.Total };
                        items.Add(new ItemPaypal() { name = item.Descripcion, description = item.Descripcion, quantity = item.Cantidad, unit_amount = unitAmount });
                    }

                    var data = paypalHelper.OrderHelper(items);
                    var orderServ = await paypalService.PaypalOrderService(data, token?.Message1, token.Production);

                    if (orderServ.Success)
                    {
                        responseApi = orderServ;
                        var links = (List<Link>)orderServ.Date;
                        if (links.Count > 0) { responseApi.Date = links.Where(e => e.rel == "approve"); }
                    }
                    else
                    {
                        responseApi = orderServ;
                    }
                }
                else { responseApi = token; }
            }

            return HttpResponseJson(responseApi);
        }


        //list account payments by business
        [HttpGet]
        [Route("account/payments")]
        public HttpResponseMessage AccountPayments([FromBody] RequestSimple req)
        {
            ResponseApi responseApi = new ResponseApi();
            if (!string.IsNullOrEmpty(req.DefaultKeyEmpresa))
            {
                responseApi.Message3 = req.DefaultKeyEmpresa;
                HomeRepository homeRepository = new HomeRepository();
                responseApi = homeRepository.AccountPaymentsRepo(req.DefaultKeyEmpresa);
            }
            else
            {
                responseApi.Message1 = "Campo DefaultKeyEmpresa requerida";
                responseApi.Success = false;
                responseApi.Status = 2;
            }

            return HttpResponseJson(responseApi);
        }




        //list account payments by business
        [HttpGet]
        [Route("account/payments/suscription")]
        public HttpResponseMessage AccountPaymentsSuscription([FromBody] SimplePaymentsSuscrption req)
        {
            ResponseApi responseApi = new ResponseApi();
            if (req == null)
            {
                responseApi.Success = false;
                responseApi.Message1 = "Los campos no pueden ser nulos";

                return HttpResponseJson(responseApi);
            }
            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                List<PlanesDTO> lista = new List<PlanesDTO>();
                List<PaymentSuscriptionApi> list = new List<PaymentSuscriptionApi>();
                PlanesDTO oDto = new PlanesDTO();
                oDto.CodigoUnidadNegocio = req.CodigoUnidadNegocio;
                oDto.CodigoSede = req.CodigoSede;
                oDto.CodigoPaquete = req.CodigoPaquete;

                ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()

                {
                    FilterCase = filterCasePlanes.ListPlanPasarelaByPaquete,
                    Item = oDto,
                    User = "AppFit",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 0,
                        PageRecords = 99999
                    }
                };

                RespListPlanesDTO oResp = null;
                using (PlanesLogic oLogic = new PlanesLogic())
                {
                    oResp = oLogic.PlanesGetList(oReq);
                }

                if (oResp.Success)
                {

                    
                    foreach (var item in oResp.List)
                    {
                        list.Add(new PaymentSuscriptionApi()
                        {
                            CodigoPlantillaFormaPago = item.CodigoPlantillaFormaPago,
                            DesPasarelaPago = item.DesPasarelaPago,
                            CodigoPaquete = item.CodigoPaquete,
                            CodigoPaqueteSuscripcion = item.CodigoPaqueteSuscripcion,
                            PlanId = item.IdSuscripcionPasarela,
                            UrlImage = item.UrlImage,
                        }); 
                    }

                }

                responseApi.Date = list;
            }

            return HttpResponseJson(responseApi);
        }






        //*********************************************** PLANES **********************************************


        //************************************ SUSCRIPCION **************************************************

        [HttpPost]
        [Route("suscription/culqi")]
        public async Task<HttpResponseMessage> SuscriptionCulqi([FromBody] ReqSuscriptionCulqi req)
        {
            ResponseApi responseApi = new ResponseApi();

            if (!ModelState.IsValid)
            {
                //validate inputs
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                responseApi.Success = false;
                responseApi.Status = 2;
                responseApi.Errors = allErrors;
            }
            else
            {
                HomeRepository hrepo = new HomeRepository();

                var account = await hrepo.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);
                if (account.Success)
                {
                    //1.- generate token
                    CulqiService culqiService = new CulqiService();
                    req.card.email = req.Email;
                    var token = await culqiService.validateCardTokenService(req.card, account.Message1);

                    if (token.Success)
                    {
                        //verifi exist register db
                        var verifi = hrepo.ListMembSuscriptionDefaultKUserRepo(req.DefaultKeyUser);
                        ResponseApi customer = new ResponseApi();
                        if (verifi.Count > 0)
                        {
                            //existe IdClientePasarela

                            customer.Success = true;
                            customer.Message1 = verifi[0].IdClientePasarela;
                        }
                        else
                        {
                            //2.- create client
                            req.customer.email = req.Email;
                            customer = await culqiService.CreateCustomerServ(req.customer, account.Message2);
                        }



                        if (customer.Success)
                        {
                            //3.- create card
                            CardCulqi cardCulqi = new CardCulqi();
                            cardCulqi.customer_id = customer.Message1;
                            cardCulqi.token_id = token.Message1;
                            cardCulqi.validate = true;
                            cardCulqi.metadata = new Metadatax() { marca_tarjeta = "VISA" };
                            var card = await culqiService.CreateCardServ(cardCulqi, account.Message2);

                            if (card.Success)
                            {
                                //4.- suscription
                                SuscriptionCulqi suscriptionModel = new SuscriptionCulqi();

                                Sale sale = new Sale();
                                sale.RUC_DNI = req.NroDoc;

                                suscriptionModel.plan_id = req.PlanId;
                                suscriptionModel.card_id = card.Message1;
                                //formater data metadata
                                suscriptionModel.metadata = new MetadataSuscription()
                                {
                                    cliente_id = customer.Message1,
                                    documento_identidad = req.NroDoc,
                                };

                                var suscription = await culqiService.CreateSuscriptionServ(suscriptionModel, account?.Message2);
                                if (suscription.Success)
                                {

                                    //********************* last process charge culqi********************

                                    MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
                                    AspNetHelper oHelper = new AspNetHelper();

                                    //save membresia
                                    int value = oHelper.ValidateInputMembresia(req.membresia.FechaFinMembresia);
                                    req.membresia.TipoIngreso = value;
                                    req.membresia.CodigoMebresiaOrigen = value;
                                    req.membresia.DefaultKeyEmpresa = req.DefaultKeyEmpresa;
                                    req.membresia.CodigoSocio = req.CodigoSocio;
                                    int IdMembresia = repositoryMem.GuardarMembresiaContratoRepository(req.membresia);

                                    //pay
                                    MembresiaAPI membresiaAPI = new MembresiaAPI();
                                    membresiaAPI.CodigoMembresia = IdMembresia;
                                    membresiaAPI.Costo = req.membresia.Costo;
                                    membresiaAPI.Descripcion = req.membresia.Descripcion;

                                    VentasDTO oVentasDTO = new VentasDTO();
                                    oVentasDTO.DefaultKeyEmpresa = req.DefaultKeyEmpresa;
                                    oVentasDTO.CodigoSocio = req.CodigoSocio;
                                    oVentasDTO.RazonSocial_Sr = req.sale.RazonSocial_Sr;
                                    oVentasDTO.RUC_DNI = req.sale.RUC_DNI;
                                    oVentasDTO.Direccion = req.sale.Direccion;
                                    oVentasDTO.TotalNeto = req.membresia.Costo;
                                    oVentasDTO.Comentario = $"suscripción culqi, IdSuscripción:{suscription.Message1}";
                                    oVentasDTO.NroTarjeta = "";
                                    oVentasDTO.NroBoucher = req.sale.NroBoucher;

                                    var pay = repositoryMem.PayMembresiaRepository(oVentasDTO, req.CodigoSede, req.CodigoUnidadNegocio, membresiaAPI);
                                    if (pay.Success == true)
                                    {

                                        //************************* BackgroundJob *********************
                                        var Request = HttpContext.Current.Request;
                                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                                        _ = JobApiHelper.SendReceiptJob("", req.CodigoSede, req.CodigoUnidadNegocio, int.Parse(pay.Message1), req.Email, baseUrl, 2);
                                        //************************* BackgroundJob *********************
                                        responseApi.Message1 = "Su compra ha sido exitosa.";
                                        responseApi.Message2 = suscription.Message1;
                                        responseApi.Success = true;
                                        responseApi.Status = 0;
                                    }
                                    else
                                    {
                                        responseApi.Message1 = "Ocurrio un error en el proceso, intentelo más tarde";
                                        responseApi.Success = false;
                                        responseApi.Status = 1;
                                    }



                                    //************************************ register table temp ********************

                                    PlanesDTO planDto = new PlanesDTO();
                                    planDto.CodigoUnidadNegocio = req.CodigoUnidadNegocio;
                                    planDto.CodigoSede = req.CodigoSede;
                                    planDto.DefaultKeyEmpresa = req.DefaultKeyEmpresa;
                                    planDto.DefaultKeyUser = req.DefaultKeyUser;
                                    planDto.CodigoPlantillaFormaPago = req.CodigoPlantillaFormaPago;
                                    planDto.IdClientePasarela = customer.Message1;
                                    planDto.IdSuscripcionPasarela = suscription.Message1;

                                    req.card = null;
                                    req.customer = null;

                                    planDto.DataJsonPasarela = JsonConvert.SerializeObject(req);
                                    planDto.CodigoSocio = req.CodigoSocio;
                                    planDto.NroDocumento = req.NroDoc;

                                    planDto.CodigoPaquete = req.membresia.CodigoPaquete;
                                    var tempRegister = hrepo.RegisterSuscriptionMembresia(planDto);

                                    //************************************ register table temp ********************

                                }
                                else { responseApi = suscription; }
                            }
                            else { responseApi = card; }

                        }
                        else { responseApi = customer; }

                    }
                    else { responseApi = token; }
                }
                else { responseApi = account; }
            }


            return HttpResponseJson(responseApi);
        }

        //************************************ SUSCRIPCION **************************************************

        [HttpGet]
        [Route("dev")]
        public HttpResponseMessage dev()
        {
            ResponseApi responseApi = new ResponseApi();
            responseApi.Message1 = "dev api success";
            responseApi.Status = 0;
            return HttpResponseJson(responseApi);
        }


        public HttpResponseMessage HttpResponseJson(ResponseApi responseModel)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(responseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}