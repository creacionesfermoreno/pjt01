
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
	//Archivo     : UsuariosIngresosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 07/09/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class UsuariosIngresosLogic: IDisposable
	{
		UsuariosIngresosData oUsuariosIngresosData = null;
		public UsuariosIngresosLogic()
		{
			oUsuariosIngresosData = new UsuariosIngresosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	UsuariosIngresosGetList
		//Objetivo: Retorna una colección de registros de tipo UsuariosIngresosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListUsuariosIngresosDTO UsuariosIngresosGetList(ReqFilterUsuariosIngresosDTO oReqFilterUsuariosIngresosDTO)
		{
		
			RespListUsuariosIngresosDTO oRespListUsuariosIngresosDTO = new RespListUsuariosIngresosDTO();
		
			oRespListUsuariosIngresosDTO.List = new List<UsuariosIngresosDTO>();
			oRespListUsuariosIngresosDTO.User = oReqFilterUsuariosIngresosDTO.User;
			oRespListUsuariosIngresosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterUsuariosIngresosDTO.User))
            {
                oRespListUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de UsuariosIngresos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterUsuariosIngresosDTO.Paging == null)
            {
                oRespListUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListUsuariosIngresosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterUsuariosIngresosDTO.Paging.All && oReqFilterUsuariosIngresosDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterUsuariosIngresosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<UsuariosIngresosDTO> UsuariosIngresosDTOList = new List<UsuariosIngresosDTO>();

                    switch (oReqFilterUsuariosIngresosDTO.FilterCase)
                    {
                        default:
                            {
                                //UsuariosIngresosDTOList = oUsuariosIngresosData.Listar(oReqFilterUsuariosIngresosDTO.Item, oReqFilterUsuariosIngresosDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListUsuariosIngresosDTO.List = UsuariosIngresosDTOList;
                    oRespListUsuariosIngresosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListUsuariosIngresosDTO.Success = false;
                    oRespListUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListUsuariosIngresosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	UsuariosIngresosGetItem
		//Objetivo: Retorna un registro de tipo UsuariosIngresosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemUsuariosIngresosDTO UsuariosIngresosGetItem(ReqFilterUsuariosIngresosDTO oReqFilterUsuariosIngresosDTO)
		{
			RespItemUsuariosIngresosDTO oRespItemUsuariosIngresosDTO = new RespItemUsuariosIngresosDTO();

            oRespItemUsuariosIngresosDTO.Success = false;
            oRespItemUsuariosIngresosDTO.Item = null;
            oRespItemUsuariosIngresosDTO.User = oReqFilterUsuariosIngresosDTO.User;
            oRespItemUsuariosIngresosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterUsuariosIngresosDTO.User))
            {
                oRespItemUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de UsuariosIngresos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemUsuariosIngresosDTO.MessageList.Count == 0)
            {
                UsuariosIngresosDTO oUsuariosIngresosDTO = null;
                try
                {
                    switch (oReqFilterUsuariosIngresosDTO.FilterCase)
                    {
                       
                        case filterCaseUsuariosIngresos.PorCodigo:
                            {
                                oUsuariosIngresosDTO = new UsuariosIngresosDTO();
                               // oUsuariosIngresosDTO = oUsuariosIngresosData.BuscarPorCodigoUsuariosIngresos(oReqFilterUsuariosIngresosDTO.Item);
                            }
                            break;
                          case filterCaseUsuariosIngresos.uspValidarAccesoSistema:
                            {
                                oUsuariosIngresosDTO = new UsuariosIngresosDTO();
                                oUsuariosIngresosDTO = oUsuariosIngresosData.uspValidarAccesoSistema(oReqFilterUsuariosIngresosDTO.Item);
                            }
                            break;

                        default:
                            {
                                oUsuariosIngresosDTO = new UsuariosIngresosDTO();
                            }
                            break;
                    }

                    oRespItemUsuariosIngresosDTO.Item = new UsuariosIngresosDTO();
                    oRespItemUsuariosIngresosDTO.Item = oUsuariosIngresosDTO;
                    oRespItemUsuariosIngresosDTO.Success = true;
                    oRespItemUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemUsuariosIngresosDTO.Success = false;
                    oRespItemUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemUsuariosIngresosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo UsuariosIngresosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespUsuariosIngresosDTO ExecuteTransac(ReqUsuariosIngresosDTO oReqUsuariosIngresosDTO)
		{
			RespUsuariosIngresosDTO oRespUsuariosIngresosDTO = new RespUsuariosIngresosDTO();

            oRespUsuariosIngresosDTO.MessageList = new List<Mensaje>();
            oRespUsuariosIngresosDTO.User = oReqUsuariosIngresosDTO.User;
            
            if (String.IsNullOrEmpty(oReqUsuariosIngresosDTO.User))
            {
                oRespUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de UsuariosIngresos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespUsuariosIngresosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoIngreso = 0;
                        foreach (UsuariosIngresosDTO item in oReqUsuariosIngresosDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoIngreso = oUsuariosIngresosData.Registrar(item);
                                    break;
                                case Operation.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo:
                                    oUsuariosIngresosData.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(item);
                                    break;
                                case Operation.Update:
                                   // oUsuariosIngresosData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                   // oUsuariosIngresosData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespUsuariosIngresosDTO.Success = true;
                        oRespUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoIngreso,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespUsuariosIngresosDTO.Success = false;
                        oRespUsuariosIngresosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespUsuariosIngresosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
