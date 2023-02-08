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
	//Archivo     : AsistenciaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 12/2/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class AsistenciaLogic: IDisposable
	{
		AsistenciaData oAsistenciaData = null;
		public AsistenciaLogic()
		{
			oAsistenciaData = new AsistenciaData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AsistenciaGetList
		//Objetivo: Retorna una colección de registros de tipo AsistenciaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListAsistenciaDTO AsistenciaGetList(ReqFilterAsistenciaDTO oReqFilterAsistenciaDTO)
		{
		
			RespListAsistenciaDTO oRespListAsistenciaDTO = new RespListAsistenciaDTO();
		
			oRespListAsistenciaDTO.List = new List<AsistenciaDTO>();
			oRespListAsistenciaDTO.User = oReqFilterAsistenciaDTO.User;
			oRespListAsistenciaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterAsistenciaDTO.User))
            {
                oRespListAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Asistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterAsistenciaDTO.Paging == null)
            {
                oRespListAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListAsistenciaDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    

                    List<AsistenciaDTO> AsistenciaDTOList = new List<AsistenciaDTO>();

                    switch (oReqFilterAsistenciaDTO.FilterCase)
                    {
                       
                        case filterCaseAsistencia.uspListar_Socios_Inasistencias_Paginacion:
                            {
                                
                                if (!oReqFilterAsistenciaDTO.Paging.All && oReqFilterAsistenciaDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAsistenciaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListar_Socios_Inasistencias_NumeroRegistro"]);
                                }
                                AsistenciaDTOList = oAsistenciaData.uspListar_Socios_Inasistencias_Paginacion(oReqFilterAsistenciaDTO.Item,oReqFilterAsistenciaDTO.Paging);
                            }
                            break;
                       
                       case filterCaseAsistencia.ListarAsistenciaTodosFiltro_Paginacion:
                            {
                                
                                if (!oReqFilterAsistenciaDTO.Paging.All && oReqFilterAsistenciaDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAsistenciaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarAsistenciaTodosFiltro_Paginacion"]);
                                }
                                AsistenciaDTOList = oAsistenciaData.ListarAsistenciaTodosFiltro_Paginacion(oReqFilterAsistenciaDTO.Item, oReqFilterAsistenciaDTO.Paging);
                            }
                            break;
                       case filterCaseAsistencia.uspListarDetalleAsistenciaSocio_Paginacion:
                            {
                                
                                if (!oReqFilterAsistenciaDTO.Paging.All && oReqFilterAsistenciaDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAsistenciaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDetalleAsistenciaSocio_Paginacion"]);
                                }
                                AsistenciaDTOList = oAsistenciaData.uspListarDetalleAsistenciaSocio_Paginacion(oReqFilterAsistenciaDTO.Item, oReqFilterAsistenciaDTO.Paging);
                            }
                            break;
                       
                    }

                    oRespListAsistenciaDTO.List = AsistenciaDTOList;
                    oRespListAsistenciaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAsistenciaDTO.Success = false;
                    oRespListAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListAsistenciaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AsistenciaGetItem
		//Objetivo: Retorna un registro de tipo AsistenciaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemAsistenciaDTO AsistenciaGetItem(ReqFilterAsistenciaDTO oReqFilterAsistenciaDTO)
		{
			RespItemAsistenciaDTO oRespItemAsistenciaDTO = new RespItemAsistenciaDTO();

            oRespItemAsistenciaDTO.Success = false;
            oRespItemAsistenciaDTO.Item = null;
            oRespItemAsistenciaDTO.User = oReqFilterAsistenciaDTO.User;
            oRespItemAsistenciaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterAsistenciaDTO.User))
            {
                oRespItemAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Asistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAsistenciaDTO.MessageList.Count == 0)
            {
                AsistenciaDTO oAsistenciaDTO = null;
                try
                {
                    switch (oReqFilterAsistenciaDTO.FilterCase)
                    {
                       
                        case filterCaseAsistencia.porCodigo:
                            {
                                oAsistenciaDTO = new AsistenciaDTO();
                            }
                            break;
                       case filterCaseAsistencia.uspListarAsistenciaTodosFiltro_NumeroRegistros:
                            {
                                oAsistenciaDTO = new AsistenciaDTO();
                                oAsistenciaDTO = oAsistenciaData.uspListarAsistenciaTodosFiltro_NumeroRegistros(oReqFilterAsistenciaDTO.Item);
                            }
                            break;
                        case filterCaseAsistencia.BuscarAsistenciaEfectiva:
                            {
                                oAsistenciaDTO = new AsistenciaDTO();
                                oAsistenciaDTO = oAsistenciaData.BuscarAsistenciaEfectiva(oReqFilterAsistenciaDTO.Item);
                            }
                            break;
                        case filterCaseAsistencia.uspListarDetalleAsistenciaSocio_NumeroRegistros:
                            {
                                oAsistenciaDTO = new AsistenciaDTO();
                                oAsistenciaDTO = oAsistenciaData.uspListarDetalleAsistenciaSocio_NumeroRegistros(oReqFilterAsistenciaDTO.Item);
                            }
                            break;
                        case filterCaseAsistencia.uspListar_Socios_Inasistencias_NumeroRegistro:
                            {
                                oAsistenciaDTO = new AsistenciaDTO();
                                oAsistenciaDTO = oAsistenciaData.uspListar_Socios_Inasistencias_NumeroRegistro(oReqFilterAsistenciaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oAsistenciaDTO = new AsistenciaDTO();
                            }
                            break;
                    }

                    oRespItemAsistenciaDTO.Item = new AsistenciaDTO();
                    oRespItemAsistenciaDTO.Item = oAsistenciaDTO;
                    oRespItemAsistenciaDTO.Success = true;
                    oRespItemAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAsistenciaDTO.Success = false;
                    oRespItemAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAsistenciaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo AsistenciaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespAsistenciaDTO ExecuteTransac(ReqAsistenciaDTO oReqAsistenciaDTO)
		{
			RespAsistenciaDTO oRespAsistenciaDTO = new RespAsistenciaDTO();

            oRespAsistenciaDTO.MessageList = new List<Mensaje>();
            oRespAsistenciaDTO.User = oReqAsistenciaDTO.User;
            
            if (String.IsNullOrEmpty(oReqAsistenciaDTO.User))
            {
                oRespAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Asistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAsistenciaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoIngreso = 0;
                        foreach (AsistenciaDTO item in oReqAsistenciaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oAsistenciaData.Registrar(item);
                                    break;
                                case Operation.DeleteAsistencia:
                                    CodigoIngreso = 100;
                                    oAsistenciaData.EliminarAsistencia(item);
                                    //UsuariosIngresosData oUsuariosIngresosDataDelete = new UsuariosIngresosData();
                                    //UsuariosIngresosDTO oUsuariosIngresosDTODelete = new UsuariosIngresosDTO();

                                    //oUsuariosIngresosDTODelete.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oUsuariosIngresosDTODelete.CodigoSede = item.CodigoSede;
                                    //oUsuariosIngresosDTODelete.UsuarioCreacion = item.UsuarioCreacion;
                                    //oUsuariosIngresosDTODelete.CodigoIngreso = item.TK_ID;
                                    //oUsuariosIngresosDTODelete.Latitud = item.TK_Latitude;
                                    //oUsuariosIngresosDTODelete.Longitud = item.TK_Longitude;

                                    //item.CodigoInicioSesion = item.TK_ID;

                                    //oUsuariosIngresosDTODelete = oUsuariosIngresosDataDelete.uspValidarAccesoSistema(oUsuariosIngresosDTODelete);
                                    //if (oUsuariosIngresosDTODelete.CodigoValidacion == 3)
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    CodigoIngreso = 0;
                                    //}
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespAsistenciaDTO.Success = true;
                        oRespAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoIngreso,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAsistenciaDTO.Success = false;
                        oRespAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAsistenciaDTO;
		}


		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
	}
}
