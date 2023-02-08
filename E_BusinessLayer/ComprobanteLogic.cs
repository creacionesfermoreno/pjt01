using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;

namespace E_BusinessLayer
{
    public class ComprobanteLogic : IDisposable
    {
        ComprobanteData oComprobanteData = null;
        public ComprobanteLogic()
        {
            oComprobanteData = new ComprobanteData();
        }


        public RespListComprobanteDTO ComprobanteGetList(ReqFilterComprobanteDTO oReqFilter)
        {
            RespListComprobanteDTO oRespList = new RespListComprobanteDTO();

            oRespList.List = new List<ComprobanteDTO>();
            oRespList.User = oReqFilter.User;
            oRespList.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilter.Paging == null)
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oRespList.MessageList.Count == 0)
            {
                try
                {

                    if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                    {
                        oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ComprobanteDTO> CategoriaDTOList = new List<ComprobanteDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseComprobante.ecommerce_uspListarComprobante:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }

                            uint NumeroRegistros = 0;
                            CategoriaDTOList = oComprobanteData.ecommerce_uspListarComprobante(oReqFilter.Item, oReqFilter.Paging, out NumeroRegistros);

                            oRespList.Paging = new Paging();
                            oRespList.Paging.TotalRecord = NumeroRegistros;
                            oRespList.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            //oRespList.Paging.PageRecords
                            //oRespList.Paging.PageNumber

                            break;
                        case filterCaseComprobante.ecommerce_uspListarComprobanteParaAnular:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oComprobanteData.ecommerce_uspListarComprobanteParaAnular(oReqFilter.Item, oReqFilter.Paging);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oComprobanteData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
                            }
                            break;
                    }

