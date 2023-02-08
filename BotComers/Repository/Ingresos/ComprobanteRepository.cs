using BotComers.Helpers;
using BotComers.ViewModels;
using BotComers.ViewModels.Ingresos;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Net;

namespace BotComers.Repository.Ingresos
{
    public class ComprobanteRepository : IDisposable
    {

        public List<ComprobanteViewModel> ecommerce_uspListarComprobante_Paginacion(ComprobanteViewModel request)
        {
            List<ComprobanteViewModel> lista = new List<ComprobanteViewModel>();

            ReqFilterComprobanteDTO oReq = new ReqFilterComprobanteDTO()
            {
                Item = new ComprobanteDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoEstadoEntrega = request.CodigoEstadoEntrega,
                    CodigoComprobante = request.CodigoComprobante,
                    CodigoCliente = request.CodigoCliente,
                    b_FechaEmisionInicio = request.b_FechaEmisionInicio,
                    b_FechaEmisionFin = request.b_FechaEmisionFin,
                    Estado = request.Estado
                },
                FilterCase = filterCaseComprobante.ecommerce_uspListarComprobante,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(request.PageNumber),
                    PageRecords = 0
                }
            };

            RespListComprobanteDTO oResp = null;

            using (ComprobanteLogic oComprobanteLogic = new ComprobanteLogic())
            {
                oResp = oComprobanteLogic.ComprobanteGetList(oReq);
            }

