
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

namespace E_BusinessLayer.Gimnasio
{
	//-------------------------------------------------------------------
	//Archivo     : ComprasSuplementosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 18/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ComprasSuplementosLogic: IDisposable
	{
		ComprasSuplementosData oComprasSuplementosData = null;
		public ComprasSuplementosLogic()
		{
			oComprasSuplementosData = new ComprasSuplementosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ComprasSuplementosGetList
		//Objetivo: Retorna una colección de registros de tipo ComprasSuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListComprasSuplementosDTO ComprasSuplementosGetList(ReqFilterComprasSuplementosDTO oReqFilterComprasSuplementosDTO)
		{
		
			RespListComprasSuplementosDTO oRespListComprasSuplementosDTO = new RespListComprasSuplementosDTO();
		
			oRespListComprasSuplementosDTO.List = new List<ComprasSuplementosDTO>();
			oRespListComprasSuplementosDTO.User = oReqFilterComprasSuplementosDTO.User;
			oRespListComprasSuplementosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterComprasSuplementosDTO.User))
            {
                oRespListComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ComprasSuplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterComprasSuplementosDTO.Paging == null)
            {
                oRespListComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListComprasSuplementosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                   

                    List<ComprasSuplementosDTO> ComprasSuplementosDTOList = new List<ComprasSuplementosDTO>();

                    switch (oReqFilterComprasSuplementosDTO.FilterCase)
                    {
                        case filterCaseComprasSuplementos.uspListarComprasSuplementos_Paginacion:
                            {
                                if (!oReqFilterComprasSuplementosDTO.Paging.All && oReqFilterComprasSuplementosDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterComprasSuplementosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarComprasSuplementos_NumeroRegistros"]);
                                    ComprasSuplementosDTOList = oComprasSuplementosData.uspListarComprasSuplementos_Paginacion(oReqFilterComprasSuplementosDTO.Item, oReqFilterComprasSuplementosDTO.Paging);
                                }
                                
                            }
                            break;
                    }

                    oRespListComprasSuplementosDTO.List = ComprasSuplementosDTOList;
                    oRespListComprasSuplementosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListComprasSuplementosDTO.Success = false;
                    oRespListComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListComprasSuplementosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ComprasSuplementosGetItem
		//Objetivo: Retorna un registro de tipo ComprasSuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemComprasSuplementosDTO ComprasSuplementosGetItem(ReqFilterComprasSuplementosDTO oReqFilterComprasSuplementosDTO)
		{
			RespItemComprasSuplementosDTO oRespItemComprasSuplementosDTO = new RespItemComprasSuplementosDTO();

            oRespItemComprasSuplementosDTO.Success = false;
            oRespItemComprasSuplementosDTO.Item = null;
            oRespItemComprasSuplementosDTO.User = oReqFilterComprasSuplementosDTO.User;
            oRespItemComprasSuplementosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterComprasSuplementosDTO.User))
            {
                oRespItemComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ComprasSuplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemComprasSuplementosDTO.MessageList.Count == 0)
            {
                ComprasSuplementosDTO oComprasSuplementosDTO = null;
                try
                {
                    switch (oReqFilterComprasSuplementosDTO.FilterCase)
                    {
                      
                        case filterCaseComprasSuplementos.uspListarComprasSuplementos_NumeroRegistros:
                            {
                                oComprasSuplementosDTO = new ComprasSuplementosDTO();
                                oComprasSuplementosDTO = oComprasSuplementosData.uspListarComprasSuplementos_NumeroRegistros(oReqFilterComprasSuplementosDTO.Item);
                            }
                            break;
                        default:
                            {
                                oComprasSuplementosDTO = new ComprasSuplementosDTO();
                            }
                            break;
                    }

                    oRespItemComprasSuplementosDTO.Item = new ComprasSuplementosDTO();
                    oRespItemComprasSuplementosDTO.Item = oComprasSuplementosDTO;
                    oRespItemComprasSuplementosDTO.Success = true;
                    oRespItemComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemComprasSuplementosDTO.Success = false;
                    oRespItemComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemComprasSuplementosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ComprasSuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespComprasSuplementosDTO ExecuteTransac(ReqComprasSuplementosDTO oReqComprasSuplementosDTO)
		{
			RespComprasSuplementosDTO oRespComprasSuplementosDTO = new RespComprasSuplementosDTO();

            oRespComprasSuplementosDTO.MessageList = new List<Mensaje>();
            oRespComprasSuplementosDTO.User = oReqComprasSuplementosDTO.User;
            
            if (String.IsNullOrEmpty(oReqComprasSuplementosDTO.User))
            {
                oRespComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ComprasSuplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespComprasSuplementosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (ComprasSuplementosDTO item in oReqComprasSuplementosDTO.List)
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
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "ComprasSuplementos";

                                        CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                        if (CodigoValidacionOperaciones == 0)
                                        {

                                            codigo = 999999999;
                                            //GastosDTO edto = new GastosDTO();
                                            //edto.Codigo = 0;
                                            //edto.Responsable = item.UsuarioCreacion;
                                            //edto.TipoEgreso = 0;
                                            //edto.Descripcion = "Compra Suplemento - " + item.NroDocumento;
                                            //edto.MontoEgreso = item.TotalNeto;
                                            //edto.CodigoSede = item.CodigoSede;
                                            //edto.UsuarioCreacion = item.UsuarioCreacion;
                                            //edto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            //GastosData eData = new GastosData();
                                            //eData.Registrar(edto);

                                            SuplementosData oSuplementosData = new SuplementosData();
                                            foreach (ComprasSuplementosDTO itemDetalle in item.ListaDetalleCSuplementosDTO)
                                            {
                                                itemDetalle.CodigoCompraSuplemento = 0;
                                                itemDetalle.FechaCompra = item.FechaCompra;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oComprasSuplementosData.Registrar(itemDetalle);

                                                SuplementosDTO oSuplementosDTO = new SuplementosDTO();
                                                oSuplementosDTO.CodigoUnidadNegocio = itemDetalle.CodigoUnidadNegocio;
                                                oSuplementosDTO.CodigoSede = itemDetalle.CodigoSede;
                                                oSuplementosDTO.CodigoSuplemento = itemDetalle.CodigoSuplemento;
                                                oSuplementosDTO.Cantidad = (int)itemDetalle.CantidadIngreso;
                                                oSuplementosDTO.PrecioVenta = itemDetalle.PrecioVenta;
                                                oSuplementosDTO.flag = 1;
                                                oSuplementosDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oSuplementosData.ActualizarPrecioVentaCantidadSuplementos(oSuplementosDTO);
                                            }

                                        }

                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }


                                    break;
                                case Operation.Update:
                                    //oComprasSuplementosData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    UsuariosIngresosData oUsuariosIngresosDataDelete = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTODelete = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTODelete.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTODelete.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTODelete.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTODelete.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTODelete.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTODelete.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTODelete = oUsuariosIngresosDataDelete.uspValidarAccesoSistema(oUsuariosIngresosDTODelete);
                                    if (oUsuariosIngresosDTODelete.CodigoValidacion == 3)
                                    {
                                        codigo = 999999999;
                                        oComprasSuplementosData.Eliminar(item);
                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespComprasSuplementosDTO.Success = true;
                        oRespComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespComprasSuplementosDTO.Success = false;
                        oRespComprasSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespComprasSuplementosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
