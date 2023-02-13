using E_BusinessLayer;
using E_DataModel.Common;
using E_DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AppsfitWebApi.ViewModels;
using AppsfitWebApi.Models;
using System.Net.Http;
using E_DataModel.Gimnasio;
using AppsfitWebApi.Helpers;

namespace AppsfitWebApi.Repository
{
    public class PostApiRepository
    {

        public int RegisterComprobanteRepo(ComprobanteRequestModel oItem)
        {
            int resp = 0;

            TipoComprobanteViewModel tipoComprobanteViewModel = new TipoComprobanteViewModel();
            tipoComprobanteViewModel.CodigoSede = oItem.CodigoSede;
            tipoComprobanteViewModel.CodigoUnidadNegocio = oItem.CodigoUnidadNegocio;
            tipoComprobanteViewModel.CodigoTipoDocumentoEmpresa = 5;

            List<TipoComprobanteViewModel> Correlativo = getTipoComprobanteRepo(tipoComprobanteViewModel);
            List<ComprobanteDTO> list = new List<ComprobanteDTO>();

            try
            {
                var data = Correlativo[0]?.Serie;
                String[] serie = data.Split('_');
                list.Add(new ComprobanteDTO()
                {
                    DefaultKeyEmpresa = oItem.DefaultKeyEmpresa,
                    CodigoSede = oItem.CodigoSede,
                    CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                    UsuarioCreacion = "Admin",
                    CodigoComprobante = 0,
                    CodigoTipoComprobante = 5,
                    TipoMoneda = 2,
                    CodigoAlmacen = oItem.CodigoAlmacen,
                    Correlativo = serie[1],
                    NroIdentificacion = oItem.NroIdentificacion,
                    CodigoVendedor = 0,
                    FechaEmision = DateTime.Now,
                    CodigoPlazoPago = 6,
                    TerminosCondiciones = "",
                    Notas = "",
                    Comentarios = "",
                    SubTotal = oItem.Total,
                    Descuento = 0,
                    IGV = 0,
                    SubTotal2 = oItem.Total,
                    Total = oItem.Total,
                    Estado = oItem.Estado,
                    Operation = Operation.api_registerComprobante,
                });


                list[0].listaDetalle = new List<ComprobanteDetalleDTO>();
                for (int i = 0; i < oItem.listaDetalle.Count; i++)
                {
                    list[0].listaDetalle.Add(new ComprobanteDetalleDTO()
                    {
                        DefaultKeyEmpresa = oItem.DefaultKeyEmpresa,
                        CodigoComprobanteDetalle = 0,
                        CodigoComprobante = 0,
                        CodigoAlmacen = oItem.CodigoAlmacen,
                        CodigoItemsVenta = oItem.listaDetalle[i].CodigoItemVenta,
                        Referencia = "",
                        Precio = oItem.listaDetalle[i].Precio,
                        Descuento = oItem.listaDetalle[i].Descuento,
                        CodigoTipoImpuesto = oItem.listaDetalle[i].CodigoTipoImpuesto,
                        Descripcion = oItem.listaDetalle[i].Descripcion == null ? string.Empty : oItem.listaDetalle[i].Descripcion,
                        Cantidad = oItem.listaDetalle[i].Cantidad,
                        Total = oItem.listaDetalle[i].Total,
                        Estado = 1,
                    });
                }
                list[0].listaDetallePago = new List<ComprobantePagoDTO>();

                list[0].listaDetallePago.Add(new ComprobantePagoDTO()
                {
                    DefaultKeyEmpresa = oItem.DefaultKeyEmpresa,
                    CodigoComprobantePago = 0,
                    CodigoComprobante = 0,
                    CodigoCuentaBancaria = 0,
                    CodigoMetodoPago = 5,
                    TipoMoneda = 2,
                    Monto = oItem.Total,
                    Nota = "",
                    Estado = 1,
                });

                ReqComprobanteDTO oReq = new ReqComprobanteDTO()
                {
                    List = list,
                    User = "admin"
                };
                RespComprobanteDTO oResp = null;

                using (ComprobanteLogic oComprobanteLogic = new ComprobanteLogic())
                {
                    oResp = oComprobanteLogic.ExecuteTransac(oReq);
                }
                if (oResp.Success) { resp = oResp.MessageList[0].Codigo; }
            }
            catch (Exception)  {   resp = 0;}

            return resp;
        }

        public List<TipoComprobanteViewModel> getTipoComprobanteRepo(TipoComprobanteViewModel request)
        {
            List<TipoComprobanteViewModel> lista = null;

            ReqFilterTipoComprobanteDTO oReq = new ReqFilterTipoComprobanteDTO()
            {
                Item = new TipoComprobanteDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoTipoDocumentoEmpresa = request.CodigoTipoDocumentoEmpresa
                },
                FilterCase = filterCaseTipoComprobante.ecommerce_uspListarTipoComprobante,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListTipoComprobanteDTO oResp = null;

            using (TipoComprobanteLogic oTipoComprobanteLogic = new TipoComprobanteLogic())
            {
                oResp = oTipoComprobanteLogic.TipoComprobanteGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<TipoComprobanteViewModel>();
                foreach (TipoComprobanteDTO item in oResp.List)
                {
                    lista.Add(new TipoComprobanteViewModel()
                    {
                        CodigoTipoComprobante = item.CodigoTipoComprobante,
                        CodigoTipoDocumentoEmpresa = item.CodigoTipoDocumentoEmpresa,
                        Serie = item.Serie,
                        Nombre = item.Nombre
                    });
                }
            }

            return lista;

        }

