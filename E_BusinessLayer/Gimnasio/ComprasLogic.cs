using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.IO;

namespace E_BusinessLayer.Gimnasio
{
	
	public class ComprasLogic: IDisposable
	{
		ComprasData oComprasData = null;
		public ComprasLogic()
		{
			oComprasData = new ComprasData();            
		}
       
        public RespListComprasDTO ComprasGetList(ReqFilterComprasDTO oReqFilterComprasDTO)
		{
			RespListComprasDTO oRespListComprasDTO = new RespListComprasDTO();
		
			oRespListComprasDTO.List = new List<ComprasDTO>();
			oRespListComprasDTO.User = oReqFilterComprasDTO.User;
			oRespListComprasDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterComprasDTO.User))
            {
                oRespListComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterComprasDTO.Paging == null)
            {
                oRespListComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

			if (oRespListComprasDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterComprasDTO.Paging.All && oReqFilterComprasDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterComprasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ComprasDTO> ComprasDTOList = new List<ComprasDTO>();
                    List<UsuarioDTO> UsuarioDTOList = new List<UsuarioDTO>();

                    switch (oReqFilterComprasDTO.FilterCase)
                    {

                        case filterCaseCompras.ListarControlIngresosPorND:
                            {                                
                                ComprasDTOList = oComprasData.ListarControlIngresosPorND(oReqFilterComprasDTO.Item);
                            }
                            break;
                    }

                    oRespListComprasDTO.List = ComprasDTOList;
                    oRespListComprasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListComprasDTO.Success = false;
                    oRespListComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListComprasDTO;	
           
		}
		
		public RespItemComprasDTO ComprasGetItem(ReqFilterComprasDTO oReqFilterComprasDTO)
		{
			RespItemComprasDTO oRespItemComprasDTO = new RespItemComprasDTO();

            oRespItemComprasDTO.Success = false;
            oRespItemComprasDTO.Item = null;
            oRespItemComprasDTO.User = oReqFilterComprasDTO.User;
            oRespItemComprasDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterComprasDTO.User))
            {
                oRespItemComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemComprasDTO.MessageList.Count == 0)
            {
                ComprasDTO oComprasDTO = null;
                try
                {
                    switch (oReqFilterComprasDTO.FilterCase)
                    {
                       
                        default:
                            {
                                oComprasDTO = new ComprasDTO();
                            }
                            break;
                    }

                    oRespItemComprasDTO.Item = new ComprasDTO();
                    oRespItemComprasDTO.Item = oComprasDTO;
                    oRespItemComprasDTO.Success = true;
                    oRespItemComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemComprasDTO.Success = false;
                    oRespItemComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemComprasDTO;
		}
	    
		public RespComprasDTO ExecuteTransac(ReqComprasDTO oReqComprasDTO)
		{
			RespComprasDTO oRespComprasDTO = new RespComprasDTO();

            oRespComprasDTO.MessageList = new List<Mensaje>();
            oRespComprasDTO.User = oReqComprasDTO.User;            

            if (String.IsNullOrEmpty(oReqComprasDTO.User))
            {
                oRespComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespComprasDTO.MessageList.Count == 0)
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (ComprasDTO item in oReqComprasDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    UsuariosIngresosData oUsuariosIngresosDataCreate = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOCreate = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOCreate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOCreate.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOCreate.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOCreate.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOCreate.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOCreate.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOCreate = oUsuariosIngresosDataCreate.uspValidarAccesoSistema(oUsuariosIngresosDTOCreate);
                                    if (oUsuariosIngresosDTOCreate.CodigoValidacion == 3)
                                    {

                                        UsuariosIngresosData oUsuariosIngresosDataValidacion = new UsuariosIngresosData();
                                        UsuariosIngresosDTO oUsuariosIngresosDTOValidacion = new UsuariosIngresosDTO();
                                        oUsuariosIngresosDTOValidacion.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        oUsuariosIngresosDTOValidacion.CodigoSede = item.CodigoSede;
                                        oUsuariosIngresosDTOValidacion.UsuarioCreacion = item.UsuarioCreacion;
                                        oUsuariosIngresosDTOValidacion.CodigoInicioSesion = item.CodigoInicioSesion;
                                        oUsuariosIngresosDTOValidacion.Operacion = "I";
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "DetalleCIngreso";

                                        CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                        if (CodigoValidacionOperaciones == 0)
                                        {

                                            codigo = 999999999;
                                            
                                            ComprasDetalleData oComprasDetalleData = new ComprasDetalleData();
                                            ProductoData oProductoData = new ProductoData();
                                            foreach (ComprasDetalleDTO itemDetalle in item.ListaComprasDetalleDTO)
                                            {
                                                itemDetalle.CodigoIngreso = 0;
                                                itemDetalle.FechaCompra = item.FechaIngreso;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oComprasDetalleData.Registrar(itemDetalle);

                                                ProductoDTO oProductoDTO = new ProductoDTO();
                                                oProductoDTO.CodigoUnidadNegocio = itemDetalle.CodigoUnidadNegocio;
                                                oProductoDTO.CodigoSede = itemDetalle.CodigoSede;
                                                oProductoDTO.CodigoProducto = itemDetalle.CodigoProducto;
                                                oProductoDTO.Cantidad = (int)itemDetalle.CantidadIngreso;
                                                oProductoDTO.PrecioVenta = itemDetalle.PrecioVenta;
                                                oProductoDTO.flag = 1;
                                                oProductoDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oProductoData.ActualizarPrecioVentaCantidad(oProductoDTO);
                                            }

                                        }


                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }                                        
                                    break;
                            }
                        }

                        tx.Complete();
                        oRespComprasDTO.Success = true;
                        oRespComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespComprasDTO.Success = false;
                        oRespComprasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespComprasDTO;
		}
        
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
