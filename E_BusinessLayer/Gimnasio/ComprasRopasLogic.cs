
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
	//Archivo     : ComprasRopasLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 24/01/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ComprasRopasLogic: IDisposable
	{
		ComprasRopasData oComprasRopasData = null;
		public ComprasRopasLogic()
		{
			oComprasRopasData = new ComprasRopasData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ComprasRopasGetList
		//Objetivo: Retorna una colección de registros de tipo ComprasRopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListComprasRopasDTO ComprasRopasGetList(ReqFilterComprasRopasDTO oReqFilterComprasRopasDTO)
		{
		
			RespListComprasRopasDTO oRespListComprasRopasDTO = new RespListComprasRopasDTO();
		
			oRespListComprasRopasDTO.List = new List<ComprasRopasDTO>();
			oRespListComprasRopasDTO.User = oReqFilterComprasRopasDTO.User;
			oRespListComprasRopasDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterComprasRopasDTO.User))
            {
                oRespListComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ComprasRopas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterComprasRopasDTO.Paging == null)
            {
                oRespListComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListComprasRopasDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                
                    List<ComprasRopasDTO> ComprasRopasDTOList = new List<ComprasRopasDTO>();

                    switch (oReqFilterComprasRopasDTO.FilterCase)
                    {

                        case filterCaseComprasRopas.uspListarComprasRopas_Paginacion:
                            {
                                if (!oReqFilterComprasRopasDTO.Paging.All && oReqFilterComprasRopasDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterComprasRopasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarComprasRopas_NumeroRegistros"]);
                                    ComprasRopasDTOList = oComprasRopasData.uspListarComprasRopas_Paginacion(oReqFilterComprasRopasDTO.Item, oReqFilterComprasRopasDTO.Paging);
                                }

                            }
                            break;
                    
                    }

                    oRespListComprasRopasDTO.List = ComprasRopasDTOList;
                    oRespListComprasRopasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListComprasRopasDTO.Success = false;
                    oRespListComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListComprasRopasDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ComprasRopasGetItem
		//Objetivo: Retorna un registro de tipo ComprasRopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemComprasRopasDTO ComprasRopasGetItem(ReqFilterComprasRopasDTO oReqFilterComprasRopasDTO)
		{
			RespItemComprasRopasDTO oRespItemComprasRopasDTO = new RespItemComprasRopasDTO();

            oRespItemComprasRopasDTO.Success = false;
            oRespItemComprasRopasDTO.Item = null;
            oRespItemComprasRopasDTO.User = oReqFilterComprasRopasDTO.User;
            oRespItemComprasRopasDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterComprasRopasDTO.User))
            {
                oRespItemComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ComprasRopas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemComprasRopasDTO.MessageList.Count == 0)
            {
                ComprasRopasDTO oComprasRopasDTO = null;
                try
                {
                    switch (oReqFilterComprasRopasDTO.FilterCase)
                    {

                        case filterCaseComprasRopas.uspListarComprasRopas_NumeroRegistros:
                            {
                                oComprasRopasDTO = new ComprasRopasDTO();
                                oComprasRopasDTO = oComprasRopasData.uspListarComprasRopas_NumeroRegistros(oReqFilterComprasRopasDTO.Item);
                            }
                            break;
                        default:
                            {
                                oComprasRopasDTO = new ComprasRopasDTO();
                            }
                            break;
                    }

                    oRespItemComprasRopasDTO.Item = new ComprasRopasDTO();
                    oRespItemComprasRopasDTO.Item = oComprasRopasDTO;
                    oRespItemComprasRopasDTO.Success = true;
                    oRespItemComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemComprasRopasDTO.Success = false;
                    oRespItemComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemComprasRopasDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ComprasRopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespComprasRopasDTO ExecuteTransac(ReqComprasRopasDTO oReqComprasRopasDTO)
		{
			RespComprasRopasDTO oRespComprasRopasDTO = new RespComprasRopasDTO();

            oRespComprasRopasDTO.MessageList = new List<Mensaje>();
            oRespComprasRopasDTO.User = oReqComprasRopasDTO.User;
            
            if (String.IsNullOrEmpty(oReqComprasRopasDTO.User))
            {
                oRespComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ComprasRopas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespComprasRopasDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (ComprasRopasDTO item in oReqComprasRopasDTO.List)
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
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "ComprasRopas";

                                        CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                        if (CodigoValidacionOperaciones == 0)
                                        {

                                            codigo = 999999999;
                                            //GastosDTO edto = new GastosDTO();
                                            //edto.Codigo = 0;
                                            //edto.Responsable = item.UsuarioCreacion;
                                            //edto.TipoEgreso = 0;
                                            //edto.Descripcion = "Compra Ropas- " + item.NroDocumento;
                                            //edto.MontoEgreso = item.TotalNeto;
                                            //edto.CodigoSede = item.CodigoSede;
                                            //edto.UsuarioCreacion = item.UsuarioCreacion;
                                            //edto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            //GastosData eData = new GastosData();
                                            //eData.Registrar(edto);

                                            RopasData oRopasData = new RopasData();
                                            foreach (ComprasRopasDTO itemDetalle in item.ListaDetalleCRopasDTO)
                                            {
                                                itemDetalle.CodigoCompraRopa = 0;
                                                itemDetalle.FechaCompra = item.FechaCompra;
                                                itemDetalle.UsuarioCreacion = item.UsuarioCreacion;
                                                itemDetalle.CodigoSede = item.CodigoSede;
                                                itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oComprasRopasData.Registrar(itemDetalle);

                                                RopasDTO oRopasDTO = new RopasDTO();
                                                oRopasDTO.CodigoUnidadNegocio = itemDetalle.CodigoUnidadNegocio;
                                                oRopasDTO.CodigoSede = itemDetalle.CodigoSede;
                                                oRopasDTO.CodigoProducto = itemDetalle.CodigoProducto;
                                                oRopasDTO.Cantidad = (int)itemDetalle.CantidadIngreso;
                                                oRopasDTO.PrecioVenta = itemDetalle.PrecioVenta;
                                                oRopasDTO.flag = 1;
                                                oRopasDTO.UsuarioCreacion = item.UsuarioCreacion;
                                                oRopasData.ActualizarPrecioVentaCantidadRopas(oRopasDTO);
                                            }

                                        }

                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }
                                    break;
                                case Operation.Update:
                                    //oComprasRopasData.Actualizar(item);
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
                                        oComprasRopasData.Eliminar(item);
                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespComprasRopasDTO.Success = true;
                        oRespComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespComprasRopasDTO.Success = false;
                        oRespComprasRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespComprasRopasDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
