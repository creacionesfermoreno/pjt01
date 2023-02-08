
using E_DataLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Transactions;

namespace E_BusinessLayer.Gimnasio
{

    public class VentasLogic : IDisposable
    {
        VentasData oVentasData = null;
        public VentasLogic()
        {
            oVentasData = new VentasData();
        }

        public RespListVentasDTO VentasGetList(ReqFilterVentasDTO oReqFilterVentasDTO)
        {
            RespListVentasDTO oRespListVentasDTO = new RespListVentasDTO();

            oRespListVentasDTO.List = new List<VentasDTO>();
            oRespListVentasDTO.User = oReqFilterVentasDTO.User;
            oRespListVentasDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterVentasDTO.User))
            {
                oRespListVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlSalida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterVentasDTO.Paging == null)
            {
                oRespListVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListVentasDTO.MessageList.Count == 0)
            {

                try
                {
                    if (!oReqFilterVentasDTO.Paging.All && oReqFilterVentasDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterVentasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarControlSalidaPorFechaAnular_Paginacion"]);
                    }

                    List<VentasDTO> VentasDTOList = new List<VentasDTO>();

                    switch (oReqFilterVentasDTO.FilterCase)
                    {

                        case filterCaseVentas.BuscarInformacionDetalleDeVentaPorCodigo:
                            VentasDTOList = oVentasData.BuscarInformacionDetalleDeVentaPorCodigo(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspListarControlSalidaPorFechaAnular_Paginacion:
                            VentasDTOList = oVentasData.uspListarControlSalidaPorFechaAnular_Paginacion(oReqFilterVentasDTO.Item, oReqFilterVentasDTO.Paging);
                            break;
                        case filterCaseVentas.uspListarVentasRapidasAnular_Paginacion:
                            VentasDTOList = oVentasData.uspListarVentasRapidasAnular_Paginacion(oReqFilterVentasDTO.Item, oReqFilterVentasDTO.Paging);
                            break;
                        case filterCaseVentas.uspListarDeudasSuplementosClientes:
                            VentasDTOList = oVentasData.uspListarDeudasSuplementosClientes(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspListarDeudasRopasClientes:
                            VentasDTOList = oVentasData.uspListarDeudasRopasClientes(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspListarCierreMesVentas:
                            VentasDTOList = oVentasData.ListarControlSalidaFacturacionMensual(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas:
                            VentasDTOList = oVentasData.uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspListarEstadistica_VentasDiarios:
                            VentasDTOList = oVentasData.uspListarEstadistica_VentasDiarios(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaVentasPorTiempoMembresia_Ventas:
                            VentasDTOList = oVentasData.uspEstadisticaVentasPorTiempoMembresia_Ventas(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaMatriculadosPorNombrePlan:
                            VentasDTOList = oVentasData.uspEstadisticaMatriculadosPorNombrePlan(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaVentasPorDiaSemana_Ventas:
                            VentasDTOList = oVentasData.uspEstadisticaVentasPorDiaSemana_Ventas(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaVentasPorDia_Ventas:
                            VentasDTOList = oVentasData.uspEstadisticaVentasPorDia_Ventas(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaVentasPorHoras_Ventas:
                            VentasDTOList = oVentasData.uspEstadisticaVentasPorHoras_Ventas(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.uspEstadisticaVentasPorFormaPago_Ventas:
                            VentasDTOList = oVentasData.uspEstadisticaVentasPorFormaPago_Ventas(oReqFilterVentasDTO.Item);
                            break;
                        case filterCaseVentas.ventaDiariaByCodigo:
                            VentasDTOList = oVentasData.VentaDiarioPorCodigo(oReqFilterVentasDTO.Item);
                            break;




                    }

                    oRespListVentasDTO.List = VentasDTOList;
                    oRespListVentasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListVentasDTO.Success = false;
                    oRespListVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListVentasDTO;

        }

        public RespItemVentasDTO VentasGetItem(ReqFilterVentasDTO oReqFilterVentasDTO)
        {
            RespItemVentasDTO oRespItemVentasDTO = new RespItemVentasDTO();

            oRespItemVentasDTO.Success = false;
            oRespItemVentasDTO.Item = null;
            oRespItemVentasDTO.User = oReqFilterVentasDTO.User;
            oRespItemVentasDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterVentasDTO.User))
            {
                oRespItemVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlSalida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemVentasDTO.MessageList.Count == 0)
            {
                VentasDTO oVentasDTO = null;
                try
                {
                    switch (oReqFilterVentasDTO.FilterCase)
                    {
                        case filterCaseVentas.uspValidarNroComprobante:
                            {
                                oVentasDTO = new VentasDTO();
                                oVentasDTO = oVentasData.uspValidarNroComprobante(oReqFilterVentasDTO.Item);
                            }
                            break;
                        case filterCaseVentas.BuscarInfoGeneralVentaPorCodigo:
                            {
                                oVentasDTO = new VentasDTO();
                                oVentasDTO = oVentasData.BuscarInformacionGeneralVentaPorCodigo(oReqFilterVentasDTO.Item);
                            }
                            break;

                        case filterCaseVentas.uspListarControlSalidaPorFechaAnular_NumeroRegistros:
                            {
                                oVentasDTO = new VentasDTO();
                                oVentasDTO = oVentasData.uspListarControlSalidaPorFechaAnular_NumeroRegistros(oReqFilterVentasDTO.Item);
                            }
                            break;
                        case filterCaseVentas.uspListarVentasRapidasAnular_NumeroRegistros:
                            {
                                oVentasDTO = new VentasDTO();
                                oVentasDTO = oVentasData.uspListarVentasRapidasAnular_NumeroRegistros(oReqFilterVentasDTO.Item);
                            }
                            break;
                        default:
                            {
                                oVentasDTO = new VentasDTO();
                            }
                            break;
                    }

                    oRespItemVentasDTO.Item = new VentasDTO();
                    oRespItemVentasDTO.Item = oVentasDTO;
                    oRespItemVentasDTO.Success = true;
                    oRespItemVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemVentasDTO.Success = false;
                    oRespItemVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemVentasDTO;
        }

        public RespVentasDTO ExecuteTransac(ReqVentasDTO oReqVentasDTO)
        {
            RespVentasDTO oRespVentasDTO = new RespVentasDTO();

            oRespVentasDTO.MessageList = new List<Mensaje>();
            oRespVentasDTO.User = oReqVentasDTO.User;

            if (String.IsNullOrEmpty(oReqVentasDTO.User))
            {
                oRespVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlSalida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespVentasDTO.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigoVenta = 0;

                        foreach (VentasDTO item in oReqVentasDTO.List)
                        {
                            switch (item.Operation)
                            {
                                //*****************************************APP************************
                                case Operation.RegisterControlSalidaAPP:
                                   
                                    //update serie
                                    if (item.GenerarSerie)
                                    {
                                        if (!item.TieneFacturacionElectronica)
                                        {
                                            SeriesData oSeriesData = new SeriesData();
                                            SeriesDTO oSeriesDTO = new SeriesDTO();
                                            oSeriesDTO.TipoDocumento = item.CodigoTipoComprobante;
                                            oSeriesDTO.SubTipoDocumento = item.CodigoSubTipoDocumento;
                                            oSeriesDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oSeriesDTO.CodigoSede = item.CodigoSede;
                                            oSeriesDTO.UsuarioCreacion = "Admin";
                                            oSeriesDTO.CodigoInicioSesion = 0;
                                            oSeriesData.ActualizarSerieAumentar(oSeriesDTO);
                                        }

                                    }
                                    PagosContratoData oPagosContratoData_ = new PagosContratoData();
                                    VentasDetalleData oVentasDetalleData_ = new VentasDetalleData();
                                    foreach (VentasDetalleDTO itemDetalle in item.ListaDetalle)
                                    {

                                        if (itemDetalle.Tipo == 1)
                                        {

                                        }
                                        else if (itemDetalle.Tipo == 2 || itemDetalle.Tipo == 7 || itemDetalle.Tipo == 8)
                                        {
                                            //membresia
                                            //register
                                            codigoVenta = oVentasData.RegistrarAPP(item);

                                            //register forma pago
                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();
                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTOMenbre = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTOMenbre.Codigo = 0;
                                                oControlSalidaFormaPagoDTOMenbre.DefaultKeyEmpresa = item.DefaultKeyEmpresa;
                                                oControlSalidaFormaPagoDTOMenbre.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTOMenbre.Monto = item.TotalNeto;
                                                oControlSalidaFormaPagoDTOMenbre.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTOMenbre.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTOMenbre.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoData.RegistrarAPP(oControlSalidaFormaPagoDTOMenbre);
                                            }

                                            //agregar el pago de la membresia
                                            PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
                                            ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                            oPagoMembresiaDTO.DefaultKeyEmpresa = item.DefaultKeyEmpresa;
                                            oPagoMembresiaDTO.Monto = itemDetalle.PrecioUnitario;
                                            oPagoMembresiaDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagoMembresiaDTO.FechaPago = item.FechaVenta;
                                            oPagoMembresiaDTO.FormaPago = item.FormaPago;
                                            oPagoMembresiaDTO.nroTarjeta = item.NroTarjeta;
                                            oPagoMembresiaDTO.CodigoMembresia = itemDetalle.CodigoProducto;
                                            int codigoPagoMem = oPagosContratoData_.RegistrarAPP(oPagoMembresiaDTO);

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoProducto = codigoPagoMem;
                                            oVentasDetalleData_.RegistrarAPP(itemDetalle);

                                        }
                                    }
                                    //*****************************************APP************************
                                    break;

                                case Operation.Create:

                                    if (item.GenerarSerie)
                                    {
                                        if (!item.TieneFacturacionElectronica)
                                        {
                                            SeriesData oSeriesData = new SeriesData();
                                            SeriesDTO oSeriesDTO = new SeriesDTO();
                                            oSeriesDTO.TipoDocumento = item.CodigoTipoComprobante;
                                            oSeriesDTO.SubTipoDocumento = item.CodigoSubTipoDocumento;
                                            oSeriesDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oSeriesDTO.CodigoSede = item.CodigoSede;
                                            oSeriesDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oSeriesDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oSeriesData.ActualizarSerieAumentar(oSeriesDTO);
                                        }

                                    }

                                    VentasDetalleData oVentasDetalleData = new VentasDetalleData();
                                    ProductoData oProductoData = new ProductoData();
                                    SuplementosData oSuplementosData = new SuplementosData();
                                    PagosSuplementosData oPagosSuplementosData = new PagosSuplementosData();
                                    RopasData oRopasData = new RopasData();
                                    PagosRopasData oPagosRopasData = new PagosRopasData();
                                    ComprasDetalleData oComprasDetalleData = new ComprasDetalleData();
                                    ComprasSuplementosData oComprasSuplementosData = new ComprasSuplementosData();
                                    PagosContratoData oPagosContratoData = new PagosContratoData();

                                    foreach (VentasDetalleDTO itemDetalle in item.ListaDetalle)
                                    {
                                        if (itemDetalle.Tipo == 1)
                                        {
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = itemDetalle.Importe;
                                            item.Comentario = "";
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;

                                            oVentasDetalleData.Registrar(itemDetalle);
                                            //producto
                                            ProductoDTO oProductoDTO = new ProductoDTO();
                                            oProductoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oProductoDTO.CodigoSede = item.CodigoSede;
                                            oProductoDTO.CodigoProducto = itemDetalle.CodigoProducto;
                                            oProductoDTO.Cantidad = itemDetalle.Cantidad;
                                            oProductoDTO.PrecioVenta = itemDetalle.PrecioUnitario;
                                            oProductoDTO.flag = 2; //quitar stock
                                            oProductoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oProductoDTO.CodigoInicioSesion = item.CodigoInicioSesion;

                                            //actualizar el stock
                                            oProductoData.ActualizarPrecioVentaCantidad(oProductoDTO);

                                            //actualizar stock de la compra
                                            ComprasDetalleDTO oComprasDetalleDTO = new ComprasDetalleDTO();
                                            oComprasDetalleDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oComprasDetalleDTO.CodigoSede = item.CodigoSede;
                                            oComprasDetalleDTO.CodigoDetalleIngreso = itemDetalle.codigoDetalle_Ingreso;
                                            oComprasDetalleDTO.CantidadSalida = itemDetalle.Cantidad;
                                            oComprasDetalleDTO.Param_Flag = 1; //aumenta
                                            oComprasDetalleDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oComprasDetalleDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oComprasDetalleData.ActualizarCantidadSalida(oComprasDetalleDTO);

                                        }
                                        else if (itemDetalle.Tipo == 2 || itemDetalle.Tipo == 7 || itemDetalle.Tipo == 8)
                                        {
                                            //membresia
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();
                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTOMenbre = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTOMenbre.Codigo = 0;
                                                oControlSalidaFormaPagoDTOMenbre.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTOMenbre.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTOMenbre.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTOMenbre.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTOMenbre.Monto = itemDetalleFormaPago.Monto;
                                                oControlSalidaFormaPagoDTOMenbre.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTOMenbre.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTOMenbre.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTOMenbre.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTOMenbre.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTOMenbre.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTOMenbre.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTOMenbre);
                                            }

                                            //agregar el pago de la membresia
                                            PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
                                            ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                            oPagoMembresiaDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagoMembresiaDTO.CodigoSede = item.CodigoSede;
                                            oPagoMembresiaDTO.CodigoMembresia = itemDetalle.CodigoProducto;
                                            oPagoMembresiaDTO.Monto = itemDetalle.PrecioUnitario;
                                            oPagoMembresiaDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagoMembresiaDTO.FechaPago = item.FechaVenta;
                                            oPagoMembresiaDTO.FormaPago = item.FormaPago;
                                            // oControlSalidaFormaPagoDTO.SubFormaPago
                                            oPagoMembresiaDTO.nroTarjeta = item.NroTarjeta;
                                            oPagoMembresiaDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagoMembresiaDTO.CodigoInicioSesion = item.CodigoInicioSesion;

                                            int codigoPagoMem = oPagosContratoData.Registrar(oPagoMembresiaDTO);

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoProducto = codigoPagoMem;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;

                                            oVentasDetalleData.Registrar(itemDetalle);

                                            //registrando cuotas
                                            if (item.ListaCuotas != null)
                                            {
                                                foreach (ContratoCuotaDTO itemCuotas in item.ListaCuotas)
                                                {
                                                    ContratoCuotaDTO oContratoCuotaDTO = new ContratoCuotaDTO();
                                                    ContratoCuotaData oContratoCuotaData = new ContratoCuotaData();
                                                    oContratoCuotaDTO.CodigoUnidadNegocio = itemCuotas.CodigoUnidadNegocio;
                                                    oContratoCuotaDTO.CodigoSede = itemCuotas.CodigoSede;
                                                    oContratoCuotaDTO.CodigoCuota = itemCuotas.CodigoCuota;
                                                    oContratoCuotaDTO.CodigoMembresia = itemCuotas.CodigoMembresia;
                                                    oContratoCuotaDTO.Monto = itemCuotas.Monto;
                                                    oContratoCuotaDTO.Fecha = itemCuotas.Fecha;
                                                    oContratoCuotaDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                    oContratoCuotaDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                    oContratoCuotaData.Registrar(oContratoCuotaDTO);
                                                }
                                            }

                                        }
                                        else if (itemDetalle.Tipo == 3)
                                        {
                                            //LibreDTO oLibreDTO = new LibreDTO();
                                            //ya no se usa
                                        }
                                        else if (itemDetalle.Tipo == 4)
                                        {
                                            //producto elaborado
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = itemDetalle.Importe;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oVentasDetalleData.Registrar(itemDetalle);
                                        }
                                        else if (itemDetalle.Tipo == 5)
                                        {
                                            //eventos
                                            //ya no se usa
                                        }
                                        else if (itemDetalle.Tipo == 6)
                                        {
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = itemDetalle.Importe;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }
                                            //Suplementos
                                            SuplementosDTO oSuplementosDTO = new SuplementosDTO();
                                            oSuplementosDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oSuplementosDTO.CodigoSede = item.CodigoSede;
                                            oSuplementosDTO.CodigoSuplemento = itemDetalle.CodigoProducto;
                                            oSuplementosDTO.Cantidad = itemDetalle.Cantidad;
                                            oSuplementosDTO.PrecioVenta = itemDetalle.PrecioUnitario;
                                            oSuplementosDTO.flag = 2; //quitar stock
                                            oSuplementosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oSuplementosDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            //actualizar el stock suplementos
                                            oSuplementosData.ActualizarPrecioVentaCantidadSuplementos(oSuplementosDTO);

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            oVentasDetalleData.Registrar(itemDetalle);

                                            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();

                                            oPagosSuplementosDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagosSuplementosDTO.CodigoSede = item.CodigoSede;
                                            oPagosSuplementosDTO.CodigoPago = 0;
                                            oPagosSuplementosDTO.CodigoSalida = codigoVenta;
                                            oPagosSuplementosDTO.Monto = itemDetalle.Acuenta;
                                            oPagosSuplementosDTO.FechaPago = item.FechaVenta;
                                            oPagosSuplementosDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagosSuplementosDTO.FormaPago = item.FormaPago;
                                            oPagosSuplementosDTO.SubFormaPago = item.SubFormaPago;
                                            oPagosSuplementosDTO.NroBoucher = item.NroTarjeta;
                                            oPagosSuplementosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagosSuplementosDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                            oPagosSuplementosDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                            oPagosSuplementosDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagosSuplementosData.Registrar(oPagosSuplementosDTO);

                                        }
                                        else if (itemDetalle.Tipo == 9)
                                        {
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = itemDetalle.Importe;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }
                                            //Ropas
                                            RopasDTO oRopasDTO = new RopasDTO();
                                            oRopasDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oRopasDTO.CodigoSede = item.CodigoSede;
                                            oRopasDTO.CodigoProducto = itemDetalle.CodigoProducto;
                                            oRopasDTO.Cantidad = itemDetalle.Cantidad;
                                            oRopasDTO.PrecioVenta = itemDetalle.PrecioUnitario;
                                            oRopasDTO.flag = 2; //quitar stock
                                            oRopasDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oRopasDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            //actualizar el stock Ropas
                                            oRopasData.ActualizarPrecioVentaCantidadRopas(oRopasDTO);

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oVentasDetalleData.Registrar(itemDetalle);

                                            PagosRopasDTO oPagosRopasDTO = new PagosRopasDTO();
                                            oPagosRopasDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagosRopasDTO.CodigoSede = item.CodigoSede;
                                            oPagosRopasDTO.CodigoPago = 0;
                                            oPagosRopasDTO.CodigoSalida = codigoVenta;
                                            oPagosRopasDTO.Monto = itemDetalle.Acuenta;
                                            oPagosRopasDTO.FechaPago = item.FechaVenta;
                                            oPagosRopasDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagosRopasDTO.FormaPago = item.FormaPago;
                                            oPagosRopasDTO.SubFormaPago = item.SubFormaPago;
                                            oPagosRopasDTO.NroBoucher = item.NroTarjeta;
                                            oPagosRopasDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagosRopasDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                            oPagosRopasDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                            oPagosRopasDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagosRopasData.Registrar(oPagosRopasDTO);

                                        }
                                        else if (itemDetalle.Tipo == 20)
                                        {
                                            codigoVenta = 999999999;
                                            //pago deuda de suplementos
                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = itemDetalle.codigoDetalle_Ingreso;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();
                                            oPagosSuplementosDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagosSuplementosDTO.CodigoSede = item.CodigoSede;
                                            oPagosSuplementosDTO.CodigoPago = 0;
                                            oPagosSuplementosDTO.CodigoSalida = itemDetalle.codigoDetalle_Ingreso;
                                            oPagosSuplementosDTO.Monto = itemDetalle.Acuenta;
                                            oPagosSuplementosDTO.FechaPago = item.FechaVenta;
                                            oPagosSuplementosDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagosSuplementosDTO.FormaPago = item.FormaPago;
                                            oPagosSuplementosDTO.SubFormaPago = item.SubFormaPago;
                                            oPagosSuplementosDTO.NroBoucher = item.NroTarjeta;
                                            oPagosSuplementosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagosSuplementosDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                            oPagosSuplementosDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                            oPagosSuplementosDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagosSuplementosData.Registrar(oPagosSuplementosDTO);
                                        }
                                        else if (itemDetalle.Tipo == 30)
                                        {
                                            codigoVenta = 999999999;
                                            //pago de ropa
                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();
                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = itemDetalle.codigoDetalle_Ingreso;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            PagosRopasDTO oPagosRopasDTO = new PagosRopasDTO();
                                            oPagosRopasDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagosRopasDTO.CodigoSede = item.CodigoSede;
                                            oPagosRopasDTO.CodigoPago = 0;
                                            oPagosRopasDTO.CodigoSalida = itemDetalle.codigoDetalle_Ingreso;
                                            oPagosRopasDTO.Monto = itemDetalle.Acuenta;
                                            oPagosRopasDTO.FechaPago = item.FechaVenta;
                                            oPagosRopasDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagosRopasDTO.FormaPago = item.FormaPago;
                                            oPagosRopasDTO.SubFormaPago = item.SubFormaPago;
                                            oPagosRopasDTO.NroBoucher = item.NroTarjeta;
                                            oPagosRopasDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagosRopasDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                            oPagosRopasDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                            oPagosRopasDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagosRopasData.Registrar(oPagosRopasDTO);

                                        }
                                    }


                                    break;
                                case Operation.CreatePagarFiados:

                                    if (item.GenerarSerie)
                                    {
                                        SeriesData oSeriesData = new SeriesData();
                                        SeriesDTO oSeriesDTO = new SeriesDTO();
                                        oSeriesDTO.TipoDocumento = item.CodigoTipoComprobante;
                                        oSeriesDTO.SubTipoDocumento = item.CodigoSubTipoDocumento;
                                        oSeriesDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        oSeriesDTO.CodigoSede = item.CodigoSede;
                                        oSeriesDTO.UsuarioCreacion = item.UsuarioCreacion;
                                        oSeriesDTO.CodigoInicioSesion = item.CodigoInicioSesion;

                                        oSeriesData.ActualizarSerieAumentar(oSeriesDTO);
                                    }

                                    VentasDetalleData oVentasDetalleData2 = new VentasDetalleData();
                                    ProductoData oProductoData2 = new ProductoData();
                                    SuplementosData oSuplementosData2 = new SuplementosData();
                                    PagosSuplementosData oPagosSuplementosData2 = new PagosSuplementosData();
                                    RopasData oRopasData2 = new RopasData();
                                    PagosRopasData oPagosRopasData2 = new PagosRopasData();
                                    ComprasDetalleData oComprasDetalleData2 = new ComprasDetalleData();
                                    ComprasSuplementosData oComprasSuplementosData2 = new ComprasSuplementosData();
                                    PedidosData oPedidosData2 = new PedidosData();
                                    PedidosDTO oPedidosDTO2 = new PedidosDTO();

                                    foreach (VentasDetalleDTO itemDetalle in item.ListaDetalle)
                                    {
                                        //itemDetalle.Acuenta == itemDetalle.Importe
                                        //&& itemDetalle.Debe == itemDetalle.Importe
                                        if (itemDetalle.Acuenta > 0)
                                        {
                                            if (itemDetalle.Tipo == 1)
                                            {
                                                //Registrar cabeza
                                                item.SubTotal = 0;
                                                item.IGV = 0;
                                                item.TotalNeto = itemDetalle.Acuenta;//itemDetalle.Importe;
                                                codigoVenta = oVentasData.Registrar(item);

                                                ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                                foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                                {
                                                    ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                    oControlSalidaFormaPagoDTO.Codigo = 0;
                                                    oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                    oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                    oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                    oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                    oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                    oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                    oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                    oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                    oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                    oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                    oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                    oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                    oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                                }

                                                //registrar detalle
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                itemDetalle.CodigoSalida = codigoVenta;
                                                itemDetalle.CodigoSalidaDetalle = 0;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.FechaCreacion = item.FechaVenta;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                                itemDetalle.Descripcion = itemDetalle.Descripcion + " - pago fiado";
                                                itemDetalle.Cantidad = 0;
                                                itemDetalle.Importe = itemDetalle.Acuenta;
                                                oVentasDetalleData2.Registrar(itemDetalle);

                                                oPedidosDTO2.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPedidosDTO2.CodigoSede = item.CodigoSede;
                                                oPedidosDTO2.Codigo = itemDetalle.CodigoPedido;
                                                oPedidosDTO2.Debe = itemDetalle.Acuenta;
                                                oPedidosData2.Actualizar(oPedidosDTO2);

                                            }
                                            else if (itemDetalle.Tipo == 4)
                                            {
                                                //producto elaborado
                                                //Registrar cabeza
                                                item.SubTotal = 0;
                                                item.IGV = 0;
                                                item.TotalNeto = itemDetalle.Acuenta;//itemDetalle.Importe;
                                                codigoVenta = oVentasData.Registrar(item);

                                                ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                                foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                                {
                                                    ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                    oControlSalidaFormaPagoDTO.Codigo = 0;
                                                    oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                    oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                    oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                    oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                    oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                    oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                    oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                    oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                    oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                    oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                    oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                    oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                    oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                                }

                                                //registrar detalle
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoSalida = codigoVenta;
                                                itemDetalle.CodigoSalidaDetalle = 0;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.FechaCreacion = item.FechaVenta;
                                                itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                                itemDetalle.Descripcion = itemDetalle.Descripcion + " - pago fiado";
                                                itemDetalle.Cantidad = 0;
                                                itemDetalle.Importe = itemDetalle.Acuenta;
                                                oVentasDetalleData2.Registrar(itemDetalle);

                                                oPedidosDTO2.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPedidosDTO2.CodigoSede = item.CodigoSede;
                                                oPedidosDTO2.Codigo = itemDetalle.CodigoPedido;
                                                oPedidosDTO2.Debe = itemDetalle.Acuenta;
                                                oPedidosData2.Actualizar(oPedidosDTO2);
                                            }
                                            else if (itemDetalle.Tipo == 6)
                                            {
                                                //Registrar cabeza
                                                item.SubTotal = 0;
                                                item.IGV = 0;
                                                item.TotalNeto = itemDetalle.Acuenta;//itemDetalle.Importe;
                                                codigoVenta = oVentasData.Registrar(item);

                                                ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                                foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                                {
                                                    ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                    oControlSalidaFormaPagoDTO.Codigo = 0;
                                                    oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                    oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                    oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                    oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                    oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                    oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                    oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                    oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                    oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                    oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                    oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                    oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                    oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                                }

                                                //registrar detalle
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                itemDetalle.CodigoSalida = codigoVenta;
                                                itemDetalle.CodigoSalidaDetalle = 0;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.FechaCreacion = item.FechaVenta;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                                itemDetalle.Descripcion = itemDetalle.Descripcion + " - pago fiado";
                                                itemDetalle.Cantidad = 0;
                                                oVentasDetalleData2.Registrar(itemDetalle);

                                                PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();

                                                oPagosSuplementosDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPagosSuplementosDTO.CodigoSede = item.CodigoSede;
                                                oPagosSuplementosDTO.CodigoPago = 0;
                                                oPagosSuplementosDTO.CodigoSalida = codigoVenta;
                                                oPagosSuplementosDTO.Monto = itemDetalle.Acuenta;
                                                oPagosSuplementosDTO.FechaPago = item.FechaVenta;
                                                oPagosSuplementosDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                                oPagosSuplementosDTO.FormaPago = item.FormaPago;
                                                oPagosSuplementosDTO.SubFormaPago = item.SubFormaPago;
                                                oPagosSuplementosDTO.NroBoucher = item.NroTarjeta;
                                                oPagosSuplementosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oPagosSuplementosDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                                oPagosSuplementosDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                                oPagosSuplementosDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oPagosSuplementosData2.Registrar(oPagosSuplementosDTO);

                                                oPedidosDTO2.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPedidosDTO2.CodigoSede = item.CodigoSede;
                                                oPedidosDTO2.Codigo = itemDetalle.CodigoPedido;
                                                oPedidosDTO2.Debe = itemDetalle.Acuenta;
                                                oPedidosData2.Actualizar(oPedidosDTO2);
                                            }
                                            else if (itemDetalle.Tipo == 9)
                                            {
                                                //Registrar cabeza
                                                item.SubTotal = 0;
                                                item.IGV = 0;
                                                item.TotalNeto = itemDetalle.Acuenta;//itemDetalle.Importe;
                                                codigoVenta = oVentasData.Registrar(item);

                                                ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                                foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                                {
                                                    ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                    oControlSalidaFormaPagoDTO.Codigo = 0;
                                                    oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                    oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                    oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                    oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                    oControlSalidaFormaPagoDTO.Monto = itemDetalle.Acuenta;
                                                    oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                    oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                    oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                    oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                    oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                    oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                    oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                    oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                                }

                                                //registrar detalle
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                itemDetalle.CodigoSalida = codigoVenta;
                                                itemDetalle.CodigoSalidaDetalle = 0;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.FechaCreacion = item.FechaVenta;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                                itemDetalle.Descripcion = itemDetalle.Descripcion + " - pago fiado";
                                                itemDetalle.Cantidad = 0;
                                                itemDetalle.Importe = itemDetalle.Acuenta;
                                                oVentasDetalleData2.Registrar(itemDetalle);

                                                PagosRopasDTO oPagosRopasDTO = new PagosRopasDTO();
                                                oPagosRopasDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPagosRopasDTO.CodigoSede = item.CodigoSede;
                                                oPagosRopasDTO.CodigoPago = 0;
                                                oPagosRopasDTO.CodigoSalida = codigoVenta;
                                                oPagosRopasDTO.Monto = itemDetalle.Acuenta;
                                                oPagosRopasDTO.FechaPago = item.FechaVenta;
                                                oPagosRopasDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                                oPagosRopasDTO.FormaPago = item.FormaPago;
                                                oPagosRopasDTO.SubFormaPago = item.SubFormaPago;
                                                oPagosRopasDTO.NroBoucher = item.NroTarjeta;
                                                oPagosRopasDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oPagosRopasDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                                oPagosRopasDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                                oPagosRopasDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oPagosRopasData2.Registrar(oPagosRopasDTO);

                                                oPedidosDTO2.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPedidosDTO2.CodigoSede = item.CodigoSede;
                                                oPedidosDTO2.Codigo = itemDetalle.CodigoPedido;
                                                oPedidosDTO2.Debe = itemDetalle.Acuenta;
                                                oPedidosData2.Actualizar(oPedidosDTO2);
                                            }
                                        }

                                    }

                                    break;
                                case Operation.CreateFiados:

                                    foreach (VentasDetalleDTO itemDetalle in item.ListaDetalle)
                                    {
                                        if (itemDetalle.Tipo == 1)
                                        {
                                            PedidosData oPedidosData = new PedidosData();
                                            PedidosDTO oPedidosDTO = new PedidosDTO();
                                            oPedidosDTO.Codigo = 0;
                                            oPedidosDTO.CodigoDetalle = 0;
                                            oPedidosDTO.CodigoSocio = item.CodigoSocio;
                                            oPedidosDTO.CodigoProducto = itemDetalle.CodigoProducto;
                                            oPedidosDTO.Tipo = itemDetalle.Tipo;
                                            oPedidosDTO.Descripcion = itemDetalle.Descripcion;
                                            oPedidosDTO.Cantidad = itemDetalle.Cantidad;
                                            oPedidosDTO.PrecioUnitario = itemDetalle.PrecioUnitario;
                                            oPedidosDTO.Importe = (itemDetalle.Cantidad * itemDetalle.PrecioUnitario);
                                            oPedidosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPedidosDTO.FechaCreacion = item.FechaVenta;
                                            oPedidosDTO.CodigoSede = item.CodigoSede;
                                            oPedidosDTO.TipoCodigoLlavePersona = 0;
                                            oPedidosDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;

                                            oPedidosData.Registrar(oPedidosDTO);

                                        }

                                        VentasDetalleData oVentasDetalleData3 = new VentasDetalleData();
                                        ProductoData oProductoData3 = new ProductoData();
                                        SuplementosData oSuplementosData3 = new SuplementosData();
                                        PagosSuplementosData oPagosSuplementosData3 = new PagosSuplementosData();
                                        RopasData oRopasData3 = new RopasData();
                                        PagosRopasData oPagosRopasData3 = new PagosRopasData();
                                        ComprasDetalleData oComprasDetalleData3 = new ComprasDetalleData();
                                        ComprasSuplementosData oComprasSuplementosData3 = new ComprasSuplementosData();
                                        PedidosData oPedidosData3 = new PedidosData();
                                        PedidosDTO oPedidosDTO3 = new PedidosDTO();

                                        if (itemDetalle.Tipo == 1)
                                        {
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = 0;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = 0;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            itemDetalle.Descripcion = itemDetalle.Descripcion + " - fiado";
                                            itemDetalle.Importe = 0;
                                            oVentasDetalleData3.Registrar(itemDetalle);

                                        }
                                        else if (itemDetalle.Tipo == 4)
                                        {
                                            //producto elaborado
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = 0;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = 0;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            itemDetalle.Descripcion = itemDetalle.Descripcion + " - fiado";
                                            itemDetalle.Importe = 0;
                                            oVentasDetalleData3.Registrar(itemDetalle);

                                        }
                                        else if (itemDetalle.Tipo == 6)
                                        {
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = itemDetalle.Acuenta;//itemDetalle.Importe;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = 0;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            itemDetalle.Descripcion = itemDetalle.Descripcion + " - fiado";
                                            itemDetalle.Importe = 0;
                                            oVentasDetalleData3.Registrar(itemDetalle);

                                            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();

                                            oPagosSuplementosDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagosSuplementosDTO.CodigoSede = item.CodigoSede;
                                            oPagosSuplementosDTO.CodigoPago = 0;
                                            oPagosSuplementosDTO.CodigoSalida = codigoVenta;
                                            oPagosSuplementosDTO.Monto = 0;
                                            oPagosSuplementosDTO.FechaPago = item.FechaVenta;
                                            oPagosSuplementosDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagosSuplementosDTO.FormaPago = item.FormaPago;
                                            oPagosSuplementosDTO.SubFormaPago = item.SubFormaPago;
                                            oPagosSuplementosDTO.NroBoucher = item.NroTarjeta;
                                            oPagosSuplementosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagosSuplementosDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                            oPagosSuplementosDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                            oPagosSuplementosDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagosSuplementosData3.Registrar(oPagosSuplementosDTO);

                                        }
                                        else if (itemDetalle.Tipo == 9)
                                        {
                                            //Registrar cabeza
                                            item.SubTotal = 0;
                                            item.IGV = 0;
                                            item.TotalNeto = itemDetalle.Acuenta;//itemDetalle.Importe;
                                            codigoVenta = oVentasData.Registrar(item);

                                            ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();

                                            foreach (ControlSalidaFormaPagoDTO itemDetalleFormaPago in item.ListaFormaPago)
                                            {
                                                ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                                oControlSalidaFormaPagoDTO.Codigo = 0;
                                                oControlSalidaFormaPagoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oControlSalidaFormaPagoDTO.CodigoSede = item.CodigoSede;
                                                oControlSalidaFormaPagoDTO.CodigoIngreso = codigoVenta;
                                                oControlSalidaFormaPagoDTO.TipoMoneda = itemDetalleFormaPago.TipoMoneda;
                                                oControlSalidaFormaPagoDTO.Monto = 0;
                                                oControlSalidaFormaPagoDTO.TipoCambio = itemDetalleFormaPago.TipoCambio;
                                                oControlSalidaFormaPagoDTO.FormaPago = itemDetalleFormaPago.FormaPago;
                                                oControlSalidaFormaPagoDTO.SubFormaPago = itemDetalleFormaPago.SubFormaPago;
                                                oControlSalidaFormaPagoDTO.NroBoucher = itemDetalleFormaPago.NroBoucher;
                                                oControlSalidaFormaPagoDTO.UrlBoucher = itemDetalleFormaPago.UrlBoucher;
                                                oControlSalidaFormaPagoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oControlSalidaFormaPagoDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                                oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTO);
                                            }

                                            //registrar detalle
                                            itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemDetalle.CodigoSalida = codigoVenta;
                                            itemDetalle.CodigoSalidaDetalle = 0;
                                            itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                            itemDetalle.AsesorComercial = item.UsuarioCreacion;
                                            itemDetalle.FechaCreacion = item.FechaVenta;
                                            itemDetalle.CodigoSede = item.CodigoSede;
                                            itemDetalle.CodigoInicioSesion = item.CodigoInicioSesion;
                                            itemDetalle.Descripcion = itemDetalle.Descripcion + " - fiado";
                                            itemDetalle.Importe = 0;
                                            oVentasDetalleData3.Registrar(itemDetalle);

                                            PagosRopasDTO oPagosRopasDTO = new PagosRopasDTO();
                                            oPagosRopasDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagosRopasDTO.CodigoSede = item.CodigoSede;
                                            oPagosRopasDTO.CodigoPago = 0;
                                            oPagosRopasDTO.CodigoSalida = codigoVenta;
                                            oPagosRopasDTO.Monto = 0;
                                            oPagosRopasDTO.FechaPago = item.FechaVenta;
                                            oPagosRopasDTO.NroComprobante = (item.CodigoTipoComprobante == 1 ? "Fac-" : (item.CodigoTipoComprobante == 2 ? "Bol-" : "Rec-")) + item.NroComprobante;
                                            oPagosRopasDTO.FormaPago = item.FormaPago;
                                            oPagosRopasDTO.SubFormaPago = item.SubFormaPago;
                                            oPagosRopasDTO.NroBoucher = item.NroTarjeta;
                                            oPagosRopasDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagosRopasDTO.CodigoTipoComprobante = item.CodigoTipoComprobante;
                                            oPagosRopasDTO.CodigoSubTipoComprobante = item.CodigoSubTipoDocumento;
                                            oPagosRopasDTO.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagosRopasData3.Registrar(oPagosRopasDTO);

                                        }

                                    }

                                    // }
                                    break;
                                case Operation.Delete:

                                    codigoVenta = 999999999;
                                    VentasDetalleData oControlDetalleSalidaDataD = new VentasDetalleData();
                                    VentasDetalleDTO oControlDetalleSalidaDTOD = new VentasDetalleDTO();
                                    List<VentasDetalleDTO> listaDetalleD = new List<VentasDetalleDTO>();
                                    ProductoData oProductoDataD = new ProductoData();
                                    PagosContratoData oPagoMembresiaDataD = new PagosContratoData();
                                    ComprasDetalleData DetalleCIngresoDataD = new ComprasDetalleData();

                                    oControlDetalleSalidaDTOD.CodigoSalida = item.CodigoIngreso;
                                    oControlDetalleSalidaDTOD.CodigoSede = item.CodigoSede;
                                    oControlDetalleSalidaDTOD.CodigoUnidadNegocio = item.CodigoUnidadNegocio;

                                    listaDetalleD = oControlDetalleSalidaDataD.Listar(oControlDetalleSalidaDTOD);

                                    foreach (VentasDetalleDTO itemD in listaDetalleD)
                                    {
                                        if (itemD.Tipo == 1) //producto
                                        {
                                            ////producto
                                            ProductoDTO oProductoDTO = new ProductoDTO();
                                            oProductoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oProductoDTO.CodigoSede = itemD.CodigoSede;
                                            oProductoDTO.CodigoProducto = itemD.CodigoProducto;
                                            oProductoDTO.Cantidad = itemD.Cantidad;
                                            oProductoDTO.PrecioVenta = itemD.PrecioUnitario;
                                            oProductoDTO.flag = 1; //aumentar stock
                                            oProductoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                            oProductoDTO.CodigoInicioSesion = item.CodigoInicioSesion;

                                            oProductoDataD.ActualizarPrecioVentaCantidad(oProductoDTO);

                                            ///-------------------------------------------------------
                                            //obtener el nombre del producto por el nombre
                                            oProductoDTO.Descripcion = itemD.Descripcion; //.Substring(0, 4)
                                            oProductoDTO.codigoSocio = 99999;
                                            //oProductoDTO.CodigoSede = itemD.CodigoSede;
                                            List<ProductoDTO> listaProductos = new List<ProductoDTO>();
                                            listaProductos = oProductoDataD.ListarPorNombre(oProductoDTO);

                                            foreach (ProductoDTO itemProductos in listaProductos)
                                            {

                                                if (itemD.CodigoProducto == itemProductos.CodigoProducto)
                                                {
                                                    ////actualizar stock de la compra
                                                    ComprasDetalleDTO oDetalleCIngresoDTOD = new ComprasDetalleDTO();
                                                    oDetalleCIngresoDTOD.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                    oDetalleCIngresoDTOD.CodigoSede = itemD.CodigoSede;
                                                    oDetalleCIngresoDTOD.CodigoDetalleIngreso = itemProductos.CodigoDetalle;
                                                    oDetalleCIngresoDTOD.CantidadSalida = itemD.Cantidad;  //disminuir
                                                    oDetalleCIngresoDTOD.Param_Flag = 2; //disminuye
                                                    oDetalleCIngresoDTOD.UsuarioCreacion = item.UsuarioCreacion;
                                                    oDetalleCIngresoDTOD.CodigoInicioSesion = item.CodigoInicioSesion;
                                                    DetalleCIngresoDataD.ActualizarCantidadSalida(oDetalleCIngresoDTOD);
                                                }

                                            }

                                        }
                                        else if (itemD.Tipo == 2 || itemD.Tipo == 7 || itemD.Tipo == 8) //membresia
                                        {
                                            PagosContratoDTO oPagoMembresiaDTOD = new PagosContratoDTO();
                                            oPagoMembresiaDTOD.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPagoMembresiaDTOD.CodigoPagoMembresia = itemD.CodigoProducto;
                                            oPagoMembresiaDTOD.CodigoMembresia = 0;
                                            oPagoMembresiaDTOD.CodigoSede = item.CodigoSede;
                                            oPagoMembresiaDTOD.UsuarioCreacion = item.UsuarioCreacion;
                                            oPagoMembresiaDTOD.CodigoInicioSesion = item.CodigoInicioSesion;
                                            oPagoMembresiaDataD.ActualizarEstado(oPagoMembresiaDTOD);

                                        }
                                        else if (itemD.Tipo == 3) //Libre
                                        {

                                        }

                                    }

                                    oVentasData.ActualizarEstado(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespVentasDTO.Success = true;
                        oRespVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigoVenta,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespVentasDTO.Success = false;
                        oRespVentasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespVentasDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
