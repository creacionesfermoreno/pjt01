
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
	//Archivo     : InteresProspectosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 12/12/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class InteresProspectosLogic: IDisposable
	{
		InteresProspectosData oInteresProspectosData = null;
		public InteresProspectosLogic()
		{
			oInteresProspectosData = new InteresProspectosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	InteresProspectosGetList
		//Objetivo: Retorna una colección de registros de tipo InteresProspectosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListInteresProspectosDTO InteresProspectosGetList(ReqFilterInteresProspectosDTO oReqFilterInteresProspectosDTO)
		{
		
			RespListInteresProspectosDTO oRespListInteresProspectosDTO = new RespListInteresProspectosDTO();
		
			oRespListInteresProspectosDTO.List = new List<InteresProspectosDTO>();
			oRespListInteresProspectosDTO.User = oReqFilterInteresProspectosDTO.User;
			oRespListInteresProspectosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterInteresProspectosDTO.User))
            {
                oRespListInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de InteresProspectos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterInteresProspectosDTO.Paging == null)
            {
                oRespListInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListInteresProspectosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterInteresProspectosDTO.Paging.All && oReqFilterInteresProspectosDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterInteresProspectosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<InteresProspectosDTO> InteresProspectosDTOList = new List<InteresProspectosDTO>();

                    switch (oReqFilterInteresProspectosDTO.FilterCase)
                    {
                        default:
                            {
                                InteresProspectosDTOList = oInteresProspectosData.uspListarInteresProspectos(oReqFilterInteresProspectosDTO.Item);
                            }
                            break;
                    }

                    oRespListInteresProspectosDTO.List = InteresProspectosDTOList;
                    oRespListInteresProspectosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListInteresProspectosDTO.Success = false;
                    oRespListInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListInteresProspectosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	InteresProspectosGetItem
		//Objetivo: Retorna un registro de tipo InteresProspectosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemInteresProspectosDTO InteresProspectosGetItem(ReqFilterInteresProspectosDTO oReqFilterInteresProspectosDTO)
		{
			RespItemInteresProspectosDTO oRespItemInteresProspectosDTO = new RespItemInteresProspectosDTO();

            oRespItemInteresProspectosDTO.Success = false;
            oRespItemInteresProspectosDTO.Item = null;
            oRespItemInteresProspectosDTO.User = oReqFilterInteresProspectosDTO.User;
            oRespItemInteresProspectosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterInteresProspectosDTO.User))
            {
                oRespItemInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de InteresProspectos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemInteresProspectosDTO.MessageList.Count == 0)
            {
                InteresProspectosDTO oInteresProspectosDTO = null;
                try
                {
                    switch (oReqFilterInteresProspectosDTO.FilterCase)
                    {
                       
                        //case filterCaseInteresProspectos.porCodigo:
                        //    {
                        //        oInteresProspectosDTO = new InteresProspectosDTO();
                        //        oInteresProspectosDTO = oInteresProspectosData.BuscarPorCodigoInteresProspectos(oReqFilterInteresProspectosDTO.Item);
                        //    }
                        //    break;
                        default:
                            {
                                oInteresProspectosDTO = new InteresProspectosDTO();
                            }
                            break;
                    }

                    oRespItemInteresProspectosDTO.Item = new InteresProspectosDTO();
                    oRespItemInteresProspectosDTO.Item = oInteresProspectosDTO;
                    oRespItemInteresProspectosDTO.Success = true;
                    oRespItemInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemInteresProspectosDTO.Success = false;
                    oRespItemInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemInteresProspectosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo InteresProspectosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespInteresProspectosDTO ExecuteTransac(ReqInteresProspectosDTO oReqInteresProspectosDTO)
		{
			RespInteresProspectosDTO oRespInteresProspectosDTO = new RespInteresProspectosDTO();

            oRespInteresProspectosDTO.MessageList = new List<Mensaje>();
            oRespInteresProspectosDTO.User = oReqInteresProspectosDTO.User;
            
            if (String.IsNullOrEmpty(oReqInteresProspectosDTO.User))
            {
                oRespInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de InteresProspectos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespInteresProspectosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        var Codigo = 0;
                        foreach (InteresProspectosDTO item in oReqInteresProspectosDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    //UsuariosIngresosData oUsuariosIngresosDataCreate = new UsuariosIngresosData();
                                    //UsuariosIngresosDTO oUsuariosIngresosDTOCreate = new UsuariosIngresosDTO();

                                    //oUsuariosIngresosDTOCreate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oUsuariosIngresosDTOCreate.CodigoSede = item.CodigoSede;
                                    //oUsuariosIngresosDTOCreate.UsuarioCreacion = item.UsuarioCreacion;
                                    //oUsuariosIngresosDTOCreate.CodigoIngreso = item.TK_ID;
                                    //oUsuariosIngresosDTOCreate.Latitud = item.TK_Latitude;
                                    //oUsuariosIngresosDTOCreate.Longitud = item.TK_Longitude;

                                    //item.CodigoInicioSesion = item.TK_ID;
                                    //oUsuariosIngresosDTOCreate = oUsuariosIngresosDataCreate.uspValidarAccesoSistema(oUsuariosIngresosDTOCreate);
                                    //if (oUsuariosIngresosDTOCreate.CodigoValidacion == 3)
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    Codigo = 0;
                                    //}
                                    Codigo = 999999999;
                                    oInteresProspectosData.Registrar(item);
                                    break;
                                //case Operation.Update:
                                //    oInteresProspectosData.Actualizar(item);
                                //    break;
                                case Operation.Delete:
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
                                    //    Codigo = 0;
                                    //}
                                    Codigo = 999999999;
                                    oInteresProspectosData.Eliminar(item);
                                    break;
                                   
                            }
                        }
                        tx.Complete();
                        oRespInteresProspectosDTO.Success = true;
                        oRespInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespInteresProspectosDTO.Success = false;
                        oRespInteresProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespInteresProspectosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