        //********************************************** COMPROBANTE ***********************************************

        //get item comprobante
        public ComprobanteDTO comprobanteByCodeRepo(string DefaultKeyEmpresa, int CodigoComprobante)
        { 
            ComprobanteDTO oComprobanteDTO = new ComprobanteDTO();
            oComprobanteDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
            oComprobanteDTO.CodigoComprobante = CodigoComprobante;

            ReqFilterComprobanteDTO oReq = new ReqFilterComprobanteDTO()
            {
                FilterCase = filterCaseComprobante.ItemCompCabezeraApp,
                Item = oComprobanteDTO,
                User = "Admin",
            };
            RespItemComprobanteDTO oResp = null;
            using (ComprobanteLogic oComprobanteLogic = new ComprobanteLogic())
            {
                oResp = oComprobanteLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oComprobanteDTO = oResp.Item;
            }
            return oComprobanteDTO;
        }


        //list detail - comprobante
        public List<ComprobanteDetalleDTO> detailComprobanteRepo(string DefaultKeyEmpresa, int CodigoComprobante)
        {
            List<ComprobanteDetalleDTO> lista = null;
            ComprobanteDetalleDTO oComprobanteDetalleDTO = new ComprobanteDetalleDTO();
            oComprobanteDetalleDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
            oComprobanteDetalleDTO.CodigoComprobante = CodigoComprobante;

            ReqFilterComprobanteDetalleDTO oReq = new ReqFilterComprobanteDetalleDTO()
            {
                FilterCase = filterCaseComprobanteDetalle.detalleCompApp,
                Item = oComprobanteDetalleDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin",
            };

            RespListComprobanteDetalleDTO oResp = null;
            using (ComprobanteDetalleLogic oComprobanteDetalleLogic = new ComprobanteDetalleLogic())
            {
                oResp = oComprobanteDetalleLogic.ComprobanteDetalleGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<ComprobanteDetalleDTO>();
                lista = oResp.List;
            }
            return lista;
        }

        //generate data comprobante pdf
        public ResponseApi generateDataPDF(string DefaultKeyEmpresa, int codigo, int CodigoUnidadNegocio, int CodigoSede, string baseUrl)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            MembresiaApiRepository repository = new MembresiaApiRepository();

            var config = repository.getConfiguracionRepository(CodigoSede, CodigoUnidadNegocio);
            var vent = comprobanteByCodeRepo(DefaultKeyEmpresa, codigo);
            HeaderItem hearder = new HeaderItem();
            hearder.ruc = config.Ticket_RUC;
            hearder.name = config.Ticket_RazonSocial;
            hearder.phone = config.Ticket_Telefono;
            hearder.cell = config.Ticket_Celular;


            hearder.nro = vent.Correlativo;
            hearder.date = vent.DesFechaEmision;
            hearder.hour = vent.DesHourEmision;

            hearder.FormaPago = vent.DesFormaPago;

            hearder.customer = vent.NombresCliente;
            hearder.dni = vent.RUC_DNI;
            hearder.address = vent.Direccion;

            hearder.Total = vent.Total;
            hearder.SubTotal = vent.SubTotal;
            hearder.Debe = vent.Debe;
            hearder.created = vent.UsuarioCreacion;
            hearder.Frase = config.Frase;
            var detail = detailComprobanteRepo(DefaultKeyEmpresa, codigo);

            List<DetailV> list = new List<DetailV>();
            foreach (ComprobanteDetalleDTO v in detail)
            {
                DetailV prod = new DetailV();
                prod.Cantidad = (int)v.Cantidad;
                prod.Descripcion = v.Descripcion;
                prod.Precio = v.Precio;
                list.Add(prod);
            }

            var generate = repository.generatePDFTicket(hearder, list, baseUrl);

            if (generate.Status == 0)
            {
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = generate.Message1;
                _objResponseModel.Message2 = generate.Message2;
            }
            else
            {
                _objResponseModel.Status = 2;
            }
            return _objResponseModel;
        }

        //send comprobante post
        public static void sendEmailValiateAccountComp(string key, int CodigoSede, int CodigoUnidadNegocio, int Venta, string Email, string baseUrl)
        {
            MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
            PostApiRepository oPostApiRepository = new PostApiRepository();
            //validate config email account
            var account = repositoryMem.getConfiguracionRepository(CodigoSede, CodigoUnidadNegocio);
            if (!String.IsNullOrEmpty(account.EmailHost) && !String.IsNullOrEmpty(account.EmailKey) && !String.IsNullOrEmpty(account.EmailUser) && !String.IsNullOrEmpty(account.EmailPort))
            {
                //generate recibo
                var ticket = oPostApiRepository.generateDataPDF(key, Venta, CodigoUnidadNegocio,CodigoSede, baseUrl);

                //send tiket email
                var send = AspNetHelper.SendEmailOne(account, Email, "Recibo", "", ticket?.Message2);

                //remove ticket
                var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/assets/pdf/");
                var pathFileName = path + ticket.Message2;
                AspNetHelper.removeFile(pathFileName);

            }

        }
        //********************************************** COMPROBANTE ***********************************************
    }
}