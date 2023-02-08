
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
	//Archivo     : HistorialCongelamientoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 10/11/2015
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class HistorialCongelamientoLogic: IDisposable
	{
		HistorialCongelamientoData oHistorialCongelamientoData = null;
		public HistorialCongelamientoLogic()
		{
			oHistorialCongelamientoData = new HistorialCongelamientoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HistorialCongelamientoGetList
		//Objetivo: Retorna una colección de registros de tipo HistorialCongelamientoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListHistorialCongelamientoDTO HistorialCongelamientoGetList(ReqFilterHistorialCongelamientoDTO oReqFilterHistorialCongelamientoDTO)
		{
		
			RespListHistorialCongelamientoDTO oRespListHistorialCongelamientoDTO = new RespListHistorialCongelamientoDTO();
		
			oRespListHistorialCongelamientoDTO.List = new List<HistorialCongelamientoDTO>();
			oRespListHistorialCongelamientoDTO.User = oReqFilterHistorialCongelamientoDTO.User;
			oRespListHistorialCongelamientoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterHistorialCongelamientoDTO.User))
            {
                oRespListHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HistorialCongelamiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterHistorialCongelamientoDTO.Paging == null)
            {
                oRespListHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListHistorialCongelamientoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterHistorialCongelamientoDTO.Paging.All && oReqFilterHistorialCongelamientoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHistorialCongelamientoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HistorialCongelamientoDTO> HistorialCongelamientoDTOList = new List<HistorialCongelamientoDTO>();

                    switch (oReqFilterHistorialCongelamientoDTO.FilterCase)
                    {
                        case filterCaseHistorialCongelamiento.ListarHitorialFreezingPorMenbresia:
                            {
                                HistorialCongelamientoDTOList = oHistorialCongelamientoData.ListarHitorialFreezingPorMenbresia(oReqFilterHistorialCongelamientoDTO.Item);
                            }
                            break;
                      
                    }

                    oRespListHistorialCongelamientoDTO.List = HistorialCongelamientoDTOList;
                    oRespListHistorialCongelamientoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHistorialCongelamientoDTO.Success = false;
                    oRespListHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListHistorialCongelamientoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HistorialCongelamientoGetItem
		//Objetivo: Retorna un registro de tipo HistorialCongelamientoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemHistorialCongelamientoDTO HistorialCongelamientoGetItem(ReqFilterHistorialCongelamientoDTO oReqFilterHistorialCongelamientoDTO)
		{
			RespItemHistorialCongelamientoDTO oRespItemHistorialCongelamientoDTO = new RespItemHistorialCongelamientoDTO();

            oRespItemHistorialCongelamientoDTO.Success = false;
            oRespItemHistorialCongelamientoDTO.Item = null;
            oRespItemHistorialCongelamientoDTO.User = oReqFilterHistorialCongelamientoDTO.User;
            oRespItemHistorialCongelamientoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterHistorialCongelamientoDTO.User))
            {
                oRespItemHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HistorialCongelamiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHistorialCongelamientoDTO.MessageList.Count == 0)
            {
                HistorialCongelamientoDTO oHistorialCongelamientoDTO = null;
                try
                {
                    switch (oReqFilterHistorialCongelamientoDTO.FilterCase)
                    {
                       
                        
                        default:
                            {
                                oHistorialCongelamientoDTO = new HistorialCongelamientoDTO();
                            }
                            break;
                    }

                    oRespItemHistorialCongelamientoDTO.Item = new HistorialCongelamientoDTO();
                    oRespItemHistorialCongelamientoDTO.Item = oHistorialCongelamientoDTO;
                    oRespItemHistorialCongelamientoDTO.Success = true;
                    oRespItemHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHistorialCongelamientoDTO.Success = false;
                    oRespItemHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHistorialCongelamientoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo HistorialCongelamientoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespHistorialCongelamientoDTO ExecuteTransac(ReqHistorialCongelamientoDTO oReqHistorialCongelamientoDTO)
		{
			RespHistorialCongelamientoDTO oRespHistorialCongelamientoDTO = new RespHistorialCongelamientoDTO();

            oRespHistorialCongelamientoDTO.MessageList = new List<Mensaje>();
            oRespHistorialCongelamientoDTO.User = oReqHistorialCongelamientoDTO.User;
            
            if (String.IsNullOrEmpty(oReqHistorialCongelamientoDTO.User))
            {
                oRespHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HistorialCongelamiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespHistorialCongelamientoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        foreach (HistorialCongelamientoDTO item in oReqHistorialCongelamientoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                        oHistorialCongelamientoData.Registrar(item);
                                    break;
                                case Operation.Delete:
                                    //UsuariosIngresosData oUsuariosIngresosDataDelete= new UsuariosIngresosData();
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
                                        codigo = 999999999;
                                        oHistorialCongelamientoData.Eliminar(item);
                                    //}
                                    //else
                                    //{
                                    //    codigo = 0;
                                    //}
                                    
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespHistorialCongelamientoDTO.Success = true;
                        oRespHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHistorialCongelamientoDTO.Success = false;
                        oRespHistorialCongelamientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHistorialCongelamientoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
