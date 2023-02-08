using AppsfitWebApi.Controllers;
using AppsfitWebApi.Helpers;
using AppsfitWebApi.Models;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace AppsfitWebApi.Repository
{
    public class MembresiaApiRepository
    {

        public int GuardarMembresiaContratoRepository(MembresiaAPI oMembresiaAPI)
        {

            int codigoMembresia = 0;
            List<ContratoDTO> list = new List<ContratoDTO>();

            list.Add(new ContratoDTO()
            {
                DefaultKeyEmpresa = oMembresiaAPI.DefaultKeyEmpresa,
                CodigoSocio = oMembresiaAPI.CodigoSocio,
                CodigoMenbresia = 0,
                CodigoPaquete = oMembresiaAPI.CodigoPaquete,
                FechaInicio = oMembresiaAPI.FechaFin,
                FechaFin = oMembresiaAPI.FechaInicio,
                Costo = oMembresiaAPI.Costo,
                NroIngreso = oMembresiaAPI.NroSessiones,
                FrezenDisponibles = oMembresiaAPI.FrezenDisponibles,
                Observacion = oMembresiaAPI.Observacion,
                CodigoMebresiaOrigen = oMembresiaAPI.CodigoMebresiaOrigen,
                TipoIngreso = oMembresiaAPI.TipoIngreso,
                Operation = Operation.RegisterContratoApi,
            });
            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = list,
                User = "app",
            };
            RespContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                codigoMembresia = oResp.MessageList[0].Codigo;
            }
            return codigoMembresia;
        }


        public ResponseApi PayMembresiaRepository(VentasDTO item, int CodigoSede, int CodigoUnidadNegocio, MembresiaAPI membresia)
        {
            ResponseApi resp = new ResponseApi();
            try
            {
                List<VentasDTO> lista = new List<VentasDTO>();
                VentasDTO oVentasDTO = new VentasDTO();


                //oConfiguracionDTO
                ConfiguracionDTO oConfiguracionDTO = getConfiguracionRepository(CodigoSede, CodigoUnidadNegocio);
                //get Serie
                string NroComprobante = getSerieReceiptRepository(CodigoSede, CodigoUnidadNegocio);


                //DETALLE SALIDA
                List<VentasDetalleDTO> Detalle = new List<VentasDetalleDTO>();
                Detalle.Add(new VentasDetalleDTO() { Tipo = 2, CodigoProducto = membresia.CodigoMembresia, PrecioUnitario = membresia.Costo, Importe = membresia.Costo, Descripcion = membresia.Descripcion, DefaultKeyEmpresa = item.DefaultKeyEmpresa });

                int FormaPago = 5;

                //FORMA DE PAGO
                List<ControlSalidaFormaPagoDTO> FPDetalle = new List<ControlSalidaFormaPagoDTO>();
                FPDetalle.Add(new ControlSalidaFormaPagoDTO()
                {
                    Monto = item.TotalNeto,
                    FormaPago = FormaPago,
                    NroBoucher = item.NroBoucher,
                    UrlBoucher = ""
                });

                //venta
                oVentasDTO.DefaultKeyEmpresa = item.DefaultKeyEmpresa;
                oVentasDTO.CodigoSocio = item.CodigoSocio;
                oVentasDTO.RazonSocial_Sr = item.RazonSocial_Sr;
                oVentasDTO.RUC_DNI = item.RUC_DNI;
                oVentasDTO.Direccion = item.Direccion;
                oVentasDTO.NroComprobante = NroComprobante;
                oVentasDTO.TotalNeto = item.TotalNeto;
                oVentasDTO.Comentario = item.Comentario;
                oVentasDTO.NroTarjeta = item.NroTarjeta;
                oVentasDTO.Operation = Operation.RegisterControlSalidaAPP;
                //other
                oVentasDTO.TieneFacturacionElectronica = false;
                oVentasDTO.GenerarSerie = oConfiguracionDTO.GenerarSerie;
                oVentasDTO.CodigoSede = CodigoSede;
                oVentasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
                oVentasDTO.ListaDetalle = Detalle;
                oVentasDTO.ListaFormaPago = FPDetalle;
                oVentasDTO.FormaPago = FormaPago;
                oVentasDTO.CodigoTipoComprobante = 3;
                oVentasDTO.CodigoSubTipoDocumento = 3;
                oVentasDTO.FechaVenta = DateTime.Now;
                lista.Add(oVentasDTO);

                ReqVentasDTO oReq = new ReqVentasDTO()
                {
                    User = "Admin",
                    List = lista
                };
                RespVentasDTO oResp = null;
                using (VentasLogic oControlSalidaLogic = new VentasLogic())
                {
                    oResp = oControlSalidaLogic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    resp.Success = true;
                    resp.Message1 = "" + oResp.MessageList[0].Codigo;
                }
                else { resp.Success = false; }

            }

            catch (Exception) { resp.Success = false; }

            return resp;
        }


        //search configurarions
        public ConfiguracionDTO getConfiguracionRepository(int CodigoSede, int CodigoUnidadNegocio)
        {
            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.Codigo = CodigoSede;
            oConfiguracionDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                Item = oConfiguracionDTO,
                User = "Admin",
                FilterCase = filterCaseConfiguracion.BuscarPorCodigo
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


        //generate Ticket
        public ResponseApi generatePDF(int codigo, int CodigoUnidadNegocio, int CodigoSede,string baseUrl)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            var config = getConfiguracionRepository(CodigoSede, CodigoUnidadNegocio);
            var vent = BuscarVentaPorCodigo(codigo, CodigoUnidadNegocio, CodigoSede);
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
            var detail = DetalleDeVentaPorCodigo(codigo, CodigoUnidadNegocio, CodigoSede);

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
            var generate = generatePDFTicket(hearder, list, baseUrl);

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

        public ResponseApi generatePDFTicket(HeaderItem item, List<DetailV> list, string baseUrl)
        {
            ResponseApi _objResponseModel = new ResponseApi();
            var MyTempPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/assets/pdf/");
            if (!Directory.Exists(MyTempPath))
            {
                Directory.CreateDirectory(MyTempPath);
            }

            Random random = new Random();
            var OutputPath = random.Next() + ".pdf";
            //var Request = HttpContext.Current.Request;
            //string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            try
            {
                Document document = new Document(new Rectangle(216, 600), 5f, 5f, 5f, 5f);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(MyTempPath, OutputPath), FileMode.Create));
                document.Open();

                //font custom
                Font bold = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);
                Font normal = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);
                Font trasparent = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.GRAY);

                var gym = ITextApiHelper.ParagraphCustom(item.name, bold, 1);
                document.Add(gym);

                PdfPTable table = new PdfPTable(1);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(ITextApiHelper.cellCustom("RUC : " + item.ruc, bold, 1));
                table.AddCell(ITextApiHelper.cellCustom("CELULAR : " + item.cell, bold, 1));
                table.AddCell(ITextApiHelper.cellCustom("TELEFONO : " + item.phone, bold, 1));
                document.Add(table);

                Chunk linebreak = new Chunk(new DottedLineSeparator());
                document.Add(linebreak);

                PdfPTable table2 = new PdfPTable(1);
                table2.DefaultCell.Border = Rectangle.NO_BORDER;
                table2.HorizontalAlignment = Element.ALIGN_CENTER;

                table2.AddCell(ITextApiHelper.cellCustom("CLIENTE : " + item.customer, bold, 1));
                table2.AddCell(ITextApiHelper.cellCustom("RUC/DNI/CE : " + item.dni, bold, 1));
                table2.AddCell(ITextApiHelper.cellCustom("DIRECCION : " + item.address, bold, 1));
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

                table3.AddCell(ITextApiHelper.cellCustom("FECHA : " + item.date, normal, 1));
                table3.AddCell(ITextApiHelper.cellCustom("HORA :  " + item.hour, normal, 1));
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


                table4.AddCell(ITextApiHelper.cellCustom("Cant.", bold, 0, 0, true, 6));
                table4.AddCell(ITextApiHelper.cellCustom("Descripción", bold, 0, 0, true, 6));
                table4.AddCell(ITextApiHelper.cellCustom("Importe", bold, 2, 0, true, 6));
                foreach (DetailV det in list)
                {
                    table4.AddCell(ITextApiHelper.cellCustom("" + det.Cantidad, normal, 0, 0, true, 6));
                    table4.AddCell(ITextApiHelper.cellCustom("" + det.Descripcion, normal, 0, 0, true, 6));
                    table4.AddCell(ITextApiHelper.cellCustom("" + det.Precio, normal, 2, 0, true, 3));
                }

                table4.AddCell(ITextApiHelper.cellCustom(item.FormaPago, bold, 2, 2, false, 3));
                table4.AddCell(ITextApiHelper.cellCustom("" + item.SubTotal, bold, 2, 0, false, 3));

                table4.AddCell(ITextApiHelper.cellCustom("TOTAL", bold, 2, 2, false, 3));
                table4.AddCell(ITextApiHelper.cellCustom("" + item.Total, bold, 2, 0, false, 3));

                table4.AddCell(ITextApiHelper.cellCustom("A CUENTA", bold, 2, 2, false, 3));
                table4.AddCell(ITextApiHelper.cellCustom("" + item.SubTotal, bold, 2, 0, false, 3));

                table4.AddCell(ITextApiHelper.cellCustom("DEBE", bold, 2, 2, false, 3));
                table4.AddCell(ITextApiHelper.cellCustom("" + item.Debe, bold, 2, 0, false, 3));
                document.Add(table4);

                var user = ITextApiHelper.ParagraphCustom("Responsable : " + item.created, normal, 1);
                user.SpacingBefore = 20;
                document.Add(user);


                var txtEnd = ITextApiHelper.ParagraphCustom(AspNetHelper.HTMLToText(item.Frase), bold, 1);
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



        //send comprobante venta app
        public static void sendEmailValiateAccount (int CodigoSede, int CodigoUnidadNegocio,int Venta, string Email, string baseUrl)
        {
            MembresiaApiRepository repositoryMem = new MembresiaApiRepository();
            //validate config email account
            var account = repositoryMem.getConfiguracionRepository(CodigoSede, CodigoUnidadNegocio);
            if (!String.IsNullOrEmpty(account.EmailHost) && !String.IsNullOrEmpty(account.EmailKey) && !String.IsNullOrEmpty(account.EmailUser) && !String.IsNullOrEmpty(account.EmailPort))
            {
                //generate recibo
                var ticket = repositoryMem.generatePDF(Venta, CodigoUnidadNegocio, CodigoSede, baseUrl);

                //send tiket email
                var send = AspNetHelper.SendEmailOne(account, Email, "Recibo", "", ticket?.Message2);

                //remove ticket
                var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/assets/pdf/");
                var pathFileName = path + ticket.Message2;
                AspNetHelper.removeFile(pathFileName);

            }

        }

    //***********************************************   VENTAS *******************************************
    //venta by codigo
    public VentasDTO BuscarVentaPorCodigo(int codigo, int CodigoUnidadNegocio ,int CodigoSede)
        {
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoVenta = codigo;
            oVentasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = CodigoSede;
            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.BuscarInfoGeneralVentaPorCodigo,
                Item = oVentasDTO,
                User = "Admin",
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


        //venta detail by venta
        public List<VentasDTO> DetalleDeVentaPorCodigo(int CodigoVenta, int CodigoUnidadNegocio, int CodigoSede)
        {
            List<VentasDTO> lista = null;
            VentasDTO oVentasDTO = new VentasDTO();
            oVentasDTO.CodigoVenta = CodigoVenta;
            oVentasDTO.CodigoUnidadNegocio =CodigoUnidadNegocio;
            oVentasDTO.CodigoSede = CodigoSede;
            ReqFilterVentasDTO oReq = new ReqFilterVentasDTO()
            {
                FilterCase = filterCaseVentas.BuscarInformacionDetalleDeVentaPorCodigo,
                Item = oVentasDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin",
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
        //***********************************************   VENTAS *******************************************


        //get serieBoleta
        public string getSerieReceiptRepository(int CodigoSede, int CodigoUnidadNegocio)
        {
            string nro = string.Empty;

            SeriesDTO oSeriesDTO = new SeriesDTO();
            oSeriesDTO.flag = 3;
            oSeriesDTO.subFlag = 3;
            oSeriesDTO.longitudSerie = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["longitudSerie"]);
            oSeriesDTO.CodigoSede = CodigoSede;
            oSeriesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio; ;
            ReqFilterSeriesDTO oReq = new ReqFilterSeriesDTO()
            {
                FilterCase = filterCaseSeries.BuscarGenerarCorrelativo,
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
                nro = oResp.Item.NroCorrelativoActual;
            }

            return nro;
        }

    }
}