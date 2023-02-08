
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
	//Archivo     : SuplementosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 16/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class SuplementosLogic: IDisposable
	{
		SuplementosData oSuplementosData = null;
		public SuplementosLogic()
		{
			oSuplementosData = new SuplementosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SuplementosGetList
		//Objetivo: Retorna una colección de registros de tipo SuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListSuplementosDTO SuplementosGetList(ReqFilterSuplementosDTO oReqFilterSuplementosDTO)
		{
		
			RespListSuplementosDTO oRespListSuplementosDTO = new RespListSuplementosDTO();
		
			oRespListSuplementosDTO.List = new List<SuplementosDTO>();
			oRespListSuplementosDTO.User = oReqFilterSuplementosDTO.User;
			oRespListSuplementosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterSuplementosDTO.User))
            {
                oRespListSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Suplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterSuplementosDTO.Paging == null)
            {
                oRespListSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListSuplementosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                
                    List<SuplementosDTO> SuplementosDTOList = new List<SuplementosDTO>();

                    switch (oReqFilterSuplementosDTO.FilterCase)
                    {
                        case filterCaseSuplementos.uspListarSuplementosPorFiltro_Paginacion:
                            {
                                if (!oReqFilterSuplementosDTO.Paging.All && oReqFilterSuplementosDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterSuplementosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarSuplementosPorFiltro_NumeroRegistros"]);
                                    SuplementosDTOList = oSuplementosData.uspListarSuplementosPorFiltro_Paginacion(oReqFilterSuplementosDTO.Item, oReqFilterSuplementosDTO.Paging);
                                }
                               
                            }
                            break;

                        case filterCaseSuplementos.uspListarSuplementos:
                            {
                                SuplementosDTOList = oSuplementosData.uspListarSuplementos(oReqFilterSuplementosDTO.Item);
                            }
                            break;
                        case filterCaseSuplementos.uspListarSuplementosPorCategoria:
                            {
                                SuplementosDTOList = oSuplementosData.uspListarSuplementosPorCategoria(oReqFilterSuplementosDTO.Item);
                            }
                            break;
                       case filterCaseSuplementos.uspListarSuplementosComprasPorCategoria:
                            {
                                SuplementosDTOList = oSuplementosData.uspListarSuplementosComprasPorCategoria(oReqFilterSuplementosDTO.Item);
                            }
                            break;
                        case filterCaseSuplementos.uspListarSuplementosComprasPorCategoriaFiltro:
                            {
                                SuplementosDTOList = oSuplementosData.uspListarSuplementosComprasPorCategoriaFiltro(oReqFilterSuplementosDTO.Item);
                            }
                            break;
                        case filterCaseSuplementos.uspListarSuplementosVentasPorCategoria:
                            {
                                SuplementosDTOList = oSuplementosData.uspListarSuplementosVentasPorCategoria(oReqFilterSuplementosDTO.Item);
                            }
                            break;

                    }

                    oRespListSuplementosDTO.List = SuplementosDTOList;
                    oRespListSuplementosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSuplementosDTO.Success = false;
                    oRespListSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListSuplementosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SuplementosGetItem
		//Objetivo: Retorna un registro de tipo SuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemSuplementosDTO SuplementosGetItem(ReqFilterSuplementosDTO oReqFilterSuplementosDTO)
		{
			RespItemSuplementosDTO oRespItemSuplementosDTO = new RespItemSuplementosDTO();

            oRespItemSuplementosDTO.Success = false;
            oRespItemSuplementosDTO.Item = null;
            oRespItemSuplementosDTO.User = oReqFilterSuplementosDTO.User;
            oRespItemSuplementosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterSuplementosDTO.User))
            {
                oRespItemSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Suplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemSuplementosDTO.MessageList.Count == 0)
            {
                SuplementosDTO oSuplementosDTO = null;
                try
                {
                    switch (oReqFilterSuplementosDTO.FilterCase)
                    {
                       
                        case filterCaseSuplementos.porCodigo:
                            {
                                oSuplementosDTO = new SuplementosDTO();
                                oSuplementosDTO = oSuplementosData.BuscarPorCodigoSuplementos(oReqFilterSuplementosDTO.Item);
                            }
                            break;
  
                        case filterCaseSuplementos.uspListarSuplementosPorFiltro_NumeroRegistros:
                            {
                                oSuplementosDTO = new SuplementosDTO();
                                oSuplementosDTO = oSuplementosData.uspListarSuplementosPorFiltro_NumeroRegistros(oReqFilterSuplementosDTO.Item);
                            }
                            break;

                        default:
                            {
                                oSuplementosDTO = new SuplementosDTO();
                            }
                            break;
                    }

                    oRespItemSuplementosDTO.Item = new SuplementosDTO();
                    oRespItemSuplementosDTO.Item = oSuplementosDTO;
                    oRespItemSuplementosDTO.Success = true;
                    oRespItemSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemSuplementosDTO.Success = false;
                    oRespItemSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemSuplementosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo SuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespSuplementosDTO ExecuteTransac(ReqSuplementosDTO oReqSuplementosDTO)
		{
			RespSuplementosDTO oRespSuplementosDTO = new RespSuplementosDTO();

            oRespSuplementosDTO.MessageList = new List<Mensaje>();
            oRespSuplementosDTO.User = oReqSuplementosDTO.User;
            
            if (String.IsNullOrEmpty(oReqSuplementosDTO.User))
            {
                oRespSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Suplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespSuplementosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (SuplementosDTO item in oReqSuplementosDTO.List)
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
                                        oSuplementosData.Registrar(item);
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
                                        oSuplementosData.Actualizar(item);
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
                                        oSuplementosData.Eliminar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespSuplementosDTO.Success = true;
                        oRespSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespSuplementosDTO.Success = false;
                        oRespSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespSuplementosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