                    oRespList.List = CategoriaDTOList;
                    oRespList.Success = true;

                }
                catch (Exception ex)
                {
                    oRespList.Success = false;
                    oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespList;

        }

        public RespComprobanteDTO ExecuteTransac(ReqComprobanteDTO oReq)
        {
            RespComprobanteDTO oResp = new RespComprobanteDTO();

            oResp.MessageList = new List<Mensaje>();
            oResp.User = oReq.User;

            if (String.IsNullOrEmpty(oReq.User))
            {
                oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Usuario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oResp.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoOutput = 0;
                        foreach (ComprobanteDTO item in oReq.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:

                                    if (item.NroIdentificacion == string.Empty || item.NroIdentificacion == "0")
                                    {
                                        item.CodigoCliente = 1;
                                    }
                                    else
                                    {
                                        int CodigoCliente_Buscador = oComprobanteData.ecommerce_uspValidar_Registrar_Clientes(item);
                                        item.CodigoCliente = CodigoCliente_Buscador;                                       
                                        //SI EL DNI EXISTE, OBTENER EL CODIGO DE CLIENTE
                                        //SI NO EXISTE EL DNI CREAR EL CLIENTE Y OBTENER EL CODIGO CLIENTE
                                        //EL CODIGO CLIENTE OBTENIDO PASARLO A ecommerce_uspRegistrarComprobante
                                    }

                                    CodigoOutput = oComprobanteData.ecommerce_uspRegistrarComprobante(item);
                                    
                                    //VERIFICAR PAGO
                                    ComprobantePagoDTO oComprobantePagoDTO = new ComprobantePagoDTO();
                                    // int TipoDeuda = 0;
                                    decimal TotalVenta = 0;
                                    decimal TotalPago = 0;
                                    decimal PagoRestante = 0;
                                    //OBTENEMOS EL TOTAL VENTA
                                    foreach (ComprobanteDetalleDTO itemComprobanteDetalle in item.listaDetalle)
                                    {
                                        TotalVenta = TotalVenta + itemComprobanteDetalle.Total;
                                    }

                                    //OBTENEMOS TOTAL PAGO
                                    if (item.listaDetallePago.Count > 0)
                                    {
                                        foreach (ComprobantePagoDTO itemPago in item.listaDetallePago)
                                        {
                                            TotalPago = TotalPago + itemPago.Monto;
                                            PagoRestante = PagoRestante + itemPago.Monto;
                                        }
                                        
                                        foreach (ComprobantePagoDTO itemPago in item.listaDetallePago)
                                        {                                           
                                            oComprobantePagoDTO.CodigoUnidadNegocio = itemPago.CodigoUnidadNegocio;
                                            oComprobantePagoDTO.CodigoSede = itemPago.CodigoSede;
                                            oComprobantePagoDTO.CodigoComprobantePago = itemPago.CodigoComprobantePago;                                          
                                            oComprobantePagoDTO.CodigoCuentaBancaria = itemPago.CodigoCuentaBancaria;
                                            oComprobantePagoDTO.CodigoMetodoPago = itemPago.CodigoMetodoPago;
                                            oComprobantePagoDTO.TipoMoneda = itemPago.TipoMoneda;
                                            oComprobantePagoDTO.Nota = itemPago.Nota;
                                            oComprobantePagoDTO.Estado = itemPago.Estado;
                                            oComprobantePagoDTO.UsuarioCreacion = itemPago.UsuarioCreacion;
                                            oComprobantePagoDTO.FechaCreacion = item.FechaEmision;
                                        }
                                    }

                                    ComprobanteDetalleData oComprobanteDetalleData = new ComprobanteDetalleData();
                                    foreach (ComprobanteDetalleDTO itemComprobanteDetalle in item.listaDetalle)
                                    {
                                        int CodigoComprobanteDetalle = 0;
                                        itemComprobanteDetalle.CodigoComprobante = CodigoOutput;
                                        itemComprobanteDetalle.FechaCreacion = item.FechaEmision;
                                        CodigoComprobanteDetalle = oComprobanteDetalleData.ecommerce_uspRegistrarComprobanteDetalle(itemComprobanteDetalle);

                                        if (TotalPago == 0)
                                        {
                                            //FIADO

                                        }
                                        else if (TotalVenta == TotalPago)
                                        {
                                            ComprobantePagoData ComprobantePagoData = new ComprobantePagoData();
                                            oComprobantePagoDTO.CodigoComprobante = CodigoOutput;
                                            oComprobantePagoDTO.CodigoComprobanteDetalle = CodigoComprobanteDetalle;
                                            oComprobantePagoDTO.Monto = itemComprobanteDetalle.Total;
                                            ComprobantePagoData.ecommerce_uspRegistrarComprobantePago(oComprobantePagoDTO);
                                        }
                                        else if (TotalVenta > TotalPago && TotalPago > 0)
                                        {
                                            if (item.listaDetalle.Count == 1)
                                            {
                                                ComprobantePagoData ComprobantePagoData = new ComprobantePagoData();
                                                oComprobantePagoDTO.CodigoComprobante = CodigoOutput;
                                                oComprobantePagoDTO.CodigoComprobanteDetalle = CodigoComprobanteDetalle;
                                                oComprobantePagoDTO.Monto = TotalPago; // / item.listaDetalle.Count; //FRACCIONAMOS
                                                ComprobantePagoData.ecommerce_uspRegistrarComprobantePago(oComprobantePagoDTO);
                                            }
                                            else if (item.listaDetalle.Count > 1)
                                            {
                                                ComprobantePagoData ComprobantePagoData = new ComprobantePagoData();
                                                oComprobantePagoDTO.CodigoComprobante = CodigoOutput;
                                                oComprobantePagoDTO.CodigoComprobanteDetalle = CodigoComprobanteDetalle;
                                                
                                                if (PagoRestante > itemComprobanteDetalle.Total)
                                                {
                                                    oComprobantePagoDTO.Monto = itemComprobanteDetalle.Total; 
                                                    ComprobantePagoData.ecommerce_uspRegistrarComprobantePago(oComprobantePagoDTO);
                                                }
                                                else
                                                {
                                                    if (PagoRestante > 0)
                                                    {
                                                        oComprobantePagoDTO.Monto = PagoRestante;
                                                        ComprobantePagoData.ecommerce_uspRegistrarComprobantePago(oComprobantePagoDTO);
                                                    }                                                   
                                                }
                                                PagoRestante = PagoRestante - itemComprobanteDetalle.Total;                                               
                                            }
                                           
                                        }

                                    }
                                    
                                    break;
                                case Operation.ecommerce_uspRegistrarPagoComprobante:
                                                                    
                                    if (item.listaDetallePago.Count > 0)
                                    {
                                        foreach (ComprobantePagoDTO itemPago in item.listaDetallePago)
                                        {                                                                                     
                                            ComprobantePagoData ComprobantePagoData = new ComprobantePagoData();
                                            if (itemPago.Monto > 0)
                                            {
                                                ComprobantePagoData.ecommerce_uspRegistrarComprobantePago(itemPago);
                                            }                                            
                                        }
                                    }
                                
                                    break;

                                case Operation.ecommerce_uspRegistrarComprobante_TiendaVirtual:
                                    CodigoOutput = oComprobanteData.ecommerce_uspRegistrarComprobanteTiendaVirtual(item);

                                    ComprobanteDetalleData oCDetalleData = new ComprobanteDetalleData();
                                    foreach (ComprobanteDetalleDTO itemComprobanteDetalle in item.listaDetalle)
                                    {
                                        itemComprobanteDetalle.CodigoComprobante = CodigoOutput;
                                        oCDetalleData.ecommerce_uspRegistrarComprobanteDetalleTiendaVirtual(itemComprobanteDetalle);
                                    }

                                    if (item.listaDetallePago != null && item.listaDetallePago.Count > 0)
                                    {
                                        ComprobantePagoData ComprobantePagoData = new ComprobantePagoData();
                                        foreach (ComprobantePagoDTO itemPago in item.listaDetallePago)
                                        {
                                            itemPago.CodigoComprobante = CodigoOutput;
                                            ComprobantePagoData.ecommerce_uspRegistrarComprobantePago(itemPago);
                                        }
                                    }
                                    break;
                                case Operation.Update:
                                    
                                case Operation.Delete:
                                    CodigoOutput = oComprobanteData.CentroEntrenamiento_uspEliminarComprobante(item);

                                    break;
                            }
                        }
                        tx.Complete();
                        oResp.Success = true;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoOutput,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oResp.Success = false;
                        oResp.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oResp;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    
}
