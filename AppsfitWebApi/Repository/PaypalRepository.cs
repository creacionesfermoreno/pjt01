using AppsfitWebApi.Controllers;
using AppsfitWebApi.Helpers;
using AppsfitWebApi.Models;
using AppsfitWebApi.Repository.Services;
using AppsfitWebApi.ViewModels;
using E_DataModel.Gimnasio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;


namespace AppsfitWebApi.Repository
{
    public class PaypalRepository
    {

        //capture order membresia
        public async Task<ResponseApi> CaptureOrderRepo(RequestCapture req, string baseUrl)
        {
            ResponseApi responseApi = new ResponseApi();

            PaypalService paypalService = new PaypalService();
            HomeRepository homeRepository = new HomeRepository();

            var token = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);
            if (token.Success)
            {
                //capture order
                var capture = await paypalService.PaypalOrderCaptureService(new { }, token?.Message1, req.OrderId);
                if (capture.Success)
                {
                    //show detail order
                    //var show = await paypalService.PaypalOrderDetailService(token?.Message1, req.OrderId);
                    //if (show.Success)
                    //{

                    //    var details = (List<PurchaseUnit>)show.Date;
                    //    responseApi.Date = details[0].items;
                    //    responseApi.Message1 = details[0].amount.value;
                    //    responseApi.Message2 = details[0].reference_id;
                    //}

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
                    oVentasDTO.RazonSocial_Sr = req.sale.RazonSocial_Sr;
                    oVentasDTO.RUC_DNI = req.sale.RUC_DNI;
                    oVentasDTO.Direccion = req.sale.Direccion;
                    oVentasDTO.TotalNeto = membresia.Costo;
                    oVentasDTO.Comentario = req.sale.Comentario;
                    oVentasDTO.NroTarjeta = "";
                    oVentasDTO.NroBoucher = req.sale.NroBoucher;

                    var pay = repositoryMem.PayMembresiaRepository(oVentasDTO, req.CodigoSede, req.CodigoUnidadNegocio, membresiaAPI);
                    if (pay.Success == true)
                    {

                        //************************* BackgroundJob *********************

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
                else { responseApi = capture; }

            }
            else { responseApi = token; }

            return responseApi;
        }


        //capture order product
        public async Task<ResponseApi> CaptureOrderProductRepo(RequestProductCapturePaypal req, string baseUrl)
        {
            ResponseApi responseApi = new ResponseApi();

            PaypalService paypalService = new PaypalService();
            HomeRepository homeRepository = new HomeRepository();

            var token = await homeRepository.ValidPasarelaRepo(req.DefaultKeyEmpresa, req.CodigoPlantillaFormaPago);
            if (token.Success)
            {
                //capture order
                var capture = await paypalService.PaypalOrderCaptureService(new { }, token?.Message1, req.OrderId);
                if (capture.Success)
                {
                    //show detail order
                    //var show = await paypalService.PaypalOrderDetailService(token?.Message1, req.OrderId);
                    //if (show.Success)
                    //{

                    //    var details = (List<PurchaseUnit>)show.Date;
                    //    responseApi.Date = details[0].items;
                    //    responseApi.Message1 = details[0].amount.value;
                    //    responseApi.Message2 = details[0].reference_id;
                    //}

                    //********************* last process register bd ********************


                    //save post bd
                    PostApiRepository repository = new PostApiRepository();

                    ComprobanteRequestModel compro =new ComprobanteRequestModel();
                    compro.DefaultKeyEmpresa= req.DefaultKeyEmpresa;
                    compro.CodigoPlantillaFormaPago = req.CodigoPlantillaFormaPago;
                    compro.CodigoUnidadNegocio = req.CodigoUnidadNegocio;
                    compro.CodigoSede = req.CodigoSede;
                    compro.CodigoAlmacen = req.CodigoAlmacen;
                    compro.Total = req.Total;
                    compro.Estado = req.Estado;
                    compro.NroIdentificacion = req.NroIdentificacion;
                    compro.Email = req.Email;
                    compro.listaDetalle = req.listaDetalle;
                    var codePost = repository.RegisterComprobanteRepo(compro);
                    if (codePost > 0)
                    {

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
                else { responseApi = capture; }

            }
            else { responseApi = token; }

            return responseApi;
        }

    }
}