            if (oResp.Success)
            {

                foreach (ComprobanteDTO item in oResp.List)
                {
                    lista.Add(new ComprobanteViewModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        CodigoComprobante = item.CodigoComprobante,
                        CodigoTipoComprobante = item.CodigoTipoComprobante,
                        TipoMoneda = item.TipoMoneda,
                        CodigoAlmacen = item.CodigoAlmacen,
                        Correlativo = item.Correlativo,
                        CodigoCliente = item.CodigoCliente,
                        NombresCliente = item.NombresCliente,
                        Celular = item.Celular,
                        CodigoVendedor = item.CodigoVendedor,
                        FechaEmision = item.FechaEmision,
                        DesFechaEmision = item.FechaEmision.ToString("dd/MM/yyyy"),
                        CodigoPlazoPago = item.CodigoPlazoPago,
                        FechaVencimiento = item.FechaVencimiento,
                        DesFechaVencimiento = item.FechaVencimiento.ToString("dd/MM/yyyy"),
                        ColorFechaVencimiento = item.ColorFechaVencimiento,
                        TerminosCondiciones = item.TerminosCondiciones,
                        Notas = item.Notas,
                        Comentarios = item.Comentarios,
                        SubTotal = item.SubTotal,
                        Descuento = item.Descuento,
                        SubTotal2 = item.SubTotal2,
                        IGV = item.IGV,
                        Total = item.Total,
                        TotalCobrado = item.TotalCobrado,
                        TotalPorCobrar = item.TotalPorCobrar,
                        Estado = item.Estado,
                        DesEstado = item.DesEstado,
                        ColorEstado = item.ColorEstado,
                        DesEstadoEntrega = item.DesEstadoEntrega,
                        UsuarioCreacion = item.UsuarioCreacion
                    });
                }
            }
            return lista;
        }

        public List<ComprobanteViewModel> ecommerce_uspListarComprobanteParaAnular(ComprobanteViewModel request)
        {
            List<ComprobanteViewModel> lista = new List<ComprobanteViewModel>();

            ReqFilterComprobanteDTO oReq = new ReqFilterComprobanteDTO()
            {
                Item = new ComprobanteDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoCliente = request.CodigoCliente,
                    b_FechaEmisionInicio = request.b_FechaEmisionInicio,
                    b_FechaEmisionFin = request.b_FechaEmisionFin,
                    Estado = request.Estado
                },
                FilterCase = filterCaseComprobante.ecommerce_uspListarComprobanteParaAnular,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(request.PageNumber),
                    PageRecords = 0
                }
            };

            RespListComprobanteDTO oResp = null;

            using (ComprobanteLogic oComprobanteLogic = new ComprobanteLogic())
            {
                oResp = oComprobanteLogic.ComprobanteGetList(oReq);
            }

            if (oResp.Success)
            {

                foreach (ComprobanteDTO item in oResp.List)
                {
                    lista.Add(new ComprobanteViewModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        CodigoComprobante = item.CodigoComprobante,
                        CodigoTipoComprobante = item.CodigoTipoComprobante,
                        TipoMoneda = item.TipoMoneda,
                        CodigoAlmacen = item.CodigoAlmacen,
                        Correlativo = item.Correlativo,
                        CodigoCliente = item.CodigoCliente,
                        NombresCliente = item.NombresCliente,
                        Celular = item.Celular,
                        CodigoVendedor = item.CodigoVendedor,
                        FechaEmision = item.FechaEmision,
                        DesFechaEmision = item.FechaEmision.ToString("dd/MM/yyyy"),
                        CodigoPlazoPago = item.CodigoPlazoPago,
                        FechaVencimiento = item.FechaVencimiento,
                        DesFechaVencimiento = item.FechaVencimiento.ToString("dd/MM/yyyy"),
                        ColorFechaVencimiento = item.ColorFechaVencimiento,
                        TerminosCondiciones = item.TerminosCondiciones,
                        Notas = item.Notas,
                        Comentarios = item.Comentarios,
                        SubTotal = item.SubTotal,
                        Descuento = item.Descuento,
                        SubTotal2 = item.SubTotal2,
                        IGV = item.IGV,
                        Total = item.Total,
                        TotalCobrado = item.TotalCobrado,
                        TotalPorCobrar = item.TotalPorCobrar,
                        Estado = item.Estado,
                        DesEstado = item.DesEstado,
                        ColorEstado = item.ColorEstado,
                        DesEstadoEntrega = item.DesEstadoEntrega,
                        UsuarioCreacion = item.UsuarioCreacion
                    });
                }
            }
            return lista;
        }

        public string ecommerce_uspRegistrarComprobante(ComprobanteViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ComprobanteDTO> list = new List<ComprobanteDTO>();

            list.Add(new ComprobanteDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoComprobante = 0,
                CodigoTipoComprobante = oItem.CodigoTipoComprobante,
                TipoMoneda = oItem.TipoMoneda,
                CodigoAlmacen = oItem.CodigoAlmacen,
                Correlativo = oItem.Correlativo,
                CodigoCliente = oItem.CodigoCliente,
                NroIdentificacion = oItem.NroIdentificacion,
                CodigoVendedor = oItem.CodigoVendedor,
                FechaEmision = oItem.FechaEmision,
                CodigoPlazoPago = oItem.CodigoPlazoPago,
                FechaVencimiento = oItem.FechaVencimiento,
                TerminosCondiciones = oItem.TerminosCondiciones == null ? string.Empty : oItem.TerminosCondiciones,
                Notas = oItem.Notas == null ? string.Empty : oItem.Notas,
                Comentarios = oItem.Comentarios == null ? string.Empty : oItem.Comentarios,
                SubTotal = oItem.SubTotal,
                Descuento = oItem.Descuento,
                IGV = oItem.IGV,
                SubTotal2 = oItem.SubTotal2,
                Total = oItem.Total,
                Estado = oItem.Estado,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,

            });

            int CodigoUnidadNegocio = list[0].CodigoUnidadNegocio;
            int CodigoSede = list[0].CodigoSede;
            int CodigoAlmacen = list[0].CodigoAlmacen;
            string UsuarioCreacion = list[0].UsuarioCreacion;

            list[0].listaDetalle = new List<ComprobanteDetalleDTO>();
            for (int i = 0; i < oItem.listaDetalle.Count; i++)
            {
                list[0].listaDetalle.Add(new ComprobanteDetalleDTO()
                {
                    CodigoComprobanteDetalle = 0,
                    CodigoComprobante = 0,
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoAlmacen = CodigoAlmacen,
                    //CodigoComprobante = oItem.listaDetalle[i].CodigoComprobante,
                    CodigoItemsVenta = oItem.listaDetalle[i].CodigoItemsVenta,
                    Referencia = oItem.listaDetalle[i].Referencia == null ? string.Empty : oItem.listaDetalle[i].Referencia,
                    Precio = oItem.listaDetalle[i].Precio,
                    Descuento = oItem.listaDetalle[i].Descuento,
                    CodigoTipoImpuesto = oItem.listaDetalle[i].CodigoTipoImpuesto,
                    Descripcion = oItem.listaDetalle[i].Descripcion == null ? string.Empty : oItem.listaDetalle[i].Descripcion,
                    Cantidad = oItem.listaDetalle[i].Cantidad,
                    Total = oItem.listaDetalle[i].Total,
                    Estado = 1,
                    UsuarioCreacion = UsuarioCreacion
                });
            }
            list[0].listaDetallePago = new List<ComprobantePagoDTO>();
            for (int i = 0; i < oItem.listaDetallePago.Count; i++)
            {
                list[0].listaDetallePago.Add(new ComprobantePagoDTO()
                {
                    CodigoComprobantePago = 0,
                    CodigoComprobante = 0,
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoCuentaBancaria = 0,
                    CodigoMetodoPago = oItem.listaDetallePago[i].CodigoMetodoPago,
                    TipoMoneda = 2,
                    Monto = oItem.listaDetallePago[i].Monto,
                    Nota = oItem.listaDetallePago[i].Nota == null ? string.Empty : oItem.listaDetallePago[i].Nota,
                    Estado = 1,
                    UsuarioCreacion = UsuarioCreacion
                });
            }

            ReqComprobanteDTO oReq = new ReqComprobanteDTO()
            {
                List = list,
                User = "admin"
            };
            RespComprobanteDTO oResp = null;

            //Verificar Configuracion
            E_DataModel.Gimnasio.ConfiguracionDTO ConfiguracionDTO = new E_DataModel.Gimnasio.ConfiguracionDTO();
            ConfiguracionDTO = BotComers.Controllers.posController.BuscarConfiguracionServer();

            if (oItem.CodigoTipoComprobante == 5)
            {
                ConfiguracionDTO.TieneFacturacionElectronica = false;
            }

            var rptaFact = new Respuesta();
            string data_base64_pdf = "";
            if (ConfiguracionDTO.TieneFacturacionElectronica)
            {
                using (Helpers.NubefactService facturacion = new Helpers.NubefactService())
                {
                    rptaFact = facturacion.EjecutarWebServicePuntoVenta(list[0], ConfiguracionDTO);
                    if (!string.IsNullOrEmpty(rptaFact.enlace_del_pdf))
                    {
                        oReq.List[0].UrlPDF = rptaFact.enlace_del_pdf;
                        oReq.List[0].Correlativo = rptaFact.serie + "-"+ rptaFact.numero;
                      
                        if (!string.IsNullOrEmpty(rptaFact.enlace_del_pdf))
                        {
                            using (var client = new WebClient())
                            {
                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                var bytes = client.DownloadData(rptaFact.enlace_del_pdf);
                                data_base64_pdf = Convert.ToBase64String(bytes);
                            }
                        }

                    }
                }
            }

            using (ComprobanteLogic oComprobanteLogic = new ComprobanteLogic())
            {
                oResp = oComprobanteLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                if (!string.IsNullOrEmpty(data_base64_pdf))
                {
                    mensaje = "Datos Guardados Correctamente" + "|" + data_base64_pdf;
                }
                else {
                    mensaje = "Datos Guardados Correctamente";
                }
                
            }

            return mensaje;
        }

        public int ecommerce_uspRegistrarComprobante_TiendaVirtual(ComprobanteViewModel oItem)
        {
            int validacion = 0;

            List<ComprobanteDTO> list = new List<ComprobanteDTO>();

            list.Add(new ComprobanteDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoComprobante = 0,
                CodigoTipoComprobante = oItem.CodigoTipoComprobante,
                TipoMoneda = oItem.TipoMoneda,
                CodigoAlmacen = oItem.CodigoAlmacen,
                Correlativo = oItem.Correlativo == null ? string.Empty : oItem.Correlativo,
                CodigoCliente = oItem.CodigoCliente,
                CodigoVendedor = oItem.CodigoVendedor,
                FechaEmision = oItem.FechaEmision,
                CodigoPlazoPago = oItem.CodigoPlazoPago,
                FechaVencimiento = oItem.FechaVencimiento,
                TerminosCondiciones = oItem.TerminosCondiciones == null ? string.Empty : oItem.TerminosCondiciones,
                Notas = oItem.Notas == null ? string.Empty : oItem.Notas,
                Comentarios = oItem.Comentarios == null ? string.Empty : oItem.Comentarios,
                SubTotal = oItem.SubTotal,
                Envio = oItem.Envio,
                SubTotal2 = oItem.SubTotal2,
                Descuento = oItem.Descuento,
                SubTotal3 = oItem.SubTotal3,
                IGV = oItem.IGV,
                Total = oItem.Total,
                Estado = oItem.Estado,
                CodigoDireccion = oItem.CodigoDireccion,
                CodigoCupon = oItem.CodigoCupon == null ? string.Empty : oItem.CodigoCupon,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = Operation.ecommerce_uspRegistrarComprobante_TiendaVirtual

            });

            int CodigoUnidadNegocio = list[0].CodigoUnidadNegocio;
            int CodigoSede = list[0].CodigoSede;
            int CodigoAlmacen = list[0].CodigoAlmacen;
            string UsuarioCreacion = list[0].UsuarioCreacion;

            list[0].listaDetalle = new List<ComprobanteDetalleDTO>();
            for (int i = 0; i < oItem.listaDetalle.Count; i++)
            {
                list[0].listaDetalle.Add(new ComprobanteDetalleDTO()
                {
                    CodigoComprobanteDetalle = 0,
                    CodigoComprobante = 0,
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoAlmacen = CodigoAlmacen,
                    CodigoImagen = oItem.listaDetalle[i].CodigoImagen,
                    //CodigoComprobante = oItem.listaDetalle[i].CodigoComprobante,
                    CodigoItemsVenta = oItem.listaDetalle[i].CodigoItemsVenta,
                    Referencia = oItem.listaDetalle[i].Referencia == null ? string.Empty : oItem.listaDetalle[i].Referencia,
                    Precio = oItem.listaDetalle[i].Precio,
                    Descuento = oItem.listaDetalle[i].Descuento,
                    CodigoTipoImpuesto = oItem.listaDetalle[i].CodigoTipoImpuesto,
                    Descripcion = oItem.listaDetalle[i].Descripcion == null ? string.Empty : oItem.listaDetalle[i].Descripcion,
                    Cantidad = oItem.listaDetalle[i].Cantidad,
                    Total = oItem.listaDetalle[i].Total,
                    Estado = 1,
                    UsuarioCreacion = UsuarioCreacion
                });
            }
            list[0].listaDetallePago = new List<ComprobantePagoDTO>();
            if (oItem.listaDetallePago != null)
            {
                for (int i = 0; i < oItem.listaDetallePago.Count; i++)
                {
                    list[0].listaDetallePago.Add(new ComprobantePagoDTO()
                    {
                        CodigoComprobantePago = 0,
                        CodigoComprobante = 0,
                        CodigoUnidadNegocio = CodigoUnidadNegocio,
                        CodigoSede = CodigoSede,
                        CodigoCuentaBancaria = 0,
                        CodigoMetodoPago = oItem.listaDetallePago[i].CodigoMetodoPago,
                        TipoMoneda = 2,
                        Monto = oItem.listaDetallePago[i].Monto,
                        Nota = oItem.listaDetallePago[i].Nota == null ? string.Empty : oItem.listaDetallePago[i].Nota,
                        Estado = 1,
                        UsuarioCreacion = UsuarioCreacion
                    });
                }

            }

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
            if (oResp.Success)
            {
                validacion = oResp.MessageList[0].Codigo;
            }

            return validacion;
        }

        public string ecommerce_uspRegistrarPagoComprobante(ComprobanteViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ComprobanteDTO> list = new List<ComprobanteDTO>();

            list.Add(new ComprobanteDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoComprobante = 0,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = Operation.ecommerce_uspRegistrarPagoComprobante
            });

            int CodigoUnidadNegocio = list[0].CodigoUnidadNegocio;
            int CodigoSede = list[0].CodigoSede;
            string UsuarioCreacion = list[0].UsuarioCreacion;

            int validadorPago = 0;
            list[0].listaDetallePago = new List<ComprobantePagoDTO>();
            for (int i = 0; i < oItem.listaDetallePago.Count; i++)
            {
                if (oItem.listaDetallePago[i].Monto > oItem.listaDetallePago[i].Total)
                {
                    validadorPago = 1;
                }

                list[0].listaDetallePago.Add(new ComprobantePagoDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede,
                    CodigoComprobantePago = 0,
                    CodigoComprobante = oItem.listaDetallePago[i].CodigoComprobante,
                    CodigoComprobanteDetalle = oItem.listaDetallePago[i].CodigoComprobanteDetalle,
                    CodigoCuentaBancaria = 0,
                    CodigoMetodoPago = oItem.listaDetallePago[i].CodigoMetodoPago,
                    TipoMoneda = 2,
                    Monto = oItem.listaDetallePago[i].Monto,
                    Nota = oItem.listaDetallePago[i].Nota == null ? string.Empty : oItem.listaDetallePago[i].Nota,
                    Estado = 1,
                    UsuarioCreacion = UsuarioCreacion,
                    FechaCreacion = DateTime.Now
                });
            }

            if (validadorPago == 0)
            {
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
                if (oResp.Success)
                {
                    mensaje = "Datos Guardados Correctamente";
                }

            }

            return mensaje;
        }


        public List<TipoComprobanteViewModel> ecommerce_uspListarTipoComprobante(TipoComprobanteViewModel request)
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


        public string CentroEntrenamiento_uspEliminarComprobante(ComprobanteViewModel oItem)
        {
            string mensaje = string.Empty;

            List<ComprobanteDTO> list = new List<ComprobanteDTO>();

            list.Add(new ComprobanteDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoComprobante = oItem.CodigoComprobante,
                Operation = Operation.Delete
            });

            ReqComprobanteDTO oReq = new ReqComprobanteDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespComprobanteDTO oResp = null;
            using (ComprobanteLogic oComprobanteLogic = new ComprobanteLogic())
            {
                oResp = oComprobanteLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}