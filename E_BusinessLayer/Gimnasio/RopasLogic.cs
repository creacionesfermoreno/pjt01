
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
	//Archivo     : RopasLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 23/01/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class RopasLogic: IDisposable
	{
		RopasData oRopasData = null;
		public RopasLogic()
		{
			oRopasData = new RopasData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	RopasGetList
		//Objetivo: Retorna una colección de registros de tipo RopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListRopasDTO RopasGetList(ReqFilterRopasDTO oReqFilterRopasDTO)
		{
		
			RespListRopasDTO oRespListRopasDTO = new RespListRopasDTO();
		
			oRespListRopasDTO.List = new List<RopasDTO>();
			oRespListRopasDTO.User = oReqFilterRopasDTO.User;
			oRespListRopasDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterRopasDTO.User))
            {
                oRespListRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Ropas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterRopasDTO.Paging == null)
            {
                oRespListRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListRopasDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
               
                    List<RopasDTO> RopasDTOList = new List<RopasDTO>();

                    switch (oReqFilterRopasDTO.FilterCase)
                    {
                        case filterCaseRopas.uspListarRopasPorFiltro_Paginacion:
                            {
                                if (!oReqFilterRopasDTO.Paging.All && oReqFilterRopasDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterRopasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarRopasPorFiltro_NumeroRegistros"]);
                                    RopasDTOList = oRopasData.uspListarRopasPorFiltro_Paginacion(oReqFilterRopasDTO.Item, oReqFilterRopasDTO.Paging);
                                }

                            }
                        break;
                        case filterCaseRopas.uspListarRopas:
                            {
                                RopasDTOList = oRopasData.uspListarRopas(oReqFilterRopasDTO.Item);
                            }
                            break;
                        case filterCaseRopas.uspListarRopasCompras:
                            {
                                RopasDTOList = oRopasData.uspListarRopasCompras(oReqFilterRopasDTO.Item);
                            }
                            break;
                        case filterCaseRopas.uspListarRopasComprasFiltro:
                            {
                                RopasDTOList = oRopasData.uspListarRopasComprasFiltro(oReqFilterRopasDTO.Item);
                            }
                            break;
                        case filterCaseRopas.uspListarRopasVentas:
                            {
                                RopasDTOList = oRopasData.uspListarRopasVentas(oReqFilterRopasDTO.Item);
                            }
                            break;
                        //default:
                        //    {
                        //        RopasDTOList = oRopasData.Listar(oReqFilterRopasDTO.Item, oReqFilterRopasDTO.Paging, ref recordCount);
                        //    }
                        //    break;
                    }

                    oRespListRopasDTO.List = RopasDTOList;
                    oRespListRopasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListRopasDTO.Success = false;
                    oRespListRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListRopasDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	RopasGetItem
		//Objetivo: Retorna un registro de tipo RopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemRopasDTO RopasGetItem(ReqFilterRopasDTO oReqFilterRopasDTO)
		{
			RespItemRopasDTO oRespItemRopasDTO = new RespItemRopasDTO();

            oRespItemRopasDTO.Success = false;
            oRespItemRopasDTO.Item = null;
            oRespItemRopasDTO.User = oReqFilterRopasDTO.User;
            oRespItemRopasDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterRopasDTO.User))
            {
                oRespItemRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Ropas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemRopasDTO.MessageList.Count == 0)
            {
                RopasDTO oRopasDTO = null;
                try
                {
                    switch (oReqFilterRopasDTO.FilterCase)
                    {
                       
                        case filterCaseRopas.PorCodigo:
                            {
                                oRopasDTO = new RopasDTO();
                                oRopasDTO = oRopasData.BuscarPorCodigoRopas(oReqFilterRopasDTO.Item);
                            }
                            break;
                        case filterCaseRopas.uspListarRopasPorFiltro_NumeroRegistros:
                            {
                                oRopasDTO = new RopasDTO();
                                oRopasDTO = oRopasData.uspListarRopasPorFiltro_NumeroRegistros(oReqFilterRopasDTO.Item);
                            }
                            break;
                        default:
                            {
                                oRopasDTO = new RopasDTO();
                            }
                            break;
                    }

                    oRespItemRopasDTO.Item = new RopasDTO();
                    oRespItemRopasDTO.Item = oRopasDTO;
                    oRespItemRopasDTO.Success = true;
                    oRespItemRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemRopasDTO.Success = false;
                    oRespItemRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemRopasDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo RopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespRopasDTO ExecuteTransac(ReqRopasDTO oReqRopasDTO)
		{
			RespRopasDTO oRespRopasDTO = new RespRopasDTO();

            oRespRopasDTO.MessageList = new List<Mensaje>();
            oRespRopasDTO.User = oReqRopasDTO.User;
            
            if (String.IsNullOrEmpty(oReqRopasDTO.User))
            {
                oRespRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Ropas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespRopasDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (RopasDTO item in oReqRopasDTO.List)
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
                                        Codigo = 999999999;
                                        oRopasData.Registrar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }
                                    
                                    break;
                                case Operation.Update:
                                    
                                    UsuariosIngresosData oUsuariosIngresosDataUpdate = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOUpdate = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOUpdate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOUpdate.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOUpdate.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOUpdate.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOUpdate.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOUpdate.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOUpdate = oUsuariosIngresosDataUpdate.uspValidarAccesoSistema(oUsuariosIngresosDTOUpdate);
                                    if (oUsuariosIngresosDTOUpdate.CodigoValidacion == 3)
                                    {
                                        Codigo = 999999999;
                                        oRopasData.Actualizar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }
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
                                        Codigo = 999999999;
                                        oRopasData.Eliminar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespRopasDTO.Success = true;
                        oRespRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespRopasDTO.Success = false;
                        oRespRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespRopasDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
